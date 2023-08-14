using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField]
    private int hp; // ������ ü��

    [SerializeField]
    private float destroyTime; // ���� ���� �ð�

    [SerializeField]
    private SphereCollider col; // ��ü �ݶ��̴�

    // �ʿ��� ���� ������Ʈ
    [SerializeField]
    private GameObject go_rock; // �Ϲ� ���� 
    [SerializeField]
    private GameObject go_debris; // ���� ����
    [SerializeField]
    private GameObject go_effect_prefabs; // ä�� ����Ʈ
    [SerializeField]
    private GameObject go_rock_item_prefabs; // ������ ������

    private int maxCount = 5;
    private int minCount = 1;

    // ä�� ��
    public void Mining()
    {
        var clone = Instantiate(go_effect_prefabs, col.bounds.center, Quaternion.identity); // Quaternion.identity: �⺻��
        Destroy(clone, destroyTime);

        hp--;
        if (hp <= 0)
            Destruction();
    }

    // ä�� ��(�μ��� ��)
    private void Destruction()
    {
        col.enabled = false;

        // �������� ���� ������ ����
        for (int i = 0; i < Mathf.Round(Random.Range(minCount, maxCount)); i++)
        {
            Instantiate(go_rock_item_prefabs, go_rock.transform.position, Quaternion.identity);
        }

        Destroy(go_rock);

        go_debris.SetActive(true);
        Destroy(go_debris, destroyTime);
    }
}
