using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{
    // ���� ����
    public GameObject[] weapons;
    public bool[] hasWeapons;

    public GameObject equipWeapon;

    public Image[] weaponBg;

    void Update()
    {
        GetInput();
    }

    void GetInput()
    {
        // ���� ��ü: 1, 2, 3
        Swap();
    }

    void Swap()
    {
        // ���Ⱑ ������ ��üX
        for (int i = 0; i < 3; i++)
        {
            if ((Input.GetKeyDown(KeyCode.Alpha1 + i)) && !hasWeapons[i])
                return;
        }

        int weaponIndex = -1;

        // ��ȣŰ�� ���� �ε��� ����
        for (int i = 0; i < 3; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                weaponIndex = i;
                break;
            }
        }

        // �ε����� ���� ���� ��ü
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha3))
        {
            // ���� ���� ��Ȱ��ȭ
            if (equipWeapon != null)
                equipWeapon.SetActive(false);

            // �ٲ� ���� Ȱ��ȭ
            equipWeapon = weapons[weaponIndex];
            equipWeapon.SetActive(true);
        }
    }

    public void ActivateHammer()
    {
        HammerController.isActivate = true;
        hasWeapons[0] = true;
        equipWeapon = weapons[0];
        equipWeapon.SetActive(true);
        weaponBg[0].GetComponent<Image>().color = Color.white;
    }
}
