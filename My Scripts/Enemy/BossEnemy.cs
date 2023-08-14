using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossEnemy : EnemyController
{
    [SerializeField] GameObject missile;
    [SerializeField] Transform missilePortA;
    [SerializeField] Transform missilePortB;
    [SerializeField] float delayTime;

    [SerializeField] protected GameObject[] bossItem_prefab;
    [SerializeField] protected GameObject key_prefab;

    EnemyController enemy;

    Vector3 lookVec;
    bool isLook;
    
    void Start()
    {
        isLook = true;
    }

    void Awake()
    {
        nav.isStopped = true;
        StartCoroutine(Think());
    }

    void Update()
    {
        if (isLook)
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            lookVec = new Vector3(h, 0, v) * 5f;
            transform.LookAt(target.position + lookVec);
        }

        if (isDead)
        {
            StopAllCoroutines();
            return;
        }
    }

    IEnumerator Think()
    {
        yield return new WaitForSeconds(delayTime);
        StartCoroutine(MissileShot());
    }

    IEnumerator MissileShot()
    {
        anim.SetTrigger("doShot");
        yield return new WaitForSeconds(0.2f);
        GameObject instantMissileA = Instantiate(missile, missilePortA.position, missilePortA.rotation);
        BossMissile bossMissileA = instantMissileA.GetComponent<BossMissile>();
        bossMissileA.target = target;

        anim.SetTrigger("doShot");
        yield return new WaitForSeconds(0.3f);
        GameObject instantMissileB = Instantiate(missile, missilePortB.position, missilePortB.rotation);
        BossMissile bossMissileB = instantMissileB.GetComponent<BossMissile>();
        bossMissileB.target = target;

        yield return new WaitForSeconds(2f);
        StartCoroutine(Think());
    }

    public override void Dead()
    {
        DropItem();
        base.Dead();
    }

    new void DropItem()
    {
        Vector3 ItemSpawnPosition = transform.position + new Vector3(0f, 2f, 0f);

        foreach (GameObject itemPrefab in bossItem_prefab)
        {
            Instantiate(itemPrefab, ItemSpawnPosition, Quaternion.identity);
        }

    }
}
