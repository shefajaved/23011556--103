using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WinFormsAssignments.P6
{
    public class Product
    {
        public int ID {get; set;}
        public string Name {get; set;} = "";
        public decimal Price {get; set;}
        public int Quantity {get; set;}
    }

    public static class ProductStore
    {
        public static List<Product> Products {get;} = new List<Product>();
        static int nextId = 1;
        public static void Add(Product p) { p.ID = nextId++; Products.Add(p); }
    }

    public class DashboardForm : Form
    {
        public DashboardForm()
        {
            Text = "Product Inventory"; Width=420; Height=260; StartPosition=FormStartPosition.CenterParent;
            var btnAdd = new Button(){Text="Add Product", Left=20, Top=20, Width=360};
            var btnView = new Button(){Text="View Products", Left=20, Top=70, Width=360};
            var btnClose = new Button(){Text="Close", Left=20, Top=120, Width=360};
            btnAdd.Click += (_,__)=> { new AddProductForm().ShowDialog(); };
            btnView.Click += (_,__)=> { new ViewProductsForm().ShowDialog(); };
            btnClose.Click += (_,__)=> Close();
            Controls.AddRange(new Control[]{btnAdd, btnView, btnClose});
        }
    }

    public class AddProductForm : Form
    {
        TextBox txtName, txtPrice, txtQty;
        public AddProductForm()
        {
            Text = "Add Product"; Width=420; Height=260; StartPosition=FormStartPosition.CenterParent;
            var lbl1 = new Label(){Text="Name:", Left=20, Top=20, AutoSize=true};
            txtName = new TextBox(){Left=120, Top=20, Width=260};
            var lbl2 = new Label(){Text="Price:", Left=20, Top=60, AutoSize=true};
            txtPrice = new TextBox(){Left=120, Top=60, Width=260};
            var lbl3 = new Label(){Text="Quantity:", Left=20, Top=100, AutoSize=true};
            txtQty = new TextBox(){Left=120, Top=100, Width=260};
            var btn = new Button(){Text="Save", Left=120, Top=140, Width=120};
            btn.Click += Btn_Click;
            Controls.AddRange(new Control[]{lbl1, txtName, lbl2, txtPrice, lbl3, txtQty, btn});
        }

        private void Btn_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) || !decimal.TryParse(txtPrice.Text, out decimal price) || !int.TryParse(txtQty.Text, out int qty))
            {
                MessageBox.Show("Enter valid data");
                return;
            }
            ProductStore.Add(new Product(){ Name = txtName.Text.Trim(), Price = price, Quantity = qty });
            MessageBox.Show("Product added");
            Close();
        }
    }

    public class ViewProductsForm : Form
    {
        DataGridView dgv;
        public ViewProductsForm()
        {
            Text = "View Products"; Width=700; Height=420; StartPosition=FormStartPosition.CenterParent;
            dgv = new DataGridView(){Left=10, Top=10, Width=660, Height=320, ReadOnly=false, AutoGenerateColumns=false, AllowUserToAddRows=false};
            dgv.Columns.Add(new DataGridViewTextBoxColumn(){HeaderText="ID", DataPropertyName="ID", Width=80});
            dgv.Columns.Add(new DataGridViewTextBoxColumn(){HeaderText="Name", DataPropertyName="Name", Width=240});
            dgv.Columns.Add(new DataGridViewTextBoxColumn(){HeaderText="Price", DataPropertyName="Price", Width=120});
            dgv.Columns.Add(new DataGridViewTextBoxColumn(){HeaderText="Quantity", DataPropertyName="Quantity", Width=120});

            var btnRefresh = new Button(){Text="Refresh", Left=10, Top=340, Width=120};
            var btnDelete = new Button(){Text="Delete Selected", Left=140, Top=340, Width=140};
            btnRefresh.Click += (_,__)=> Load();
            btnDelete.Click += BtnDelete_Click;

            Controls.AddRange(new Control[]{dgv, btnRefresh, btnDelete});
            Load();
        }

        private void Load()
        {
            dgv.DataSource = null;
            dgv.DataSource = ProductStore.Products.Select(p=> new { p.ID, p.Name, p.Price, p.Quantity }).ToList();
        }

        private void BtnDelete_Click(object? sender, EventArgs e)
        {
            if (dgv.CurrentRow==null) return;
            var id = (int)dgv.CurrentRow.Cells[0].Value;
            ProductStore.Products.RemoveAll(p => p.ID==id);
            Load();
        }
    }
}
