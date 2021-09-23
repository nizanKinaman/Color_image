using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        class Point
        {
            public double r;
            public double g;
            public double b;
        }

        static Point[] generateRandomPoints(int count)
        {
            var rand = new Random();
            Point[] points = new Point[count];
            for (var i = 0; i < points.Length; i++)
            {
                points[i] = new Point();
                points[i].r = rand.Next(20, 235);
                points[i].g = rand.Next(20, 235);
                points[i].b = rand.Next(20, 235);
            }
            return points;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string img = "D:\\Image\\" + 5 + ".jpg";
            Bitmap bmp = new Bitmap(img);
            pictureBox1.Image = Image.FromFile(img);

            int width = bmp.Width;
            int height = bmp.Height;
            int POINTS_COUNT = width * height;
            int[] alfas = new int[POINTS_COUNT];
            Point[] points = generateRandomPoints(POINTS_COUNT);
            var num_pixel = 0;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color p = bmp.GetPixel(x, y);
                    int a = p.A;
                    int r = p.R;
                    int g = p.G;
                    int b = p.B;

                    alfas[num_pixel] = a;
                    points[num_pixel].r = r;
                    points[num_pixel].g = g;
                    points[num_pixel].b = b;
                    num_pixel++;
                }
            }

            Bitmap bmp_1 = new Bitmap(bmp);
            Bitmap bmp_2 = new Bitmap(bmp);
            Bitmap bmp_3 = new Bitmap(bmp);

            Dictionary<int, int> d1 = new Dictionary<int, int>();
            Dictionary<int, int> d2 = new Dictionary<int, int>();

            num_pixel = 0;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int r1 = (int)(points[num_pixel].r * 0.299);
                    int g1 = (int)(points[num_pixel].g * 0.587);
                    int b1 = (int)(points[num_pixel].b * 0.114);
                    int k1 = (r1 + g1 + b1);
                    if (d1.ContainsKey(k1))
                        d1[k1]++;
                    else
                        d1[k1] = 1;


                    int r2 = (int)(points[num_pixel].r * 0.2126);
                    int g2 = (int)(points[num_pixel].g * 0.7152);
                    int b2 = (int)(points[num_pixel].b * 0.0722);
                    int k2 = (r2 + g2 + b2);
                    if (d2.ContainsKey(k2))
                        d2[k2]++;
                    else
                        d2[k2] = 1;

                    int r3 = Math.Abs(k1 - k2);
                    int g3 = Math.Abs(k1 - k2);
                    int b3 = Math.Abs(k1 - k2);

                    bmp_1.SetPixel(x, y, Color.FromArgb(alfas[num_pixel], k1, k1, k1));
                    bmp_2.SetPixel(x, y, Color.FromArgb(alfas[num_pixel], k2, k2, k2));
                    bmp_3.SetPixel(x, y, Color.FromArgb(alfas[num_pixel], r3, g3, b3));
                    num_pixel++;
                }
            }
            pictureBox2.Image = bmp_1;
            pictureBox3.Image = bmp_2;
            pictureBox4.Image = bmp_3;

            double x_char1, y_char1;
            this.chart1.Series[0].Points.Clear();
            x_char1 = 0;

            foreach (var key in d1)
            {
                y_char1 = key.Value;
                this.chart1.Series[0].Points.AddXY(x_char1, y_char1);
                x_char1 = key.Key;
            }

            double x_char2, y_char2;
            this.chart2.Series[0].Points.Clear();
            x_char2 = 0;

            foreach (var key in d2)
            {
                y_char2 = key.Value;
                this.chart2.Series[0].Points.AddXY(x_char2, y_char2);
                x_char2 = key.Key;
            }
        }
    }
}
