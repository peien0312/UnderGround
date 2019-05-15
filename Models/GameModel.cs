using System;
using System.Collections.Generic;
using System.Linq;

namespace UnderGround.Models
{
    public class Game
    {
        public Game()
        {
            CurrentPlayerIndex = 0;
            GameMap = GenerateMap();
            PlayerList = SetPlayer();
        }

        public List<Player> PlayerList { get; set; }

        public int CurrentPlayerIndex { get; set; }

        public List<Layer> GameMap { get; set; }

        public Room GetRoom(int Depth, int Index)
        {
            return GameMap[Depth].RoomList[Index];
        }

        private List<Player> SetPlayer()
        {
            var PlayerList = new List<Player>();

            var rnd = new Random();

            var ColorList = new List<Color>(){
                Color.Red,Color.Blue,Color.Green,Color.Yellow
            }.OrderBy(c => rnd.Next()).ToList();
            ColorList.ForEach(x => PlayerList.Add(new Player(x)));

            return PlayerList;
        }

        private List<Layer> GenerateMap()
        {
            //gen layer
            var Map = new List<Layer>();

            var AllRoomList = new List<List<string>>(){
            new List<string> { "地面" },
            new List<string> { "礦場", "礦場", "森林", "森林", "寶藏房間", "怪獸房間", "商店", "道館", "安全小屋", "傳送門" },
            new List<string> { "礦場", "礦場", "森林", "寶藏房間", "怪獸房間", "商店", "道館", "安全小屋", "傳送門", "湖泊" },
            new List<string> { "礦場", "礦場", "森林", "寶藏房間", "怪獸房間", "傳送門", "湖泊", "熔岩谷", "高手村", "祭壇" },
            new List<string> { "史詩級寶藏房間", "史詩級怪獸房間" }
            };

            var ShuffledList = new List<List<string>>();

            var rnd = new Random();
            //shuffleArray
            AllRoomList.ForEach(x => ShuffledList.Add(x.OrderBy(c => rnd.Next()).ToList()));


            for (int i = 0; i < ShuffledList.Count; i++)
            {
                var newLayer = new Layer() { Depth = Map.Count, RoomList = new List<Room>() };

                for (int j = 0; j < ShuffledList[i].Count; j++)
                {
                    newLayer.RoomList.Add(new Room() { RoomName = ShuffledList[i][j] }.SetLocation(Map.Count, newLayer.RoomList.Count));
                    if (j == 4)
                    {
                        Map.Add(newLayer);
                        newLayer = new Layer() { Depth = i * 2, RoomList = new List<Room>() };
                    }

                }
                Map.Add(newLayer);
            }
            return Map;
        }

        public void NextPlayer()
        {
            if (CurrentPlayerIndex == PlayerList.Count - 1)
            {
                CurrentPlayerIndex = 0;
            }
            else
            {
                CurrentPlayerIndex++;
            }
        }

        public List<Movement> GetMoveActions(Player CurrentPlayer)
        {
            var CurrentRoom = GetRoom(CurrentPlayer.CurrentDepth, CurrentPlayer.CurrentRoomIndex);

            var MovementList = new List<Movement>();

            var moveActionList = new List<MoveActionType>();

            moveActionList.Add(MoveActionType.Stay);

            if (CurrentRoom.Depth > 0)
            {
                moveActionList.Add(MoveActionType.Up);
            }
            if (CurrentRoom.Index > 0 && CurrentRoom.Depth != 7 && CurrentRoom.Depth != 0)
            {
                moveActionList.Add(MoveActionType.Left);
            }
            if (CurrentRoom.Index < 4 && CurrentRoom.Depth != 7 && CurrentRoom.Depth != 0)
            {
                moveActionList.Add(MoveActionType.Right);
            }
            if (CurrentRoom.Depth < 7)
            {
                moveActionList.Add(MoveActionType.Down);
            }

            moveActionList.ForEach(
                x =>
                {
                    MovementList.Add(new Movement()
                    {
                        MoveActionType = x,
                        TargetRoomList = GetTargetRoomList(x, CurrentRoom, CurrentPlayer)
                    });
                }
            );
            //has wall?
            //is full?
            //get treasure
            //portal
            //get trap
            return MovementList;
        }
        private List<Room> GetTargetRoomList(MoveActionType MoveActionType, Room CurrentRoom, Player CurrentPlayer)
        {
            var TargetRoomList = new List<Room>();
            var CurrentLayer = GameMap.Find(x => x.RoomList.Contains(CurrentRoom));
            switch (MoveActionType)
            {
                case MoveActionType.Up:
                    if (CurrentLayer.Depth == 1)
                    {
                        TargetRoomList.Add(GetRoom(0, 0));
                    }
                    else if (CurrentLayer.Depth == 7)
                    {
                        if (CurrentRoom.Index == 0)
                        {
                            TargetRoomList.Add(GetRoom(6, 0));
                            TargetRoomList.Add(GetRoom(6, 1));
                            TargetRoomList.Add(GetRoom(6, 2));
                        }
                        else
                        {
                            TargetRoomList.Add(GetRoom(6, 2));
                            TargetRoomList.Add(GetRoom(6, 3));
                            TargetRoomList.Add(GetRoom(6, 4));
                        }
                    }
                    else
                    {
                        TargetRoomList.Add(GetRoom(CurrentRoom.Depth - 1, CurrentRoom.Index));
                    }
                    break;
                case MoveActionType.Down:
                    if (CurrentLayer.Depth == 0)
                    {
                        TargetRoomList.Add(GetRoom(1, 0));
                        TargetRoomList.Add(GetRoom(1, 1));
                        TargetRoomList.Add(GetRoom(1, 2));
                        TargetRoomList.Add(GetRoom(1, 3));
                        TargetRoomList.Add(GetRoom(1, 4));
                    }
                    else if (CurrentLayer.Depth == 6)
                    {
                        if (CurrentRoom.Index <= 2)
                        {
                            TargetRoomList.Add(GetRoom(7, 0));
                        }
                        else
                        {
                            TargetRoomList.Add(GetRoom(7, 1));
                        }
                    }
                    else
                    {
                        TargetRoomList.Add(GetRoom(CurrentRoom.Depth + 1, CurrentRoom.Index));
                    }
                    break;
                case MoveActionType.Left:
                    TargetRoomList.Add(GetRoom(CurrentRoom.Depth, CurrentRoom.Index - 1));
                    break;
                case MoveActionType.Right:
                    TargetRoomList.Add(GetRoom(CurrentRoom.Depth, CurrentRoom.Index + 1));
                    break;
                case MoveActionType.Stay:
                    TargetRoomList.Add(GetRoom(CurrentRoom.Depth, CurrentRoom.Index));
                    break;
            }
            return TargetRoomList;
        }
    }
}