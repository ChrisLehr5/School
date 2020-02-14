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
        //test code under this line 
        
        //working code
        private Gameboard _gameboard;
        private Player _playerX;
        private Player _playerO;
        private string _messageBoxContent;


        public Gameboard Gameboard
        {
            get { return _gameboard; }
            set
            {
                _gameboard = value;
                OnPropertyChanged(nameof(Gameboard));
            }
        }

        public Player PlayerX
        {
            get { return _playerX; }
            set
            {
                _playerX = value;
                OnPropertyChanged(nameof(PlayerX));
            }
        }

        public Player PlayerO
        {
            get { return _playerO; }
            set
            {
                _playerO = value;
                OnPropertyChanged(nameof(PlayerO));
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

        public GameViewModel((Player playerOne, Player playerTwo) players)
        {
            _playerX = players.playerOne;
            _playerO = players.playerTwo;

            InitializeGame();
        }

        private void InitializeGame()
        {
            _gameboard = new Gameboard();

            _gameboard.CurrentRoundState = Gameboard.GameboardState.PlayerXTurn;

            MessageBoxContent = "Player X Moves";
        }

        public void PlayerMove(int row, int column)
        {
            if (_gameboard.GameboardPositionAvailable(new GameboardPosition(row, column)))
            {
                if (_gameboard.CurrentRoundState == Gameboard.GameboardState.PlayerXTurn)
                {
                    Gameboard.CurrentBoard[row][column] = Gameboard.PLAYER_PIECE_X;
                    OnPropertyChanged(nameof(Gameboard));
                    _gameboard.CurrentRoundState = Gameboard.GameboardState.PlayerOTurn;
                    MessageBoxContent = "Player O Moves";
                }
                else
                {
                    Gameboard.CurrentBoard[row][column] = Gameboard.PLAYER_PIECE_O;
                    OnPropertyChanged(nameof(Gameboard));
                    _gameboard.CurrentRoundState = Gameboard.GameboardState.PlayerXTurn;
                    MessageBoxContent = "Player X Moves";
                }
                UpdateCurrentRoundState();
            }
        }

        internal void GameCommand(string commandName)
        {
            switch (commandName)
            {
                case "NewGame":
                    _gameboard.InitializeGameboard();
                    OnPropertyChanged(nameof(Gameboard));

                    _gameboard.CurrentRoundState = Gameboard.GameboardState.PlayerXTurn;
                    break;

                case "ResetGame":
                    _gameboard.InitializeGameboard();
                    OnPropertyChanged(nameof(Gameboard));

                    _gameboard.CurrentRoundState = Gameboard.GameboardState.PlayerXTurn;
                    break;

                case "QuitSave":
                    // add code to save game info and quit
                    break;

                case "Quit":
                    // add code to quit
                    break;

                default:
                    // add code to handle exception
                    break;
            }
        }
        
        
        public void UpdateCurrentRoundState()
        {
            _gameboard.UpdateGameboardState();
            if (_gameboard.CurrentRoundState == Gameboard.GameboardState.CatsGame)
            {
                PlayerO.Ties++;
                PlayerX.Ties++;
                MessageBoxContent = "Tie!";
                //need to find a way to pause before launching the reset 
                _gameboard.InitializeGameboard();
                OnPropertyChanged(nameof(Gameboard));
            }
            else if (_gameboard.CurrentRoundState == Gameboard.GameboardState.PlayerXWin)
            {
                PlayerX.Wins++;
                PlayerO.Losses++;
                MessageBoxContent = "Player X Wins!";
                //need to find a way to pause before launching the reset 
                _gameboard.InitializeGameboard();
                OnPropertyChanged(nameof(Gameboard));
            }
            else if (_gameboard.CurrentRoundState == Gameboard.GameboardState.PlayerOWin)
            {
                PlayerO.Wins++;
                PlayerX.Losses++;
                MessageBoxContent = "Player O Wins!";
                //need to find a way to pause before launching the reset 
                _gameboard.InitializeGameboard();
                OnPropertyChanged(nameof(Gameboard));
            }
            
        }

    }
}
