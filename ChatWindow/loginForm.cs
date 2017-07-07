using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatWindow
{
    public partial class loginForm : Form
    {

        public static string ip = "";
        public static string name = "";
        public loginForm()
        {
            InitializeComponent();
        }
        
        private void loginButton_Click(object sender, EventArgs e)
        {
            ip = ipBox.Text;
            name = nameBox.Text;

            Form chatForm = new chatForm();
            chatForm.Show();
            this.Visible = false;
        }

        private void onEnter_submit(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                loginButton_Click(sender, e);
            }
        }


    }
}
