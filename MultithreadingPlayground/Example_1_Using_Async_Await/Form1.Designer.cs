namespace Example_1_Using_Async_Await
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
            this.lblResult = new System.Windows.Forms.Label();
            this.btnCalculate = new System.Windows.Forms.Button();
            this.btnCalculateAsync = new System.Windows.Forms.Button();
            this.btnCalculateUiThread = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Location = new System.Drawing.Point(36, 9);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(37, 13);
            this.lblResult.TabIndex = 0;
            this.lblResult.Text = "Result";
            // 
            // btnCalculate
            // 
            this.btnCalculate.Location = new System.Drawing.Point(12, 70);
            this.btnCalculate.Name = "btnCalculate";
            this.btnCalculate.Size = new System.Drawing.Size(151, 23);
            this.btnCalculate.TabIndex = 1;
            this.btnCalculate.Text = "Calculate with TPL";
            this.btnCalculate.UseVisualStyleBackColor = true;
            this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);
            // 
            // btnCalculateAsync
            // 
            this.btnCalculateAsync.Location = new System.Drawing.Point(12, 99);
            this.btnCalculateAsync.Name = "btnCalculateAsync";
            this.btnCalculateAsync.Size = new System.Drawing.Size(151, 23);
            this.btnCalculateAsync.TabIndex = 2;
            this.btnCalculateAsync.Text = "Calculate with async/await";
            this.btnCalculateAsync.UseVisualStyleBackColor = true;
            this.btnCalculateAsync.Click += new System.EventHandler(this.btnCalculateAsync_Click);
            // 
            // btnCalculateUiThread
            // 
            this.btnCalculateUiThread.Location = new System.Drawing.Point(12, 41);
            this.btnCalculateUiThread.Name = "btnCalculateUiThread";
            this.btnCalculateUiThread.Size = new System.Drawing.Size(151, 23);
            this.btnCalculateUiThread.TabIndex = 3;
            this.btnCalculateUiThread.Text = "Calculate on UI Thread";
            this.btnCalculateUiThread.UseVisualStyleBackColor = true;
            this.btnCalculateUiThread.Click += new System.EventHandler(this.btnCalculateUiThread_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(119, 4);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(44, 23);
            this.btnReset.TabIndex = 4;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(175, 134);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnCalculateUiThread);
            this.Controls.Add(this.btnCalculateAsync);
            this.Controls.Add(this.btnCalculate);
            this.Controls.Add(this.lblResult);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Button btnCalculate;
        private System.Windows.Forms.Button btnCalculateAsync;
        private System.Windows.Forms.Button btnCalculateUiThread;
        private System.Windows.Forms.Button btnReset;
    }
}

