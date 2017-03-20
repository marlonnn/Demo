namespace CommandDemo
{
    partial class View
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
            this.textBoxDisplay = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button0 = new System.Windows.Forms.Button();
            this.buttonDivide = new System.Windows.Forms.Button();
            this.buttonMultiply = new System.Windows.Forms.Button();
            this.buttonSubtract = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonUndo = new System.Windows.Forms.Button();
            this.buttonRedo = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonCorrectError = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            this.textBoxLog = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxDisplay
            // 
            this.textBoxDisplay.BackColor = System.Drawing.Color.DarkGreen;
            this.textBoxDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.textBoxDisplay.ForeColor = System.Drawing.Color.White;
            this.textBoxDisplay.Location = new System.Drawing.Point(8, 8);
            this.textBoxDisplay.Name = "textBoxDisplay";
            this.textBoxDisplay.Size = new System.Drawing.Size(128, 20);
            this.textBoxDisplay.TabIndex = 0;
            this.textBoxDisplay.Text = "0";
            this.textBoxDisplay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(8, 104);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(24, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "1";
            this.button1.Click += new System.EventHandler(this.buttonNumeric_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(40, 104);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(24, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "2";
            this.button2.Click += new System.EventHandler(this.buttonNumeric_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(72, 104);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(24, 23);
            this.button3.TabIndex = 3;
            this.button3.Text = "3";
            this.button3.Click += new System.EventHandler(this.buttonNumeric_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(8, 72);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(24, 23);
            this.button4.TabIndex = 4;
            this.button4.Text = "4";
            this.button4.Click += new System.EventHandler(this.buttonNumeric_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(40, 72);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(24, 23);
            this.button5.TabIndex = 5;
            this.button5.Text = "5";
            this.button5.Click += new System.EventHandler(this.buttonNumeric_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(72, 72);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(24, 23);
            this.button6.TabIndex = 6;
            this.button6.Text = "6";
            this.button6.Click += new System.EventHandler(this.buttonNumeric_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(8, 40);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(24, 23);
            this.button7.TabIndex = 7;
            this.button7.Text = "7";
            this.button7.Click += new System.EventHandler(this.buttonNumeric_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(40, 40);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(24, 23);
            this.button8.TabIndex = 8;
            this.button8.Text = "8";
            this.button8.Click += new System.EventHandler(this.buttonNumeric_Click);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(72, 40);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(24, 23);
            this.button9.TabIndex = 9;
            this.button9.Text = "9";
            this.button9.Click += new System.EventHandler(this.buttonNumeric_Click);
            // 
            // button0
            // 
            this.button0.Location = new System.Drawing.Point(40, 136);
            this.button0.Name = "button0";
            this.button0.Size = new System.Drawing.Size(24, 23);
            this.button0.TabIndex = 10;
            this.button0.Text = "0";
            this.button0.Click += new System.EventHandler(this.buttonNumeric_Click);
            // 
            // buttonDivide
            // 
            this.buttonDivide.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.buttonDivide.Location = new System.Drawing.Point(112, 40);
            this.buttonDivide.Name = "buttonDivide";
            this.buttonDivide.Size = new System.Drawing.Size(24, 23);
            this.buttonDivide.TabIndex = 11;
            this.buttonDivide.Text = "/";
            this.buttonDivide.Click += new System.EventHandler(this.buttonDivide_Click);
            // 
            // buttonMultiply
            // 
            this.buttonMultiply.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.buttonMultiply.Location = new System.Drawing.Point(112, 72);
            this.buttonMultiply.Name = "buttonMultiply";
            this.buttonMultiply.Size = new System.Drawing.Size(24, 23);
            this.buttonMultiply.TabIndex = 12;
            this.buttonMultiply.Text = "*";
            this.buttonMultiply.Click += new System.EventHandler(this.buttonMultiply_Click);
            // 
            // buttonSubtract
            // 
            this.buttonSubtract.Location = new System.Drawing.Point(112, 104);
            this.buttonSubtract.Name = "buttonSubtract";
            this.buttonSubtract.Size = new System.Drawing.Size(24, 23);
            this.buttonSubtract.TabIndex = 13;
            this.buttonSubtract.Text = "-";
            this.buttonSubtract.Click += new System.EventHandler(this.buttonSubtract_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(112, 136);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(24, 23);
            this.buttonAdd.TabIndex = 14;
            this.buttonAdd.Text = "+";
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonUndo
            // 
            this.buttonUndo.Location = new System.Drawing.Point(8, 184);
            this.buttonUndo.Name = "buttonUndo";
            this.buttonUndo.Size = new System.Drawing.Size(64, 23);
            this.buttonUndo.TabIndex = 0;
            this.buttonUndo.Text = "Undo";
            this.buttonUndo.Click += new System.EventHandler(this.buttonUndo_Click);
            // 
            // buttonRedo
            // 
            this.buttonRedo.Location = new System.Drawing.Point(88, 184);
            this.buttonRedo.Name = "buttonRedo";
            this.buttonRedo.Size = new System.Drawing.Size(64, 23);
            this.buttonRedo.TabIndex = 1;
            this.buttonRedo.Text = "Redo";
            this.buttonRedo.Click += new System.EventHandler(this.buttonRedo_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel1.Controls.Add(this.buttonCorrectError);
            this.panel1.Controls.Add(this.buttonClear);
            this.panel1.Controls.Add(this.textBoxDisplay);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.button8);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button9);
            this.panel1.Controls.Add(this.buttonAdd);
            this.panel1.Controls.Add(this.button7);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button0);
            this.panel1.Controls.Add(this.buttonSubtract);
            this.panel1.Controls.Add(this.button6);
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.buttonDivide);
            this.panel1.Controls.Add(this.buttonMultiply);
            this.panel1.Controls.Add(this.button5);
            this.panel1.Location = new System.Drawing.Point(8, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(144, 168);
            this.panel1.TabIndex = 16;
            // 
            // buttonCorrectError
            // 
            this.buttonCorrectError.ForeColor = System.Drawing.Color.Red;
            this.buttonCorrectError.Location = new System.Drawing.Point(72, 136);
            this.buttonCorrectError.Name = "buttonCorrectError";
            this.buttonCorrectError.Size = new System.Drawing.Size(24, 23);
            this.buttonCorrectError.TabIndex = 16;
            this.buttonCorrectError.Text = "CE";
            this.buttonCorrectError.Click += new System.EventHandler(this.buttonCorrectError_Click);
            // 
            // buttonClear
            // 
            this.buttonClear.ForeColor = System.Drawing.Color.Red;
            this.buttonClear.Location = new System.Drawing.Point(8, 136);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(24, 23);
            this.buttonClear.TabIndex = 15;
            this.buttonClear.Text = "C";
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // textBoxLog
            // 
            this.textBoxLog.Location = new System.Drawing.Point(160, 8);
            this.textBoxLog.Multiline = true;
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxLog.Size = new System.Drawing.Size(176, 200);
            this.textBoxLog.TabIndex = 17;
            // 
            // View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(343, 214);
            this.Controls.Add(this.textBoxLog);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.buttonUndo);
            this.Controls.Add(this.buttonRedo);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "View";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Calculator";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxDisplay;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button0;
        private System.Windows.Forms.Button buttonDivide;
        private System.Windows.Forms.Button buttonMultiply;
        private System.Windows.Forms.Button buttonSubtract;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonUndo;
        private System.Windows.Forms.Button buttonRedo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Button buttonCorrectError;
        private System.Windows.Forms.TextBox textBoxLog;


    }
}

