using Demo_Wpf_TheSimpleGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_Wpf_TheSimpleGame.Data
{
    public static class DataService
    {
        //static class lives in the heap // calls class directly with method 
        public static (Player playerOne, Player playerTwo) GetPlayerInfo()
        {
            (Player playerOne, Player playerTwo) players;

            players.playerOne = new Player()
            {
                Name = "Sally",
                Wins = 5,
                Losses = 2,
                Ties = 1
            };

            players.playerTwo = new Player()
            {
                Name = "Bill",
                Wins = 3,
                Losses = 7,
                Ties = 2
            };

            return players;
        }
    }
}
