using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseWeapon : MonoBehaviour
{
    public string closeWeaponName; // ���� ���� �̸�

    public bool isHammer; // ��ġ����

    public int damage; // ���ݷ�
    public float attackDelay; // ���� ������
    public float attackDelayA; // ���� Ȱ��ȭ ���� ������ (������O)
    public float attackDelayB; // ���� ��Ȱ��ȭ ���� ������ (������X)

    public Animator anim;
}
