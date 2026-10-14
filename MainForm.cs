#nullable disable

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GraphicPassword
{
    public partial class MainForm : Form
    {
        private GraphicPasswordControl passwordControl;
        private List<int>? savedPassword = null;
        private bool isSettingMode = false;

        public MainForm()
        {
            InitializeComponent();

            this.btnSet.Click += btnSet_Click;
            this.btnCheck.Click += btnCheck_Click;
            this.btnReset.Click += btnReset_Click;

            passwordControl = new GraphicPasswordControl();
            passwordControl.Location = new Point(12, 120);
            passwordControl.Size = new Size(460, 400);
            passwordControl.GridSize = 3;
            passwordControl.PasswordCompleted += PasswordControl_PasswordCompleted;
            this.Controls.Add(passwordControl);

            lblStatus.Text = "Нажмите «Задать пароль» и нарисуйте узор";
        }

        private void PasswordControl_PasswordCompleted(object? sender, EventArgs e)
        {
            var current = passwordControl.GetPassword();

            if (current.Count == 0)
            {
                lblStatus.Text = "Узор пустой, попробуйте ещё раз";
                return;
            }

            if (isSettingMode)
            {
                savedPassword = new List<int>(current);
                lblStatus.Text = $"Пароль сохранён ({savedPassword.Count} точки). Проверьте его.";
                lblStatus.ForeColor = Color.Green;
                isSettingMode = false;
            }
            else
            {
                if (savedPassword == null)
                {
                    lblStatus.Text = "Сначала задайте пароль!";
                    lblStatus.ForeColor = Color.Red;
                    return;
                }

                if (GraphicPasswordControl.ComparePasswords(savedPassword, current))
                {
                    lblStatus.Text = "Пароль верный! Доступ разрешён.";
                    lblStatus.ForeColor = Color.Green;
                    MessageBox.Show("Пароль верный!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    lblStatus.Text = "Пароль неверный! Попробуйте снова.";
                    lblStatus.ForeColor = Color.Red;
                }
            }
        }

        private void btnSet_Click(object? sender, EventArgs e)
        {
            isSettingMode = true;
            passwordControl.Reset();
            lblStatus.Text = "Нарисуйте новый пароль (соедините минимум 4 точки)";
            lblStatus.ForeColor = Color.Blue;
        }

        private void btnCheck_Click(object? sender, EventArgs e)
        {
            if (savedPassword == null)
            {
                MessageBox.Show("Сначала задайте пароль!", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            isSettingMode = false;
            passwordControl.Reset();
            lblStatus.Text = "Повторите узор для проверки";
            lblStatus.ForeColor = Color.Blue;
        }

        private void btnReset_Click(object? sender, EventArgs e)
        {
            passwordControl.Reset();
            lblStatus.Text = "Поле очищено";
            lblStatus.ForeColor = Color.Black;
        }

        // --- Заглушки для привязок, которые VS создала в дизайнере ---
        private void lblStatus_Click(object? sender, EventArgs e) { }
        private void lblTitle_Click(object? sender, EventArgs e) { }
        private void MainForm_Load(object? sender, EventArgs e) { }
    }
}