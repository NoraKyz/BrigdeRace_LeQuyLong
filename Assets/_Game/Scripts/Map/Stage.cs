using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

public class Stage : MonoBehaviour
{
     [SerializeField] private int numRows, numCols;
     [SerializeField] private int maxPlayer;
     [SerializeField] private List<ColorType> currentColors = new List<ColorType>();
     [Header("==========================================")]
     [SerializeField] private Transform startPointSpawn;
     [SerializeField] private Vector3 offset;
     
     private int _maxBrick;
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

          foreach (ColorType colorType in currentColors)
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
          return GetAmountBrickByColor(colorType) > _maxBrick / maxPlayer + 1;
     }
     private bool IsMaxAllBrick()
     {
          foreach (var colorType in currentColors)
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
          yield return new WaitForSeconds(Constants.RespawnPlatformBrickTime);
          _listNoneBrickPos.Add(brick.transform.position);
          SpawnBrickRandPos(GetRandomColorValid());
     }
     private void SpawnFullBrickByColor(ColorType colorType)
     {
          while(!IsMaxBrickByColor(colorType) && HasPosValid)
          {
               SpawnBrickRandPos(colorType);
          }
     }
     public List<Vector3> GetListPosBrickTakeable(ColorType colorType)
     {
          List<Vector3> listPos = new List<Vector3>();
          
          foreach (var brick in _listBricks)
          {
               if (brick.colorType == colorType)
               {
                    listPos.Add(brick.transform.position);
               }
          }

          return listPos;
     }
}
