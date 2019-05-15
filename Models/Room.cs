namespace UnderGround.Models
{
    public interface IRoom
    {
        string Name { get; set; }
        void RoomAction();
        string RoomDescription { get; set; }
    }

    public class Surface : IRoom
    {
        public string Name { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public string RoomDescription { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public void RoomAction()
        {
            throw new System.NotImplementedException();
        }
    }
}