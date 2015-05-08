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
        static Game game = new Game();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            potLabel.Text = String.Format("{0:C}", 0);
            playButton.Enabled = false;
            claimButton.Enabled = false;

            gameResultLabel.Text = "Enter your stake...";
            player1BankBalance.Text = "";
            player2BankBalance.Text = "";
            player3BankBalance.Text = "";
            player4BankBalance.Text = "";

            displayCardBacks();
        }

        private void displayCardBacks()
        {
            List<PictureBox> pictureBoxes = new List<PictureBox>() { player1Card1, player1Card2,
                                                                    flopCard1, flopCard2, flopCard3, flopCard4, flopCard5,
                                                                    player2Card1, player2Card2,
                                                                    player3Card1, player3Card2,
                                                                    player4Card1, player4Card2};

            foreach (PictureBox card in pictureBoxes)
            {
                card.ImageLocation = @"CardImages/CardBacks/blue.png";
                card.SizeMode = PictureBoxSizeMode.AutoSize;
            }
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            gameResultLabel.Text = "";

            List<PictureBox> player1PictureBoxes = new List<PictureBox>() { player1Card1, player1Card2 };
            List<PictureBox> player2PictureBoxes = new List<PictureBox>() { player2Card1, player2Card2 };
            List<PictureBox> player3PictureBoxes = new List<PictureBox>() { player3Card1, player3Card2 };
            List<PictureBox> player4PictureBoxes = new List<PictureBox>() { player4Card1, player4Card2 };
            List<PictureBox> flopPictureBoxes = new List<PictureBox>() { flopCard1, flopCard2, flopCard3, flopCard4, flopCard5 };

            game.PlayGame(flopPictureBoxes, player1ResultLabel, player2ResultLabel, player3ResultLabel, player4ResultLabel,
                player1BankBalance, player2BankBalance, player3BankBalance, player4BankBalance,
                player1PictureBoxes, player2PictureBoxes, player3PictureBoxes, player4PictureBoxes, gameResultLabel, potLabel);

            playButton.Enabled = false;
            player1Stake.Enabled = false;
        }

        private void player1Stake_Click(object sender, EventArgs e)
        {
            string stakeInput = player1StakeTextBox.Text;

            int stake = 0;

            if (!int.TryParse(stakeInput, out stake))
            {
                gameResultLabel.Text = "Input not understood...";
                return;
            }

            else
            {
                if (stake <= game.Player1.BankBalance && stake > 0)
                {
                    game.Player1.Bet = stake;
                    gameResultLabel.Text = "Stake of " + String.Format("{0:C}", stake) + " received...";
                    playButton.Enabled = true;
                    player1Stake.Enabled = false;
                    claimButton.Enabled = true;
                    player1StakeTextBox.Enabled = false;
                }

                else
                {
                    gameResultLabel.Text = "Input not understood...";
                    return;
                }
            }
        }

        private void player1StakeTextBox_MouseClick(object sender, MouseEventArgs e)
        {
            player1StakeTextBox.Clear();
        }

        private void claimButton_Click(object sender, EventArgs e)
        {
            potLabel.Text = String.Format("{0:C}", 0);

            player1ResultLabel.Text = "";
            player2ResultLabel.Text = "";
            player3ResultLabel.Text = "";
            player4ResultLabel.Text = "";

            game.Payout(gameResultLabel);
            player1BankBalance.Text = "Player Balance: " + String.Format("{0:C}", game.Player1.BankBalance);
            player1BankBalance.Refresh();
            player2BankBalance.Text = "Player Balance: " + String.Format("{0:C}", game.Player2.BankBalance);
            player2BankBalance.Refresh();
            player3BankBalance.Text = "Player Balance: " + String.Format("{0:C}", game.Player3.BankBalance);
            player3BankBalance.Refresh();
            player4BankBalance.Text = "Player Balance: " + String.Format("{0:C}", game.Player4.BankBalance);
            player4BankBalance.Refresh();

            displayCardBacks();

            gameResultLabel.Text = "Enter your stake...";

            player1StakeTextBox.Enabled = true;
            player1Stake.Enabled = true;
            claimButton.Enabled = false;
            
        }
        
    }
}
