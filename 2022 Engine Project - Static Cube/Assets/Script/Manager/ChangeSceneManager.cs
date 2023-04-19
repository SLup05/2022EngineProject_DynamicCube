using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ChangeSceneManager : MonoBehaviour
{
    public bool isTutorial = false;
    private HowToPlay howToPlay = null;
    // Start is called before the first frame update
    void Start()
    {
        howToPlay = FindObjectOfType<HowToPlay>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!howToPlay.isHowToPlayStart)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (SceneManager.GetActiveScene().name == "Title")
                {
                    SceneManager.LoadScene("MainScene");
                }
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }

    }
}
