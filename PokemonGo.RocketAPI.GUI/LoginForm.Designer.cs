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
            this.SuspendLayout();
            // 
            // btnGoogleLogin
            // 
            this.btnGoogleLogin.Location = new System.Drawing.Point(12, 64);
            this.btnGoogleLogin.Name = "btnGoogleLogin";
            this.btnGoogleLogin.Size = new System.Drawing.Size(118, 28);
            this.btnGoogleLogin.TabIndex = 3;
            this.btnGoogleLogin.Text = "Google Login";
            this.btnGoogleLogin.UseVisualStyleBackColor = true;
            this.btnGoogleLogin.Click += new System.EventHandler(this.btnGoogleLogin_Click);
            // 
            // btnPtcLogin
            // 
            this.btnPtcLogin.Location = new System.Drawing.Point(150, 64);
            this.btnPtcLogin.Name = "btnPtcLogin";
            this.btnPtcLogin.Size = new System.Drawing.Size(84, 28);
            this.btnPtcLogin.TabIndex = 2;
            this.btnPtcLogin.Text = "PTC Login";
            this.btnPtcLogin.UseVisualStyleBackColor = true;
            this.btnPtcLogin.Click += new System.EventHandler(this.btnPtcLogin_Click);
            // 
            // boxPassword
            // 
            this.boxPassword.Location = new System.Drawing.Point(72, 38);
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
            this.label1.Location = new System.Drawing.Point(13, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Password";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Username";
            // 
            // boxUsername
            // 
            this.boxUsername.Location = new System.Drawing.Point(72, 12);
            this.boxUsername.Name = "boxUsername";
            this.boxUsername.Size = new System.Drawing.Size(161, 20);
            this.boxUsername.TabIndex = 0;
            this.boxUsername.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(245, 102);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.boxUsername);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.boxPassword);
            this.Controls.Add(this.btnPtcLogin);
            this.Controls.Add(this.btnGoogleLogin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login Form";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGoogleLogin;
        private System.Windows.Forms.Button btnPtcLogin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox boxPassword;
        public System.Windows.Forms.TextBox boxUsername;
    }
}