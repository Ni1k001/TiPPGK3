using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TiPPGK3
{
    public partial class Form1 : Form
    {
        private readonly Graphics _graphics;
        private readonly Graphics _graphics2;
        private readonly Graphics _graphics3;

        private Bitmap _bmp = null;

        private Rectangle s1;
        private Rectangle s2;
        private Rectangle s3;
        
        private Timer timer;

        public Form1()
        {
            InitializeComponent();

            _bmp = new Bitmap(1280, 720);
            _graphics = Graphics.FromImage(_bmp);
            _graphics2 = Graphics.FromImage(_bmp);
            _graphics3 = Graphics.FromImage(_bmp);

            pictureBox1.Image = _bmp;

            s1 = new Rectangle(0, 0, 40, 40);
            s2 = new Rectangle(0, 0, 40, 20);
            s3 = new Rectangle(0, 0, 40, 10);

            timer = new Timer();
            timer.Interval = 33;
            timer.Tick += OnTick;
            timer.Start();

            Invalidate();
            Position(_graphics, s1, 1280 / 2 - 19, 720 / 2 - 19);
            Position(_graphics2, s2, 1280 / 2 - 19, 720 / 2 - 9 + 150);
            Position(_graphics3, s3, 1280 / 2 - 19, 720 / 2 - 5 + 200);
        }

        float rotationAngle = 2.0f;
        float rotation = 0.0f;

        private void OnTick(object sender, EventArgs e)
        {
            _graphics.Clear(Color.AliceBlue);

            if (rotation >= 360.0f)
                rotation = 0.0f;

            using (Graphics g = Graphics.FromImage(_bmp))
            {
                rotation += rotationAngle;

                _graphics.DrawRectangle(Pens.Black, s1);
                _graphics2.DrawRectangle(Pens.Red, s2);
                _graphics3.DrawRectangle(Pens.Green, s3);

                Rotation(_graphics, rotationAngle * 1.5f, s1);

                Position(_graphics2, s2, 0, -150);
                Rotation(_graphics2, rotationAngle * 1.5f, s2);
                Position(_graphics2, s2, 0, 150);

                Position(_graphics3, s3, 0, -200);
                Rotation(_graphics3, rotationAngle, s3);
                Position(_graphics3, s3, 0, 200);

                Position(_graphics3, s3, 0, -50);
                Rotation(_graphics3, rotationAngle / 2, s3);
                Position(_graphics3, s3, 0, 50);

                Refresh();
                Invalidate();
            }
        }

        private void Rotation(Graphics g, float angle, Rectangle r)
        {
            float dx = r.X * (float)Math.Cos(angle) - r.Y * (float)Math.Sin(angle);
            float dy = r.X * (float)Math.Sin(angle) + r.Y * (float)Math.Cos(angle);

            Matrix m = new Matrix(1, 0, 0, 1, 0, 0);

            m.Multiply(new Matrix(1, 0, 0, 1, r.Width / 2, r.Height / 2));

            m.Rotate(angle);

            m.Multiply(new Matrix(1, 0, 0, 1, -r.Width / 2, -r.Height / 2));

            g.MultiplyTransform(m);
        }

        private void Position(Graphics g, Rectangle r, float offsetX, float offsetY)
        {
            Matrix m = new Matrix(1, 0, 0, 1, 0, 0);
            
            m.Multiply(new Matrix(1, 0, 0, 1, offsetX, offsetY));

            g.MultiplyTransform(m);
        }
    }
}
