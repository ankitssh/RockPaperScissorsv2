using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.Text.RegularExpressions;
using System.Threading;
using System.IO;
using System.Diagnostics;

namespace RockPaperScissorsv2
{
    public partial class Form1 : Form
    {
       
        List<string> imageLocation = new List<string>();
   
        SoundPlayer sp = new SoundPlayer();
        bool spIsPlaying = true;
        public Form1()
        {
            InitializeComponent();
            MusicPlay();
            imagesAdd();
          
           
           
            
           

            
        }

        private void imagesAdd()
        {
            imageLocation.Add("../../Resources/RPS pictures/Rock.png");
            imageLocation.Add("../../Resources/RPS pictures/Paper.png");
            imageLocation.Add("../../Resources/RPS pictures/Scissor.png");
        }

        private void MusicPlay()
        {


            sp.SoundLocation = "../../Resources/Music/Music.wav";
            sp.LoadAsync();
            sp.PlayLooping();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (spIsPlaying)
            {
                Image img = Image.FromFile("../../Resources/RPS pictures/mute.png");
                musicBtn.BackgroundImage = img;
                sp.Stop();
                spIsPlaying = false;


            }
            else {
                Image img = Image.FromFile("../../Resources/RPS pictures/volume-button-icon-93467.png");
                musicBtn.BackgroundImage = img;
                sp.PlayLooping();
                spIsPlaying = true;

            
            
            }

        }

        

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
            
            Random rnd = new Random();
            int randomNum=rnd.Next(0,3);
            pictureBox1.ImageLocation=imageLocation[randomNum];
            pictureBox1.Enabled =false;

            Thread t1 = new Thread(card2Thread);
            t1.Start();
          //  t1.Join();

           


           

        
            
        }

        private void initialImage()
        {
            pictureBox1.ImageLocation = "../../Resources/RPS pictures/back.png";
            pictureBox2.ImageLocation = "../../Resources/RPS pictures/back.png";
        }


        private void pictureBox2_Click(object sender, EventArgs e)
        {
            
            
            

        }


        private  void result(string card1, string card2)
        {
         //   MessageBox.Show(Thread.CurrentContext.ToString());

            if (card1.Equals("Rock") && card2.Equals("Scissor") || card1.Equals("Scissor") && card2.Equals("Paper") || card1.Equals("Paper") && card2.Equals("Rock"))
            {
                Invoke(new Action(() => { YourScore.Text = (Convert.ToInt32(YourScore.Text) + 1).ToString(); }));



            }
            else if (card1.Equals("Rock") && card2.Equals("Rock") || card1.Equals("Scissor") && card2.Equals("Scissor") || card1.Equals("Paper") && card2.Equals("Paper"))
            {
                Invoke(new Action(() => { YourScore.Text = (Convert.ToInt32(YourScore.Text) + 1).ToString(); }));
                Invoke(new Action(() => { CPUScore.Text = (Convert.ToInt32(CPUScore.Text) + 1).ToString(); }));
              
               

            }
            else
            {
                Invoke(new Action(() => { CPUScore.Text = (Convert.ToInt32(CPUScore.Text) + 1).ToString(); }));

            }
            Thread.Sleep(1000);

            initialImage();

            Invoke(new Action(() => { pictureBox1.Enabled = true; }));

            Invoke(new Action(() => { pictureBox2.Enabled = true; })); 
          
        }

        public void card2Thread()
        {
            Random rnd = new Random();
            int randomNum = rnd.Next(0, 3);
            Thread.Sleep(1000);
           // label1.Text = "Thinking...";
            
            pictureBox2.ImageLocation = imageLocation[randomNum];
           // label1.Text="";
            

            string card1 = Regex.Match(pictureBox1.ImageLocation, @"\/([a-zA-Z]+)\.").Groups[1].Value;
            string card2 = Regex.Match(pictureBox2.ImageLocation, @"\/([a-zA-Z]+)\.").Groups[1].Value;

           result(card1, card2);


        }

        private void resetBtn_Click(object sender, EventArgs e)
        {
            YourScore.Text = "0";
            CPUScore.Text = "0";
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            YourScore.Text = "0";
            CPUScore.Text = "0";

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StreamWriter sw = new StreamWriter("../../Resources/RPS pictures/data.txt");
            sw.WriteLine((YourScore.Text+"\t"+CPUScore.Text));
            sw.Close();
           
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StreamReader sr = new StreamReader("../../Resources/RPS pictures/data.txt");
            string []data=sr.ReadLine().Split('\t');
            YourScore.Text = data[0];
            CPUScore.Text = data[1];
            sr.Close();
        }

       

       
    }
}
