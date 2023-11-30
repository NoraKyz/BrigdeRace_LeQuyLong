using _Game.Character;

namespace _Framework.Event.Message
{
    public class EnterStageMessage
    {
        public int StageID { get; private set;}
        public Character Character { get; private set;}
        
        public EnterStageMessage(int stageID, Character character)
        {
            StageID = stageID;
            Character = character;
        }
    }
}