using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Poker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            List<PictureBox> pictureBoxes = new List<PictureBox>() { player1Card1, player1Card2, player1Card3, player1Card4, player1Card5,
                                                                    player2Card1, player2Card2, player2Card3, player2Card4, player2Card5};

            foreach (PictureBox card in pictureBoxes)
            {
                card.ImageLocation = @"CardImages/CardBacks/blue.png";
                card.SizeMode = PictureBoxSizeMode.AutoSize;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<PictureBox> player1PictureBoxes = new List<PictureBox>() { player1Card1, player1Card2, player1Card3, player1Card4, player1Card5 };
            List<PictureBox> player2PictureBoxes = new List<PictureBox>() { player2Card1, player2Card2, player2Card3, player2Card4, player2Card5 };

            Game game = new Game();
            game.PlayGame(player1ResultLabel, player2ResultLabel, player1PictureBoxes, player2PictureBoxes, gameResultLabel);

            //THIS GREYS THE BUTTON OUT AFTER CLICKING!
            //button1.Enabled = false;
        }


    }
}
