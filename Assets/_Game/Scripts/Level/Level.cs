using System.Collections.Generic;
using _Game.Map;
using UnityEngine;
using Utils;

namespace _Game.Level
{
    public class Level : MonoBehaviour
    {
        private const float RangeStartPos = 25f;
    
        [SerializeField] private Transform startCharPos;
        [SerializeField] private Stage startStage;
        [SerializeField] private List<Character.Character> characters = new List<Character.Character>();
        
        private List<ColorType> _listColor = new List<ColorType>();
        private void Start()
        {
            OnInit();
        }
        private void OnInit()
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
                characters[i].TF.position = startPos + Vector3.right * (RangeStartPos / (charCount - 1) * i - RangeStartPos / 2);
                
                characters[i].SetCurrentStage(startStage);
            }
        }
    }
}
