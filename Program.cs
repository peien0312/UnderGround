using System;
using System.Collections.Generic;
using System.Linq;
using UnderGround.Models;

namespace UnderGround
{
    class Program
    {
        static void Main(string[] args)
        {
            //init game
            var GameObject = new Game();

            //start game
            //step 1 move phase
            do
            {
                var CurrentPlayer = GameObject.PlayerList[GameObject.CurrentPlayerIndex];

                var MoveActions = GameObject.GetMoveActions(CurrentPlayer);

                CurrentPlayer.Move(MoveActions);

                GameObject.NextPlayer();
            }
            //loop until all player deployed meeple
            while (!GameObject.PlayerList.All(player => player.MeepleList.All(me => me.Used)));


            //step 2 resolve meeple action
            //Resolve();

            //step 3 end turn
            //EndTurn();
        }
    }
}
