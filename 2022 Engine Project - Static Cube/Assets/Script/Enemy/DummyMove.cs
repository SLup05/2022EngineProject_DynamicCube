using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyMove : EnemyMove
{
    private TutorialManager tutorialManager = null;
    private QuestManager questManager = null;
    public bool isDummy = true;
    protected override void Start()
    {
        base.Start();
        //gameObject.tag = "Dummy";
    }
    protected override void SetStatus()
    {
        tutorialManager = FindObjectOfType<TutorialManager>();
        questManager = FindObjectOfType<QuestManager>();
        healthpoint = 1;
        attackpoint = 10;
        speed = 0;
        airdelayTime = 0.5f;
    }
    protected override void Die()
    {
        // int boxPers = Random.RandomRange(0, 99);
        // if (boxPers > 79)
        // {
        //     Instantiate(TreasureChest, new Vector3(transform.position.x, transform.position.y + 10, transform.position.z), Quaternion.identity);
        //     //            Debug.Log("SpawnChest");
        // }
        tutorialManager.isDummyDead = true;
        Destroy(gameObject);
        gameObject.SetActive(false);
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
        else if (other.gameObject.CompareTag("Protected") && questManager.nowQuest == 1)
        {
            questManager.GetUpdateQuest();
        }
        else if (other.gameObject.CompareTag("DFS") && questManager.nowQuest == 2)
        {
            questManager.GetUpdateQuest();
        }
        else if (other.gameObject.CompareTag("Instantiate") && questManager.nowQuest == 3)
        {
            questManager.GetUpdateQuest();
        }
    }

    private void OnTriggerEnter(Collision other)
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
        else if (other.gameObject.CompareTag("Protected") && questManager.nowQuest == 1)
        {
            questManager.GetUpdateQuest();
        }
        else if (other.gameObject.CompareTag("DFS") && questManager.nowQuest == 2)
        {
            questManager.GetUpdateQuest();
        }
        else if (other.gameObject.CompareTag("Instantiate") && questManager.nowQuest == 3)
        {
            questManager.GetUpdateQuest();
        }
    }
    protected override void Move()
    {
        base.Move();
        Vector3 w2vPos = Camera.main.WorldToViewportPoint(transform.position);
        w2vPos.x = Mathf.Clamp01(w2vPos.x);
        w2vPos.y = Mathf.Clamp01(w2vPos.y);
        Vector3 worldPos = Camera.main.ViewportToWorldPoint(w2vPos);
        transform.position = new Vector3(worldPos.x, 1, worldPos.z);
    }
}
