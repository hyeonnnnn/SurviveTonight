using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Location : MonoBehaviour
{
    // 프리팹 변수
    [SerializeField] GameObject goPrefab = null;

    // 몬스터 위치를 담을 리스트
    List<Transform> objectList = new List<Transform>();

    // Hp바 리스트
    List<GameObject> locationList = new List<GameObject>();

    Camera cam = null;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;

        AddLocation();
    }

    // Update is called once per frame
    void Update()
    {
        ChaseMonster();
    }

    public void AddLocation()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Enemy");

        for (int i = 0; i < objects.Length; i++)
        {
            objectList.Add(objects[i].transform);
            GameObject location = Instantiate(goPrefab, objects[i].transform.position, Quaternion.identity, transform);
            locationList.Add(location);
        }
    }

    void ChaseMonster()
    {
        for (int i = 0; i < objectList.Count; i++)
        {
            if (objectList[i] != null)
            {
                Vector3 locationPos = objectList[i].position + new Vector3(0, 3.2f, 0);
                Vector3 screenPos = cam.WorldToScreenPoint(locationPos);

                if (screenPos.z < 0)
                {
                    screenPos *= -1f;
                    screenPos.z = 0;
                }

                locationList[i].transform.position = screenPos;

                float maxIconSize = 4.0f; // 아이콘의 최대 크기

                // 카메라와 아이콘의 Z축 거리 차이
                float zDistance = Mathf.Abs(cam.transform.position.z - objectList[i].position.z);

                // 거리 차이가 작아질수록 아이콘의 크기 커짐
                float iconSize = Mathf.Lerp(1.0f / maxIconSize, 2.0f, maxIconSize / zDistance);
                locationList[i].transform.localScale = new Vector3(iconSize, iconSize, 1f);
            }

            else
            {
                Destroy(locationList[i]);
                locationList.RemoveAt(i);
                objectList.RemoveAt(i);
                i--;
            }
        }
    }

    public void RemoveLocation(Transform monsterTransform)
    {
        int index = objectList.IndexOf(monsterTransform);
        if (index != -1)
        {
            Destroy(locationList[index]);
            locationList.RemoveAt(index);
            objectList.RemoveAt(index);
        }
    }
}