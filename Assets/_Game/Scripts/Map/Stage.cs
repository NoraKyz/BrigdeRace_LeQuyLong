using System;
using System.Collections;
using System.Collections.Generic;
using _Framework.Event.Message;
using _Game.Brick;
using _Game.Character;
using _Game.Framework.Event;
using UnityEngine;
using UnityEngine.Serialization;
using Utils;
using Random = UnityEngine.Random;

public class Stage : MonoBehaviour
{
     [SerializeField] private int stageID;
     [SerializeField] private int numRows, numCols;
     [Header("==========================================")]
     [SerializeField] private Transform startPointSpawn;
     [SerializeField] private Vector3 offset;
     [SerializeField] private List<ColorType> currentColors = new List<ColorType>();
     
     private int _maxBrick;
     private int _playerCount = 4; // TEST
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

          for (int i = 0; i < currentColors.Count; i++)
          {
               SpawnFullBrickByColor(currentColors[i]);
          }
          
          InitEvent();
     }   
     private void InitEvent()
     {
          this.RegisterListener(EventID.CharacterEnterStage, (message) => OnCharacterEnter(message as EnterStageMessage));
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
          Vector3 pos = _listNoneBrickPos[Random.Range(0, _listNoneBrickPos.Count)];
          
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
               ColorType colorType = currentColors[Random.Range(0, currentColors.Count)];

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
          for(int i = 0; i < _listBricks.Count; i++)
          {
               if (_listBricks[i].ColorType == colorType)
               {
                    count++;
               }
          }

          return count;
     }
     private bool IsMaxBrickByColor(ColorType colorType)
     {
          return GetAmountBrickByColor(colorType) > _maxBrick / _playerCount + 1;
     }
     private bool IsMaxAllBrick()
     {
          for (int i = 0; i < currentColors.Count; i++)
          {
               if (!IsMaxBrickByColor(currentColors[i]))
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
          StartCoroutine(AfterDespawnBrick(brick));
     }
     private IEnumerator AfterDespawnBrick(PlatformBrick brick)
     {
          Vector3 pos = brick.TF.position;
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
     private void OnCharacterEnter(EnterStageMessage message)
     {
          if (message.StageID == stageID)
          {
               currentColors.Add(message.ColorType);
               SpawnFullBrickByColor(message.ColorType);
          }
     }
     public Vector3? GetBrickPosTakeAble(ColorType colorType)
     {
          List<Vector3> listPos = new List<Vector3>();
          
          for(int i = 0; i < _listBricks.Count; i++)
          {
               if (_listBricks[i].ColorType == colorType)
               {
                    listPos.Add(_listBricks[i].TF.position);
               }
          }

          if (listPos.Count > 0)
          {
               return listPos[Random.Range(0, listPos.Count)];
          }

          return null;
     }
}
