using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject[] prefabs;

    // 풀 담당 리스트
    List<GameObject>[] pools;

    void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];

        for (int i = 0; i < pools.Length; i++)
        {
            pools[i] = new List<GameObject>();
        }
    }

    // 게임 오브젝트 반환 함수
    public GameObject Get(int i)
    {
        GameObject select = null;

        // 선택한 풀의 게임 오브젝트(비활성화) 접근
        // 발견하면 select 변수에 할당
        foreach(GameObject item in pools[i])
        {
            select = item;
            select.SetActive(true);
            break;
        }

        // 못 찾으면 새롭게 생성 후 select 변수에 할당
        if (!select)
        {
            select = Instantiate(prefabs[i], transform);
            pools[i].Add(select);
        }

        return select;
    }
}
