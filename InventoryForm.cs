using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace InventorySystem
{
    public partial class InventoryForm : Form
    {
        public InventoryForm()
        {
            InitializeComponent();
        }

        private void dataGridLoad()
            //Loads the datagrid from the database translator class which then accesses the database.
        {
            DataBaseTranslator read = new DataBaseTranslator();
            //Accessing database to return all inventory.
            read.ReadData();
            //Sets datagrid data to the data from the database table.
            dataGridView1.DataSource = read.GetTable();
        }

        private void InventoryForm_Load(object sender, EventArgs e)
        {
            //Datagrid being loaded.
            dataGridLoad();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
            //When a row is clicked in the datagrid it will populate the textboxes with the associated information.
        {
            //No negative numbers.
            if (e.RowIndex >= 0)
            {
                //Setting textboxes to the values within the datagrid when clicked on.
                txtID.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                txtName.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                txtCompany.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                txtCategory.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                txtPrice.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                txtDate.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            }
        }

        private void delete_button_Click(object sender, EventArgs e)
            //When the sold button is clicked it passes the name of the selected row and marks it as sold in the database.
            //This is also a soft delete as the item will no longer show in inventory but is still accessible to the database adm.
        {
            DataBaseTranslator dbt = new DataBaseTranslator();
            //Calling soft delete method.
            dbt.DeleteData(txtName.Text);
            //Refreshing datagrid.
            dataGridLoad();
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Exit application.
            Application.Exit();
        }

        private void addItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Switch form to the add item screen.
            CreateItemForm form = new CreateItemForm();
            this.Visible = false;
            form.ShowDialog();
            Application.Exit();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Switch form to the login screen.
            LoginForm form = new LoginForm();
            this.Visible = false;
            form.ShowDialog();
            Application.Exit();
        }

        private void load_button_Click(object sender, EventArgs e)
            //When the update button is clicked it passes the text fields to the database translator to update the database.
        {
            //Conversions from string to appropriate values for the database.
            double price = Convert.ToDouble(txtPrice.Text);
            DateTime date = Convert.ToDateTime(txtDate.Text);
            //Creation of database object.
            DataBaseTranslator dbt = new DataBaseTranslator();
            //Calling the update method for the database.
            dbt.UpdateData(txtName.Text, txtCompany.Text, txtCategory.Text,price,date);
            //Refreshing datagrid.
            dataGridLoad();
            //Clears textboxes
            clearTextBoxes();
        }

        private void clearTextBoxes()
        {
            //Clears Textboxes.
            txtID.Text = null;
            txtName.Text = null;
            txtCompany.Text = null;
            txtCategory.Text = null;
            txtPrice.Text = null;
            txtDate.Text = null;
        }
    }
}
