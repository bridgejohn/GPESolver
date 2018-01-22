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
            this.LaufzeitTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.ParameterButton = new System.Windows.Forms.Button();
            this.FFTCheckBox = new System.Windows.Forms.CheckBox();
            this.DFTCheckBox = new System.Windows.Forms.CheckBox();
            this.getgroundstate = new System.Windows.Forms.CheckBox();
            this.bitReverse = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.TimeStepsTextBox = new System.Windows.Forms.TextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.LZlabel = new System.Windows.Forms.Label();
            this.timeEvolutionButton = new System.Windows.Forms.Button();
            this.psiPlotButton = new System.Windows.Forms.Button();
            this.potentialButton = new System.Windows.Forms.Button();
            this.shiftPotButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TimeStepIt
            // 
            this.TimeStepIt.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.TimeStepIt.Location = new System.Drawing.Point(701, 29);
            this.TimeStepIt.Name = "TimeStepIt";
            this.TimeStepIt.Size = new System.Drawing.Size(181, 120);
            this.TimeStepIt.TabIndex = 1;
            this.TimeStepIt.Text = "Los!";
            this.TimeStepIt.UseVisualStyleBackColor = true;
            this.TimeStepIt.Click += new System.EventHandler(this.button1_Click);
            // 
            // massTextBox
            // 
            this.massTextBox.Location = new System.Drawing.Point(782, 223);
            this.massTextBox.Name = "massTextBox";
            this.massTextBox.Size = new System.Drawing.Size(100, 31);
            this.massTextBox.TabIndex = 2;
            this.massTextBox.Text = "87";
            this.massTextBox.TextChanged += new System.EventHandler(this.massTextBox_TextChanged);
            // 
            // RadFrequenzTextBox
            // 
            this.RadFrequenzTextBox.Location = new System.Drawing.Point(783, 387);
            this.RadFrequenzTextBox.Name = "RadFrequenzTextBox";
            this.RadFrequenzTextBox.Size = new System.Drawing.Size(100, 31);
            this.RadFrequenzTextBox.TabIndex = 4;
            this.RadFrequenzTextBox.Text = "100";
            // 
            // FrequenzTextBox
            // 
            this.FrequenzTextBox.Location = new System.Drawing.Point(783, 344);
            this.FrequenzTextBox.Name = "FrequenzTextBox";
            this.FrequenzTextBox.Size = new System.Drawing.Size(100, 31);
            this.FrequenzTextBox.TabIndex = 5;
            this.FrequenzTextBox.Text = "40";
            // 
            // AnzahlTextBox
            // 
            this.AnzahlTextBox.Location = new System.Drawing.Point(783, 305);
            this.AnzahlTextBox.Name = "AnzahlTextBox";
            this.AnzahlTextBox.Size = new System.Drawing.Size(100, 31);
            this.AnzahlTextBox.TabIndex = 6;
            this.AnzahlTextBox.Text = "1000";
            this.AnzahlTextBox.TextChanged += new System.EventHandler(this.AnzahlTextBox_TextChanged);
            // 
            // StreuTextBox
            // 
            this.StreuTextBox.Location = new System.Drawing.Point(782, 264);
            this.StreuTextBox.Name = "StreuTextBox";
            this.StreuTextBox.Size = new System.Drawing.Size(100, 31);
            this.StreuTextBox.TabIndex = 7;
            this.StreuTextBox.Text = "5,8";
            this.StreuTextBox.TextChanged += new System.EventHandler(this.StreuTextBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(459, 226);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 25);
            this.label1.TabIndex = 8;
            this.label1.Text = "Masse [u]";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(459, 264);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(163, 25);
            this.label2.TabIndex = 9;
            this.label2.Text = "Streulänge [nm]";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(459, 308);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(145, 25);
            this.label3.TabIndex = 10;
            this.label3.Text = "Anzahl Atome";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(459, 347);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(197, 25);
            this.label4.TabIndex = 11;
            this.label4.Text = "Fallenfrequenz [2π]";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(459, 390);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(276, 25);
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
            // LaufzeitTextBox
            // 
            this.LaufzeitTextBox.Location = new System.Drawing.Point(783, 501);
            this.LaufzeitTextBox.Name = "LaufzeitTextBox";
            this.LaufzeitTextBox.Size = new System.Drawing.Size(100, 31);
            this.LaufzeitTextBox.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(467, 501);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(217, 25);
            this.label6.TabIndex = 14;
            this.label6.Text = "Aktuelle Laufzeit [ms]";
            // 
            // ParameterButton
            // 
            this.ParameterButton.Location = new System.Drawing.Point(701, 545);
            this.ParameterButton.Name = "ParameterButton";
            this.ParameterButton.Size = new System.Drawing.Size(182, 32);
            this.ParameterButton.TabIndex = 15;
            this.ParameterButton.Text = "Liste löschen";
            this.ParameterButton.UseVisualStyleBackColor = true;
            this.ParameterButton.Click += new System.EventHandler(this.ParameterButton_Click);
            // 
            // FFTCheckBox
            // 
            this.FFTCheckBox.AutoSize = true;
            this.FFTCheckBox.Location = new System.Drawing.Point(408, 30);
            this.FFTCheckBox.Name = "FFTCheckBox";
            this.FFTCheckBox.Size = new System.Drawing.Size(83, 29);
            this.FFTCheckBox.TabIndex = 16;
            this.FFTCheckBox.Text = "FFT";
            this.FFTCheckBox.UseVisualStyleBackColor = true;
            // 
            // DFTCheckBox
            // 
            this.DFTCheckBox.AutoSize = true;
            this.DFTCheckBox.Location = new System.Drawing.Point(408, 61);
            this.DFTCheckBox.Name = "DFTCheckBox";
            this.DFTCheckBox.Size = new System.Drawing.Size(85, 29);
            this.DFTCheckBox.TabIndex = 17;
            this.DFTCheckBox.Text = "DFT";
            this.DFTCheckBox.UseVisualStyleBackColor = true;
            // 
            // getgroundstate
            // 
            this.getgroundstate.AutoSize = true;
            this.getgroundstate.Location = new System.Drawing.Point(408, 121);
            this.getgroundstate.Name = "getgroundstate";
            this.getgroundstate.Size = new System.Drawing.Size(287, 29);
            this.getgroundstate.TabIndex = 18;
            this.getgroundstate.Text = "Grundzustand berechnen";
            this.getgroundstate.UseVisualStyleBackColor = true;
            // 
            // bitReverse
            // 
            this.bitReverse.AutoSize = true;
            this.bitReverse.Location = new System.Drawing.Point(408, 91);
            this.bitReverse.Name = "bitReverse";
            this.bitReverse.Size = new System.Drawing.Size(149, 29);
            this.bitReverse.TabIndex = 19;
            this.bitReverse.Text = "BitReverse";
            this.bitReverse.UseVisualStyleBackColor = true;
            this.bitReverse.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(459, 436);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(227, 25);
            this.label7.TabIndex = 21;
            this.label7.Text = "Anzahl der Zeitschritte";
            // 
            // TimeStepsTextBox
            // 
            this.TimeStepsTextBox.Location = new System.Drawing.Point(783, 433);
            this.TimeStepsTextBox.Name = "TimeStepsTextBox";
            this.TimeStepsTextBox.Size = new System.Drawing.Size(100, 31);
            this.TimeStepsTextBox.TabIndex = 22;
            this.TimeStepsTextBox.Text = "10000";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 25;
            this.listBox1.Location = new System.Drawing.Point(433, 583);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(449, 79);
            this.listBox1.TabIndex = 23;
            // 
            // LZlabel
            // 
            this.LZlabel.AutoSize = true;
            this.LZlabel.Location = new System.Drawing.Point(421, 557);
            this.LZlabel.Name = "LZlabel";
            this.LZlabel.Size = new System.Drawing.Size(118, 25);
            this.LZlabel.TabIndex = 25;
            this.LZlabel.Text = "Laufzeiten:";
            // 
            // timeEvolutionButton
            // 
            this.timeEvolutionButton.Location = new System.Drawing.Point(423, 156);
            this.timeEvolutionButton.Name = "timeEvolutionButton";
            this.timeEvolutionButton.Size = new System.Drawing.Size(149, 62);
            this.timeEvolutionButton.TabIndex = 26;
            this.timeEvolutionButton.Text = "Zeitverlauf";
            this.timeEvolutionButton.UseVisualStyleBackColor = true;
            this.timeEvolutionButton.Click += new System.EventHandler(this.timeEvolutionButton_Click);
            // 
            // psiPlotButton
            // 
            this.psiPlotButton.Location = new System.Drawing.Point(578, 155);
            this.psiPlotButton.Name = "psiPlotButton";
            this.psiPlotButton.Size = new System.Drawing.Size(149, 62);
            this.psiPlotButton.TabIndex = 27;
            this.psiPlotButton.Text = "Start/End Psi";
            this.psiPlotButton.UseVisualStyleBackColor = true;
            this.psiPlotButton.Click += new System.EventHandler(this.psiPlotButton_Click);
            // 
            // potentialButton
            // 
            this.potentialButton.Location = new System.Drawing.Point(733, 156);
            this.potentialButton.Name = "potentialButton";
            this.potentialButton.Size = new System.Drawing.Size(149, 62);
            this.potentialButton.TabIndex = 28;
            this.potentialButton.Text = "Potential";
            this.potentialButton.UseVisualStyleBackColor = true;
            this.potentialButton.Click += new System.EventHandler(this.potentialButton_Click);
            // 
            // shiftPotButton
            // 
            this.shiftPotButton.Location = new System.Drawing.Point(888, 29);
            this.shiftPotButton.Name = "shiftPotButton";
            this.shiftPotButton.Size = new System.Drawing.Size(189, 55);
            this.shiftPotButton.TabIndex = 29;
            this.shiftPotButton.Text = "Shift Potential";
            this.shiftPotButton.UseVisualStyleBackColor = true;
            this.shiftPotButton.Click += new System.EventHandler(this.shiftPotButton_Click);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(1090, 668);
            this.Controls.Add(this.shiftPotButton);
            this.Controls.Add(this.potentialButton);
            this.Controls.Add(this.psiPlotButton);
            this.Controls.Add(this.timeEvolutionButton);
            this.Controls.Add(this.LZlabel);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.TimeStepsTextBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.bitReverse);
            this.Controls.Add(this.getgroundstate);
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
            this.Controls.Add(this.TimeStepIt);
            this.Name = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion
        private System.Windows.Forms.Button TimeStepIt;
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
        //private OxyPlot.WindowsForms.PlotView plot2;
        private System.Windows.Forms.TextBox LaufzeitTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button ParameterButton;
        private System.Windows.Forms.CheckBox FFTCheckBox;
        private System.Windows.Forms.CheckBox DFTCheckBox;
        private System.Windows.Forms.CheckBox getgroundstate;
        private System.Windows.Forms.CheckBox bitReverse;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox TimeStepsTextBox;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label LZlabel;
        private System.Windows.Forms.Button timeEvolutionButton;
        private System.Windows.Forms.Button psiPlotButton;
        private System.Windows.Forms.Button potentialButton;
        private System.Windows.Forms.Button shiftPotButton;
    }
}

