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
    
    public partial class Items : Form
    {
        public bool loggedIn = Login.loggedIn;
        public static string checkout;
        public static string fullList;
        public static DateTime date;
        public static Dictionary<string, string> values =
        new Dictionary<string, string>();


        /// <summary>
        /// Loads the Items Form
        /// </summary>
        public Items()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Loads the Items Form and passes a dictionary and a string.
        /// </summary>
        /// <param name="x"></param>
        public Items(Dictionary<string, string> x)
        {
            InitializeComponent();
            values = x;
            
            foreach (KeyValuePair<string, string> receipt in values)
            {
                ordersListBox.Items.Add(receipt.Key);
            }
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

        /// <summary>
        /// Exits the Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Opens the Home Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void homeButton_Click(object sender, EventArgs e)
        {
            MainForm Home = new MainForm();
            Home.Show();
        }

        /// <summary>
        /// Opens the Cart Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cartButton_Click(object sender, EventArgs e)
        {
            if (loggedIn)
            {
                BillingInfoForm cartForm = new BillingInfoForm("", "");
                cartForm.Show();
            }
            else
            {
                MessageBox.Show("Please Login");
            }
            
        }

        /// <summary>
        /// Opens the Login Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void profileButton_Click(object sender, EventArgs e)
        {
                Login loginForm = new Login();
                loginForm.Show();  
        }

        /// <summary>
        /// Events that happen when the Item Form loads
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Items_Load_1(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// Loads the Pets Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void petsButton_Click(object sender, EventArgs e)
        {
            searchButton petForm = new searchButton();
            petForm.Show();
        }

        /// <summary>
        /// Populates the search box with the selected item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ordersListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            searchTextBox.Text = ordersListBox.SelectedItem.ToString();
        }

        /// <summary>
        /// Searches the dictionary for a specified key
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void searchButton_Click(object sender, EventArgs e)
        {
            foreach (KeyValuePair<string, string> receipt in values)
            {
                if (searchTextBox.Text == receipt.Key)
                {
                    detailsTextBox.Text = receipt.Value;
                }
            }
        }
    }

    /*
    public class ListItem
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
    */
}

/*
 *public bool loggedIn = Login.loggedIn;

        //private IEnumerable<searchButton> shoppingList;
        private string total;
        private string finalList;
        //public List<CartList> ShoppingList { get; }
        public static Dictionary<string, string> values =
        new Dictionary<string, string>();
 */