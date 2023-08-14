using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �̿ϼ� Ŭ����. �߻� Ŭ����.
public abstract class CloseWeaponController : MonoBehaviour
{
    [SerializeField] protected CloseWeapon currentCloseWeapon; 

    protected bool isAttack = false;
    protected bool isSwing = false;

    protected RaycastHit hitInfo;
    [SerializeField] protected LayerMask layerMask;

    protected void TryAttack()
    {
        if (Input.GetButton("Fire1"))
            {
            if (!isAttack)
            {
                StartCoroutine(AttackCoroutine());
            }
        }
    }

    protected IEnumerator AttackCoroutine()
    {
        isAttack = true;
        currentCloseWeapon.anim.SetTrigger("Attack");

        yield return new WaitForSeconds(currentCloseWeapon.attackDelayA);

        // �ֵθ� �������� ����
        isSwing = true;
        StartCoroutine(HitCoroutine());
        yield return new WaitForSeconds(currentCloseWeapon.attackDelayB);
        isSwing = false;

        yield return new WaitForSeconds(currentCloseWeapon.attackDelay - currentCloseWeapon.attackDelayA - currentCloseWeapon.attackDelayB);
        isAttack = false; // �ٽ� ����
    }

    protected abstract IEnumerator HitCoroutine(); // ���� ������ �˾ƺ��� �ڷ�ƾ

    protected bool checkObject()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, 10, layerMask))
        {
            return true;
        }
        return false;
    }
}