using _Framework;
using _Framework.Event.Message;
using _Game.Framework.Event;
using UnityEngine;

namespace _Game.Map
{
    public class Gate : MonoBehaviour
    {
        [SerializeField] private int stageID;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Character"))
            {
                Character.Character character = Cache<Character.Character>.GetScript(other);

                if (character.CurrentStageId != stageID)
                {
                    character.OnEnterStage(stageID);
                    this.PostEvent(EventID.CharacterEnterStage, new EnterStageMessage(stageID, character.ColorType));
                }
            }
        }
    }
}
