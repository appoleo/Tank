using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreation : MonoBehaviour
{

    // 初始化地图所需的物体数组，0-老家，1-墙，2-障碍，3-出生效果，4-河流，5-草，6-空气墙
    public GameObject[] item;
    // 地图上所有可初始化地图的点位
    private Vector3[] vectorArray;
    // 敌人产生的点位
    private Vector3[] enemyPositionArray = { new Vector3(-13, 8, 0), new Vector3(-7, 8, 0), new Vector3(0, 8, 0), new Vector3(7, 8, 0), new Vector3(13, 8, 0) };

    private void Awake()
    {
        // 实例化老家
        InitHome();
        // 空气墙
        InitAirBarrier();
        // 把地图上可以放置地形的位置放置在集合中
        InitVectorArray();
        // 随机初始化地图
        InitMapRandom();
        // 初始化敌人和玩家
        InitEnemy();
        InitPlayer();
        // 第一次4秒之后产生一个敌人，之后每隔5秒产生一个
        InvokeRepeating("InitOneEnemy", 4, 5);
    }

    private void InitHome()
    {
        CreateItem(item[0], new Vector3(0, -8, 0), Quaternion.identity);
        // 用墙把老家围起来
        CreateItem(item[1], new Vector3(-1, -8, 0), Quaternion.identity);
        CreateItem(item[1], new Vector3(1, -8, 0), Quaternion.identity);
        for (int i = -1; i <= 1; i++)
        {
            CreateItem(item[1], new Vector3(i, -7, 0), Quaternion.identity);
        }
    }

    private void InitPlayer()
    {
        GameObject player = CreateItem(item[3], new Vector3(-2, -8, 0), Quaternion.identity);
        player.GetComponent<Born>().createPlayer = true;
    }

    private void InitOneEnemy()
    {
        CreateItem(item[3], enemyPositionArray[Random.Range(0, 5)], Quaternion.identity);
    }

    private void InitEnemy()
    {
        foreach (var vector in enemyPositionArray)
        {
            CreateItem(item[3], vector, Quaternion.identity);
        }
    }

    private void InitMapRandom()
    {
        for (int i = 0; i < vectorArray.Length; i++)
        {
            // 墙 障碍 河流 草   9:2:3:4
            int num = Random.Range(0, 18);
            int index = num >= 16 ? 2 : num >= 13 ? 4 : num >= 9 ? 5 : 1;
            // 洗牌
            int r = Random.Range(i, vectorArray.Length);
            if (Random.Range(0, 8) == 0)
            {
                vectorArray[r] = vectorArray[i];
                continue;
            }
            CreateItem(item[index], vectorArray[r], Quaternion.identity);
            vectorArray[r] = vectorArray[i];
        }

    }

    private void InitVectorArray()
    {
        List<Vector3> vectorList = new List<Vector3>();
        for (int i = -12; i <= 12; i++)
        {
            for (int j = -5; j <= 7; j++)
            {
                vectorList.Add(new Vector3(i, j, 0));
            }
            for (int j = -7; j < -5; j++)
            {
                if (i >= -2 && i <= 2)
                {
                    continue;
                }
                vectorList.Add(new Vector3(i, j, 0));
            }
        }
        vectorArray = vectorList.ToArray();
    }

    private GameObject CreateItem(GameObject gameItem, Vector3 vector3, Quaternion quaternion)
    {
        GameObject item = Instantiate(gameItem, vector3, quaternion);
        item.transform.SetParent(gameObject.transform);
        return item;
    }

    private void InitAirBarrier()
    {
        // 上/下
        for (int i = -14; i <= 14; i++)
        {
            CreateItem(item[6], new Vector3(i, 9, 0), Quaternion.identity);
            CreateItem(item[6], new Vector3(i, -9, 0), Quaternion.identity);
        }
        // 左/右
        for (int i = -8; i <= 8; i++)
        {
            CreateItem(item[6], new Vector3(-14, i, 0), Quaternion.identity);
            CreateItem(item[6], new Vector3(14, i, 0), Quaternion.identity);
        }
    }
}
