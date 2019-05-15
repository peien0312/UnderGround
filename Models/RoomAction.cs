namespace UnderGround.Models
{
    public interface IRoomAction
    {
        string ActionName { get; }
        void Action();

        bool CanChoose { get; }
    }

    public class Heal : IRoomAction
    {
        public string ActionName => "補血";

        public bool CanChoose => true;

        public void Action()
        {
            //Heal player
            //Put meeple
        }
    }
}