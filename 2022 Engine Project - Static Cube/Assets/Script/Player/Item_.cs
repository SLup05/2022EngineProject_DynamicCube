using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Item_ : MonoBehaviour
{
    protected float attackpoint = -1f;
    public float stackMemoryPerSec = 20f;
    public float deleteMemoryPerSec = 20f;
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
    public QuestManager questManager = null;
    public TutorialManager tutorialManager = null;
    public UIManager uiManager;
    // Start is called before the first frame update
    protected void Awake()
    {
        uiManager = FindObjectOfType<UIManager>();
        questManager = FindObjectOfType<QuestManager>();
        gameManager = FindObjectOfType<GameManager>();
        tutorialManager = FindObjectOfType<TutorialManager>();
    }

    // Update is called once per frame
    protected void Update()
    {
        Move();
    }

    protected virtual void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("get hit");
            other.gameObject.SendMessage("Hit", attackpoint);
        }
    }

    public void Upgrade_AttackIncrease()
    {
        Debug.Log("Upgrade Attack Button Down");
        if (gameManager == null)
        {
            attackpoint = Mathf.Round(attackpoint * 1.15f);
            attackIncreaseCount++;
            attackIncreaseText.text = "공격력 강화 +" + attackIncreaseCount.ToString();
            //Debug.Log("IncreaseAtk");
            tutorialManager.DecreasePlayerGold(5);
        }
        else if (gameManager.PlayerGold < 5) { }
        else
        {
            attackpoint = Mathf.Round(attackpoint * 1.15f);
            attackIncreaseCount++;
            attackIncreaseText.text = "공격력 강화 +" + attackIncreaseCount.ToString();
            //Debug.Log("IncreaseAtk");
            gameManager.DecreasePlayerGold(5);
        }
    }

    public void Upgrade_MemoryOptimizaton()
    {
        Debug.Log("Upgrade Memory Button Down");

        if (gameManager == null)
        {
            stackMemoryPerSec = stackMemoryPerSec * 0.95f;
            deleteMemoryPerSec = deleteMemoryPerSec * 0.95f;
            memoryOptimizationCount++;
            memoryOptimizationText.text = "메모리 최적화 +" + memoryOptimizationCount.ToString();
            //Debug.Log("IncreaseAtk");
            tutorialManager.DecreasePlayerGold(5);
        }

        else if (gameManager.PlayerGold < 5) { }

        else
        {
            stackMemoryPerSec = stackMemoryPerSec * 0.95f;
            deleteMemoryPerSec = deleteMemoryPerSec * 0.95f;
            memoryOptimizationCount++;
            memoryOptimizationText.text = "메모리 최적화 +" + memoryOptimizationCount.ToString();
            //Debug.Log("IncreaseAtk");
            gameManager.DecreasePlayerGold(5);
        }
    }
    public void Upgrade_Special()
    {
        Debug.Log("Upgrade Button Down");
        if (gameManager == null)
        {
            specialCount++;
            if (specialCount == 1)
                speicalText.text = speicalPharse;
            else
                speicalText.text = speicalPharse + " +" + (specialCount - 1).ToString();
            //Debug.Log("IncreaseAtk");
            tutorialManager.DecreasePlayerGold(specialPrice);
            specialPrice = 10;
            specialPriceText.text = specialPrice.ToString() + "G";
        }

        else if (gameManager.PlayerGold < specialPrice) { }
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
