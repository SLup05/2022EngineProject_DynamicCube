using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public string[] questList;
    public Text nowQuestText;
    public int nowQuest = 0;
    private bool isChangeQuest = false;

    private bool isQuest5 = false;
    public GameObject treasureChest = null;
    public Transform dummyTransform = null;
    public Transform chestTransform = null;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (nowQuest < 9)
            nowQuestText.text = questList[nowQuest];
        if (nowQuest == 5 && !isQuest5)
        {
            isQuest5 = true;
            SpawnChest();
        }
    }
    private IEnumerator UpdateQuest()
    {
        nowQuestText.text = "<color=#00FF00>" + nowQuestText.text + "</color>";
        yield return new WaitForSecondsRealtime(1f);
        nowQuest++;
        nowQuestText.text = "<color=#FFFFFF>" + nowQuestText.text + "</color>";
        isChangeQuest = false;
    }

    private void SpawnChest()
    {
        GameObject _treasureChest = treasureChest;
        _treasureChest = Instantiate(_treasureChest, chestTransform.position, Quaternion.identity);
    }
    public void GetUpdateQuest()
    {
        if (!isChangeQuest)
        {
            StartCoroutine("UpdateQuest");
            isChangeQuest = true;
        }
    }
    public void GetStoreButtonDown()
    {
        if (nowQuest == 7)
            GetUpdateQuest();
    }
}
