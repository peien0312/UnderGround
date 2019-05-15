using System;
using System.Collections.Generic;
using System.Linq;

namespace UnderGround.Models
{
    public class Layer
    {
        public string Name { get; set; }
        public int Depth { get; set; }
        public List<Room> RoomList { get; set; }
    }

    public class Room
    {
        public Room()
        {
            this.IsRevealed = false;
        }
        public int Depth { get; set; }
        public int Index { get; set; }
        public List<WallLocation> WallList { get; set; }
        public string RoomName { get; set; }
        public RoomType RoomType { get; set; }
        public bool IsRevealed { get; set; }
        public Room SetLocation(int Depth, int Index)
        {
            this.Depth = Depth;
            this.Index = Index;
            return this;
        }
    }

    public enum RoomType
    {
        Surface
    }

    public enum WallLocation
    {
        Up, Down, Left, Right
    }
}