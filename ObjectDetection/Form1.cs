using AForge.Imaging;
using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ObjectDetection //Alfin1998
{
    public partial class Form1 : Form
    {
        private Bitmap bit;
        public Form1()
        {
            InitializeComponent();
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {

            try
            {
                OpenFileDialog open = new OpenFileDialog();
                if (open.ShowDialog() == DialogResult.OK)
                {
                    Image<Bgr, byte> imgInput = new Image<Bgr, byte>(open.FileName);
                    pictureBox1.Image = imgInput.Bitmap;

                    //resize gambar menjadi x,y pixel
                    Image<Bgr, Byte> resize = imgInput.Resize(400, 400, Emgu.CV.CvEnum.Inter.Cubic);
                    this.pictureBox1.Image = resize.ToBitmap();


                    //mengeget gambar atau clone gambar dari picture box
                    bit = (Bitmap)pictureBox1.Image.Clone();

                    //mengambil info lokasi gambar
                    // tb_lokasigambar.Text = open.FileName;

                    //mengambil nilai resolusi
                    int a = pictureBox1.Width;
                    int b = pictureBox2.Height;

                    //   Bitmap b = new Bitmap(pbInput.Image);
                    bit.MakeTransparent(Color.FromArgb(255, 255, 255));
                    pictureBox1.Image = bit;

                  


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Bitmap b = new Bitmap(pictureBox1.Image);
            BlobCounter blobCounter = new BlobCounter();
            blobCounter.MinWidth = 5;
            blobCounter.MinHeight = 5;
            blobCounter.FilterBlobs = true;
            blobCounter.ProcessImage(b);
            blobCounter.ObjectsOrder = ObjectsOrder.XY;

            Rectangle[] rects = blobCounter.GetObjectsRectangles();
            Rectangle[] rects2 = new Rectangle[10];
            Blob[] blob = blobCounter.GetObjectsInformation();
            // NumericUpDown num = blobCounter.ObjectsCount();
            foreach (Rectangle recs in rects)

                if (rects.Length > 0)
                {

                    // int[] num = new int[9];
                    int num = 1;

                    foreach (Rectangle objectRect in rects)
                    {

                        Graphics g = Graphics.FromImage(pictureBox1.Image);

                        using (Pen pen = new Pen(Color.FromArgb(160, 255, 160), 5))
                        {
                            g.DrawRectangle(pen, objectRect);
                            //  g.FillRectangle(Brushes., recs);
                            //var stringSize = g.MeasureString(num.ToString(), this.Font);
                            // g.DrawIcon(newIcon, objectRect);

                            g.DrawString(Convert.ToString(num++), new Font("Arial", 14, FontStyle.Regular), Brushes.White, objectRect);

                            // num++;
                        }

                        g.Dispose();
                    }


                    pictureBox2.Image = pictureBox1.Image;
                }
        }
    }
}



