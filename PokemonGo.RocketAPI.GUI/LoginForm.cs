using PokemonGo.RocketAPI.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PokemonGo.RocketAPI.GUI
{
    public partial class LoginForm : Form
    {
        public AuthType auth;
        public bool loginSelected = false;

        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
        }

        private void btnPtcLogin_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(boxUsername.Text))
            {
                if (!string.IsNullOrWhiteSpace(boxPassword.Text))
                {
                    loginSelected = true;
                    auth = AuthType.Ptc;
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Password textbox cannot be empty", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Username textbox cannot be empty", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGoogleLogin_Click(object sender, EventArgs e)
        {
            loginSelected = true;
            auth = AuthType.Google;
            this.Hide();
        }

        private void boxPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnPtcLogin_Click(null, null);
        }

        private void btnResetToken_Click(object sender, EventArgs e)
        {
            UserSettings.Default.GoogleRefreshToken = string.Empty;
            UserSettings.Default.Save();

            btnGoogleLogin_Click(null, null);
        }
    }
}
