using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WaveManager : MonoBehaviour
{
    public int nowWave = 0;
    private string nowWaveString = "Wave_";
    public GameObject sphereEnemy = null;
    public float waveCountDown = 10;
    public bool waveEnd = false;
    public Text nowWaveText;
    private GameManager gameManager;
    public bool enemyDestroy = false;
    private TutorialManager tutorialManager = null;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        tutorialManager = FindObjectOfType<TutorialManager>();
        StartCoroutine("AutoWaveSystem");
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.isPlayerDead)
        {
            nowWaveText.text = nowWave.ToString();
            if (waveEnd)
            {
                waveCountDown -= Time.deltaTime;
                //Debug.Log("waveCountDown: " + waveCountDown);
            }

            if (waveCountDown <= 0 && waveEnd == true)
            {
                nowWave++;
                waveEnd = false;
                waveCountDown = 10;
                StartCoroutine(nowWaveString + nowWave.ToString());
            }
        }
    }

    private void CreateSphereEnemy()
    {
        GameObject _sphereEnemy = sphereEnemy;
        float posX, posY, posZ;
        posX = SetRandomPositionX();
        posY = 10f;
        posZ = Random.Range(5, -13);
        Instantiate(_sphereEnemy, new Vector3(posX, posY, posZ), Quaternion.identity);
        _sphereEnemy.transform.SetParent(null);
    }


    private void SeqCreateEnemy(int enemyNumber)
    {
        for (int i = 0; i < enemyNumber; i++)
        {
            CreateSphereEnemy();
        }
    }

    private IEnumerator AutoWaveSystem()
    {
        while (true)
        {
            int enemyNumber = 3 + Mathf.RoundToInt(nowWave / 3);

            for (int i = 0; i < 3; i++)
            {
                SeqCreateEnemy(enemyNumber);
                yield return new WaitForSeconds(3f);
            }

            for (int i = 0; i < nowWave / 5; i++)
            {
                SeqCreateEnemy(enemyNumber);
                yield return new WaitForSeconds(3f);
            }

            yield return new WaitForSeconds(7f);
            Debug.Log("NextWave");
            nowWave++;
        }
    }

    private IEnumerator Wave_1()
    {
        //StartCoroutine("AllEnemyDestroy");
        //AllEnemyDestroy();
        Debug.Log("In Wave 1");
        yield return null;
    }

    private IEnumerator AllEnemyDestroy()
    {
        Debug.Log("Des");
        enemyDestroy = true;
        yield return new WaitForSeconds(1f);
        enemyDestroy = false;
    }

    private float SetRandomPositionX()
    {
        float _posX;
        float randomX;
        randomX = Random.RandomRange(0, 99);
        //Debug.Log(randomX);
        if (randomX % 2 == 0)
            _posX = 25;
        else
            _posX = -25;

        return _posX;
    }
}
