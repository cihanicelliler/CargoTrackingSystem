namespace CargoTrackingSystem
{
    partial class RegisterForUser
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
            this.txtBoxPasswordRegister = new System.Windows.Forms.TextBox();
            this.txtBoxUserNameRegister = new System.Windows.Forms.TextBox();
            this.btnRegister2 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtBoxPasswordRegister
            // 
            this.txtBoxPasswordRegister.Location = new System.Drawing.Point(635, 142);
            this.txtBoxPasswordRegister.Name = "txtBoxPasswordRegister";
            this.txtBoxPasswordRegister.Size = new System.Drawing.Size(152, 26);
            this.txtBoxPasswordRegister.TabIndex = 6;
            // 
            // txtBoxUserNameRegister
            // 
            this.txtBoxUserNameRegister.Location = new System.Drawing.Point(636, 110);
            this.txtBoxUserNameRegister.Name = "txtBoxUserNameRegister";
            this.txtBoxUserNameRegister.Size = new System.Drawing.Size(152, 26);
            this.txtBoxUserNameRegister.TabIndex = 5;
            // 
            // btnRegister2
            // 
            this.btnRegister2.BackColor = System.Drawing.Color.DarkCyan;
            this.btnRegister2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnRegister2.ForeColor = System.Drawing.SystemColors.Control;
            this.btnRegister2.Location = new System.Drawing.Point(634, 174);
            this.btnRegister2.Name = "btnRegister2";
            this.btnRegister2.Size = new System.Drawing.Size(153, 65);
            this.btnRegister2.TabIndex = 3;
            this.btnRegister2.Text = "Register";
            this.btnRegister2.UseVisualStyleBackColor = false;
            this.btnRegister2.Click += new System.EventHandler(this.btnRegister2_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::CargoTrackingSystem.Properties.Resources.cargobg2;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(611, 453);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // RegisterForUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkTurquoise;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.txtBoxPasswordRegister);
            this.Controls.Add(this.txtBoxUserNameRegister);
            this.Controls.Add(this.btnRegister2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "RegisterForUser";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RegisterForUser";
            this.Load += new System.EventHandler(this.RegisterForUser_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtBoxPasswordRegister;
        private System.Windows.Forms.TextBox txtBoxUserNameRegister;
        private System.Windows.Forms.Button btnRegister2;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}