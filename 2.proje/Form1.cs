using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Imaging.Filters;
using AForge.Imaging;
using AForge.Video;
using AForge.Video.DirectShow;
using AForge.Vision;
using AForge.Vision.Motion;
namespace WindowsFormsApp11
{
    public partial class Form1 : Form
    {
        private VideoCaptureDevice pccam; // Kulanacağımız aygıt.
        private FilterInfoCollection pccamera; // Pc' de bulunan cameraları tutan bir dizi.
        public Form1()
        {
            InitializeComponent();
                      
        }
        int X;
        int Y = 0;

      

        private void Form1_Load(object sender, EventArgs e)
        {
          
          
            for (int i = 0; i < System.IO.Ports.SerialPort.GetPortNames().Length; i++)
            {
                comboBox1.Items.Add(System.IO.Ports.SerialPort.GetPortNames()[i]);
            }
            pccamera = new FilterInfoCollection(FilterCategory.VideoInputDevice); //Sisteme bagli olan Cam listesini aliyoruz

            foreach (FilterInfo VideoCaptureDevice in pccamera)
            {
                comboBox2.Items.Add(VideoCaptureDevice.Name); //PC'deki Kameralar hepsi ComboBox'da listelenir.
                comboBox2.SelectedIndex = 0;
            }
        }
     
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                pccam = new VideoCaptureDevice(pccamera[comboBox2.SelectedIndex].MonikerString);
                pccam.NewFrame += new NewFrameEventHandler(pccam_NewFrame);
                pccam.DesiredFrameRate = 30;          // Ekran Görüntü kalitesi için.
                pccam.DesiredFrameSize = new Size(512, 384);  // Ekran Görüntü büyüklüğü için.
                pccam.Start(); 
               
            }
            catch (Exception)//Kamera bulunmaması durumnda.
            {

                MessageBox.Show("HİÇ KAMERA BULUNAMADI", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pccam_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap image = (Bitmap)eventArgs.Frame.Clone();
            Bitmap image1 = (Bitmap)eventArgs.Frame.Clone();
            pictureBox1.Image = image;
            

            if (radioButton2.Checked)
            {
                EuclideanColorFiltering filter = new EuclideanColorFiltering();
                filter.CenterColor = new RGB(Color.FromArgb(180, 30, 35)); // Algılanacak Renk ve merkez noktası bulunur.
                filter.Radius = 50;
                filter.ApplyInPlace(image1);//Filitre Çalıştırılır.             
                cevreal(image1);// Algilanan rengi Çevrçevelemek veya hedeflemek için gerekli Method.
            }
        }

        private void cevreal(Bitmap image)// Algilanan rengi Çevrçevelemek veya hedeflemek için gerekli Method.
        {
            BlobCounter blobCounter = new BlobCounter();
            blobCounter.MinWidth = 2;
            blobCounter.MinHeight = 2;
            blobCounter.FilterBlobs = true;
            blobCounter.ObjectsOrder = ObjectsOrder.Size;

            Grayscale grayFilter = new Grayscale(0.2125, 0.7154, 0.0721);
            Bitmap grayImage = grayFilter.Apply(image);

            blobCounter.ProcessImage(grayImage);
            Rectangle[] rects = blobCounter.GetObjectsRectangles();
            foreach (Rectangle recs in rects)
            {

                if (rects.Length > 0)
                {
                    Rectangle objectRect = rects[0];
                    Graphics g = pictureBox1.CreateGraphics();
                    using (Pen pen = new Pen(Color.FromArgb(252, 3, 26), 2))
                    {

                        g.DrawRectangle(pen, objectRect);
                    }

                    X = objectRect.X + (objectRect.Width / 2); //Dikdörtgenin Koordinatlari alınır.
                    Y = objectRect.Y + (objectRect.Height / 2);//Dikdörtgenin Koordinatlari alınır.
                    g.DrawString(X.ToString() + "X" + Y.ToString(), new Font("Arial", 12), Brushes.Red, new System.Drawing.Point(250, 1));
                    g.Dispose();
                   
                 

                }
                
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {

            timer1.Enabled = true;


        }

      

      
        
       
        private void button1_MouseMove(object sender, MouseEventArgs e)
        {
            button1.BackColor = Color.White;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.BackColor = Color.FromArgb(192, 64, 0);
        }
       
            
                
           


       
        private void button1_Click(object sender, EventArgs e)
        {
            serialPort1.PortName = comboBox1.Text;
            label1.Text = "Arduino'ya Bağlandı (" + comboBox1.Text + ")";
            serialPort1.Open();
        }

       

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (X >= 512) X = 255;
            else if (X >= 500) X = 250;
            else if (X >= 490) X = 245;
            else if (X >= 480) X = 240;
            else if (X >= 470) X = 235;
            else if (X >= 460) X = 230;
            else if (X >= 450) X = 225;
            else if (X >= 440) X = 220;
            else if (X >= 430) X = 215;
            else if (X >= 420) X = 210;
            else if (X >= 410) X = 205;
            else if (X >= 400) X = 200;
            else if (X >= 390) X = 195;
            else if (X >= 380) X = 190;
            else if (X >= 370) X = 185;
            else if (X >= 360) X = 180;
            else if (X >= 350) X = 175;
            else if (X >= 340) X = 170;
            else if (X >= 330) X = 165;
            else if (X >= 320) X = 160;
            else if (X >= 310) X = 155;
            else if (X >= 300) X = 150;
            else if (X >= 290) X = 145;
            else if (X >= 280) X = 140;
            else if (X >= 270) X = 135;
            else if (X >= 260) X = 130;
            else if (X >= 250) X = 125;
            else if (X >= 240) X = 120;
            else if (X >= 230) X = 115;
            else if (X >= 220) X = 110;
            else if (X >= 210) X = 105;
            else if (X >= 200) X = 100;
            else if (X >= 190) X = 95;
            else if (X >= 180) X = 90;
            else if (X >= 170) X = 85;
            else if (X >= 160) X = 80;
            else if (X >= 150) X = 75;
            else if (X >= 140) X = 70;
            else if (X >= 130) X = 65;
            else if (X >= 120) X = 60;
            else if (X >= 110) X = 55;
            else if (X >= 100) X = 50;
            else if (X >= 90) X = 45;
            else if (X >= 80) X = 40;
            else if (X >= 70) X = 35;
            else if (X >= 60) X = 30;
            else if (X >= 50) X = 25;
            else if (X >= 40) X = 20;
            else if (X >= 30) X = 15;
            else if (X >= 20) X = 10;
            else if (X >= 10) X = 5;
            else X = 0;
            
            int o = Convert.ToInt32(X);
            byte[] b = BitConverter.GetBytes(o);
            serialPort1.Write(b, 0, 4);
           
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
    }
