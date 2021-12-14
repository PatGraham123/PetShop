using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace PetShop
{
    
    public partial class Login : Form    
    {
        public static bool loggedIn;
        static string userID = "";
        public static List<Profile> returnList = new List<Profile>();

        /// <summary>
        /// Opens the Login Form
        /// </summary>
        public Login()
        {
            InitializeComponent();

        }

        /// <summary>
        /// Sets the flag variable loggedIn and loads the user's Profile
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loginButton_Click(object sender, EventArgs e)
        {
            List<Profile> newList = returnList;
            loggedIn = getLogin();
            
            if (IDTextBox.Text.Length <= 20 && IDTextBox.Text.Length >= 8 && passwordTextBox.Text.Length <= 20 && passwordTextBox.Text.Length >= 8)
            foreach (Profile check in newList)
                {
                    if (check.userID == IDTextBox.Text && check.Pass == passwordTextBox.Text)
                    {
                        this.Close();
                        loggedIn = true;
                        check.Show();
                    }
                } 
        }

       
        /// <summary>
        /// Adds login credentials to the list of valid Profiles
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void createButton_Click_1(object sender, EventArgs e)
        {
            var ID1 = createIDTextBox.Text.ToString();
            var pass1 = createPasswordTextBox.Text.ToString();
            
            if (ID1.Length > 20 || ID1.Length < 8)
            {
                errorProviderLogin.SetError(this.createIDTextBox, "Usernames must be at least 8 characters and below 21 characters");
            }
            else
            {
                errorProviderLogin.SetError(this.createIDTextBox, "");
            }
            if (pass1.Length > 20 || pass1.Length < 8)
            {
                errorProviderLogin.SetError(this.createPasswordTextBox, "Passwords must be below 21 characters");  
            }
            else
            {
                errorProviderLogin.SetError(this.createPasswordTextBox, "");
            }

            if(ID1.Length <= 20 && ID1.Length >= 8 && pass1.Length <= 20 && pass1.Length >= 8)
            {
                returnList.Add(new Profile(returnList, userID)
                {
                    userID = ID1,
                    Pass = pass1,
                    empID = (returnList.Count).ToString(),
                    name = "a",
                    role = "b",
                    about = "c"
                });
                MessageBox.Show("Profile Created!");
            }
        }

        /// <summary>
        /// DO NOT ERASE.
        /// </summary>
        /// <param name="v"></param>
        public static implicit operator bool(Login v)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Loads the Login Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Login_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Validates that the profile exists in the list.
        /// </summary>
        /// <returns></returns>
        public bool getLogin()
        {

            string ID = IDTextBox.Text;
            string pass = passwordTextBox.Text;
            int i = 0;

            if (IDTextBox.Text.Length > 20 || IDTextBox.Text.Length < 8)
            {
                errorProviderLogin.SetError(this.IDTextBox, "Usernames must be at least 8 characters and below 21 characters");
            }
            else
            {
                errorProviderLogin.SetError(this.IDTextBox, "");
            }
            if (passwordTextBox.Text.Length > 20 || passwordTextBox.Text.Length < 8)
            {
                errorProviderLogin.SetError(this.passwordTextBox, "Passwords must be at least 8 characters and below 21 characters");
            }
            else
            {
                errorProviderLogin.SetError(this.passwordTextBox, "");
            }

            if (IDTextBox.Text.Length <= 20 && IDTextBox.Text.Length >= 8 && passwordTextBox.Text.Length <= 20 && passwordTextBox.Text.Length >= 8)
            {
                foreach (Profile q in returnList)
                {
                    if (ID == returnList[i].userID.ToString())
                    {
                        for (int x = 0; x < returnList.Count(); x++)
                        {
                            if (pass == returnList[x].Pass.ToString())
                            {
                                loggedIn = true;
                                return loggedIn;
                            }
                        }
                    }
                }  
            }
            return false;
        }
    }
}
