using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private CharacterController characterController;
    public float PlayerHp = 100;
    private GameManager gameManager = null;
    private UIManager uiManager = null;
    private bool isPlane = false;
    private QuestManager questManager;
    private Color tempC;
    private TutorialManager tutorialManager = null;
    private float speed = 5f;
    MeshRenderer meshRenderer;

    // Start is called before the first frame update
    private void Start()
    {
        tutorialManager = FindObjectOfType<TutorialManager>();
        questManager = FindObjectOfType<QuestManager>();
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        characterController = GetComponent<CharacterController>();
        gameManager = FindObjectOfType<GameManager>();
        uiManager = FindObjectOfType<UIManager>();
        tempC = meshRenderer.material.color;
    }

    // Update is called once per frame
    private void Update()
    {

        if (PlayerHp <= 0)
        {
            if (questManager == null)
            {

                PlayerDie();
            }
            else if (questManager.nowQuest == 8)
            {
                PlayerDie();
                questManager.GetUpdateQuest();
            }
        }

        if (isPlane)
        { PlayerTransformSet(); }
        else
        {

        }


        Vector3 moveTargetPos = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (questManager != null && questManager.nowQuest == 0)
        {
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                questManager.GetUpdateQuest();
            }
        }

        characterController.Move(moveTargetPos * Time.deltaTime * speed);
        Vector3 w2vPos = Camera.main.WorldToViewportPoint(transform.position);
        w2vPos.x = Mathf.Clamp01(w2vPos.x);
        w2vPos.y = Mathf.Clamp01(w2vPos.y);
        Vector3 worldPos = Camera.main.ViewportToWorldPoint(w2vPos);
        transform.position = new Vector3(worldPos.x, 1, worldPos.z);


        //Debug.Log(PlayerHp);
    }

    private void PlayerTransformSet()
    {
        transform.position = new Vector3(0, transform.position.y, -2.4f);
    }

    private void PlayerDie()
    {
        if (gameManager == null)
        {
            tutorialManager.isPlayerDead = true;
        }
        else
        {
            gameManager.isPlayerDead = true;
        }
        Destroy(gameObject);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Chest"))
        {
            if (questManager == null)
            {
                gameManager.PlayerGold += 10;
            }
            else if (questManager.nowQuest == 5)
            {
                questManager.GetUpdateQuest();
            }
            if (gameManager == null && tutorialManager != null)
            {
                tutorialManager.PlayerGold += 100;
            }

            other.transform.GetComponent<ChestControl>().Die();
        }
    }
    public IEnumerator Flash()
    {
        //Material material = GetComponent<Material>();
        //Color tempC = new Color(255, 59, 59, 255);
        meshRenderer.material.color = new Color(255, 0, 0, 255);
        yield return new WaitForSeconds(0.05f);
        meshRenderer.material.color = tempC;
    }

    public void PlayerHit(int damage)
    {
        //StartCoroutine("Flash");
        //Debug.Log("PlayerHit");
        if (questManager != null && questManager.nowQuest != 8)
        { damage = 0; }
        uiManager.DamageEffect(damage);
        PlayerHp -= damage;

    }


}
