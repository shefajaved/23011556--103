using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WinFormsAssignments.P3
{
    public class Book
    {
        public Guid Id {get; set;} = Guid.NewGuid();
        public string Title {get; set;} = "";
        public string Author {get; set;} = "";
        public bool Available {get; set;} = true;
    }

    public static class LibraryStore
    {
        public static List<Book> Books {get;} = new List<Book>()
        {
            new Book(){Title="The Alchemist", Author="Paulo Coelho", Available=true},
            new Book(){Title="1984", Author="George Orwell", Available=true},
            new Book(){Title="Clean Code", Author="Robert C. Martin", Available=true}
        };
        public static List<(Book book, string borrower, DateTime date)> Borrowed {get;} = new List<(Book,string,DateTime)>();
    }

    public class DashboardForm : Form
    {
        public DashboardForm()
        {
            Text = "Library Dashboard"; Width=420; Height=260; StartPosition=FormStartPosition.CenterParent;
            var btnAdd = new Button(){Text="Add Book", Left=20, Top=20, Width=360};
            var btnBorrow = new Button(){Text="Borrow Book", Left=20, Top=70, Width=360};
            var btnView = new Button(){Text="View Borrowed", Left=20, Top=120, Width=360};
            var btnClose = new Button(){Text="Close", Left=20, Top=170, Width=360};

            btnAdd.Click += (_,__)=> { new AddBookForm().ShowDialog(); };
            btnBorrow.Click += (_,__)=> { new BorrowBookForm().ShowDialog(); };
            btnView.Click += (_,__)=> { new ViewBorrowedForm().ShowDialog(); };
            btnClose.Click += (_,__)=> Close();

            Controls.AddRange(new Control[]{btnAdd, btnBorrow, btnView, btnClose});
        }
    }

    public class AddBookForm : Form
    {
        TextBox txtTitle, txtAuthor;
        public AddBookForm()
        {
            Text = "Add Book"; Width=400; Height=220; StartPosition=FormStartPosition.CenterParent;
            var lbl1 = new Label(){Text="Title:", Left=20, Top=20, AutoSize=true};
            txtTitle = new TextBox(){Left=120, Top=20, Width=240};
            var lbl2 = new Label(){Text="Author:", Left=20, Top=60, AutoSize=true};
            txtAuthor = new TextBox(){Left=120, Top=60, Width=240};
            var btn = new Button(){Text="Save", Left=120, Top=100, Width=120};
            btn.Click += (_,__)=> {
                if (string.IsNullOrWhiteSpace(txtTitle.Text)) { MessageBox.Show("Enter title"); return; }
                LibraryStore.Books.Add(new Book(){Title=txtTitle.Text.Trim(), Author=txtAuthor.Text.Trim(), Available=true});
                MessageBox.Show("Book added");
                Close();
            };

            Controls.AddRange(new Control[]{lbl1, txtTitle, lbl2, txtAuthor, btn});
        }
    }

    public class BorrowBookForm : Form
    {
        ComboBox cbBooks; TextBox txtBorrower;
        public BorrowBookForm()
        {
            Text = "Borrow Book"; Width=500; Height=220; StartPosition=FormStartPosition.CenterParent;
            var lbl1 = new Label(){Text="Select Book:", Left=20, Top=20, AutoSize=true};
            cbBooks = new ComboBox(){Left=120, Top=20, Width=340, DropDownStyle=ComboBoxStyle.DropDownList};
            var lbl2 = new Label(){Text="Borrower Name:", Left=20, Top=60, AutoSize=true};
            txtBorrower = new TextBox(){Left=120, Top=60, Width=340};
            var btn = new Button(){Text="Borrow", Left=120, Top=100, Width=120};
            btn.Click += Btn_Click;

            Controls.AddRange(new Control[]{lbl1, cbBooks, lbl2, txtBorrower, btn});
            LoadBooks();
        }

        private void LoadBooks()
        {
            cbBooks.Items.Clear();
            foreach (var b in LibraryStore.Books.Where(x=>x.Available))
            {
                cbBooks.Items.Add(new ComboItem{Text=$"{b.Title} by {b.Author}", Value=b});
            }
            if (cbBooks.Items.Count>0) cbBooks.SelectedIndex = 0;
        }

        private void Btn_Click(object? sender, EventArgs e)
        {
            if (cbBooks.SelectedItem==null) { MessageBox.Show("No book selected"); return; }
            if (string.IsNullOrWhiteSpace(txtBorrower.Text)) { MessageBox.Show("Enter borrower name"); return; }
            var item = (ComboItem)cbBooks.SelectedItem;
            var book = (Book)item.Value;
            book.Available = false;
            LibraryStore.Borrowed.Add((book, txtBorrower.Text.Trim(), DateTime.Now));
            MessageBox.Show("Book borrowed");
            Close();
        }

        class ComboItem { public string Text; public Book Value; public override string ToString()=>Text; }
    }

    public class ViewBorrowedForm : Form
    {
        DataGridView dgv;
        public ViewBorrowedForm()
        {
            Text = "Borrowed Books"; Width=700; Height=400; StartPosition=FormStartPosition.CenterParent;
            dgv = new DataGridView(){Left=10, Top=10, Width=660, Height=320, ReadOnly=true, AutoGenerateColumns=false, AllowUserToAddRows=false};
            dgv.Columns.Add(new DataGridViewTextBoxColumn(){HeaderText="Title", DataPropertyName="Title", Width=250});
            dgv.Columns.Add(new DataGridViewTextBoxColumn(){HeaderText="Author", DataPropertyName="Author", Width=200});
            dgv.Columns.Add(new DataGridViewTextBoxColumn(){HeaderText="Borrower", DataPropertyName="Borrower", Width=150});
            dgv.Columns.Add(new DataGridViewTextBoxColumn(){HeaderText="Date", DataPropertyName="Date", Width=120});

            var btnReturn = new Button(){Text="Return Selected", Left=10, Top=340, Width=140};
            btnReturn.Click += BtnReturn_Click;
            Controls.AddRange(new Control[]{dgv, btnReturn});
            LoadData();
        }

        private void LoadData()
        {
            dgv.DataSource = null;
            dgv.DataSource = LibraryStore.Borrowed.Select(b => new { Title = b.book.Title, Author = b.book.Author, Borrower = b.borrower, Date = b.date.ToString("yyyy-MM-dd HH:mm") }).ToList();
        }

        private void BtnReturn_Click(object? sender, EventArgs e)
        {
            if (dgv.CurrentRow==null) return;
            var title = dgv.CurrentRow.Cells[0].Value.ToString();
            var rec = LibraryStore.Borrowed.Find(b => b.book.Title == title);
            if (rec.book!=null) rec.book.Available = true;
            LibraryStore.Borrowed.RemoveAll(b => b.book.Title==title && b.borrower== (string)dgv.CurrentRow.Cells[2].Value);
            LoadData();
        }
    }
}
