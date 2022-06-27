using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Item_ : MonoBehaviour
{
    protected float attackpoint = -1f;
    public float stackMemoryPerSec = 20f;
    public GameManager gameManager = null;

    public Text attackIncreaseText = null;
    public int attackIncreaseCount = 0;
    public Text memoryOptimizationText = null;
    public int memoryOptimizationCount = 0;

    public Text speicalText = null;
    public int specialCount = 0;
    public string speicalPharse = "";
    public int specialPrice = 25;
    public Text specialPriceText;
    // Start is called before the first frame update
    protected void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    protected void Update()
    {
        Move();
    }
    protected void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("get hit");
            other.gameObject.SendMessage("Hit", attackpoint);
        }
    }

    public void Upgrade_AttackIncrease()
    {
        Debug.Log("Upgrade Button Down");
        if (gameManager.PlayerGold < 5) { }
        else
        {
            attackpoint = Mathf.Round(attackpoint * 1.5f);
            attackIncreaseCount++;
            attackIncreaseText.text = "공격력 강화 +" + attackIncreaseCount.ToString();
            //Debug.Log("IncreaseAtk");
            gameManager.DecreasePlayerGold(5);
        }
    }

    public void Upgrade_MemoryOptimizaton()
    {
        Debug.Log("Upgrade Button Down");
        if (gameManager.PlayerGold < 5) { }
        else
        {
            stackMemoryPerSec = Mathf.Round(stackMemoryPerSec * 0.9f);
            memoryOptimizationCount++;
            memoryOptimizationText.text = "메모리 최적화 +" + memoryOptimizationCount.ToString();
            //Debug.Log("IncreaseAtk");
            gameManager.DecreasePlayerGold(5);
        }
    }
    public void Upgrade_Special()
    {
        Debug.Log("Upgrade Button Down");
        if (gameManager.PlayerGold < specialPrice) { }
        else
        {
            specialCount++;
            if (specialCount == 1)
                speicalText.text = speicalPharse;
            else
                speicalText.text = speicalPharse + " +" + (specialCount - 1).ToString();
            //Debug.Log("IncreaseAtk");
            gameManager.DecreasePlayerGold(specialPrice);
            specialPrice = 10;
            specialPriceText.text = specialPrice.ToString() + "G";
        }
    }

    protected virtual void Move()
    {

    }
}
