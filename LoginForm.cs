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
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Equals("user") && textBox2.Text.Equals("password"))
            {
                //Switches to the inventory form.
                InventoryForm form = new InventoryForm();
                this.Visible = false;
                form.ShowDialog();
                Application.Exit();
            }
            else
            {
                label4.Text = "Incorrect Credentials";
            }
        }
    }
}
