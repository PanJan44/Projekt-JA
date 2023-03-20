namespace UI
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.cSharpRadioButton = new System.Windows.Forms.RadioButton();
            this.asmRadioButton = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.filterButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.threadCount = new System.Windows.Forms.Label();
            this.listOfTimes = new System.Windows.Forms.ListBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.radiusVal = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.histogramIn = new System.Windows.Forms.PictureBox();
            this.histogramOut = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.histogramIn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.histogramOut)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(107, 64);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(133, 45);
            this.button1.TabIndex = 0;
            this.button1.Text = "Przeglądaj";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // cSharpRadioButton
            // 
            this.cSharpRadioButton.AutoSize = true;
            this.cSharpRadioButton.Location = new System.Drawing.Point(338, 131);
            this.cSharpRadioButton.Name = "cSharpRadioButton";
            this.cSharpRadioButton.Size = new System.Drawing.Size(48, 24);
            this.cSharpRadioButton.TabIndex = 1;
            this.cSharpRadioButton.TabStop = true;
            this.cSharpRadioButton.Tag = "1";
            this.cSharpRadioButton.Text = "C#";
            this.cSharpRadioButton.UseVisualStyleBackColor = true;
            // 
            // asmRadioButton
            // 
            this.asmRadioButton.AutoSize = true;
            this.asmRadioButton.Location = new System.Drawing.Point(338, 161);
            this.asmRadioButton.Name = "asmRadioButton";
            this.asmRadioButton.Size = new System.Drawing.Size(61, 24);
            this.asmRadioButton.TabIndex = 2;
            this.asmRadioButton.TabStop = true;
            this.asmRadioButton.Tag = "2";
            this.asmRadioButton.Text = "ASM";
            this.asmRadioButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(181, 321);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Obraz wejściowy";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(705, 321);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Obraz wyjściowy";
            // 
            // filterButton
            // 
            this.filterButton.Enabled = false;
            this.filterButton.Location = new System.Drawing.Point(456, 131);
            this.filterButton.Name = "filterButton";
            this.filterButton.Size = new System.Drawing.Size(81, 53);
            this.filterButton.TabIndex = 5;
            this.filterButton.Text = "Filtruj";
            this.filterButton.UseVisualStyleBackColor = true;
            this.filterButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // saveButton
            // 
            this.saveButton.Enabled = false;
            this.saveButton.Location = new System.Drawing.Point(554, 131);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(81, 53);
            this.saveButton.TabIndex = 6;
            this.saveButton.Text = "Zapisz";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.button3_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(338, 200);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 20);
            this.label3.TabIndex = 8;
            this.label3.Text = "Liczba wątków ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(830, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 20);
            this.label4.TabIndex = 10;
            this.label4.Text = "Czasy";
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(456, 200);
            this.trackBar1.Maximum = 64;
            this.trackBar1.Minimum = 1;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(158, 56);
            this.trackBar1.TabIndex = 7;
            this.trackBar1.Value = 1;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // threadCount
            // 
            this.threadCount.AutoSize = true;
            this.threadCount.Location = new System.Drawing.Point(621, 200);
            this.threadCount.Name = "threadCount";
            this.threadCount.Size = new System.Drawing.Size(17, 20);
            this.threadCount.TabIndex = 11;
            this.threadCount.Text = "1";
            // 
            // listOfTimes
            // 
            this.listOfTimes.FormattingEnabled = true;
            this.listOfTimes.ItemHeight = 20;
            this.listOfTimes.Location = new System.Drawing.Point(830, 81);
            this.listOfTimes.Name = "listOfTimes";
            this.listOfTimes.Size = new System.Drawing.Size(155, 184);
            this.listOfTimes.TabIndex = 12;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Enabled = false;
            this.pictureBox1.Location = new System.Drawing.Point(16, 385);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(457, 320);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(528, 385);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(457, 320);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 14;
            this.pictureBox2.TabStop = false;
            // 
            // radiusVal
            // 
            this.radiusVal.AutoSize = true;
            this.radiusVal.Location = new System.Drawing.Point(621, 240);
            this.radiusVal.Name = "radiusVal";
            this.radiusVal.Size = new System.Drawing.Size(25, 20);
            this.radiusVal.TabIndex = 17;
            this.radiusVal.Text = "10";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(339, 236);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 20);
            this.label7.TabIndex = 16;
            this.label7.Text = "Promien";
            // 
            // trackBar2
            // 
            this.trackBar2.Location = new System.Drawing.Point(456, 236);
            this.trackBar2.Maximum = 30;
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Size = new System.Drawing.Size(158, 56);
            this.trackBar2.TabIndex = 15;
            this.trackBar2.Value = 10;
            this.trackBar2.Scroll += new System.EventHandler(this.trackBar2_Scroll);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1181, 59);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(219, 20);
            this.label5.TabIndex = 18;
            this.label5.Text = "Histogram obrazu wejściowego";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(1181, 411);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(218, 20);
            this.label6.TabIndex = 19;
            this.label6.Text = "Histogram obrazu wyjściowego";
            // 
            // histogramIn
            // 
            this.histogramIn.Enabled = false;
            this.histogramIn.Location = new System.Drawing.Point(1021, 81);
            this.histogramIn.Name = "histogramIn";
            this.histogramIn.Size = new System.Drawing.Size(502, 272);
            this.histogramIn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.histogramIn.TabIndex = 20;
            this.histogramIn.TabStop = false;
            this.histogramIn.Paint += new System.Windows.Forms.PaintEventHandler(this.histogramIn_Paint);
            // 
            // histogramOut
            // 
            this.histogramOut.Enabled = false;
            this.histogramOut.Location = new System.Drawing.Point(1021, 433);
            this.histogramOut.Name = "histogramOut";
            this.histogramOut.Size = new System.Drawing.Size(502, 272);
            this.histogramOut.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.histogramOut.TabIndex = 21;
            this.histogramOut.TabStop = false;
            this.histogramOut.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox3_Paint);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1541, 731);
            this.Controls.Add(this.histogramOut);
            this.Controls.Add(this.histogramIn);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.radiusVal);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.trackBar2);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.listOfTimes);
            this.Controls.Add(this.threadCount);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.filterButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.asmRadioButton);
            this.Controls.Add(this.cSharpRadioButton);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Box blur - JA 2022";
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.histogramIn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.histogramOut)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RadioButton cSharpRadioButton;
        private System.Windows.Forms.RadioButton asmRadioButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button filterButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label threadCount;
        private System.Windows.Forms.ListBox listOfTimes;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label radiusVal;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TrackBar trackBar2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox histogramIn;
        private System.Windows.Forms.PictureBox histogramOut;
    }
}
