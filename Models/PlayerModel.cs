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

            Console.WriteLine($"�{�b�O{PlayerColor}���^�X");

            Console.WriteLine();

            Console.WriteLine($"�A�b{ActionList.Find(x => x.MoveActionType == MoveActionType.Stay).TargetRoomList[0].RoomName}");

            Console.WriteLine("");

            var ActionCount = 0;
            var ActionDict = new Dictionary<string, Room>();

            var MoveDict = new Dictionary<MoveActionType, string>(){
                {MoveActionType.Up,"�W"},
                {MoveActionType.Down,"�U"},
                {MoveActionType.Left,"��"},
                {MoveActionType.Right,"�k"},
                {MoveActionType.Stay,"��a"},
            };

            ActionList.ForEach(x =>
            {
                Console.WriteLine($"��{MoveDict[x.MoveActionType]}�G");
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
                Console.Write("�п�ܫe�����ж��G");

                indexOk = ActionDict.TryGetValue(Console.ReadLine(), out targetRoom);
                if (!indexOk)
                {
                    Console.WriteLine("�䤣��ж��I�Э��s��J");
                    continue;
                }
                break;
            } while (true);
            // 
            // while (!int.TryParse(, out targetRoomIndex))
            // {
            //     Console.WriteLine("��J���~�I");
            //     while (!ActionDict.TryGetValue(targetRoomIndex, out var targetRoom))
            //     {
            //         targetRoomIndex = -1;
            //         Console.WriteLine("�䤣��ж��I");
            //     }
            //     Console.WriteLine("�п�J�A�n���ʪ��ж����X�G");
            //     int.TryParse(Console.ReadLine(), out targetRoomIndex);
            // }
            Console.WriteLine("///////////////////////////////////////////////");
            Console.WriteLine($"�A��ܫe��{targetRoom.RoomName}");
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