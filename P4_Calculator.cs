using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsAssignments.P4
{
    public static class CalcStore
    {
        public static List<string> History {get;} = new List<string>();
    }

    public class CalculatorForm : Form
    {
        TextBox aBox, bBox;
        public CalculatorForm()
        {
            Text = "Calculator - Sara"; Width=420; Height=300; StartPosition=FormStartPosition.CenterParent;
            var lbl1 = new Label(){Text="Number A:", Left=20, Top=20, AutoSize=true};
            aBox = new TextBox(){Left=120, Top=20, Width=240};
            var lbl2 = new Label(){Text="Number B:", Left=20, Top=60, AutoSize=true};
            bBox = new TextBox(){Left=120, Top=60, Width=240};

            var btnAdd = new Button(){Text="Add", Left=20, Top=110, Width=100};
            var btnSub = new Button(){Text="Subtract", Left=130, Top=110, Width=100};
            var btnMul = new Button(){Text="Multiply", Left=240, Top=110, Width=100};
            var btnDiv = new Button(){Text="Divide", Left=20, Top=160, Width=100};
            var btnHist = new Button(){Text="Show History", Left=130, Top=160, Width=210};

            btnAdd.Click += (_,__)=> DoOp((x,y)=>x+y, "+");
            btnSub.Click += (_,__)=> DoOp((x,y)=>x-y, "-");
            btnMul.Click += (_,__)=> DoOp((x,y)=>x*y, "*"); 
            btnDiv.Click += (_,__)=> DoOp((x,y)=> { if (y==0) throw new DivideByZeroException(); return x/y; }, "/");

            btnHist.Click += (_,__)=> { new HistoryForm().ShowDialog(); };

            Controls.AddRange(new Control[]{lbl1, aBox, lbl2, bBox, btnAdd, btnSub, btnMul, btnDiv, btnHist});
        }

        void DoOp(Func<double,double,double> op, string sym)
        {
            if (!double.TryParse(aBox.Text, out double a) || !double.TryParse(bBox.Text, out double b))
            {
                MessageBox.Show("Enter valid numbers");
                return;
            }
            try
            {
                var res = op(a,b);
                var s = $"{a} {sym} {b} = {res}";
                CalcStore.History.Add(s);
                MessageBox.Show(s, "Result");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error"); }
        }
    }

    public class HistoryForm : Form
    {
        ListBox lb;
        public HistoryForm()
        {
            Text = "Calculation History"; Width=420; Height=360; StartPosition=FormStartPosition.CenterParent;
            lb = new ListBox(){Left=10, Top=10, Width=380, Height=280};
            var btnClear = new Button(){Text="Clear", Left=10, Top=300, Width=120};
            btnClear.Click += (_,__)=> { CalcStore.History.Clear(); Load(); };
            Controls.AddRange(new Control[]{lb, btnClear});
            Load();
        }

        void Load()
        {
            lb.Items.Clear();
            foreach(var s in CalcStore.History) lb.Items.Add(s);
        }
    }
}
