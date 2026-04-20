using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleObjectPool : MonoBehaviour
{
    public GameObject obstacleBarrelPrefab;
    public GameObject obstacleBarrierPrefab;
    public GameObject obstacleStoneWallPrefab;
    public int poolSize = 10;

    private List<GameObject> obstacleBarrelPool;
    private List<GameObject> obstacleBarrierPool;
    private List<GameObject> obstacleStoneWallPool;

    public static ObstacleObjectPool staticInstance;
    void Awake()
    {
        obstacleBarrelPool = new List<GameObject>();
        obstacleBarrierPool = new List<GameObject>();
        obstacleStoneWallPool = new List<GameObject>();

        staticInstance = this;
    }

    private IEnumerator Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            CreateNewBarrel();
            CreateNewBarrier();
            CreateNewWall();


            if (i % 10 == 0)
            {
                yield return null;
            }
        }

    }


    private void CreateNewBarrel()
    {
        var goBarrel = Instantiate(obstacleBarrelPrefab);
        goBarrel.SetActive(false);
        obstacleBarrelPool.Add(goBarrel);
    }
    private void CreateNewBarrier()
    {
        var goBarrier = Instantiate(obstacleBarrierPrefab);
        goBarrier.SetActive(false);
        obstacleBarrierPool.Add(goBarrier);
    }

    private void CreateNewWall()
    {
        var goWall = Instantiate(obstacleStoneWallPrefab);
        goWall.SetActive(false);
        obstacleStoneWallPool.Add(goWall);
    }
 
    public GameObject Acquire(int obstacleType)
    {
        if (obstacleType == 0)
        {
            if (obstacleBarrelPool.Count <= 0)
            {
                CreateNewBarrel();
            }
            var goBarrel = obstacleBarrelPool[0];
            obstacleBarrelPool.RemoveAt(0);
            goBarrel.SetActive(true);
            return goBarrel;

        }
        else if (obstacleType == 1)
        {
            if (obstacleBarrierPool.Count <= 0)
            {
                CreateNewBarrier();
            }
            var goBarrier = obstacleBarrierPool[0];
            obstacleBarrierPool.RemoveAt(0);
            goBarrier.SetActive(true);
            return goBarrier;
        }
        else if (obstacleType == 2)
        {
            if (obstacleStoneWallPool.Count <= 0)
            {
                CreateNewWall();
            }
            var goWall = obstacleStoneWallPool[0];
            obstacleStoneWallPool.RemoveAt(0);
            goWall.SetActive(true);
            return goWall;
        }

        return null;
    }

    public void Release(GameObject obstacle, int obstacleType)
    {
        if (obstacleType == 0)
        {
            obstacleBarrelPool.Add(obstacle);
            obstacle.SetActive(false);
        }
        else if (obstacleType == 1) 
        {
            obstacleBarrierPool.Add(obstacle);
            obstacle.SetActive(false);
        }
        else if (obstacleType == 2)
        {
            obstacleStoneWallPool.Add(obstacle);
            obstacle.SetActive(false);
        }
    }
}
