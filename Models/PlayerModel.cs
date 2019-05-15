using System;
using System.Collections.Generic;

namespace UnderGround.Models
{
    public class Player
    {

        public Player(Color playerColor)
        {
            this.PlayerColor = playerColor;
            this.Health = 3;
            this.Mental = 5;
            this.Gold = 0;
            this.Score = 0;
            this.Attack = 2;
            this.MaxMeeple = 4;
            this.MaxDepth = 2;
            this.CurrentDepth = 0;
            this.CurrentRoomIndex = 0;
            this.MeepleList = new List<Meeple>() { new Meeple(), new Meeple(), new Meeple(), new Meeple() };
        }
        public Color PlayerColor { get; set; }
        public int Health { get; set; }
        public int Mental { get; set; }
        public int Gold { get; set; }
        public int Score { get; set; }
        public int Attack { get; set; }
        public int MaxMeeple { get; set; }
        public int MaxDepth { get; set; }
        public int CurrentDepth { get; set; }
        public int CurrentRoomIndex { get; set; }
        public int Treasure { get; set; }
        public List<Meeple> MeepleList { get; set; }
        public List<Items> Inventory { get; set; }

        public Equipment Equipment { get; set; }

        public void Move(List<Movement> ActionList)
        {


            Console.WriteLine("==============================================");

            Console.WriteLine();

            Console.WriteLine($"現在是{PlayerColor}的回合");

            Console.WriteLine();

            Console.WriteLine($"你在{ActionList.Find(x => x.MoveActionType == MoveActionType.Stay).TargetRoomList[0].RoomName}");

            Console.WriteLine("");

            var ActionCount = 0;
            var ActionDict = new Dictionary<string, Room>();

            var MoveDict = new Dictionary<MoveActionType, string>(){
                {MoveActionType.Up,"上"},
                {MoveActionType.Down,"下"},
                {MoveActionType.Left,"左"},
                {MoveActionType.Right,"右"},
                {MoveActionType.Stay,"原地"},
            };

            ActionList.ForEach(x =>
            {
                Console.WriteLine($"往{MoveDict[x.MoveActionType]}：");
                x.TargetRoomList.ForEach(y =>
                {
                    Console.WriteLine($"({ActionCount}){y.RoomName}");
                    ActionDict.Add(ActionCount.ToString(), y);
                    ActionCount++;
                });
                Console.WriteLine("-----------------------------------------------");
            });
            //wait input
            Room targetRoom;
            bool indexOk;
            do
            {
                Console.Write("請選擇前往的房間：");

                indexOk = ActionDict.TryGetValue(Console.ReadLine(), out targetRoom);
                if (!indexOk)
                {
                    Console.WriteLine("找不到房間！請重新輸入");
                    continue;
                }
                break;
            } while (true);
            // 
            // while (!int.TryParse(, out targetRoomIndex))
            // {
            //     Console.WriteLine("輸入錯誤！");
            //     while (!ActionDict.TryGetValue(targetRoomIndex, out var targetRoom))
            //     {
            //         targetRoomIndex = -1;
            //         Console.WriteLine("找不到房間！");
            //     }
            //     Console.WriteLine("請輸入你要移動的房間號碼：");
            //     int.TryParse(Console.ReadLine(), out targetRoomIndex);
            // }
            Console.WriteLine("///////////////////////////////////////////////");
            Console.WriteLine($"你選擇前往{targetRoom.RoomName}");
            //resolve movement action
            CurrentDepth = targetRoom.Depth;
            CurrentRoomIndex = targetRoom.Index;

        }

    }

    public class Equipment
    {

    }

    public class Items
    {
        public string Name { get; set; }
    }

    public class Meeple
    {
        public Meeple()
        {
            Used = false;
        }

        public bool Used { get; set; }

    }

    public enum Color
    {
        Red,
        Blue,
        Green,
        Yellow
    }
    public class Movement
    {
        public MoveActionType MoveActionType { get; set; }
        public List<Room> TargetRoomList { get; set; }


    }
    public enum MoveActionType
    {
        Up, Down, Left, Right, Stay
    }

}