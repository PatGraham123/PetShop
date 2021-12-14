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
    public partial class Profile : Form
    {
        public bool loggedIn = Login.loggedIn;

        public string userID { get; set; }
        public string Pass { get; set; }
        public string empID { get; set; }
        public string about { get; set; }
        public string name { get; set; }
        public string role { get; set; }

        public static List<Profile> returnList;

        /// <summary>
        /// Opens the Profile Form
        /// </summary>
        public Profile()
        {
            InitializeComponent();     
        }

        /// <summary>
        /// Opens the Profile Form and passes it a list
        /// </summary>
        /// <param name="abc"></param>
        public Profile(List<Profile> abc)
        {
            InitializeComponent();
            returnList = abc;
        }

        /// <summary>
        /// Opens the Profile Form and passes it a list and a string
        /// </summary>
        /// <param name="abc"></param>
        /// <param name="passedUser"></param>
        public Profile(List<Profile> abc, string passedUser)
        {
            InitializeComponent();
            returnList = abc;
        }


        /// <summary>
        /// Opens the Home Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void homeButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm Home = new MainForm();
            Home.Show();
        }

        /// <summary>
        /// Updates the Profile attributes with the values in the text boxes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void updateButton_Click( object sender, EventArgs e)
        {
            if (aboutTextBox.Text.Length > 180)
            {
                errorProviderProfile.SetError(this.aboutTextBox,"About Me must be a maximum of 180 characters");
            }
            if (nameTextBox.Text.Length > 30)
            {
                errorProviderProfile.SetError(this.nameTextBox, "Name must be a maximum of 30 characters");
            }
            if (roleTextBox.Text.Length > 30)
            {
                errorProviderProfile.SetError(this.roleTextBox, "Role must be a maximum of 30 characters");
            }
            if (aboutTextBox.Text.Length < 180 && nameTextBox.Text.Length < 30 && aboutTextBox.Text.Length < 180)
            {
                this.about = aboutTextBox.Text;
                this.name = nameTextBox.Text;
                this.role = roleTextBox.Text;
            }
        }

        /// <summary>
        /// Events that happen when the Profile Form loads
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Profile_Load( object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// Logs the current profile out
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void logoutButton_Click_1(object sender, EventArgs e)
        {
            loggedIn = false;
            this.Hide();  
        }

        /// <summary>
        /// Closes the Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Opens the Help Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void helpMenuItem_Click(object sender, EventArgs e)
        {
            Help helpForm = new Help();
            helpForm.Show();
        }

    }
}
