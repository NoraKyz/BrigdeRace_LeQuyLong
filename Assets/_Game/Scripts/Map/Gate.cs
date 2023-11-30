using _Framework;
using _Framework.Event.Message;
using _Game.Character;
using _Game.Framework.Event;
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
                Character.Character character = Cache<Character.Character>.GetScript(other);

                if (character.CurrentStageId != stageID)
                {
                    character.OnEnterStage(stageID);
                    this.PostEvent(EventID.CharacterEnterStage, new EnterStageMessage(stageID, character));
                }
            }
        }
    }
}
