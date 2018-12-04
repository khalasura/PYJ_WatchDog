using PYJ_WatchDog.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PYJ_WatchDog.Common
{
    public class BackgroudForm : Form
    {
        private bool isDraw = false;
        public int nStxX = 0;
        public int nStxY = 0;
        public int nWidth = 0;
        public int nHeight = 0;

        public int gX = 0;
        public int gY = 0;
        public int gW = 0;
        public int gH = 0;

        public TaskInfo Task { get; set; }


        Point pt = new Point();
        public BackgroudForm()
        {
            this.ShowInTaskbar = false;
            ////BackColor = Color.LightGreen;
            ////TransparencyKey = Color.LightGreen;
            DoubleBuffered = true;

            Paint += new PaintEventHandler(BackgroudForm_Paint);
            this.MouseMove += new MouseEventHandler(BackgroudForm_MouseMove);
            this.MouseDown += new MouseEventHandler(BackgroudForm_MouseDown);
            this.MouseUp += new MouseEventHandler(BackgroudForm_MouseUp);

        }


        void BackgroudForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (!isDraw)
            {
                nStxX = pt.X;
                nStxY = pt.Y;
                isDraw = true;
            }
        }

        void BackgroudForm_MouseUp(object sender, MouseEventArgs e)
        {
            isDraw = false;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        void BackgroudForm_MouseMove(object sender, MouseEventArgs e)
        {
            pt = Cursor.Position;
            if (!isDraw) return;
            nWidth = Math.Abs(nStxX - pt.X);
            nHeight = Math.Abs(nStxY - pt.Y);
            Invalidate();
        }

        void BackgroudForm_Paint(object sender, PaintEventArgs e)
        {
            if (isDraw)
            {
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;
                Rectangle rect = new Rectangle(nStxX, nStxY, nWidth, nHeight);
                e.Graphics.DrawRectangle(new Pen(Brushes.Blue, 2), rect);
                e.Graphics.DrawString(string.Format("(x:{0}, y:{1}, w:{2}, h:{3})",
                    nStxX, nStxY, nWidth, nHeight), new Font("Arial", 10), Brushes.Black, rect.X + 2, rect.Y + 4);
                
                e.Graphics.DrawString($"{Task?.Name}", new Font("Arial", 20, FontStyle.Bold), Brushes.Black, new Rectangle(rect.X+1, rect.Y+1, rect.Width, rect.Height), sf);
                e.Graphics.DrawString($"{Task?.Name}", new Font("Arial", 20, FontStyle.Bold), Brushes.Blue, rect, sf);
            }
            else
            {
                //Rectangle rect = new Rectangle(1, 1, gW, gH);
                //e.Graphics.DrawRectangle(new Pen(Brushes.Blue, 2), rect);

                // 가이드 사각형
                e.Graphics.FillRectangle(Brushes.DimGray, new Rectangle(ClientRectangle.X, (ClientRectangle.Height / 2) - 50, ClientRectangle.Width, 100));

                // 화면 가이드 메세지
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;
                var msg = $"{Task?.Name} 프로그램 위치를 마우스로 드래그 하세요.";
                e.Graphics.DrawString(msg, new Font("굴림체", 20, FontStyle.Bold), Brushes.Black, new Rectangle(ClientRectangle.X + 1, ClientRectangle.Y + 1, ClientRectangle.Width, ClientRectangle.Height), sf);
                e.Graphics.DrawString(msg, new Font("굴림체", 20, FontStyle.Bold), Brushes.White, ClientRectangle, sf);

            }
        }
    }
}
