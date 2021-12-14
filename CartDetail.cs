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
    public partial class CartDetail : Form
    {
        searchButton obj = new searchButton();
        public bool loggedIn = Login.loggedIn;

        public CartDetail()
        {
            InitializeComponent();
        }

        private void cartTableBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.cartTableBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.cartDataSet);

        }

        private void CartDetail_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'cartDataSet.CartTable' table. You can move, or remove it, as needed.
            //this.cartTableTableAdapter.Fill(this.cartDataSet.CartTable);

            string confirm = searchButton.checkout;
            checkoutTextBox.Text = confirm;
        }
    }
}
