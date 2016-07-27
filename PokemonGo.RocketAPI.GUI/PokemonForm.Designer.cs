namespace PokemonGo.RocketAPI.GUI
{
    partial class PokemonForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PokemonForm));
            this.pokemonListView = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // pokemonListView
            // 
            this.pokemonListView.BackColor = System.Drawing.SystemColors.Control;
            this.pokemonListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pokemonListView.GridLines = true;
            this.pokemonListView.Location = new System.Drawing.Point(0, 0);
            this.pokemonListView.Name = "pokemonListView";
            this.pokemonListView.Size = new System.Drawing.Size(582, 302);
            this.pokemonListView.TabIndex = 0;
            this.pokemonListView.UseCompatibleStateImageBehavior = false;
            // 
            // PokemonForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 302);
            this.Controls.Add(this.pokemonListView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PokemonForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "My Pokemon List";
            this.Load += new System.EventHandler(this.PokemonForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView pokemonListView;
    }
}