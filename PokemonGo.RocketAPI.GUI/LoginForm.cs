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

        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void btnPtcLogin_Click(object sender, EventArgs e)
        {
            auth = AuthType.Ptc;
            this.Hide();
        }

        private void btnGoogleLogin_Click(object sender, EventArgs e)
        {
            auth = AuthType.Google;
            this.Hide();
        }

        private void boxPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnPtcLogin_Click(null, null);
        }
    }
}
