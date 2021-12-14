using PetShop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace BillingInfo
{
    public partial class BillingInfoForm : Form
    {
        public bool loggedIn = Login.loggedIn;

        //private IEnumerable<searchButton> shoppingList;
        private string total;
        private string finalList;
        //public List<CartList> ShoppingList { get; }
        public static Dictionary<string, string> values =
        new Dictionary<string, string>();

        public BillingInfoForm()
        {
            InitializeComponent();
        }

        public BillingInfoForm(string fullList,string checkout)
        {
            
            InitializeComponent();
            total = checkout;
            finalList = fullList;
        }

        /// <summary>
        /// Loads the billing form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BillingInfoForm_Load(object sender, EventArgs e)
        {
            this.AutoValidate = AutoValidate.Disable;

            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "yyyy-MM-dd";

            loadListBox("states.txt");
            totalLabel.Text = total;
            listTextBox.Text = finalList;
        }

        /// <summary>
        /// Validates the name box. I added another condition in this and subsequent methods to catch white space since it wouldnt be a valid name.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nameTextBox_Validating(object sender, CancelEventArgs e)
        {
            var name = nameTextBox.Text;

            if (name.Length <= 0 || String.IsNullOrWhiteSpace(name))
            {
                errorProvider.SetError(this.nameTextBox, "Please enter a valid name.");

            }
            else
            {
                errorProvider.SetError(this.nameTextBox, "");
            }

        }

        /// <summary>
        /// Validates the address box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addressTextBox_Validating(object sender, CancelEventArgs e)
        {
            var address = addressTextBox.Text;


            if (address.Length <= 0 || String.IsNullOrWhiteSpace(address))
            {
                errorProvider.SetError(this.addressTextBox, "Please enter a valid address.");

            }
            else
            {
                errorProvider.SetError(this.addressTextBox, "");
            }
        }

        /// <summary>
        /// Validates the city box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cityTextBox_Validating(object sender, CancelEventArgs e)
        {
            var city = cityTextBox.Text;


            if (city.Length <= 0 || String.IsNullOrWhiteSpace(city))
            {
                errorProvider.SetError(this.cityTextBox, "Please enter a valid city.");

            }
            else
            {
                errorProvider.SetError(this.cityTextBox, "");
            }
        }

        /// <summary>
        /// Validates the postal code box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void postalCodeTextBox_Validating(object sender, CancelEventArgs e)
        {
            var postalCode = postalCodeTextBox.Text;


            if (postalCode.Length <= 0 || String.IsNullOrWhiteSpace(postalCode) || postalCode.Contains(" "))
            {
                errorProvider.SetError(this.postalCodeTextBox, "Please enter a valid postal code.");

            }
            else if (postalCode.Length != 5 || !(int.TryParse(postalCode, out int extra)))
            {
                errorProvider.SetError(this.postalCodeTextBox, "Please enter a valid five digit postal code.");
            }

            else
            {
                errorProvider.SetError(this.postalCodeTextBox, "");
            }
        }

        /// <summary>
        /// Validates the email box. 
        /// Checks for inclusion of an @ symbol, exclusion of empty fields, a string less than 3 characters (including @), and exclusion of whitespaces (" ").
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void emailTextBox_Validating(object sender, CancelEventArgs e)
        {
            var email = emailTextBox.Text;


            if (email.Length < 3 || String.IsNullOrWhiteSpace(email) || !(email.Contains("@")) || email.Contains(" ") || email.ToString().Substring(0,1) == "")
            {
                errorProvider.SetError(this.emailTextBox, "Please enter a valid email.");

            }

            else
            {
                errorProvider.SetError(this.emailTextBox, "");
            }
        }

        /// <summary>
        /// Validates the securty code box.
        /// Note: For Visa, American Express, MasterCard, and Discover, CVV's (security code) must be at leats 3 digits.
        /// If exceptions to this rule are added change the logic marked with the *** moniker.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void securityCodeTextBox_Validating(object sender, CancelEventArgs e)
        {

            string securityCode = securityCodeTextBox.Text;

            ///***///
            if (securityCode.Length < 3 || String.IsNullOrWhiteSpace(securityCode) || securityCode.Contains(" "))
            {
                errorProvider.SetError(this.securityCodeTextBox, "Please enter a valid security code.");

            }
            else
            {
                errorProvider.SetError(this.securityCodeTextBox, "");
            }
        }

        /// <summary>
        /// Closes the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Validates the credit card and submits the data to the order history
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void submitButton_Click(object sender, EventArgs e)
        {
             DateTime date = DateTime.Now;
             bool validSubmit = ValidateChildren();
            
             if (validSubmit)
             {
                 string cardNumber = creditCardTextBox.Text.ToString();
                 int length = cardNumber.Length;
                 string firstDigit;
                 if (length > 0)
                 {
                    firstDigit = cardNumber.Substring(0, 1);
                 }
                 else
                 {
                     firstDigit = "0";
                 }

                 var cardType = "";
                 if (firstDigit == "4")
                 {
                     cardType = "Visa";
                 }
                 else if (firstDigit == "5")
                 {
                     americanExpressPictureBox.Visible = false;
                     discoverPictureBox.Visible = false;
                     masterCardPictureBox.Visible = true;
                     visaPictureBox.Visible = false;
                     cardType = "MasterCard";
                 }
                 else if (firstDigit == "6")
                 {
                     americanExpressPictureBox.Visible = false;
                     discoverPictureBox.Visible = true;
                     masterCardPictureBox.Visible = false;
                     visaPictureBox.Visible = false;
                     cardType = "Discover";
                 }
                 else if (firstDigit == "3" && length >= 2)
                 {
                     string secondDigit = cardNumber.Substring(1, 1);

                     if (secondDigit == "7")
                     {
                         americanExpressPictureBox.Visible = true;
                         discoverPictureBox.Visible = false;
                         masterCardPictureBox.Visible = false;
                         visaPictureBox.Visible = false;
                         cardType = "American Express";
                     }

                 }

                 
                 if (!(String.IsNullOrEmpty(nameTextBox.Text.ToString())) && !(String.IsNullOrEmpty(addressTextBox.Text.ToString())) && !(String.IsNullOrEmpty(cityTextBox.Text.ToString())) 
                     && stateListBox.SelectedIndex != -1 && !(String.IsNullOrEmpty(postalCodeTextBox.Text.ToString())) && !(String.IsNullOrEmpty(emailTextBox.Text.ToString())) 
                     &&!(String.IsNullOrEmpty(cardType.ToString())) && !(String.IsNullOrEmpty(creditCardTextBox.Text.ToString())) && !(String.IsNullOrEmpty(dateTimePicker1.Value.ToString())) 
                     && !String.IsNullOrEmpty(securityCodeTextBox.Text.ToString()) && long.TryParse(creditCardTextBox.Text, out long x) && int.TryParse(postalCodeTextBox.Text, out int y)
                     && int.TryParse(securityCodeTextBox.Text, out int test) && emailTextBox.Text.ToString().Contains('@') && emailTextBox.Text.Length >= 3)
                 {
                     
                    values.Add(date.ToString(), (finalList + '\n' + " Total: " + total));
                    MessageBox.Show("___ORDER CONFIRMATION___" + '\n' + "Date: " + date.ToString() + '\n' + finalList + '\n' + " Total: " + total);
                    Items history = new Items(values);
                    history.Show();
                }
                 else
                 {
                     MessageBox.Show("Something went wrong. Please make sure all fields are completed.");
                 }

                
            }
            
            
            





        }

        /// <summary>
        /// Validates the card number
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void creditCardTextBox_Validating(object sender, CancelEventArgs e)
        {
            var cardString = creditCardTextBox.Text;
            long.TryParse(cardString, out var cardNumber);

            var valid = isCreditCardValid(cardNumber);
            if (valid == false)
            {
                errorProvider.SetError(this.creditCardTextBox, "Not a valid card number");
                creditCardTextBox.Text = "";
            }

        }

        /// <summary>
        /// Checks if the entered credit card is valid
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        private bool isCreditCardValid(long number)
        {
            long cardNumber;


            var length = number.ToString().Length;
            if (length < 13 || length > 16)
            {
                creditCardTextBox.Text = number.ToString();
                return false;
            }
            else
            {             
                if (number.ToString().Substring(0, 1) == "4" || number.ToString().Substring(0, 1) == "5" || number.ToString().Substring(0, 1) == "6" || number.ToString().Substring(0, 1) == "3")
                {
                    cardNumber = (long)number;
                }
                else if ((number.ToString().Substring(0, 1) == "3"))
                {
                    if (number.ToString().Substring(1, 1) == "7")
                    {
                        cardNumber = (long)number;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
                ////////////////
                var sumOdd = sumOfOddPlaces(cardNumber);
                var sumEven = sumOfEvenPlaces(cardNumber);
                var sum = sumOdd + sumEven;
                Console.WriteLine(sum);
                if (sum % 10 == 0)
                {
                    errorProvider.SetError(this.creditCardTextBox, "");
                    return true;
                }
                else
                {
                    errorProvider.SetError(this.creditCardTextBox, "Not a valid Credit Card");
                    return false;
                }
            }
        }

        /// <summary>
        /// Gets the sum of odd places for use in Luhn's Algorithm
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public int sumOfOddPlaces(long num)
        {
            long number = num;
            long sum = 0;
            int index = 0;

            while (number > 0)
            {
                if (index == 0)
                {
                    number = number / 10;
                    index++;
                }

                var numString = number.ToString();
                var length = numString.Length;
                //Console.WriteLine(hi.Substring(length-1,1));
                int.TryParse(numString.Substring(length - 1, 1), out int digit);
                //Console.WriteLine(digit);
                digit = digit * 2;
                var total = digit;

                if (digit > 9)
                {
                    var temp = digit.ToString();
                    int.TryParse(temp.Substring(0, 1), out int firstDigit);
                    int.TryParse(temp.Substring(1, 1), out int secondDigit);
                    total = firstDigit + secondDigit;
                    Console.WriteLine(total);
                    sum += total;
                }
                else
                {
                    Console.WriteLine(total);
                    sum += total;
                }

                number = number / 100;
                length = length - 2;
            }
            return (int)sum;
        }

        /// <summary>
        /// Gets the sum of even places for use in Luhn's Algorithm
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public int sumOfEvenPlaces(long number)
        {
            int sum = 0;
            var numString = number.ToString();
            var length = numString.Length;

            while (number > 0)
            {
                //Console.WriteLine(hi.Substring(length-1,1));
                int.TryParse(numString.Substring(length - 1, 1), out int digit);
                //Console.WriteLine(digit);
                //digit = digit * 2;
                var total = digit;
                sum += total;
                number = number / 100;
                length = length - 2;
            }
            return sum;
        }

        /// <summary>
        /// Shows which brand of card is being entered
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void creditCardTextBox_TextChanged(object sender, EventArgs e)
        {
            setCreditCardVisibility();
        }

        /// <summary>
        /// Makes images of cards enable and disable based on the entered credit card number
        /// </summary>
        public void setCreditCardVisibility()
        {
            string creditString = creditCardTextBox.Text;
            int length = creditString.Length;

            if (length == 0)
            {
                americanExpressPictureBox.Visible = true;
                discoverPictureBox.Visible = true;
                masterCardPictureBox.Visible = true;
                visaPictureBox.Visible = true;
            }
            else
            {
                string firstDigit = creditString.Substring(0, 1);

                if (firstDigit == "4")
                {
                    americanExpressPictureBox.Visible = false;
                    discoverPictureBox.Visible = false;
                    masterCardPictureBox.Visible = false;
                    visaPictureBox.Visible = true;
                }
                else if (firstDigit == "5")
                {
                    americanExpressPictureBox.Visible = false;
                    discoverPictureBox.Visible = false;
                    masterCardPictureBox.Visible = true;
                    visaPictureBox.Visible = false;
                }
                else if (firstDigit == "6")
                {
                    americanExpressPictureBox.Visible = false;
                    discoverPictureBox.Visible = true;
                    masterCardPictureBox.Visible = false;
                    visaPictureBox.Visible = false;
                }
                else if (firstDigit == "3" && length >= 2)
                {
                    string secondDigit = creditString.Substring(1, 1);

                    if (secondDigit == "7")
                    {
                        americanExpressPictureBox.Visible = true;
                        discoverPictureBox.Visible = false;
                        masterCardPictureBox.Visible = false;
                        visaPictureBox.Visible = false;
                    }

                }
                else
                {
                    americanExpressPictureBox.Visible = true;
                    discoverPictureBox.Visible = true;
                    masterCardPictureBox.Visible = true;
                    visaPictureBox.Visible = true;
                }

            }

        }

        
       /// <summary>
       /// Ensures the card expiration date is after the current date
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
       private void dateTimePicker1_Validating(object sender, CancelEventArgs e)
        {
            var output = DateTime.Compare(dateTimePicker1.Value, DateTime.Today);
            
            if (output > 0 )
            {
                errorProvider.SetError(this.dateTimePicker1, "");
            }
            else
            {
                errorProvider.SetError(this.dateTimePicker1, "Please select a valid expiration date");
            }
        }

        /// <summary>
        /// Validates the state listbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void stateListBox_Validating(object sender, CancelEventArgs e)
        {
            if (stateListBox.SelectedIndex != -1)
            {
                errorProvider.SetError(this.stateListBox, "");
            }
            else
            {
                errorProvider.SetError(this.stateListBox, "Please select a valid state");
            }
        }

        private void loadListBox(string fileToLoad)
        {
            var fileName = fileToLoad;
            if (File.Exists(fileName))
            {
                using (StreamReader txtFile = new StreamReader(fileName))
                {
                    string stateName;

                    while ((stateName = txtFile.ReadLine()) != null)
                    {
                        stateListBox.Items.Add(stateName);
                    }
                }
            }
        }

    //End of class
    }

//End of namespace
}
