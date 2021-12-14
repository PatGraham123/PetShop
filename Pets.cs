using BillingInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PetShop
{
    public partial class searchButton : Form
    {
        public static List<CartList> shoppingList = new List<CartList>();
        public static List<string> nameList = new List<string>();
        public bool loggedIn = Login.loggedIn;
        public static string checkout;
        public static string fullList;
        public static CartList itemToRemove;

        //public DataTable PetTable { get; private set; }

        /// <summary>
        /// Opens the Pets Form
        /// </summary>
        public searchButton()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets the total cost of the order
        /// </summary>
        /// <returns></returns>
        public string getCost()
        {
            double sum = 0;
            double total = 0;
            foreach (CartList x in searchButton.shoppingList)
            {
                sum = x.Cost;
                total += sum;
            }
            checkout = total.ToString();
            return checkout;
        }

        /// <summary>
        /// Loads the Pet Table
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void addPetForm_Load(object sender, EventArgs e)
        {
            this.petTableTableAdapter.Fill(this.petShopDataSet.PetTable);
            refreshList();
        }

        /// <summary>
        /// Saves any changes to the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void petTableBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.petTableBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.petShopDataSet);
        }

        /// <summary>
        /// If logged in, adds the record to the table
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addButton_Click_1(object sender, EventArgs e)
        {
            if (loggedIn)
            {
                AddRecords();
            }
            else
            {
                MessageBox.Show("Please Login");
            }
        }

        /// <summary>
        /// Adds a row to the table with values pulled from the text boxes.
        /// </summary>
        private void AddRecords()
        {
            var costString = costTextBox.Text;
            Double.TryParse(costString, out double cost);

            if(string.IsNullOrWhiteSpace(nameTextBox.Text))
            {
                errorProviderPets.SetError(this.nameTextBox, "Please enter a valid name");
            }

            else if (string.IsNullOrWhiteSpace(typeTextBox.Text))
            {
                errorProviderPets.SetError(this.typeTextBox, "Please enter a valid type");
            }
            
            else if (string.IsNullOrWhiteSpace(costTextBox.Text) || !Double.TryParse(costString, out double test))
            {
                errorProviderPets.SetError(this.costTextBox, "Please enter a valid price");
            }
             
            else
            {
                errorProviderPets.SetError(this.nameTextBox, "");
                errorProviderPets.SetError(this.typeTextBox, "");
                errorProviderPets.SetError(this.costTextBox, "");

                this.petTableTableAdapter.InsertQuery1(nameTextBox.Text, typeTextBox.Text, cost);
                this.petTableTableAdapter.Fill(this.petShopDataSet.PetTable);
            }

            
                
            
        }

        /// <summary>
        /// Sorts the table by type
        /// </summary>
        private void SortType()
        {
            this.petTableTableAdapter.FillByTypeAsc(this.petShopDataSet.PetTable);
        }

        /// <summary>
        /// Adds the selected item to the checkout list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listButton_Click(object sender, EventArgs e)
        {
            //Double.TryParse(costBox.Text, out double result);
            //cartDict.Add(listTextBox.Text, (float)result);

            string name = nameBox.Text;
            string type = typeBox.Text;
            var qtyString = qtyTextBox.Text;
            var costString = costBox.Text;

            int.TryParse(qtyString, out int qty);
            Double.TryParse(costString, out double cost);
            cost = cost * qty;

            if (name != "" && type != "" && qtyString != "" && costString != "" && loggedIn)
            {
                if (nameList.Contains(name))
                {
                    return;
                }
                else
                {
                    nameList.Add(name);
                    shoppingList.Add(new CartList()
                    {
                        Name = name,
                        Type = type,
                        Qty = qty,
                        Cost = cost
                    });
                }
            }
            else
            {
                MessageBox.Show("Please Login");
            }
            
            listTextBox.Text = "";
            foreach (CartList item in searchButton.shoppingList)
            {
                listTextBox.Text = listTextBox.Text + " " + item.Name + " " + item.Type + " " + item.Qty + " " + item.Cost + "\r\n";
            }
            fullList = listTextBox.Text;
        }

        /// <summary>
        /// Populates the costBox field
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void costTextBox_TextChanged(object sender, EventArgs e)
        {
            costBox.Text = costTextBox.Text;
        }

        /// <summary>
        /// Updates the quantity and resulting price of the specified item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void updateButton_Click(object sender, EventArgs e)
        {
            var updateName = updateNameTextBox.Text;
            var updateQty = updateQtyTextBox.Text;
            if (loggedIn)
            {
                foreach (CartList item in searchButton.shoppingList)
                {
                    double originalCost = item.Cost;
                    if (item.Name == updateName)
                    {
                        Double.TryParse(updateQty, out double newQty);
                        double oldQty = item.Qty;
                        item.Qty = newQty;
                        if (item.Qty == 0 || string.IsNullOrWhiteSpace(updateQty))
                        {
                            errorProviderPets.SetError(updateQtyTextBox, "New quantity can't be 0");
                            return;
                        }
                        if (oldQty > item.Qty)
                        {
                            item.Cost = (item.Qty / oldQty) * item.Cost;
                        }
                        else
                        {
                            item.Cost = item.Cost * item.Qty;
                        }
                    }
                }
                listTextBox.Text = "";
                refreshList();
            }
            
        }

        /// <summary>
        /// Searches the table for a specified name
        /// </summary>
        /// <param name="name"></param>
        private void searchByName(string name)
        {
            this.petTableTableAdapter.FillByName(this.petShopDataSet.PetTable,nameTextBox.Text);
        }

        /// <summary>
        /// Refreshes the shopping list
        /// </summary>
        public void refreshList()
        {
            foreach (CartList x in searchButton.shoppingList)
            {
                listTextBox.Text = listTextBox.Text + " " + x.Name + " " + x.Type + " " + x.Qty + " " + x.Cost + "\r\n";
            }
        }

        /// <summary>
        /// Opens the Billing Info Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cartButton_Click(object sender, EventArgs e)
        {
            if (loggedIn)
            {
                fullList = listTextBox.Text;
                checkout = ("$" + getCost());
                BillingInfoForm cart = new BillingInfoForm(fullList, checkout);
                cart.Show();
            }
            
        }

        /// <summary>
        /// Removes an item from the shopping list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void removeButton_Click(object sender, EventArgs e)
        {
            var removeName = updateNameTextBox.Text;
            var updateQty = updateQtyTextBox.Text;
            if (loggedIn)
            {
                foreach (CartList item in searchButton.shoppingList)
                {
                    double originalCost = item.Cost;
                    if (item.Name == removeName)
                    {
                        item.Qty = 0;
                        itemToRemove = item;
                    }
                }
                if (itemToRemove != null)
                {
                    shoppingList.Remove(itemToRemove);
                    nameList.Remove(itemToRemove.Name);

                }
                listTextBox.Text = "";
                refreshList();
            }
            
        }

        /// <summary>
        /// Query to delete an item from the table
        /// </summary>
        /// <param name="name"></param>
        private void Delete(string name)
        {
            if (loggedIn)
            {
                string x = name;
                this.petTableTableAdapter.DeleteQuery(x);
                this.petTableTableAdapter.Fill(this.petShopDataSet.PetTable);
            }
            
        }

        /// <summary>
        /// Checks to make sure user is logged in before deleting an item from the table
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void removeDBButton_Click(object sender, EventArgs e)
        {
            if (loggedIn)
            {
                Delete(nameTextBox.Text);
            }
        }

        /// <summary>
        /// Populates text fields to show the user the selected item that will be added to the shopping list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void petTableDataGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (petTableDataGridView.CurrentCell.RowIndex != -1)
            {
                var currentIndex = petTableDataGridView.CurrentCell.RowIndex;
                BindingSource BS = (BindingSource)petTableDataGridView.DataSource;
                DataSet ds = (DataSet)BS.DataSource;
                DataTable dt = ds.Tables[0];
                if (currentIndex < dt.Rows.Count)
                {
                    nameBox.Text = dt.Rows[currentIndex]["Name"].ToString();
                    typeBox.Text = dt.Rows[currentIndex]["Type"].ToString();
                    qtyTextBox.Text = "1";
                    costBox.Text = dt.Rows[currentIndex]["Cost"].ToString();
                }
            }
        }

        /// <summary>
        /// Calls the sort by type method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sortButton_Click(object sender, EventArgs e)
        {
            SortType();
        }

        /// <summary>
        /// Searches the table for a specified name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            searchByName(nameTextBox.Text);
        }

        private void exitMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void helpMenuItem_Click(object sender, EventArgs e)
        {
            Help helpForm = new Help();
            helpForm.Show();
        }
    }
}

