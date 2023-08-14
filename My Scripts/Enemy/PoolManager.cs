using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject[] prefabs;

    // Ǯ ��� ����Ʈ
    List<GameObject>[] pools;

    void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];

        for (int i = 0; i < pools.Length; i++)
        {
            pools[i] = new List<GameObject>();
        }
    }

    // ���� ������Ʈ ��ȯ �Լ�
    public GameObject Get(int i)
    {
        GameObject select = null;

        // ������ Ǯ�� ���� ������Ʈ(��Ȱ��ȭ) ����
        // �߰��ϸ� select ������ �Ҵ�
        foreach(GameObject item in pools[i])
        {
            select = item;
            select.SetActive(true);
            break;
        }

        // �� ã���� ���Ӱ� ���� �� select ������ �Ҵ�
        if (!select)
        {
            select = Instantiate(prefabs[i], transform);
            pools[i].Add(select);
        }

        return select;
    }
}
