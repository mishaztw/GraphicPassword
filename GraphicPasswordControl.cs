using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#pragma warning disable WFO1000

namespace GraphicPassword
{
    public class GraphicPasswordControl : Control
    {
        [System.ComponentModel.Browsable(false)]
        public int GridSize { get; set; } = 3;
        [System.ComponentModel.Browsable(false)]
        public int DotRadius { get; set; } = 12;
        [System.ComponentModel.Browsable(false)]
        public int LineWidth { get; set; } = 6;

        private List<int> selectedDots = new List<int>();
        private bool isDrawing = false;
        private Point currentMouse;

        public event EventHandler? PasswordCompleted;

        public GraphicPasswordControl()
        {
            this.DoubleBuffered = true;
            this.BackColor = Color.White;
        }

        // Сброс узора
        public void Reset()
        {
            selectedDots.Clear();
            isDrawing = false;
            Invalidate();
        }

        // Получить список выбранных точек (индексы от 0)
        public List<int> GetPassword()
        {
            return new List<int>(selectedDots);
        }

        // Сравнить два узора
        public static bool ComparePasswords(List<int> a, List<int> b)
        {
            if (a == null || b == null) return false;
            if (a.Count != b.Count || a.Count == 0) return false;
            for (int i = 0; i < a.Count; i++)
                if (a[i] != b[i]) return false;
            return true;
        }

        // Рассчитать координаты точек
        private Point GetDotCenter(int index)
        {
            int row = index / GridSize;
            int col = index % GridSize;
            int padding = 40;
            int w = this.Width - padding * 2;
            int h = this.Height - padding * 2;
            int stepX = w / (GridSize - 1);
            int stepY = h / (GridSize - 1);
            int x = padding + col * stepX;
            int y = padding + row * stepY;
            return new Point(x, y);
        }

        // Найти точку под курсором
        private int GetDotAtPoint(Point p)
        {
            for (int i = 0; i < GridSize * GridSize; i++)
            {
                Point c = GetDotCenter(i);
                int dx = p.X - c.X;
                int dy = p.Y - c.Y;
                if (dx * dx + dy * dy <= (DotRadius + 6) * (DotRadius + 6))
                    return i;
            }
            return -1;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                Reset();
                isDrawing = true;
                int idx = GetDotAtPoint(e.Location);
                if (idx >= 0 && !selectedDots.Contains(idx))
                {
                    selectedDots.Add(idx);
                    Invalidate();
                }
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (!isDrawing) return;
            currentMouse = e.Location;
            int idx = GetDotAtPoint(e.Location);
            if (idx >= 0 && !selectedDots.Contains(idx))
            {
                selectedDots.Add(idx);
            }
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (isDrawing)
            {
                isDrawing = false;
                Invalidate();
                PasswordCompleted?.Invoke(this, EventArgs.Empty);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Линии между точками
            if (selectedDots.Count > 1)
            {
                using (Pen pen = new Pen(Color.DodgerBlue, LineWidth))
                {
                    pen.StartCap = LineCap.Round;
                    pen.EndCap = LineCap.Round;
                    for (int i = 0; i < selectedDots.Count - 1; i++)
                    {
                        e.Graphics.DrawLine(pen,
                            GetDotCenter(selectedDots[i]),
                            GetDotCenter(selectedDots[i + 1]));
                    }

                    // Линия до курсора, если ещё рисуем
                    if (isDrawing && selectedDots.Count > 0)
                    {
                        e.Graphics.DrawLine(pen,
                            GetDotCenter(selectedDots[selectedDots.Count - 1]),
                            currentMouse);
                    }
                }
            }

            // Точки
            for (int i = 0; i < GridSize * GridSize; i++)
            {
                Point c = GetDotCenter(i);
                bool isSelected = selectedDots.Contains(i);

                using (Brush fill = new SolidBrush(isSelected ? Color.DodgerBlue : Color.LightGray))
                using (Pen outline = new Pen(Color.Gray, 2))
                {
                    e.Graphics.FillEllipse(fill, c.X - DotRadius, c.Y - DotRadius, DotRadius * 2, DotRadius * 2);
                    e.Graphics.DrawEllipse(outline, c.X - DotRadius, c.Y - DotRadius, DotRadius * 2, DotRadius * 2);
                }
            }
        }
    }
}

#pragma warning restore WFO1000