﻿using System;
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
            roundLabel.Text = "GAME: " + game.RoundNumber.ToString();
            potLabel.Text = String.Format("{0:C}", 0);
            player1StakeTextBox.Enabled = false;
            player1Stake.Enabled = false;
            claimButton.Enabled = false;
            displayCardBacks();
            displayPlayerBankBalances();
            gameResultLabel.Text = "CLICK PLAY...";
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            roundLabel.Text = "GAME: " + game.RoundNumber.ToString();
            gameResultLabel.Text = "";

            List<PictureBox> player1PictureBoxes = new List<PictureBox>() { player1Card1, player1Card2 };
            List<PictureBox> player2PictureBoxes = new List<PictureBox>() { player2Card1, player2Card2 };
            List<PictureBox> player3PictureBoxes = new List<PictureBox>() { player3Card1, player3Card2 };
            List<PictureBox> player4PictureBoxes = new List<PictureBox>() { player4Card1, player4Card2 };
            List<PictureBox> flopPictureBoxes = new List<PictureBox>() { flopCard1, flopCard2, flopCard3, flopCard4, flopCard5 };

            if (game.Player2.Bankrupt && game.Player3.Bankrupt && game.Player4.Bankrupt)
            {
                gameResultLabel.Text = "YOU WIN!";
                playButton.Enabled = false;
                claimButton.Enabled = false;
                player1Stake.Enabled = false;
                player1StakeTextBox.Enabled = false;
                restartButton.Enabled = true;
            }

            else if (game.FirstClick)
            {
                game.BetManager.bettingRound(game, potLabel, betOutputLabel, player1BankBalance, player2BankBalance, player3BankBalance, player4BankBalance);

                playButton.Enabled = false;
                gameResultLabel.Text = "ENTER STAKE";
                playButton.Enabled = false;
                player1StakeTextBox.Enabled = true;
                player1Stake.Enabled = true;

                game.FirstClick = false;
            }

            else if (game.ResetGame)
            {
                game.Pot = 0;

                betOutputLabel.Text = game.BetManager.BetOutput.ToString();

                game.PlayGame(flopPictureBoxes, player1ResultLabel, player2ResultLabel, player3ResultLabel, player4ResultLabel,
                player1BankBalance, player2BankBalance, player3BankBalance, player4BankBalance,
                player1PictureBoxes, player2PictureBoxes, player3PictureBoxes, player4PictureBoxes, gameResultLabel, potLabel);

            }

            else if (game.EndGame)
            {
                playButton.Enabled = false;
                claimButton.Enabled = true;

                betOutputLabel.Text = game.BetManager.BetOutput.ToString();

                game.PlayGame(flopPictureBoxes, player1ResultLabel, player2ResultLabel, player3ResultLabel, player4ResultLabel,
                player1BankBalance, player2BankBalance, player3BankBalance, player4BankBalance,
                player1PictureBoxes, player2PictureBoxes, player3PictureBoxes, player4PictureBoxes, gameResultLabel, potLabel);

            }

            else if (game.Flop.Count == 0 || game.Flop.Count == 3 || game.Flop.Count == 4 || game.Flop.Count == 5)
            {
                game.BetManager.bettingRound(game, potLabel, betOutputLabel, player1BankBalance, player2BankBalance, player3BankBalance, player4BankBalance);

                game.PlayGame(flopPictureBoxes, player1ResultLabel, player2ResultLabel, player3ResultLabel, player4ResultLabel,
                player1BankBalance, player2BankBalance, player3BankBalance, player4BankBalance,
                player1PictureBoxes, player2PictureBoxes, player3PictureBoxes, player4PictureBoxes, gameResultLabel, potLabel);

                stakeMethod();
            }
        }

        private void stakeMethod()
        {
            if (game.Player1.BankBalance == 0)
            {
                gameResultLabel.Text = "ALL IN ...";
                playButton.Enabled = true;
                player1Stake.Enabled = false;
                player1StakeTextBox.Text = "0";
                player1StakeTextBox.Enabled = false;
            }

            else
            {
                playButton.Enabled = false;
                gameResultLabel.Text = "ENTER STAKE";
                playButton.Enabled = false;
                player1StakeTextBox.Enabled = true;
                player1Stake.Enabled = true;
            }

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
                if (stake <= game.Player1.BankBalance && stake >= 0)
                {
                    if (game.Player1.BankBalance > 0 && stake == 0)
                    {
                        gameResultLabel.Text = "Input not understood...";
                        return;
                    }

                    else
                    {
                        game.Player1.Bet = stake;
                        gameResultLabel.Text = String.Format("{0:C}", stake) + " STAKED...";
                        playButton.Enabled = true;
                        player1Stake.Enabled = false;
                        player1StakeTextBox.Enabled = false;
                        game.Player1.BankBalance -= stake;
                        player1BankBalance.Text = "Player Balance: " + String.Format("{0:C}", game.Player1.BankBalance);
                        player1BankBalance.Refresh();
                        game.Pot += game.Player1.Bet;
                        potLabel.Text = String.Format("{0:C}", game.Pot);
                        potLabel.Refresh();
                    }

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
            player1StakeTextBox.Text = game.Player1.BankBalance.ToString();
        }

        private void claimButton_Click(object sender, EventArgs e)
        {
            potLabel.Text = String.Format("{0:C}", 0);
            betOutputLabel.Text = "";
            game.BetManager.BetOutput.Clear();

            player1ResultLabel.Text = "";
            player2ResultLabel.Text = "";
            player3ResultLabel.Text = "";
            player4ResultLabel.Text = "";

            game.Payout(gameResultLabel);
            displayPlayerBankBalances();
            displayCardBacks();

            gameResultLabel.Text = "";
            game.ResetGame = false;
            game.EndGame = false;
            game.FirstClick = true;
            game.Flop.Clear();

            playButton.Enabled = true;
            claimButton.Enabled = false;

            if (game.Player1.BankBalance > 0)
            {
                game.Player1.Bankrupt = false;
                game.GameOver = false;
            }

            if (game.Player1.Bankrupt || game.GameOver)
            {
                gameResultLabel.Text = "GAME OVER...";
                playButton.Enabled = false;
                claimButton.Enabled = false;
                player1Stake.Enabled = false;
                player1StakeTextBox.Enabled = false;
                restartButton.Enabled = true;
            }
            player1BankBalance.Text = "Player Balance: " + String.Format("{0:C}", game.Player1.BankBalance);
            player1BankBalance.Refresh();
            game.Pot = 0;
        }

        private void restartButton_Click(object sender, EventArgs e)
        {
            restartButton.Enabled = false;
            game = new Game();

            displayPlayerBankBalances();
            displayCardBacks();
            gameResultLabel.Text = "CLICK PLAY...";
            player1Stake.Enabled = false;
            player1StakeTextBox.Enabled = false;
            playButton.Enabled = true;
            roundLabel.Text = "GAME: " + game.RoundNumber.ToString();
            betOutputLabel.Text = "";
            roundLabel.Refresh();
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

        private void displayPlayerBankBalances()
        {
            player1BankBalance.Text = "Player Balance: " + String.Format("{0:C}", game.Player1.BankBalance);
            player1BankBalance.Refresh();
            player2BankBalance.Text = "Player Balance: " + String.Format("{0:C}", game.Player2.BankBalance);
            player2BankBalance.Refresh();
            player3BankBalance.Text = "Player Balance: " + String.Format("{0:C}", game.Player3.BankBalance);
            player3BankBalance.Refresh();
            player4BankBalance.Text = "Player Balance: " + String.Format("{0:C}", game.Player4.BankBalance);
            player4BankBalance.Refresh();
        }
    }
}
