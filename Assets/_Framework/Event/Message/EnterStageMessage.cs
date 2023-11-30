using Utils;

namespace _Framework.Event.Message
{
    public class EnterStageMessage
    {
        public int StageID { get; private set;}
        public ColorType ColorType { get; private set;}
        
        public EnterStageMessage(int stageID, ColorType colorType)
        {
            StageID = stageID;
            ColorType = colorType;
        }
    }
}