using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] private Gun currentGun;
    private float currentFireRate;

    private Vector3 originPos;

    private RaycastHit hitInfo;

    [SerializeField] private Camera theCam;

    void Start()
    {
        originPos = Vector3.zero;
    }

    void Update()
    {
        GunFireRateCalc();
        TryFire();
    }

    // ���� �ӵ� ���
    void GunFireRateCalc()
    {
        if (currentFireRate > 0)
            currentFireRate -= Time.deltaTime;
    }

    // �߻� �õ�
    void TryFire()
    {
        if(Input.GetButton("Fire1") && currentFireRate <= 0)
        {
            Fire();
        }
    }

    // �߻� �� ���
    void Fire()
    {
        currentFireRate = currentGun.fireRate;
        Shoot();
    }

    // �߻� �� ���
    void Shoot()
    {
        currentGun.muzzleFlash.Play();
        Hit();
    }

    void Hit()
    {
        if (Physics.Raycast(theCam.transform.position, theCam.transform.forward, out hitInfo, 100)) 
        {
            if (hitInfo.transform.tag == "Enemy")
            {
                Debug.Log("������ �� Ÿ��");
                hitInfo.transform.GetComponent<EnemyController>().Damage(2, transform.position);
            }
        }
    }
}
