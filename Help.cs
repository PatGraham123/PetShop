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
    public partial class Help : Form
    {
        public bool loggedIn = Login.loggedIn;

        /// <summary>
        /// Loads the Help Form
        /// </summary>
        public Help()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Events that happen when the Help Form loads
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Help_Load(object sender, EventArgs e)
        {

        }
    }
}
