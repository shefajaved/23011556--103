using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace WinFormsAssignments.P5
{
    public class Expense
    {
        public string Type {get; set;} = "";
        public decimal Amount {get; set;}
        public DateTime Date {get; set;}
        public string Description {get; set;} = "";

        public override string ToString() => $"{Date:yyyy-MM-dd}\t{Type}\t{Amount}\t{Description}";
    }

    public static class ExpenseStore
    {
        static string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "expenses.txt");
        public static List<Expense> Expenses {get; private set;} = Load();

        static List<Expense> Load()
        {
            var list = new List<Expense>();
            try
            {
                if (!File.Exists(path)) return list;
                var lines = File.ReadAllLines(path);
                foreach(var ln in lines)
                {
                    var parts = ln.Split('\t');
                    if (parts.Length<4) continue;
                    if (!DateTime.TryParse(parts[0], out DateTime dt)) continue;
                    if (!decimal.TryParse(parts[2], out decimal amt)) continue;
                    list.Add(new Expense(){ Date = dt, Type = parts[1], Amount = amt, Description = parts[3] });
                }
            } catch {}
            return list;
        }

        public static void Save()
        {
            try
            {
                File.WriteAllLines(path, Expenses.Select(e => e.ToString()));
            } catch {}
        }
    }

    public class DashboardForm : Form
    {
        public DashboardForm()
        {
            Text = "Expense Tracker"; Width=420; Height=260; StartPosition=FormStartPosition.CenterParent;
            var btnAdd = new Button(){Text="Add Expense", Left=20, Top=20, Width=360};
            var btnView = new Button(){Text="View Expenses", Left=20, Top=70, Width=360};
            var btnClose = new Button(){Text="Close", Left=20, Top=120, Width=360};
            btnAdd.Click += (_,__)=> { new AddExpenseForm().ShowDialog(); };
            btnView.Click += (_,__)=> { new ViewExpensesForm().ShowDialog(); };
            btnClose.Click += (_,__)=> Close();
            Controls.AddRange(new Control[]{btnAdd, btnView, btnClose});
        }
    }

    public class AddExpenseForm : Form
    {
        ComboBox cbType; TextBox txtAmount, txtDesc; DateTimePicker dt;
        public AddExpenseForm()
        {
            Text = "Add Expense"; Width=480; Height=280; StartPosition=FormStartPosition.CenterParent;
            cbType = new ComboBox(){Left=140, Top=20, Width=300, DropDownStyle=ComboBoxStyle.DropDown};
            cbType.Items.AddRange(new string[]{"Food","Transport","Bills","Others"});
            var lbl1 = new Label(){Text="Type:", Left=20, Top=20, AutoSize=true};
            var lbl2 = new Label(){Text="Amount:", Left=20, Top=60, AutoSize=true};
            txtAmount = new TextBox(){Left=140, Top=60, Width=300};
            var lbl3 = new Label(){Text="Date:", Left=20, Top=100, AutoSize=true};
            dt = new DateTimePicker(){Left=140, Top=100, Width=300};
            var lbl4 = new Label(){Text="Description:", Left=20, Top=140, AutoSize=true};
            txtDesc = new TextBox(){Left=140, Top=140, Width=300};
            var btn = new Button(){Text="Save", Left=140, Top=180, Width=120};
            btn.Click += Btn_Click;
            Controls.AddRange(new Control[]{lbl1, cbType, lbl2, txtAmount, lbl3, dt, lbl4, txtDesc, btn});
        }

        private void Btn_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cbType.Text) || !decimal.TryParse(txtAmount.Text, out decimal amt))
            {
                MessageBox.Show("Enter valid type and amount");
                return;
            }
            var ex = new Expense(){ Type = cbType.Text.Trim(), Amount = amt, Date = dt.Value.Date, Description = txtDesc.Text.Trim() };
            ExpenseStore.Expenses.Add(ex);
            ExpenseStore.Save();
            MessageBox.Show("Expense saved (stored in My Documents/expenses.txt)", "Saved");
            Close();
        }
    }

    public class ViewExpensesForm : Form
    {
        DataGridView dgv;
        public ViewExpensesForm()
        {
            Text = "View Expenses"; Width=700; Height=420; StartPosition=FormStartPosition.CenterParent;
            dgv = new DataGridView(){Left=10, Top=10, Width=660, Height=340, ReadOnly=true, AutoGenerateColumns=false, AllowUserToAddRows=false};
            dgv.Columns.Add(new DataGridViewTextBoxColumn(){HeaderText="Date", DataPropertyName="Date", Width=120});
            dgv.Columns.Add(new DataGridViewTextBoxColumn(){HeaderText="Type", DataPropertyName="Type", Width=120});
            dgv.Columns.Add(new DataGridViewTextBoxColumn(){HeaderText="Amount", DataPropertyName="Amount", Width=120});
            dgv.Columns.Add(new DataGridViewTextBoxColumn(){HeaderText="Description", DataPropertyName="Description", Width=280});
            var btnRefresh = new Button(){Text="Refresh", Left=10, Top=360, Width=120};
            btnRefresh.Click += (_,__)=> Load();
            Controls.AddRange(new Control[]{dgv, btnRefresh});
            Load();
        }

        private void Load()
        {
            dgv.DataSource = null;
            dgv.DataSource = ExpenseStore.Expenses.Select(e => new { Date = e.Date.ToString("yyyy-MM-dd"), e.Type, Amount = e.Amount, e.Description }).ToList();
        }
    }
}
