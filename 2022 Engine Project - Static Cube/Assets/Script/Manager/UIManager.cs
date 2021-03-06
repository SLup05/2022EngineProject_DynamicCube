using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public bool isPause = false;
    public GameObject text_damagEffect = null;
    public Transform transform_damageEffect = null;
    public Canvas PauseCanvas = null;

    void Start()
    {
        //Material.DoFade(0, 1);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPause)
            {
                Debug.Log("inPause");
                PauseCanvas.gameObject.SetActive(true);
                Time.timeScale = 0;
                isPause = true;
            }
            else
            {
                Debug.Log("outPause");
                PauseCanvas.gameObject.SetActive(false);
                Time.timeScale = 1;
                isPause = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isPause)
            {
                Time.timeScale = 1;
                SceneManager.LoadScene("Title");
            }
        }
    }

    public void DamageEffect(int Damage)
    {
        float damagePosX = transform_damageEffect.position.x + Random.RandomRange(-0.5f, 0.6f);
        float damagePosZ = transform_damageEffect.position.z + Random.RandomRange(-0.6f, 0.5f);
        GameObject damageEffect = Instantiate(text_damagEffect, new Vector3(damagePosX, transform_damageEffect.position.y, damagePosZ), Quaternion.identity); ;
        damageEffect.GetComponent<UI_DamageEffect>().getDamage = Damage;
        damageEffect.transform.rotation = Quaternion.Euler(75, 0, 0);
    }
}
