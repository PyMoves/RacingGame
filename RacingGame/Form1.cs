using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RacingGame
{
    public partial class Form1 : Form
    {

        private Point pos;
        private bool dragging, lose = false;
        private int CountCoins = 0;
        public Form1()
        {
            InitializeComponent();

            bg1.MouseDown += MouseClickDown;
            bg1.MouseUp += MouseClickUp;
            bg1.MouseMove += MouseClickMove;
            bg2.MouseDown += MouseClickDown;
            bg2.MouseUp += MouseClickUp;
            bg2.MouseMove += MouseClickMove;

            labelLose.Visible = false;
            buttonRestart.Visible = false;
            KeyPreview = true;
        }

        private void MouseClickDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            pos.X = e.X; 
            pos.Y = e.Y;
        }
        private void MouseClickUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }
        private void MouseClickMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point currentPoint = PointToScreen(new Point(e.X, e.Y));
                this.Location = new Point(currentPoint.X - pos.X, currentPoint.Y - pos.Y + bg1.Top);
            }
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
                this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int speed = 5;
            bg1.Top += speed;
            bg2.Top += speed;

            int EnemyCarSpeed = 7;
            enemy1.Top += EnemyCarSpeed;
            enemy2.Top += EnemyCarSpeed;

            coin.Top += speed;

            if (bg1.Top >= 650)
            {
                bg1.Top = 0;
                bg2.Top = -650;
            }

            if (coin.Top >= 650)
            {
                coin.Top = -50;
                Random rand = new Random();
                coin.Left = rand.Next(150, 300);
            }

            if (enemy1.Top >= 650)
            {
                enemy1.Top = -130;
                Random rand = new Random();
                enemy1.Left = rand.Next(150, 300);
            }

            if (enemy2.Top >= 650)
            {
                enemy2.Top = -400;
                Random rand = new Random();
                enemy2.Left = rand.Next(310, 560);
            } 

            if (player.Bounds.IntersectsWith(enemy1.Bounds) 
                || player.Bounds.IntersectsWith(enemy2.Bounds))
            {
                timer1.Enabled = false;
                lose = true;
                labelLose.Visible = true;
                buttonRestart.Visible = true;
            }

            if (player.Bounds.IntersectsWith(coin.Bounds))
            {
                CountCoins++;
                labelCoin.Text = "Монеты: " + CountCoins.ToString();
                coin.Top = -50;
                Random rand = new Random();
                coin.Left = rand.Next(150, 300);
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (lose) return;

            int speed = 10;
            //смещение игрока влево и вправо
            if ((e.KeyCode == Keys.Left || e.KeyCode == Keys.A) && player.Left > 150)
                player.Left -= speed;
            else if ((e.KeyCode == Keys.Right || e.KeyCode == Keys.D) && player.Right < 700)
                player.Left += speed;
        }

        private void buttonRestart_Click(object sender, EventArgs e)
        {
            enemy1.Top = -130;
            enemy2.Top = -400;
            labelLose.Visible = false;
            buttonRestart.Visible = false;
            timer1.Enabled = true;
            lose = false;
            CountCoins = 0;
            labelCoin.Text = "Монеты: 0";
            coin.Top = -500;
        }
    }
}
