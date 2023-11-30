using System.Collections.Generic;
using _Game.Map;
using UnityEngine;
using Utils;

namespace _Game.Level
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private Transform startCharPos;
        [SerializeField] private float rangeStartPos;
        [SerializeField] private Stage startStage;
        [SerializeField] private List<Character.Character> characters = new List<Character.Character>();
        
        private List<ColorType> _listColor = new List<ColorType>();
        private void Start()
        {
            _listColor = GetListColor();
            startStage.SetCurrentColors(_listColor);
            SetStartCharPos();
        }
        private List<ColorType> GetListColor()
        {
            List<ColorType> listColor = new List<ColorType>();
        
            for(int i = 0; i < characters.Count; i++)
            {
                listColor.Add(characters[i].ColorType);
            }

            return listColor;
        }
        private void SetStartCharPos()
        {
            int charCount = characters.Count;
            Vector3 startPos = startCharPos.position + Vector3.up;
        
            for(int i = 0; i < characters.Count; i++)
            {
                characters[i].TF.position = startPos + Vector3.right * (rangeStartPos / (charCount - 1) * i - rangeStartPos / 2);
                
                characters[i].SetCurrentStage(startStage);
            }
        }
    }
}
