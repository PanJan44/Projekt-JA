using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using JaProj;

namespace UI
{
    public partial class Form1 : Form
    {
        Bitmap sourceImageBmp;
        Image sourceImage;

        byte[] imageWithHeaders;
        byte[] imageWithoutHeaders;
        byte[] header;
        byte[] outByteImage;
        int bitWidth;
        int bitHeight;

        public Form1()
        {
            InitializeComponent();
            cSharpRadioButton.Checked = true;
            trackBar1.Value = Environment.ProcessorCount;
            threadCount.Text = trackBar1.Value.ToString();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            pictureBox1.Enabled = false;
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.InitialDirectory = @"C:\Documents";
                ofd.Filter = "bmp files (*.bmp)|*.bmp";
                ofd.FilterIndex = 2;
                ofd.RestoreDirectory = true;

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    var fileStream = ofd.OpenFile();

                    using (StreamReader sr = new StreamReader(fileStream))
                    {
                        {
                            pictureBox1.Image = new Bitmap(fileStream);
                            sourceImage = pictureBox1.Image;
                            sourceImageBmp = new Bitmap(pictureBox1.Image);

                            imageWithHeaders = ImgToByte(sourceImage);
                            header = imageWithHeaders.Take(54).ToArray();
                            imageWithoutHeaders = new byte[imageWithHeaders.Length - 54];

                            for (int i = 54; i < imageWithHeaders.Length; i++)
                            {
                                imageWithoutHeaders[i - 54] = imageWithHeaders[i];
                            }

                            bitWidth = BitConverter.ToInt32(header.Skip(18).Take(4).ToArray(), 0) * 3;
                            bitHeight = BitConverter.ToInt32(header.Skip(22).Take(4).ToArray(), 0);

                            outByteImage = new byte[imageWithHeaders.Length];

                            pictureBox1.Image = ByteToImg(imageWithHeaders);
                            filterButton.Enabled = true;
                            pictureBox1.Enabled = true;

                            histogramIn.Enabled = true;
                        }
                    }

                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (var sfd = new SaveFileDialog())
            {
                sfd.InitialDirectory = @"C:\Documents";
                sfd.Filter = "bmp files (*.bmp)|*.bmp";
                sfd.FilterIndex = 2;
                sfd.RestoreDirectory = true;

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    var fileStream = sfd.OpenFile();
                    pictureBox2.Image.Save(fileStream, ImageFormat.Bmp);
                }
            }
        }

        private Image ByteToImg(byte[] img)
        {
            ImageConverter converter = new ImageConverter();
            return (Image)converter.ConvertFrom(img);
        }

        private static byte[] ImgToByte(Image img)
        {
            ImageConverter converter = new ImageConverter();
            byte[] image = (byte[])converter.ConvertTo(img, typeof(byte[]));

            return image;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            threadCount.Text = trackBar1.Value.ToString();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var watch = new Stopwatch();
                Algorithms algs;

                if (cSharpRadioButton.Checked)
                    algs = new Algorithms(Int32.Parse(cSharpRadioButton.Tag.ToString()));
                else
                    algs = new Algorithms(Int32.Parse(asmRadioButton.Tag.ToString()));

                watch.Start();
                byte[] result = algs.BlurImage(imageWithoutHeaders, bitWidth, bitHeight, Convert.ToInt32(radiusVal.Text), Convert.ToInt32(threadCount.Text));
                watch.Stop();

                for (int i = 0; i < 54; i++)
                    outByteImage[i] = header[i];

                for (int i = 54; i < outByteImage.Length; i++)
                    outByteImage[i] = result[i - 54];

                pictureBox2.Image = ByteToImg(outByteImage);

                saveButton.Enabled = true;
                var elapsedTime = watch.Elapsed;

                listOfTimes.Items.Add($"{elapsedTime.Minutes:00}:{elapsedTime.Seconds:00}:{elapsedTime.Milliseconds / 10:000}");
                histogramOut.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            radiusVal.Text = trackBar2.Value.ToString();
        }

        private HistogramValues GetValuesForHistograms(byte[] image, int height, int width)
        {
            int[] red = new int[256];
            int[] green = new int[256];
            int[] blue = new int[256];

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j += 3)
                {
                    red[image[i * width + j + 2]]++;
                    green[image[i * width + j + 1]]++;
                    blue[image[i * width + j]]++;
                }
            }

            return new HistogramValues(red, green, blue);
        }

        private void DrawHistogram(byte[] image, int width, int height, Graphics g)
        {
            {
                HistogramValues histogram = GetValuesForHistograms(image, width, height);
                var red = histogram.red;
                var green = histogram.green;
                var blue = histogram.blue;

                float part = histogramIn.Width / 3;
                float dx = part / 256;

                int[] maxVals = new int[] { red.Max(), green.Max(), blue.Max() };
                int maxVal = maxVals.Max();

                float barHeight = 0;

                var coeff = (float)histogramIn.Height / maxVal;

                for (int i = 0; i < 256; i++)
                {
                    barHeight = (float)(red[i] * coeff);
                    g.DrawLine(Pens.Red, new PointF(i * dx, histogramIn.Bottom), new PointF(i * dx, (histogramIn.Height - barHeight)));
                }

                for (int i = 0; i < 256; i++)
                {
                    barHeight = (float)(green[i] * coeff);
                    g.DrawLine(Pens.Green, new PointF(i * dx + part + 2, histogramIn.Bottom), new PointF(i * dx + part + 2, histogramIn.Height - barHeight));
                }

                for (int i = 0; i < 256; i++)
                {
                    barHeight = (float)(blue[i] * coeff);
                    g.DrawLine(Pens.Blue, new PointF(i * dx + part * 2 + 2, histogramIn.Bottom), new PointF(i * dx + part * 2 + 2, histogramIn.Height - barHeight));

                }

                g.DrawString("0", new Font("Arial", 10), new SolidBrush(Color.Black), new PointF(0, histogramIn.Height - 15));
                g.DrawString("255", new Font("Arial", 10), new SolidBrush(Color.Black), new PointF(part - 32, histogramIn.Height - 15));
                g.DrawString("0", new Font("Arial", 10), new SolidBrush(Color.Black), new PointF(part + 3, histogramIn.Height - 15));
                g.DrawString("255", new Font("Arial", 10), new SolidBrush(Color.Black), new PointF(part * 2 - 32, histogramIn.Height - 15));
                g.DrawString("0", new Font("Arial", 10), new SolidBrush(Color.Black), new PointF(part * 2 + 3, histogramIn.Height - 15));
                g.DrawString("255", new Font("Arial", 10), new SolidBrush(Color.Black), new PointF(3 * part - 32, histogramIn.Height - 15));

                g.DrawString(maxVal.ToString(), new Font("Arial", 10), new SolidBrush(Color.Black), new PointF(0, 0));
            }
        }

        private void histogramIn_Paint(object sender, PaintEventArgs e)
        {
            lock (pictureBox1)
            {
                if (histogramIn.Enabled && pictureBox1.Enabled)
                {
                    DrawHistogram(imageWithHeaders, bitHeight, bitWidth, e.Graphics);
                    histogramIn.Enabled = false;
                    pictureBox1.Enabled = false;
                }
            }
        }

        private void pictureBox3_Paint(object sender, PaintEventArgs e)
        {
            if (histogramOut.Enabled)
            {
                DrawHistogram(outByteImage, bitHeight, bitWidth, e.Graphics);
                histogramOut.Enabled = false;
            }
        }
    }
}
