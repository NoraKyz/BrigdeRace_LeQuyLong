using System.Collections;
using System.Collections.Generic;
using _Framework.Event.Message;
using _Framework.Event.Scripts;
using _Game.Brick;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

namespace _Game.Map
{
     public class Stage : MonoBehaviour
     {
          [SerializeField] private int stageID;
          [SerializeField] private int playerCount;
          [SerializeField] private int numRows, numCols;
          [Header("==========================================")]
          [SerializeField] private Transform startPointSpawn;
          [SerializeField] private Vector3 offset;
          [SerializeField] private List<ColorType> currentColors = new List<ColorType>();
          [SerializeField] private List<Transform> listTargetPoint = new List<Transform>();
          
          private int _maxBrick;
          private bool HasPosValid => _listNoneBrickPos.Count > 0;
          private List<PlatformBrick> _listBricks = new List<PlatformBrick>();
          private List<Vector3> _listNoneBrickPos = new List<Vector3>();
          private void Start()
          {
               _maxBrick = numRows * numCols;
               
               GetAllNoneBrickPos();

               for (int i = 0; i < currentColors.Count; i++)
               {
                    SpawnFullBrickByColor(currentColors[i]);
               }
               
               RegisterEvents();
          }
          private void RegisterEvents()
          {
               this.RegisterListener(EventID.CharacterEnterStage, (message) => OnCharacterEnter(message as EnterStageMessage));
          }
          private void OnDestroy()
          {
               RemoveEvents();
          }
          private void RemoveEvents()
          {
               this.RemoveListener(EventID.CharacterEnterStage, (message) => OnCharacterEnter(message as EnterStageMessage));
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
          private ColorType GetRandomColorValid()
          {
               if (IsMaxAllBrick())
               {
                    return ColorType.None;
               }
               
               while (true)
               {
                    ColorType colorType = currentColors[Random.Range(0, currentColors.Count)];

                    if (!IsMaxBrickByColor(colorType))
                    {
                         return colorType;
                    }
               }
          }
          private void SpawnFullBrickByColor(ColorType colorType)
          {
               while(!IsMaxBrickByColor(colorType) && HasPosValid)
               {
                    SpawnBrickRandPos(colorType);
               }
          }
          private void SpawnBrickRandPos(ColorType colorType)
          {
               if (colorType == ColorType.None)
               {
                    return;
               }
               
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
          private bool IsMaxBrickByColor(ColorType colorType)
          {
               return GetAmountBrickByColor(colorType) > _maxBrick / playerCount + 1;
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
          private void OnDespawnBrickEvent(PlatformBrick brick)
          {
               _listBricks.Remove(brick);
               StartCoroutine(AfterDespawnBrick(brick));
          }
          private IEnumerator AfterDespawnBrick(PlatformBrick brick)
          {
               Vector3 pos = brick.TF.position;
               yield return new WaitForSeconds(Constants.RespawnPlatformBrickTime);
               _listNoneBrickPos.Add(pos);
          
               if(currentColors.Count != 0)
               { 
                    SpawnBrickRandPos(GetRandomColorValid());
               }
          }
          private void OnCharacterEnter(EnterStageMessage message)
          {
               Character.Character character = message.Character;
               
               if (message.StageID == stageID)
               {
                    character.SetCurrentStage(this);
                    currentColors.Add(character.ColorType);
                    SpawnFullBrickByColor(character.ColorType);
               }
               else
               {
                    currentColors.Remove(character.ColorType);
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
          public void SetCurrentColors(List<ColorType> colors)
          {
               currentColors = colors;
          }
          public Vector3 GetRandomTargetPos()
          {
               return listTargetPoint[Random.Range(0, listTargetPoint.Count)].position;
          }
     }
}
