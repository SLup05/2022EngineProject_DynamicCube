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

    public GameObject hpBar = null;

    private float speed = 5f;

    // Start is called before the first frame update
    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        gameManager = FindObjectOfType<GameManager>();
        uiManager = FindObjectOfType<UIManager>();
    }

    // Update is called once per frame
    private void Update()
    {

        if (PlayerHp <= 0)
        {
            PlayerDie();
        }

        if (isPlane)
        { PlayerTransformSet(); }
        else
        {

        }


        Vector3 moveTargetPos = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        characterController.Move(moveTargetPos * Time.deltaTime * speed);
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position); //캐릭터의 월드 좌표를 뷰포트 좌표계로 변환해준다.
        viewPos.x = Mathf.Clamp01(viewPos.x); //x값을 0이상, 1이하로 제한한다.
        viewPos.y = Mathf.Clamp01(viewPos.y); //y값을 0이상, 1이하로 제한한다.
        Vector3 worldPos = Camera.main.ViewportToWorldPoint(viewPos); //다시 월드 좌표로 변환한다.
        transform.position = new Vector3(worldPos.x, 1, worldPos.z); //좌표를 적용한다.

        hpBar.transform.localScale = new Vector3(PlayerHp * 0.01f, hpBar.transform.localScale.y, hpBar.transform.localScale.z);
        Debug.Log(PlayerHp);
    }

    private void PlayerTransformSet()
    {
        transform.position = new Vector3(0, transform.position.y, -2.4f);
    }

    private void PlayerDie()
    {
        gameManager.isPlayerDead = true;
        Destroy(gameObject);
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Plane"))
        {
            isPlane = true;
        }
    }



    public void PlayerHit(int damage)
    {
        //Debug.Log("PlayerHit");
        uiManager.DamageEffect(damage);
        PlayerHp -= damage;
    }
}
