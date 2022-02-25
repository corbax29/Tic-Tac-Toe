
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Tic_Tac_Toe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Private Members

        /// <summary>
        /// Holds the current results in the active game
        /// </summary>
        private MarkType[] mResults;

        /// <summary>
        /// True if it is player 1's turn (X) or player 2's turn (O)
        /// </summary>
        private bool mPlayer1Turn;

        /// <summary>
        /// True if game has ended
        /// </summary>
        private bool mGameEnded;

        #endregion
        #region Constructor

        public MainWindow()
        {
            InitializeComponent();

            NewGame(); 
        }

        #endregion
        /// <summary>
        /// starts a new game and clears all the values back to the start
        /// </summary>
        private void NewGame()
        {
            //Create a new blank array od free cells
            mResults = new MarkType[9];

            for(var i = 0; i < mResults.Length; i++)
                mResults[i] = MarkType.Free;

                //make sure player 1 starts the game
                mPlayer1Turn = true;

                //interate every button on the grind
                Container.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    //change background, foreground and contet to default values
                    button.Content = string.Empty;
                    button.Background = Brushes.White;
                    button.Foreground = Brushes.Blue;
                });

                //make sure the game hasn't finished
                mGameEnded = false;
        }

        /// <summary>
        /// handles a button click event
        /// </summary>
        /// <param name="sender">The button was clicked</param>
        /// <param name="e">The events od the click</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //start new game on click after it finised
            if(mGameEnded)
            {
                NewGame();
                return;
            }

            //cast the sender to a button
            var button = (Button)sender;

            //find the button position in the array
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            var index = column + (row * 3);

            //dont do anything if cell already has a value in it
            if (mResults[index] != MarkType.Free)
                return;

            //set the cell value based on witch players turn it is
            mResults[index] = mPlayer1Turn ? MarkType.Cross : MarkType.Nought;

            //set button text to the result
            button.Content = mPlayer1Turn ? "X" : "O";

            //change noughts to red
            if (!mPlayer1Turn)
                button.Foreground = Brushes.Red;

            //toggle the players turns
            mPlayer1Turn ^= true;

            //check for the winner
            CheckForWinner();
        }

        /// <summary>
        /// checks if there is a winner of 3 line straight
        /// </summary>
        private void CheckForWinner()
        {
            #region Horzontal wins

            //check for horizontal wins
            //
            //row 0
            //
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[1] & mResults[2]) == mResults[0])
            {
                //game ends
                mGameEnded = true;

                //highlight winning cells in green
                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Green;
            }
            //
            //row 1
            //
            if (mResults[3] != MarkType.Free && (mResults[3] & mResults[4] & mResults[5]) == mResults[3])
            {
                //game ends
                mGameEnded = true;

                //highlight winning cells in green
                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Green;
            }
            //
            //row 2
            //
            if (mResults[6] != MarkType.Free && (mResults[6] & mResults[7] & mResults[8]) == mResults[6])
            {
                //game ends
                mGameEnded = true;

                //highlight winning cells in green
                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Green;
            }
            #endregion

            #region Vertical wins
            //check for vertical wins
            //
            //column 0
            //
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[3] & mResults[6]) == mResults[0])
            {
                //game ends
                mGameEnded = true;

                //highlight winning cells in green
                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.Green;
            }
            //
            //column 1
            //
            if (mResults[1] != MarkType.Free && (mResults[1] & mResults[4] & mResults[7]) == mResults[1])
            {
                //game ends
                mGameEnded = true;

                //highlight winning cells in green
                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.Green;
            }
            //
            //column 2
            //
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[5] & mResults[8]) == mResults[2])
            {
                //game ends
                mGameEnded = true;

                //highlight winning cells in green
                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.Green;
            }
            #endregion

            #region Diagonal wins
            //check for diagonal wins
            //
            //diagonal 0
            //
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[4] & mResults[8]) == mResults[0])
            {
                //game ends
                mGameEnded = true;

                //highlight winning cells in green
                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Green;
            }
            //
            //diagonal 1
            //
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[4] & mResults[6]) == mResults[2])
            {
                //game ends
                mGameEnded = true;

                //highlight winning cells in green
                Button2_0.Background = Button1_1.Background = Button0_2.Background = Brushes.Green;
            }
            #endregion

            #region No winner
            //check for no winner and full board
            if (!mResults.Any(f => f == MarkType.Free))
            {
                //game ended
                mGameEnded = true;

                //turn all cells orange
                Container.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    button.Background = Brushes.Orange;
                });
            }
            #endregion


        }
    }
}
