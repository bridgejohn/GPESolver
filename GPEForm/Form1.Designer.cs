namespace GPEForm
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.TimeStepIt = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.massTextBox = new System.Windows.Forms.TextBox();
            this.RadFrequenzTextBox = new System.Windows.Forms.TextBox();
            this.FrequenzTextBox = new System.Windows.Forms.TextBox();
            this.AnzahlTextBox = new System.Windows.Forms.TextBox();
            this.StreuTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.plot1 = new OxyPlot.WindowsForms.PlotView();
            this.plot2 = new OxyPlot.WindowsForms.PlotView();
            this.LaufzeitTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.ParameterButton = new System.Windows.Forms.Button();
            this.FFTCheckBox = new System.Windows.Forms.CheckBox();
            this.DFTCheckBox = new System.Windows.Forms.CheckBox();
            this.RKCheckBox = new System.Windows.Forms.CheckBox();
            this.bitReverse = new System.Windows.Forms.CheckBox();
            this.CalcingType = new System.Windows.Forms.CheckedListBox();
            this.label7 = new System.Windows.Forms.Label();
            this.TimeStepsTextBox = new System.Windows.Forms.TextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // TimeStepIt
            // 
            this.TimeStepIt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TimeStepIt.Location = new System.Drawing.Point(863, 27);
            this.TimeStepIt.Name = "TimeStepIt";
            this.TimeStepIt.Size = new System.Drawing.Size(92, 74);
            this.TimeStepIt.TabIndex = 1;
            this.TimeStepIt.Text = "Los!";
            this.TimeStepIt.UseVisualStyleBackColor = true;
            this.TimeStepIt.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(26, 27);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(587, 609);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // massTextBox
            // 
            this.massTextBox.Location = new System.Drawing.Point(868, 220);
            this.massTextBox.Name = "massTextBox";
            this.massTextBox.Size = new System.Drawing.Size(100, 26);
            this.massTextBox.TabIndex = 2;
            this.massTextBox.Text = "87";
            // 
            // RadFrequenzTextBox
            // 
            this.RadFrequenzTextBox.Location = new System.Drawing.Point(869, 384);
            this.RadFrequenzTextBox.Name = "RadFrequenzTextBox";
            this.RadFrequenzTextBox.Size = new System.Drawing.Size(100, 26);
            this.RadFrequenzTextBox.TabIndex = 4;
            this.RadFrequenzTextBox.Text = "100";
            // 
            // FrequenzTextBox
            // 
            this.FrequenzTextBox.Location = new System.Drawing.Point(869, 341);
            this.FrequenzTextBox.Name = "FrequenzTextBox";
            this.FrequenzTextBox.Size = new System.Drawing.Size(100, 26);
            this.FrequenzTextBox.TabIndex = 5;
            this.FrequenzTextBox.Text = "40";
            // 
            // AnzahlTextBox
            // 
            this.AnzahlTextBox.Location = new System.Drawing.Point(869, 302);
            this.AnzahlTextBox.Name = "AnzahlTextBox";
            this.AnzahlTextBox.Size = new System.Drawing.Size(100, 26);
            this.AnzahlTextBox.TabIndex = 6;
            this.AnzahlTextBox.Text = "1000";
            // 
            // StreuTextBox
            // 
            this.StreuTextBox.Location = new System.Drawing.Point(868, 261);
            this.StreuTextBox.Name = "StreuTextBox";
            this.StreuTextBox.Size = new System.Drawing.Size(100, 26);
            this.StreuTextBox.TabIndex = 7;
            this.StreuTextBox.Text = "5,8";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(764, 223);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 20);
            this.label1.TabIndex = 8;
            this.label1.Text = "Masse [u]";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(720, 264);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 20);
            this.label2.TabIndex = 9;
            this.label2.Text = "Streulänge [nm]";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(732, 305);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 20);
            this.label3.TabIndex = 10;
            this.label3.Text = "Anzahl Atome";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(696, 341);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(145, 20);
            this.label4.TabIndex = 11;
            this.label4.Text = "Fallenfrequenz [2π]";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(638, 387);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(203, 20);
            this.label5.TabIndex = 12;
            this.label5.Text = "Radiale Fallenfrequenz [2π]";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // plot1
            // 
            this.plot1.Location = new System.Drawing.Point(0, 0);
            this.plot1.Name = "plot1";
            this.plot1.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plot1.Size = new System.Drawing.Size(300, 600);
            this.plot1.TabIndex = 0;
            this.plot1.Text = "plot1";
            this.plot1.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plot1.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plot1.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // plot2
            // 
            this.plot2.Location = new System.Drawing.Point(300, 0);
            this.plot2.Name = "plot2";
            this.plot2.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plot2.Size = new System.Drawing.Size(300, 600);
            this.plot2.TabIndex = 0;
            this.plot2.Text = "plot2";
            this.plot2.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plot2.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plot2.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // LaufzeitTextBox
            // 
            this.LaufzeitTextBox.Location = new System.Drawing.Point(871, 533);
            this.LaufzeitTextBox.Name = "LaufzeitTextBox";
            this.LaufzeitTextBox.Size = new System.Drawing.Size(100, 26);
            this.LaufzeitTextBox.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(753, 536);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(99, 20);
            this.label6.TabIndex = 14;
            this.label6.Text = "Laufzeit [ms]";
            // 
            // ParameterButton
            // 
            this.ParameterButton.Location = new System.Drawing.Point(863, 474);
            this.ParameterButton.Name = "ParameterButton";
            this.ParameterButton.Size = new System.Drawing.Size(110, 32);
            this.ParameterButton.TabIndex = 15;
            this.ParameterButton.Text = "Aktualisieren";
            this.ParameterButton.UseVisualStyleBackColor = true;
            this.ParameterButton.Click += new System.EventHandler(this.ParameterButton_Click);
            // 
            // FFTCheckBox
            // 
            this.FFTCheckBox.AutoSize = true;
            this.FFTCheckBox.Location = new System.Drawing.Point(713, 27);
            this.FFTCheckBox.Name = "FFTCheckBox";
            this.FFTCheckBox.Size = new System.Drawing.Size(64, 24);
            this.FFTCheckBox.TabIndex = 16;
            this.FFTCheckBox.Text = "FFT";
            this.FFTCheckBox.UseVisualStyleBackColor = true;
            // 
            // DFTCheckBox
            // 
            this.DFTCheckBox.AutoSize = true;
            this.DFTCheckBox.Location = new System.Drawing.Point(713, 58);
            this.DFTCheckBox.Name = "DFTCheckBox";
            this.DFTCheckBox.Size = new System.Drawing.Size(66, 24);
            this.DFTCheckBox.TabIndex = 17;
            this.DFTCheckBox.Text = "DFT";
            this.DFTCheckBox.UseVisualStyleBackColor = true;
            // 
            // RKCheckBox
            // 
            this.RKCheckBox.AutoSize = true;
            this.RKCheckBox.Location = new System.Drawing.Point(713, 118);
            this.RKCheckBox.Name = "RKCheckBox";
            this.RKCheckBox.Size = new System.Drawing.Size(126, 24);
            this.RKCheckBox.TabIndex = 18;
            this.RKCheckBox.Text = "Runge-Kutta";
            this.RKCheckBox.UseVisualStyleBackColor = true;
            // 
            // bitReverse
            // 
            this.bitReverse.AutoSize = true;
            this.bitReverse.Location = new System.Drawing.Point(713, 88);
            this.bitReverse.Name = "bitReverse";
            this.bitReverse.Size = new System.Drawing.Size(113, 24);
            this.bitReverse.TabIndex = 19;
            this.bitReverse.Text = "BitReverse";
            this.bitReverse.UseVisualStyleBackColor = true;
            this.bitReverse.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // CalcingType
            // 
            this.CalcingType.FormattingEnabled = true;
            this.CalcingType.Items.AddRange(new object[] {
            "FFT",
            "DFT",
            "BR",
            "RungeKutta"});
            this.CalcingType.Location = new System.Drawing.Point(988, 27);
            this.CalcingType.Name = "CalcingType";
            this.CalcingType.Size = new System.Drawing.Size(323, 172);
            this.CalcingType.TabIndex = 20;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(673, 433);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(168, 20);
            this.label7.TabIndex = 21;
            this.label7.Text = "Anzahl der Zeitschritte";
            // 
            // TimeStepsTextBox
            // 
            this.TimeStepsTextBox.Location = new System.Drawing.Point(869, 430);
            this.TimeStepsTextBox.Name = "TimeStepsTextBox";
            this.TimeStepsTextBox.Size = new System.Drawing.Size(100, 26);
            this.TimeStepsTextBox.TabIndex = 22;
            this.TimeStepsTextBox.Text = "10000";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 20;
            this.listBox1.Location = new System.Drawing.Point(1049, 357);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(139, 204);
            this.listBox1.TabIndex = 23;
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(1343, 828);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.TimeStepsTextBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.CalcingType);
            this.Controls.Add(this.bitReverse);
            this.Controls.Add(this.RKCheckBox);
            this.Controls.Add(this.DFTCheckBox);
            this.Controls.Add(this.FFTCheckBox);
            this.Controls.Add(this.ParameterButton);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.LaufzeitTextBox);
            this.Controls.Add(this.plot1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.StreuTextBox);
            this.Controls.Add(this.AnzahlTextBox);
            this.Controls.Add(this.FrequenzTextBox);
            this.Controls.Add(this.RadFrequenzTextBox);
            this.Controls.Add(this.massTextBox);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.TimeStepIt);
            this.Name = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion
        private System.Windows.Forms.Button TimeStepIt;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox massTextBox;
        private System.Windows.Forms.TextBox RadFrequenzTextBox;
        private System.Windows.Forms.TextBox FrequenzTextBox;
        private System.Windows.Forms.TextBox AnzahlTextBox;
        private System.Windows.Forms.TextBox StreuTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;

        private OxyPlot.WindowsForms.PlotView plot1;
        private OxyPlot.WindowsForms.PlotView plot2;
        private System.Windows.Forms.TextBox LaufzeitTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button ParameterButton;
        private System.Windows.Forms.CheckBox FFTCheckBox;
        private System.Windows.Forms.CheckBox DFTCheckBox;
        private System.Windows.Forms.CheckBox RKCheckBox;
        private System.Windows.Forms.CheckBox bitReverse;
        private System.Windows.Forms.CheckedListBox CalcingType;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox TimeStepsTextBox;
        private System.Windows.Forms.ListBox listBox1;
    }
}

