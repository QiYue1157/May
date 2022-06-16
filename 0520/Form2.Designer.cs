
namespace _0520
{
    partial class Form2
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.fenSan5 = new _0520.FenSan();
            this.fenSan4 = new _0520.FenSan();
            this.fenSan2 = new _0520.FenSan();
            this.fenSan1 = new _0520.FenSan();
            this.fenSan3 = new _0520.FenSan();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(180, 29);
            this.panel1.TabIndex = 1;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Interval = 10;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Default;
            this.pictureBox1.Image = global::_0520.Properties.Resources.close4;
            this.pictureBox1.Location = new System.Drawing.Point(155, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(22, 22);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            this.pictureBox1.MouseEnter += new System.EventHandler(this.pictureBox1_MouseEnter);
            // 
            // fenSan5
            // 
            this.fenSan5.Color1 = System.Drawing.Color.Lime;
            this.fenSan5.Isrun = false;
            this.fenSan5.Location = new System.Drawing.Point(154, 35);
            this.fenSan5.Name = "fenSan5";
            this.fenSan5.Size = new System.Drawing.Size(23, 23);
            this.fenSan5.TabIndex = 8;
            this.fenSan5.Text = "fenSan5";
            // 
            // fenSan4
            // 
            this.fenSan4.Color1 = System.Drawing.Color.DarkTurquoise;
            this.fenSan4.Isrun = false;
            this.fenSan4.Location = new System.Drawing.Point(145, 145);
            this.fenSan4.Name = "fenSan4";
            this.fenSan4.Size = new System.Drawing.Size(23, 23);
            this.fenSan4.TabIndex = 7;
            this.fenSan4.Text = "fenSan4";
            // 
            // fenSan2
            // 
            this.fenSan2.Color1 = System.Drawing.Color.Aqua;
            this.fenSan2.Isrun = false;
            this.fenSan2.Location = new System.Drawing.Point(13, 145);
            this.fenSan2.Name = "fenSan2";
            this.fenSan2.Size = new System.Drawing.Size(23, 23);
            this.fenSan2.TabIndex = 6;
            this.fenSan2.Text = "fenSan2";
            // 
            // fenSan1
            // 
            this.fenSan1.Color1 = System.Drawing.Color.Fuchsia;
            this.fenSan1.Isrun = false;
            this.fenSan1.Location = new System.Drawing.Point(73, 91);
            this.fenSan1.Name = "fenSan1";
            this.fenSan1.Size = new System.Drawing.Size(23, 23);
            this.fenSan1.TabIndex = 5;
            this.fenSan1.Text = "fenSan1";
            // 
            // fenSan3
            // 
            this.fenSan3.Color1 = System.Drawing.Color.CornflowerBlue;
            this.fenSan3.Isrun = false;
            this.fenSan3.Location = new System.Drawing.Point(12, 35);
            this.fenSan3.Name = "fenSan3";
            this.fenSan3.Size = new System.Drawing.Size(23, 23);
            this.fenSan3.TabIndex = 4;
            this.fenSan3.Text = "fenSan3";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(180, 180);
            this.Controls.Add(this.fenSan5);
            this.Controls.Add(this.fenSan4);
            this.Controls.Add(this.fenSan2);
            this.Controls.Add(this.fenSan1);
            this.Controls.Add(this.fenSan3);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Timer timer1;
        private FenSan fenSan3;
        private FenSan fenSan1;
        private FenSan fenSan2;
        private FenSan fenSan4;
        private FenSan fenSan5;
        private System.Windows.Forms.Timer timer2;
    }
}