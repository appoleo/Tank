using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreation : MonoBehaviour
{

    // 初始化地图所需的物体数组，0-老家，1-墙，2-障碍，3-出生效果，4-河流，5-草，6-空气墙
    public GameObject[] item;


    private void Awake()
    {
        // 实例化老家
        createItem(item[0], new Vector3(0, -8, 0), Quaternion.identity);
        // 用墙把老家围起来
        createItem(item[1], new Vector3(-1, -8, 0), Quaternion.identity);
        createItem(item[1], new Vector3(1, -8, 0), Quaternion.identity);
        for (int i = -1; i <= 1; i++)
        {
            createItem(item[1], new Vector3(i, -7, 0), Quaternion.identity);
        }
    }

    private void createItem(GameObject gameItem, Vector3 vector3, Quaternion quaternion)
    {
        GameObject item = Instantiate(gameItem, vector3, quaternion);
        item.transform.SetParent(gameObject.transform);
    }
}
