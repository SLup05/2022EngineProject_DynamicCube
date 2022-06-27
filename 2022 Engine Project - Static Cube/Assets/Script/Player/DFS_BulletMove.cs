using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DFS_BulletMove : MonoBehaviour
{
    [SerializeField]
    private Vector3 targetPos = new Vector3(0, 0, 0);

    //[SerializeField]
    private float speed = 50f;
    public bool isMoveStart = true;

    public float attackpoint = 0;

    private float destroyCount = 0f;
    // Start is called before the first frame update
    void Start()
    {
        FindFarthestEnemy();
        transform.LookAt(targetPos);
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoveStart)
        {
            destroyCount += Time.deltaTime;

            if (destroyCount > 3f)
            {
                //Debug.Log("DFS Die");
                Destroy(gameObject);
            }
            // Vector3 dir = targetPos - transform.position;
            // //transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * speed);
            // //transform.position = Vector3.MoveTowards(transform.position, new Vector3(dir.x * 1000, transform.position.y, dir.z * 1000), Time.deltaTime * speed);
            // transform.Translate(dir.normalized * speed * Time.deltaTime);

            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        //Debug.Log("DFS Hit");
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("get hit:DFS");
            other.gameObject.SendMessage("Hit", attackpoint);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("DFS Hit");
        if (other.gameObject.CompareTag("Enemy"))
        {
            //            Debug.Log("get hit:DFS");
            other.gameObject.SendMessage("Hit", attackpoint);
        }
    }

    public void SetAttackPoint(float _attackpoint)
    {
        attackpoint = _attackpoint;
    }

    private void FindFarthestEnemy()
    {
        GameObject[] enemyList;
        enemyList = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemyList.Length == 0) { Debug.Log("Enem is Null!"); }
        else
        {
            targetPos = enemyList[0].transform.position;

            float farthestDis = Vector3.Distance(gameObject.transform.position, enemyList[0].transform.position);

            foreach (GameObject enemy in enemyList)
            {
                float nowDis = Vector3.Distance(transform.position, enemy.transform.position);

                if (enemy.GetComponent<EnemyMove>().PrintNowState() == "Air")
                {
                    //Debug.Log("Skip Air State");
                }
                else
                {
                    if (nowDis > farthestDis)
                    {
                        targetPos = enemy.transform.position;
                        farthestDis = nowDis;
                    }
                }
            }
        }
    }
}
