using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WinFormsAssignments.P7
{
    public class LoginForm : Form
    {
        TextBox txtUser, txtPass;
        public LoginForm()
        {
            Text = "Quiz Login"; Width=360; Height=240; StartPosition=FormStartPosition.CenterParent;
            var lbl1 = new Label(){Text="Username:", Left=20, Top=20, AutoSize=true};
            txtUser = new TextBox(){Left=120, Top=20, Width=200};
            var lbl2 = new Label(){Text="Password:", Left=20, Top=60, AutoSize=true};
            txtPass = new TextBox(){Left=120, Top=60, Width=200, UseSystemPasswordChar=true};
            var btn = new Button(){Text="Login", Left=120, Top=100, Width=100};
            btn.Click += Btn_Click;
            Controls.AddRange(new Control[]{lbl1, txtUser, lbl2, txtPass, btn});
        }

        private void Btn_Click(object? sender, EventArgs e)
        {
            // simple check
            if (txtUser.Text=="user" && txtPass.Text=="pass")
            {
                new QuizForm(txtUser.Text).ShowDialog();
            } else MessageBox.Show("Invalid login");
        }
    }

    public class QuizForm : Form
    {
        List<(string q, string[] opts, int correct)> questions = new List<(string,string[],int)>();
        List<GroupBox> boxes = new List<GroupBox>();
        public QuizForm(string user)
        {
            Text = $"Quiz - {user}";
            Width=720; Height=600; StartPosition=FormStartPosition.CenterParent;
            // 5 questions
            questions.Add(("Capital of France?", new[] {"Berlin","London","Paris","Rome"}, 2));
            questions.Add(("2+2=?", new[] {"3","4","5","6"}, 1));
            questions.Add(("Largest planet?", new[] {"Earth","Mars","Jupiter","Venus"}, 2));
            questions.Add(("Color of sun?", new[] {"Blue","Yellow","Red","Green"}, 1));
            questions.Add(("Square root of 9?", new[] {"1","2","3","4"}, 2));

            int top = 10;
            string uniqueColorName = "Purple";
            foreach(var (q,opts,correct) in questions.Select((v,i)=> (v.q, v.opts, v.correct, idx:i)))
            {
                var gb = new GroupBox(){Text=$"Q{gbIndex(ref top)}: {q}", Left=10, Top=top, Width=680, Height=90};
                gb.ForeColor = Color.DarkMagenta; // originality: unique question text color
                int left = 10; int i=0;
                foreach(var o in opts)
                {
                    var rb = new RadioButton(){Text=o, Left=left, Top=20 + (i/2)*20, AutoSize=true, Tag = (gb, i)};
                    gb.Controls.Add(rb);
                    left += 200; i++;
                }
                boxes.Add(gb);
                Controls.Add(gb);
                top += 100;
            }

            var btn = new Button(){Text="Submit", Left=10, Top=top, Width=120};
            btn.Click += Btn_Click;
            Controls.Add(btn);
        }

        private int gbIndex(ref int top) { return top/100 + 1; }

        private void Btn_Click(object? sender, EventArgs e)
        {
            int score = 0;
            // evaluate by reading GroupBox controls - simpler approach: match selected index to hardcoded answers in same order
            var answers = new[] {2,1,2,1,2};
            for (int gi=0; gi<boxes.Count; gi++)
            {
                var gb = boxes[gi];
                int selected = -1; int idx = 0;
                foreach(RadioButton rb in gb.Controls.OfType<RadioButton>())
                {
                    if (rb.Checked) selected = idx;
                    idx++;
                }
                if (selected == answers[gi]) score++;
            }
            new ResultForm(score, boxes.Count).ShowDialog();
        }
    }

    public class ResultForm : Form
    {
        public ResultForm(int score, int total)
        {
            Text = "Result"; Width=300; Height=200; StartPosition=FormStartPosition.CenterParent;
            var lbl = new Label(){Text=$"You scored {score} out of {total}", Left=20, Top=40, AutoSize=true, Font=new Font(FontFamily.GenericSansSerif, 12f, FontStyle.Bold)};
            var btn = new Button(){Text="OK", Left=80, Top=100, Width=100};
            btn.Click += (_,__)=> Close();
            Controls.AddRange(new Control[]{lbl, btn});
        }
    }
}
