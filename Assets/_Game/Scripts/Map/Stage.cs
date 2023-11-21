using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

public class Stage : MonoBehaviour
{
     // Warning: All brick in this class is platform brick
     [SerializeField] private int numRows, numCols;
     [SerializeField] private int maxPlayer;
     [SerializeField] private List<ColorType> currentColors = new List<ColorType>();
     [Header("==========================================")]
     [SerializeField] private Transform startPointSpawn;
     [SerializeField] private Vector3 offset;
     
     private int _maxBrick;
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
     
     private void SpawnBrick()
     {
          int index = Random.Range(0, _listNoneBrickPos.Count);

          ColorType colorType = GetRandomColorValid();
          
          PlatformBrick brick = SimplePool.Spawn<PlatformBrick>(
               PoolType.PlatformBrick,
               _listNoneBrickPos[index],
               Quaternion.identity
          );
          brick.OnDespawnEvent += OnDespawnBrickEvent;
          brick.ChangeColor(colorType);
          
          _listBricks.Add(brick);
          _listNoneBrickPos.RemoveAt(index);
     }
     private void SpawnFullBrickByColor(ColorType colorType)
     {
          while (!IsMaxBrickByColor(colorType) && _listNoneBrickPos.Count > 0)
          {
               SpawnBrick();
          }
     }
     private IEnumerator SpawnBrickDelayTime(float timer)
     {
          yield return new WaitForSeconds(timer);
          SpawnBrick();
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
               if (brick.ColorType == colorType)
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
          StartCoroutine(SpawnBrickDelayTime(3f));
     }
}
