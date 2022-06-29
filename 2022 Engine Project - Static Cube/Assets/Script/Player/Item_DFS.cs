using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_DFS : Item_
{

    public float attackDelay = 1f;
    private float attackCount = 0;
    private bool isAttack = false;
    public GameObject dfsBullet = null;


    // Start is called before the first frame update
    void Start()
    {
        stackMemoryPerSec = 20f;
        deleteMemoryPerSec = 20f;
        speicalPharse = "연사 속도 강화";
        attackpoint = 7f;
    }

    protected override void Move()
    {
        if (!uiManager.isPause)
        {
            attackCount += Time.deltaTime;
            if (attackCount >= attackDelay && !isAttack)
            {
                isAttack = true;
                Attack();
                attackCount = 0;
                isAttack = false;
            }
            CheckSpecialUpgrade();
        }
    }
    public void CheckSpecialUpgrade()
    {
        if (specialCount == 1)
            attackDelay = 0.7f;
        else if (specialCount > 0)
        {
            attackDelay = 0.7f - specialCount * 0.05f;
        }
    }
    private void Attack()
    {
        //Debug.Log("DFS Attack");
        GameObject _dfsBullet = dfsBullet;
        _dfsBullet = Instantiate(_dfsBullet, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        //_dfsBullet.transform.SetParent(null);
        _dfsBullet.GetComponent<DFS_BulletMove>().SetAttackPoint(attackpoint);
    }
}
