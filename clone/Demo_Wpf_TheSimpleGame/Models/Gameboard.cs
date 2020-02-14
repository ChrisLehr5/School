using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Demo_Wpf_TheSimpleGame.Presentation;
using System.Windows.Input;
using System.Windows.Media;


//using TicTacToe.ConsoleApp.Model;

namespace Demo_Wpf_TheSimpleGame.Models
{
    public class Gameboard : ObservableObject
    {
        #region ENUMS

        public const string PLAYER_PIECE_X = "X";
        public const string PLAYER_PIECE_O = "O";
        public const string PLAYER_PIECE_NONE = "";


        public enum GameboardState
        {
            NewRound,
            PlayerXTurn,
            PlayerOTurn,
            PlayerXWin,
            PlayerOWin,
            CatsGame
        }

        #endregion

        #region FIELDS

        private const int MAX_NUM_OF_ROWS_COLUMNS = 6;

        private string[][] _currentBoard;

        #endregion

        #region PROPERTIES

        public int MaxNumOfRowsColumns
        {
            get { return MAX_NUM_OF_ROWS_COLUMNS; }
        }

        public string[][] CurrentBoard
        {
            get { return _currentBoard; }
            set
            {
                _currentBoard = value;
                OnPropertyChanged(nameof(CurrentBoard));
            }
        }

        public GameboardState CurrentRoundState { get; set; }
        #endregion

        #region CONSTRUCTORS

        public Gameboard()
        {
            CurrentBoard = new string[6][];
            CurrentBoard[0] = new string[6];
            CurrentBoard[1] = new string[6];
            CurrentBoard[2] = new string[6];
            CurrentBoard[3] = new string[6];
            CurrentBoard[4] = new string[6];
            CurrentBoard[5] = new string[6];


            InitializeGameboard();
        }

        #endregion

        #region METHODS      
        

        /// <summary>
        /// fill the game board array with "None" enum values
        /// </summary>
        public void InitializeGameboard()
        {
            CurrentRoundState = GameboardState.NewRound;

            //
            // Set all PlayerPiece array values to "None"
            //
            for (int row = 0; row < MAX_NUM_OF_ROWS_COLUMNS; row++)
            {
                for (int column = 0; column < MAX_NUM_OF_ROWS_COLUMNS; column++)
                {
                    CurrentBoard[row][column] = PLAYER_PIECE_NONE;
                }
            }
        }


        /// <summary>
        /// Determine if the game board position is taken
        /// </summary>
        /// <param name="gameboardPosition"></param>
        /// <returns>true if position is open</returns>
        public bool GameboardPositionAvailable(GameboardPosition gameboardPosition)
        {
            //
            // Confirm that the board position is empty
            // Note: gameboardPosition converted to array index by subtracting 1
            //

            if (CurrentBoard[gameboardPosition.Row][gameboardPosition.Column] == PLAYER_PIECE_NONE)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
       

        /// <summary>
        /// Update the game board state if a player wins or a cat's game happens.
        /// </summary>
        public void UpdateGameboardState()
        {
            if (ThreeInARow(PLAYER_PIECE_X))
            {
                CurrentRoundState = GameboardState.PlayerXWin;
            }
            //
            // A player O has won
            //
            else if (ThreeInARow(PLAYER_PIECE_O))
            {
                CurrentRoundState = GameboardState.PlayerOWin;
            }
            //
            // All positions filled
            //
            else if (IsCatsGame())
            {
                CurrentRoundState = GameboardState.CatsGame;
            }
        }
        
        public bool IsCatsGame()
        {
            //
            // All positions on board are filled and no winner
            //
            for (int row = 0; row < 6; row++)
            {
                for (int column = 0; column < 6; column++)
                {
                    if (CurrentBoard[row][column] == PLAYER_PIECE_NONE)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Check for any three in a row.
        /// </summary>
        /// <param name="playerPieceToCheck">Player's game piece to check</param>
        /// <returns>true if a player has won</returns>
        private bool ThreeInARow(string playerPieceToCheck)
        {
            //
            // Check rows for player win
            //
            for (int row = 0; row < 6; row++)
            {
                if (CurrentBoard[row][0] == playerPieceToCheck &&
                    CurrentBoard[row][1] == playerPieceToCheck &&
                    CurrentBoard[row][2] == playerPieceToCheck &&
                    CurrentBoard[row][3] == playerPieceToCheck &&
                    CurrentBoard[row][4] == playerPieceToCheck &&                                      
                    CurrentBoard[row][5] == playerPieceToCheck)
                {
                    return true;
                }
            }

            //
            // Check columns for player win
            //
            for (int column = 0; column < 6; column++)
            {
                if (CurrentBoard[0][column] == playerPieceToCheck &&
                    CurrentBoard[1][column] == playerPieceToCheck &&
                    CurrentBoard[2][column] == playerPieceToCheck &&
                    CurrentBoard[3][column] == playerPieceToCheck &&
                    CurrentBoard[4][column] == playerPieceToCheck &&                                     
                    CurrentBoard[5][column] == playerPieceToCheck)
                {
                    return true;
                }
            }

            //
            // Check diagonals for player win
            //
            if (
                (CurrentBoard[0][0] == playerPieceToCheck &&
                 CurrentBoard[1][1] == playerPieceToCheck &&
                 CurrentBoard[2][2] == playerPieceToCheck &&
                 CurrentBoard[3][3] == playerPieceToCheck &&
                 CurrentBoard[4][4] == playerPieceToCheck &&
                 CurrentBoard[5][5] == playerPieceToCheck &&                 
                 CurrentBoard[6][6] == playerPieceToCheck
                 )
                ||
                (CurrentBoard[0][2] == playerPieceToCheck &&
                 CurrentBoard[1][1] == playerPieceToCheck &&
                 CurrentBoard[2][2] == playerPieceToCheck &&
                 CurrentBoard[3][3] == playerPieceToCheck &&
                 CurrentBoard[4][4] == playerPieceToCheck &&
                 CurrentBoard[5][5] == playerPieceToCheck &&                 
                 CurrentBoard[6][0] == playerPieceToCheck)
                )
            {
                return true;
            }

            //
            // No Player Has Won
            //

            return false;
        }

        //testing for string color convert
        //https://code.4noobz.net/c-enum-color-converter-ivalueconverter/
        //uses Enum values to assign color
        //PlayerXTurn,
        //PlayerOTurn,

        //   public void ColorConverter ()
        //
        //if GameboardState is NewRound color needs to equal grey-- default?
        //switch()
        //case PlayerXTurn:
        //color= new SolidColorBrush(Colors.Red);
        //break;
        //case PlayerOTurn:
        //color = new SolidColorBrush(Colors.Yellow);
        //break
        //default:
        //color= new SolidColorBrush(Colors.Gray);
        //break;

        //this won't target the fill 


        /// <summary>
        /// Add player's move to the game board.
        /// </summary>
        /// <param name="gameboardPosition"></param>
        /// <param name="PlayerPiece"></param>
        public void SetPlayerPiece(GameboardPosition gameboardPosition, string PlayerPiece)
        {
            //
            // Row and column value adjusted to match array structure
            // Note: gameboardPosition converted to array index by subtracting 1
            //
            CurrentBoard[gameboardPosition.Row][gameboardPosition.Column] = PlayerPiece;

            //
            // Change game board state to next player
            //
            SetNextPlayer();
        }

        /// <summary>
        /// Switch the game board state to the next player.
        /// </summary>
        private void SetNextPlayer()
        {
            if (CurrentRoundState == GameboardState.PlayerXTurn)
            {
                CurrentRoundState = GameboardState.PlayerOTurn;
            }
            else
            {
                CurrentRoundState = GameboardState.PlayerXTurn;
            }
        }

        #endregion
    }
}

