using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Location : MonoBehaviour
{
    // ������ ����
    [SerializeField] GameObject goPrefab = null;

    // ���� ��ġ�� ���� ����Ʈ
    List<Transform> objectList = new List<Transform>();

    // Hp�� ����Ʈ
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

                float maxIconSize = 4.0f; // �������� �ִ� ũ��

                // ī�޶�� �������� Z�� �Ÿ� ����
                float zDistance = Mathf.Abs(cam.transform.position.z - objectList[i].position.z);

                // �Ÿ� ���̰� �۾������� �������� ũ�� Ŀ��
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