namespace GraphicPassword
{
    partial class MainForm
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
            btnSet = new Button();
            btnCheck = new Button();
            btnReset = new Button();
            lblStatus = new Button();
            lblTitle = new Button();
            SuspendLayout();
            // 
            // btnSet
            // 
            btnSet.Location = new Point(12, 12);
            btnSet.Name = "btnSet";
            btnSet.Size = new Size(150, 40);
            btnSet.TabIndex = 0;
            btnSet.Text = "Задать пароль";
            btnSet.UseVisualStyleBackColor = true;
            
            // 
            // btnCheck
            // 
            btnCheck.Location = new Point(170, 12);
            btnCheck.Name = "btnCheck";
            btnCheck.Size = new Size(150, 40);
            btnCheck.TabIndex = 1;
            btnCheck.Text = "Проверить пароль";
            btnCheck.UseVisualStyleBackColor = true;
            
            // 
            // btnReset
            // 
            btnReset.Location = new Point(328, 12);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(100, 40);
            btnReset.TabIndex = 2;
            btnReset.Text = "Сброс";
            btnReset.UseVisualStyleBackColor = true;
            
            // 
            // lblStatus
            // 
            lblStatus.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lblStatus.Location = new Point(12, 60);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(450, 25);
            lblStatus.TabIndex = 3;
            lblStatus.Text = "Задайте пароль";
            lblStatus.UseVisualStyleBackColor = true;
            
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 204);
            lblTitle.Location = new Point(12, 90);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(450, 25);
            lblTitle.TabIndex = 4;
            lblTitle.Text = "Нарисуйте узор";
            lblTitle.UseVisualStyleBackColor = true;
            
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(484, 561);
            Controls.Add(lblTitle);
            Controls.Add(lblStatus);
            Controls.Add(btnReset);
            Controls.Add(btnCheck);
            Controls.Add(btnSet);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Графический пароль";
            ResumeLayout(false);
        }

        #endregion

        private Button btnSet;
        private Button btnCheck;
        private Button btnReset;
        private Button lblStatus;
        private Button lblTitle;
    }
}
