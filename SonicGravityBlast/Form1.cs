using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SonicGravityBlast
{
    public partial class Form1 : Form
    {
        //Global Variables
        bool landed = false;
        int gravity = 8;
        int score = 0;
        int platformSpeed = 30;
        Random rnd = new Random();

        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            player.Top += gravity;
            player.Left = 80;
            label1.Text = "Score: " + score;

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && x.Tag == "Platform")
                {
                    x.Left -= platformSpeed;

                    if (x.Left < -500)
                    {
                        x.Width = rnd.Next(100, 300);
                        x.Left = 500;
                        score++;
                    }
                }
            }

            if (player.Bounds.IntersectsWith(p2.Bounds) || player.Bounds.IntersectsWith(p1.Bounds))
            {
                landed = true;
                player.Top = p2.Top - player.Height;
                player.Image = Properties.Resources.SmallSonic1;
            }

            if (player.Bounds.IntersectsWith(p3.Bounds) || player.Bounds.IntersectsWith(p4.Bounds))
            {
                landed = true;
                player.Top = p3.Top + p3.Height;
                player.Image = Properties.Resources.SmallSonic2;
            }

            if (player.Top < -40 || player.Top > ClientSize.Height)
            {
                timer1.Stop();
                label1.Text += " Press R to Restart the Game!";
            }
        }


        //What occurs when you push specific buttons. 
        private void keyisdown(object sender, KeyEventArgs e)
        {
            //If the Spacebar is pressed and the Player is on a Platform, Gravity is turned on and they can jump.
            if (e.KeyCode == Keys.Space && landed == true)
            {
                gravity = -gravity;
                landed = false;
            }

            //If the R key is pressed, the game is reset back to the default settings.
            if (e.KeyCode == Keys.R)
            {
                reset();
            }
        }

        //Settings for the game when it gets reset.
        //....1) Score is reset to 0
        //....2) The Platforms are reset to the default size
        //....3) The player is brought back to its default X and Y position
        //....4) The Platforms are brought back to their default X and Y position
        //....5) The Timer restarts back at 0
        private void reset()
        {
            score = 0;
            p1.Width = 273;
            p2.Width = 273;
            p3.Width = 273;
            p4.Width = 273;
            player.Left = 147;
            player.Top = 131;

            p1.Left = 104;
            p1.Top = 235;

            p2.Left = 491;
            p2.Top = 235;

            p3.Left = 304;
            p3.Top = 26;

            p4.Left = 702;
            p4.Top = 26;

            timer1.Start();
        }
    }
}
