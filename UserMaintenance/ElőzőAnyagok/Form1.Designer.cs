namespace ElőzőAnyagok
{
    partial class Form1
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
            this.tb_vName = new System.Windows.Forms.TextBox();
            this.tb_kName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tb_szülIdő = new System.Windows.Forms.TextBox();
            this.tb_nem = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tb_vName
            // 
            this.tb_vName.Location = new System.Drawing.Point(95, 32);
            this.tb_vName.Name = "tb_vName";
            this.tb_vName.Size = new System.Drawing.Size(161, 20);
            this.tb_vName.TabIndex = 0;
            // 
            // tb_kName
            // 
            this.tb_kName.Location = new System.Drawing.Point(95, 58);
            this.tb_kName.Name = "tb_kName";
            this.tb_kName.Size = new System.Drawing.Size(161, 20);
            this.tb_kName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Vezetéknév";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Keresztnév";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Születési idő";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 113);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Nem (F/N)";
            // 
            // tb_szülIdő
            // 
            this.tb_szülIdő.Location = new System.Drawing.Point(95, 84);
            this.tb_szülIdő.Name = "tb_szülIdő";
            this.tb_szülIdő.Size = new System.Drawing.Size(161, 20);
            this.tb_szülIdő.TabIndex = 8;
            this.tb_szülIdő.Leave += new System.EventHandler(this.tb_szülLeave);
            // 
            // tb_nem
            // 
            this.tb_nem.Location = new System.Drawing.Point(95, 113);
            this.tb_nem.Name = "tb_nem";
            this.tb_nem.Size = new System.Drawing.Size(161, 20);
            this.tb_nem.TabIndex = 9;
            this.tb_nem.Leave += new System.EventHandler(this.tb_nemLeave);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(181, 152);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "Felvétel";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tb_nem);
            this.Controls.Add(this.tb_szülIdő);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_kName);
            this.Controls.Add(this.tb_vName);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tb_vName;
        private System.Windows.Forms.TextBox tb_kName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tb_szülIdő;
        private System.Windows.Forms.TextBox tb_nem;
        private System.Windows.Forms.Button button1;
    }
}

