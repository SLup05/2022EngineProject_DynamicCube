using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item_Protected : Item_
{
    private string Direction = "MoveBack";
    public float speed = 0;
    private Animator animator;
    private bool isRotate = false;
    private float setPos = 1f;
    private float setY = 0.4f;
    // Start is called before the first frame update
    protected void Start()
    {
        stackMemoryPerSec = 20f;
        deleteMemoryPerSec = 20f;
        speed = 15f;
        attackpoint = 10f;
        gameManager = FindObjectOfType<GameManager>();
        animator = GetComponent<Animator>();
        speicalPharse = "회전속도 강화";
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            //            Debug.Log("get hit");
            other.gameObject.SendMessage("Hit", attackpoint);
        }
    }

    public void Upgrade_SetRotateMove()
    {
        if (gameManager.PlayerGold < 10) { }
        else
        {
            isRotate = true;
            gameManager.DecreasePlayerGold(5);
        }

    }


    // public void Upgrade_SpeedIncrease()
    // {
    //     if (gameManager.PlayerGold < 5) { }
    //     else
    //     {
    //         speed += 0.2f;
    //         gameManager.DecreasePlayerGold(5);
    //     }
    // }

    public void CheckSpecialUpgrade()
    {
        if (specialCount == 1)
        {
            isRotate = true;
            speed = 35;
        }
        else if (specialCount > 0)
        {
            animator.speed = 1 + specialCount * 0.15f;
            speed = 35 + specialCount * 0.5f;
        }
    }

    protected override void Move()
    {
        CheckSpecialUpgrade();

        if (Direction == "MoveBack")
        {
            transform.Translate(Vector3.back * speed * Time.deltaTime);
            if (transform.localPosition.z <= -setPos)
            {
                transform.localPosition = new Vector3(transform.localPosition.x, setY, -setPos);
                if (isRotate)
                    animator.Play("Animation_SqareShooter-Z");
                Direction = "MoveLeft";
            }
        }

        else if (Direction == "MoveLeft")
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
            if (transform.localPosition.x <= -setPos)
            {
                transform.localPosition = new Vector3(-setPos, setY, transform.localPosition.z);
                if (isRotate)
                    animator.Play("Animation_SqareShooter-X");
                Direction = "MoveUp";
            }
        }

        else if (Direction == "MoveUp")
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            if (transform.localPosition.z >= setPos)
            {
                transform.localPosition = new Vector3(transform.localPosition.x, setY, setPos);
                if (isRotate)
                    animator.Play("Animation_SqareShooter-Z");

                Direction = "MoveRight";
            }

        }

        else if (Direction == "MoveRight")
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            if (transform.localPosition.x >= setPos)
            {
                transform.localPosition = new Vector3(setPos, setY, transform.localPosition.z);
                if (isRotate)
                    animator.Play("Animation_SqareShooter-X");
                Direction = "MoveBack";
            }

        }
        transform.localPosition = new Vector3(transform.localPosition.x, setY, transform.localPosition.z);
    }

    private void OnEnable()
    {
        if (isRotate)
        {
            if (Direction == "MoveRight" || Direction == "MoveLeft")
            {
                animator.Play("Animation_SqareShooter-X");
            }
            else if (Direction == "MoveRight" || Direction == "MoveLeft")
            {
                animator.Play("Animation_SqareShooter-Z");
            }
        }

    }
}
