using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventorySystem
{
    public partial class CreateItemForm : Form
    {
        public CreateItemForm()
        {
            InitializeComponent();
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Application exit.
            Application.Exit();
        }

        private void homeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Switches to the inventory form.
            InventoryForm form = new InventoryForm();
            this.Visible = false;
            form.ShowDialog();
            Application.Exit();
        }

        private void createButton_Click(object sender, EventArgs e)
            //When the create button is clicked the text fields are passes to the translator class and then inserted into the database.
        {
            //Conversions from string to appropriate values for the database.
            double price = Convert.ToDouble(crtPrice.Text);
            DateTime date = Convert.ToDateTime(crtDate.Text);
            //Creation of database object.
            DataBaseTranslator dbt = new DataBaseTranslator();
            //Calling the update method for the database.
            dbt.InsertData(crtName.Text, crtCompany.Text, crtCategory.Text, price, date);
            //Makes success label visible.
            successLabel.Visible = true;
            //Clears textboxes for the next entry.
            clearTextFields();
        }

        private void clearTextFields()
        {
            //Clears the textbox fields.
            crtName.Text = null;
            crtCategory.Text = null;
            crtCompany.Text = null;
            crtPrice.Text = null;
            crtDate.Text = null;
        }
    }
}
