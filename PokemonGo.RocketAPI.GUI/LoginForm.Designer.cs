namespace PokemonGo.RocketAPI.GUI
{
    partial class LoginForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.btnGoogleLogin = new System.Windows.Forms.Button();
            this.btnPtcLogin = new System.Windows.Forms.Button();
            this.boxPassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.boxUsername = new System.Windows.Forms.TextBox();
            this.btnResetToken = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnGoogleLogin
            // 
            this.btnGoogleLogin.Location = new System.Drawing.Point(58, 16);
            this.btnGoogleLogin.Name = "btnGoogleLogin";
            this.btnGoogleLogin.Size = new System.Drawing.Size(129, 41);
            this.btnGoogleLogin.TabIndex = 3;
            this.btnGoogleLogin.Text = "Login";
            this.btnGoogleLogin.UseVisualStyleBackColor = true;
            this.btnGoogleLogin.Click += new System.EventHandler(this.btnGoogleLogin_Click);
            // 
            // btnPtcLogin
            // 
            this.btnPtcLogin.Location = new System.Drawing.Point(166, 67);
            this.btnPtcLogin.Name = "btnPtcLogin";
            this.btnPtcLogin.Size = new System.Drawing.Size(68, 28);
            this.btnPtcLogin.TabIndex = 2;
            this.btnPtcLogin.Text = "Login";
            this.btnPtcLogin.UseVisualStyleBackColor = true;
            this.btnPtcLogin.Click += new System.EventHandler(this.btnPtcLogin_Click);
            // 
            // boxPassword
            // 
            this.boxPassword.Location = new System.Drawing.Point(72, 41);
            this.boxPassword.Name = "boxPassword";
            this.boxPassword.PasswordChar = '*';
            this.boxPassword.Size = new System.Drawing.Size(161, 20);
            this.boxPassword.TabIndex = 1;
            this.boxPassword.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.boxPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.boxPassword_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Password";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Username";
            // 
            // boxUsername
            // 
            this.boxUsername.Location = new System.Drawing.Point(72, 15);
            this.boxUsername.Name = "boxUsername";
            this.boxUsername.Size = new System.Drawing.Size(161, 20);
            this.boxUsername.TabIndex = 0;
            this.boxUsername.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnResetToken
            // 
            this.btnResetToken.ForeColor = System.Drawing.Color.Red;
            this.btnResetToken.Location = new System.Drawing.Point(58, 63);
            this.btnResetToken.Name = "btnResetToken";
            this.btnResetToken.Size = new System.Drawing.Size(129, 26);
            this.btnResetToken.TabIndex = 6;
            this.btnResetToken.Text = "Reset Token";
            this.btnResetToken.UseVisualStyleBackColor = true;
            this.btnResetToken.Click += new System.EventHandler(this.btnResetToken_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(258, 135);
            this.tabControl1.TabIndex = 7;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.btnPtcLogin);
            this.tabPage1.Controls.Add(this.boxPassword);
            this.tabPage1.Controls.Add(this.boxUsername);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(250, 109);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "PTC Login";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnGoogleLogin);
            this.tabPage2.Controls.Add(this.btnResetToken);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(250, 109);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Google Login";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(258, 135);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select Login Method";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnGoogleLogin;
        private System.Windows.Forms.Button btnPtcLogin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox boxPassword;
        public System.Windows.Forms.TextBox boxUsername;
        private System.Windows.Forms.Button btnResetToken;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
    }
}