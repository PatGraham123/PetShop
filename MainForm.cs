using BillingInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PetShop
{
    public partial class MainForm : Form
    {
       public bool loggedIn = Login.loggedIn;


        public MainForm()
        {
            InitializeComponent();
            
            
        }

        public void MainForm_Load(object sender, EventArgs e)
        {
            if (loggedIn)
            {
                itemsButton.Enabled = true;
            }
            else
            {
                itemsButton.Enabled = false;
            }


        }

        /// <summary>
        /// Exits the program
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void helpMenuItem_Click(object sender, EventArgs e)
        {
            
            Help helpForm = new Help();
            helpForm.Show();

        }

        private void petsButton_Click(object sender, EventArgs e)
        {
            
            searchButton petForm = new searchButton();
            petForm.Show();
            
        }

        private void profileButton_Click(object sender, EventArgs e)
        {
            var valid = Login.loggedIn;
         
                Login loginForm = new Login();
                loginForm.Show();
            
        }

        private void itemsButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            
                Items itemForm = new Items();
                itemForm.Show();

        }

        private void cartButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            BillingInfoForm cartForm = new BillingInfoForm("","");
            cartForm.Show();
        }
    }
}
