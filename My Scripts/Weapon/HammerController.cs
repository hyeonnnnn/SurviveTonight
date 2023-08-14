using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerController : CloseWeaponController
{
    // Ȱ��ȭ ����
    public static bool isActivate = false;

    void Update()
    {
        if (isActivate)
            TryAttack();
    }

    protected override IEnumerator HitCoroutine() // ������
    {
        while (isSwing) // ���� üũ
        {
            if (checkObject())
            {
                // ����
                if (hitInfo.transform.tag == "Rock")
                    hitInfo.transform.GetComponent<Rock>().Mining();

                // ����
                if (hitInfo.transform.tag == "Enemy")
                {
                    Debug.Log("Hit");
                    hitInfo.transform.GetComponent<EnemyController>().Damage(1, transform.position);
                }

                isSwing = false;
            }
            yield return null;
        }
    }
}
