namespace Demo.MiniDump
{
    partial class MiniDumpForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonException = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonException
            // 
            this.buttonException.Location = new System.Drawing.Point(180, 219);
            this.buttonException.Name = "buttonException";
            this.buttonException.Size = new System.Drawing.Size(171, 37);
            this.buttonException.TabIndex = 3;
            this.buttonException.Text = "Create Exception";
            this.buttonException.UseVisualStyleBackColor = true;
            this.buttonException.Click += new System.EventHandler(this.buttonException_Click);
            // 
            // MiniDumpForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 573);
            this.Controls.Add(this.buttonException);
            this.Name = "MiniDumpForm";
            this.Text = "MiniDumpForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonException;
    }
}

