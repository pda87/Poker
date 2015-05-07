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
            List<PictureBox> pictureBoxes = new List<PictureBox>() { player1Card1, player1Card2,
                                                                    flopCard1, flopCard2, flopCard3, flopCard4, flopCard5,
                                                                    player2Card1, player2Card2 };

            foreach (PictureBox card in pictureBoxes)
            {
                card.ImageLocation = @"CardImages/CardBacks/blue.png";
                card.SizeMode = PictureBoxSizeMode.AutoSize;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<PictureBox> player1PictureBoxes = new List<PictureBox>() { player1Card1, player1Card2 };
            List<PictureBox> player2PictureBoxes = new List<PictureBox>() { player2Card1, player2Card2 };
            List<PictureBox> flopPictureBoxes = new List<PictureBox>() { flopCard1, flopCard2, flopCard3, flopCard4, flopCard5 };

            Game game = new Game();

            game.PlayGame(flopPictureBoxes, player1ResultLabel, player2ResultLabel, player1PictureBoxes, player2PictureBoxes, gameResultLabel);

            //THIS GREYS THE BUTTON OUT AFTER CLICKING!
            //button1.Enabled = false;
        }

   


    }
}
