using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; //import

public class EnemyMove : MonoBehaviour
{
    public GameObject TreasureChest = null;
    [SerializeField]
    protected Transform attackTransform;
    protected WaveManager waveManager = null;
    protected GameManager gameManager = null;
    private MeshRenderer meshRenderer;

    public Vector3 targetPos;
    [SerializeField]
    protected float healthpoint;
    protected float attackpoint;
    protected float speed;

    protected float airdelayTime;
    protected float airdelayCount = 0;

    protected float attackdelayTime = 0.33f;
    protected float attackdelayCount = 0;
    protected GameObject attackTarget;
    protected float hitDelay = 0.5f;

    [SerializeField]
    protected string nowState = "Air";

    [SerializeField]
    protected bool isAttack = false;
    private Color tempC;
    protected virtual void Start()
    {
        //waveManager = FindObjectOfType<WaveManager>();
        gameObject.tag = "Enemy";
        gameManager = FindObjectOfType<GameManager>();
        waveManager = FindObjectOfType<WaveManager>();
        meshRenderer = GetComponent<MeshRenderer>();
        SetStatus();
        tempC = meshRenderer.material.color;
    }

    protected void Update()
    {
        if (gameManager == null) { }
        else
        {
            if (gameManager.isPlayerDead == true)
            {
                //Debug.Log("StopEnemy");
                Destroy(gameObject);
                gameObject.SetActive(false);

            }
            else
            {
                //Debug.Log("Not Died");
            }
        }


        SetTargetPos();
        SetStatusErrorCheck();

        if (nowState == "Air") { /*Debug.Log("Air");*/ }
        else if (nowState == "Delay")
        {
            airdelayCount += Time.deltaTime;
            if (airdelayCount >= airdelayTime)
            {
                SetMoveState();
            }
        }
        else if (nowState == "Move")
        {
            //Debug.Log("Attack");
            Move();
        }
        if (waveManager == null) { }
        else
        {
            if (healthpoint <= 0 || waveManager.enemyDestroy)
            {
                //Debug.Log("Die");
                Die();
            }
        }
    }


    /// <summary>
    /// 캐릭터 처치
    /// </summary>
    protected virtual void Die()
    {
        int boxPers = Random.RandomRange(0, 99);
        if (boxPers > 79)
        {
            Instantiate(TreasureChest, new Vector3(transform.position.x, transform.position.y + 10, transform.position.z), Quaternion.identity);
            //            Debug.Log("SpawnChest");
        }
        gameManager.dieEnemyCount++;
        Destroy(gameObject);
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 캐릭터 이동
    /// </summary>
    protected virtual void Move()
    {
        SetTargetPos();
        //        Debug.Log(targetPos);
        Vector3 dir = targetPos - transform.position;
        //transform.position = Vector3.MoveTowards(transform.position, targetPos.normalized, speed * Time.deltaTime);
        transform.Translate(dir.normalized * speed * Time.deltaTime);
    }

    protected void Hit(float hitdamage)
    {
        //        Debug.Log("Hited " + hitdamage);
        healthpoint -= hitdamage;
        if (hitdamage > 0)
            StartCoroutine("Flash");
        Invoke("SetMoveState", 0.5f);
    }

    protected IEnumerator Flash()
    {
        //Material material = GetComponent<Material>();
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        //Color tempC = new Color(255, 59, 59, 255);
        meshRenderer.material.color = new Color(255, 255, 255, 255);
        yield return new WaitForSeconds(0.05f);
        meshRenderer.material.color = tempC;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Plane"))
        {
            nowState = "Delay";
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            //attackTarget = other.gameObject;
            //nowState = "Attack";
            PlayerAttack(other.gameObject);
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerAttack(other.gameObject);
        }
    }

    protected void PlayerAttack(GameObject player)
    {

        //Debug.Log("inAttack");
        if (attackdelayCount < attackdelayTime)
        {
            attackdelayCount += Time.deltaTime;
        }
        else if (attackdelayCount >= attackdelayTime)
        {
            //Debug.Log("PlayerAttack");
            player.SendMessage("PlayerHit", attackpoint);
            attackdelayCount = 0;
        }

        if (attackTransform != transform)
        {
            //Debug.Log("Change Attack to Move");
            Hit(0);
            isAttack = false;
        }

    }

    protected void SetTargetPos()
    {
        if (GameObject.Find("Cube") == null) { }
        //Destroy(gameObject);
        else
            targetPos = GameObject.Find("Cube").transform.position;
    }

    protected void SetMoveState()
    {
        nowState = "Move";
    }

    /// <summary>
    /// 적 스테이터스 세팅
    /// </summary>
    protected virtual void SetStatus()
    {
        healthpoint = -1;
        attackpoint = -1;
        speed = -1;
        airdelayTime = -1;
    }

    /// <summary>
    /// 적 스테이터스 로드 에러 체크
    /// </summary>
    public void SetStatusErrorCheck()
    {
        if (healthpoint == -1)
        {
            Debug.LogWarning("Enemy Status Error: healthpoint");
            Destroy(gameObject);
        }
        else if (attackpoint == -1)
        {
            Debug.LogWarning("Enemy Status Error: attackpoint");
            Destroy(gameObject);
        }
        else if (speed == -1)
        {
            Debug.LogWarning("Enemy Status Error: speed");
            Destroy(gameObject);
        }
        else if (airdelayTime == -1)
        {
            Debug.LogWarning("Enemy Status Error: DelayTime");
            Destroy(gameObject);
        }
    }
    public string PrintNowState()
    {
        return nowState;
    }
}