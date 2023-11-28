using System;
using System.Collections;
using System.Collections.Generic;
using _Game.Brick;
using _Game.Character;
using _Game.Framework.Event;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

public class Stage : MonoBehaviour
{
     [SerializeField] private int numRows, numCols;
     [Header("==========================================")]
     [SerializeField] private Transform startPointSpawn;
     [SerializeField] private Vector3 offset;
     
     private List<ColorType> _currentColors = new List<ColorType>();
     private int _maxBrick;
     private int _maxPlayer;
     private bool HasPosValid => _listNoneBrickPos.Count > 0;
     private List<PlatformBrick> _listBricks = new List<PlatformBrick>();
     private List<Vector3> _listNoneBrickPos = new List<Vector3>();
     
     private void Awake()
     {
          _maxBrick = numRows * numCols;
     }
     private void Start()
     {
          OnInit();
     }
     private void OnInit()
     {
          GetAllNoneBrickPos();

          foreach (ColorType colorType in _currentColors)
          {
               SpawnFullBrickByColor(colorType);
          }
     }    
     private void GetAllNoneBrickPos()
    {
        Vector3 startPos = startPointSpawn.position;
         
        for(int i = 0; i < numRows; i++)
        {
            for(int j = 0; j < numCols; j++)
            {
                Vector3 pos = new Vector3(startPos.x + offset.x * j, startPos.y, startPos.z - offset.z * i);
                _listNoneBrickPos.Add(pos);
            }
        }
    }
     private void SpawnBrickRandPos(ColorType colorType)
     {
          int index = Random.Range(0, _listNoneBrickPos.Count);
          Vector3 pos = _listNoneBrickPos[index];
          
          PlatformBrick brick = SimplePool.Spawn<PlatformBrick>(
               PoolType.PlatformBrick,
               pos,
               Quaternion.identity
          );
          brick.OnDespawnEvent += OnDespawnBrickEvent;
          brick.ChangeColor(colorType);
          
          _listBricks.Add(brick);
          _listNoneBrickPos.Remove(pos);
     }
     private ColorType GetRandomColorValid()
     {
          while (true)
          {
               ColorType colorType = _currentColors[Random.Range(0, _currentColors.Count)];

               if (!IsMaxBrickByColor(colorType))
               {
                    return colorType;
               }

               if (IsMaxAllBrick())
               {
                    return ColorType.None;
               }
          }
     }
     private int GetAmountBrickByColor(ColorType colorType)
     {
          int count = 0;
          foreach (var brick in _listBricks)
          {
               if (brick.colorType == colorType)
               {
                    count++;
               }
          }

          return count;
     }
     private bool IsMaxBrickByColor(ColorType colorType)
     {
          return GetAmountBrickByColor(colorType) > _maxBrick / _maxPlayer + 1;
     }
     private bool IsMaxAllBrick()
     {
          foreach (var colorType in _currentColors)
          {
               if (!IsMaxBrickByColor(colorType))
               {
                    return false;
               }
          }

          return true;
     }
     private void OnDespawnBrickEvent(PlatformBrick brick)
     {
          _listBricks.Remove(brick);
          brick.OnDespawnEvent -= OnDespawnBrickEvent;
          StartCoroutine(AfterDespawn(brick));
     }
     private IEnumerator AfterDespawn(PlatformBrick brick)
     {
          Vector3 pos = brick.transform.position;
          yield return new WaitForSeconds(Constants.RespawnPlatformBrickTime);
          _listNoneBrickPos.Add(pos);
          SpawnBrickRandPos(GetRandomColorValid());
     }
     private void SpawnFullBrickByColor(ColorType colorType)
     {
          while(!IsMaxBrickByColor(colorType) && HasPosValid)
          {
               SpawnBrickRandPos(colorType);
          }
     }
     private void OnCharacterOnNextStage(Character character)
     {
          SpawnFullBrickByColor(character.colorType);
     }
     public Vector3? GetBrickPosTakeAble(ColorType colorType)
     {
          List<Vector3> listPos = new List<Vector3>();
          
          for(int i = 0; i < _listBricks.Count; i++)
          {
               if (_listBricks[i].colorType == colorType)
               {
                    listPos.Add(_listBricks[i].transform.position);
               }
          }

          if (listPos.Count > 0)
          {
               int index = Random.Range(0, listPos.Count);
               return listPos[index];
          }

          return null;
     }
     public void SetListColor(List<ColorType> colors)
     {
          _currentColors = colors;
     }
     public void OnCharacterEnter(Character character)
     {
          _currentColors.Add(character.colorType);
          SpawnFullBrickByColor(character.colorType);
     }
     public void SetMaxPlayer(int maxPlayer)
     {
          _maxPlayer = maxPlayer;
     }
}
