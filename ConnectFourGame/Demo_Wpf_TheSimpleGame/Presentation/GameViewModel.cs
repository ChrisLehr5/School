using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo_Wpf_TheSimpleGame.Models;
using Demo_Wpf_TheSimpleGame.Business;

namespace Demo_Wpf_TheSimpleGame.Presentation
{
    public class GameViewModel : ObservableObject
    {
        //private GameBusiness _gameBusiness;

        private enum GameState
        {
            PLAYER_RED,
            PLAYER_BLUE
        }

        private const string PLAYER_RED_CHIP_COLOR = "Red";
        private const string PLAER_BLUE_CHIP_COLOR = "Yellow";
        private const string EMPTY_BOARD_LOCATION_COLOR = "Gray";

        public string _currentPlayer;
        private GameState _currentGameState;
        private string[][] _currentBoard;
        private string _messageBoxContent;



        public string[][] CurrentBoard
        {
            get { return _currentBoard; }
            set
            {
                _currentBoard = value;
                OnPropertyChanged(nameof(CurrentBoard));
            }
        }

        public string CurrentPlayer
        {
            get { return _currentPlayer; }
            set
            {
                _currentPlayer = value;
                OnPropertyChanged(nameof(CurrentPlayer));
            }
        }

        public string MessageBoxContent
        {
            get { return _messageBoxContent; }
            set
            {
                _messageBoxContent = value;
                OnPropertyChanged(nameof(MessageBoxContent));
            }
        }

        public GameViewModel()
        {
            InitializeGame();
        }

        public GameViewModel(string currentPlayer)
        {
            CurrentPlayer = currentPlayer;
        }

        private void InitializeGame()
        {
            _currentGameState = GameState.PLAYER_RED;
            CurrentPlayer = GameState.PLAYER_RED.ToString();

            CurrentBoard = new string[10][];
            CurrentBoard[0] = new string[10];
            CurrentBoard[1] = new string[10];
            CurrentBoard[2] = new string[10];
            CurrentBoard[3] = new string[10];
            CurrentBoard[4] = new string[10];
            CurrentBoard[5] = new string[10];
            CurrentBoard[6] = new string[10];
            CurrentBoard[7] = new string[10];


            InitializeGameboard();
            MessageBoxContent = "Player X Moves";
        }

        /// <summary>
        /// fill the game board array with gray pieces
        /// </summary>
        public void InitializeGameboard()
        {
            //
            // Set all PlayerPiece array values to "None"
            //
            for (int row = 0; row < 7; row++)
            {
                for (int column = 0; column < 7; column++)
                {
                    CurrentBoard[row][column] = EMPTY_BOARD_LOCATION_COLOR;
                }
            }
            OnPropertyChanged(nameof(CurrentBoard));
        }

        //switch on button clicks 
        internal void GameCommand(string commandName)
        {
            switch (commandName)
            {
                case "NewGame":
                    InitializeGameboard();
                    break;

                case "ResetGame":
                    InitializeGameboard();
                    break;

                case "QuitSave":
                    //_gameBusiness.SaveAllPlayers();
                    break;

                case "Quit":
                    Close();
                    break;

                default:
                    // add code to handle exception
                    break;
            }
        }

        private void Close()
        {
            System.Environment.Exit(1);
        }

        internal void PlayerMove(int row, int column)
        {
            if (CurrentBoard[row][column] == "Gray")
            {
                if (_currentGameState == GameState.PLAYER_RED)
                {
                    CurrentBoard[row][column] = PLAYER_RED_CHIP_COLOR;
                    OnPropertyChanged(nameof(CurrentBoard));
                    _currentGameState = GameState.PLAYER_BLUE;
                    CurrentPlayer = GameState.PLAYER_BLUE.ToString();
                    MessageBoxContent = "Player O Moves";
                }
                else
                {
                    CurrentBoard[row][column] = PLAER_BLUE_CHIP_COLOR;
                    OnPropertyChanged(nameof(CurrentBoard));
                    _currentGameState = GameState.PLAYER_RED;
                    CurrentPlayer = GameState.PLAYER_RED.ToString();
                    MessageBoxContent = "Player X Moves";
                }

            }
        }

    }
}