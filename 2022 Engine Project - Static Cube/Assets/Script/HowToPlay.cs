using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HowToPlay : MonoBehaviour
{
    // Start is called before the first frame update

    public Canvas[] canvaslist = null;
    private bool isHowToPlayStart = false;
    private int count = 0;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void nextHelp()
    {
        if (isHowToPlayStart)
        {
            canvaslist[count].gameObject.SetActive(false);
            count++;
            if (count > canvaslist.Length - 1)
            {
                isHowToPlayStart = false;
                canvaslist[count - 1].gameObject.SetActive(false);
                count = 0;
            }
            else
                canvaslist[count].gameObject.SetActive(true);
        }
    }
    public void StartHowToPlay()
    {
        canvaslist[0].gameObject.SetActive(true);
        isHowToPlayStart = true;
    }
    public void MoveToTutorialStage()
    {
        SceneManager.LoadScene("Tutorial");
    }
}
