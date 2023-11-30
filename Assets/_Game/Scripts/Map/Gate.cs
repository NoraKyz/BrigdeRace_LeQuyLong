using _Framework;
using _Framework.Event.Message;
using _Framework.Event.Scripts;
using _Game.Utils;
using UnityEngine;

namespace _Game.Map
{
    public class Gate : MonoBehaviour
    {
        [SerializeField] private int stageID;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(TagName.Character))
            {
                Character.Character character = Cache<Character.Character>.GetComponent(other);

                if (character.CurrentStageId != stageID)
                {
                    this.PostEvent(EventID.CharacterEnterStage, new EnterStageMessage(stageID, character));
                    character.SetCurrentStageID(stageID);
                }
            }
        }
    }
}
