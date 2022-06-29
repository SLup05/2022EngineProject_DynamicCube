using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class TutorialManager : MonoBehaviour
{
    public Camera camera = null;
    public bool isPlayerDead = false;

    public float PlayerGold = 100;
    public Canvas canvasStore = null;
    private bool isStoreActive = false;
    private bool isStoreButtonDown = false;

    public GameObject object_DFS = null;
    public GameObject progressbar_DFS = null;
    public float memory_DFS = 0;
    public bool isOverflow_DFS = false;
    public bool isStack_DFS = false;
    private Material material = null;

    public GameObject object_Protected = null;
    public GameObject progressbar_Protected = null;
    public float memory_Protected = 0;
    public bool isOverflow_Protected = false;
    public bool isStack_Protected = false;

    public GameObject object_Instantiate = null;
    public GameObject progressbar_Instantiate = null;
    public float memory_Instantiate = 0;
    public bool isOverflow_Instantiate = false;
    public bool isStack_Instantiate = false;
    public bool isDeleting_Instantiate = false;
    public float solveOverflowPerSec = 15f;
    public Text playerGoldText = null;

    public Text playerkilledText = null;
    public Canvas gameoverCanvas = null;
    public int dieEnemyCount = 0;

    private PlayerControl playerControl = null;
    public Text playerHp = null;
    public GameObject hpBar = null;

    private bool isGameOver = false;

    public GameObject dummy = null;
    public bool isDummyDead = true;
    public Transform dummyTransform = null;
    private QuestManager questManager;
    public Transform chestTransform = null;
    private bool isChangeScene = false;
    //public Text nowWaveText = null;
    private void Start()
    {
        questManager = FindObjectOfType<QuestManager>();
        playerControl = FindObjectOfType<PlayerControl>();
        material = GetComponent<Material>();
    }

    private void Update()
    {
        Ray CamRay = camera.ScreenPointToRay(Input.mousePosition);
        playerGoldText.text = PlayerGold.ToString() + "G";

        RaycastHit raycastHit;
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("CLick");
            // if (Physics.Raycast(CamRay, out raycastHit, 10000))
            // {
            //     //                Debug.Log("RayHit");
            //     if (raycastHit.transform.tag == "Chest")
            //     {
            //         PlayerGold += 10;
            //         //Debug.Log("CHest");
            //         raycastHit.transform.GetComponent<ChestControl>().Die();
            //     }
            // }
        }
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("CLick");
            if (Physics.Raycast(CamRay, out raycastHit, 10000))
            {
                //                Debug.Log("RayHit");
                if (raycastHit.transform.tag == "UI")
                {
                    //PlayerGold += 10;
                    //Debug.Log("CHest");
                    Debug.Log("Ray Button");
                    isStoreButtonDown = true;
                }
            }
        }

        if (isPlayerDead && !isGameOver)
        {
            isGameOver = true;
            StartCoroutine("GameOver");
        }
        if (Input.GetKeyDown(KeyCode.Tab))
            StoreButtonDown();

        if (Input.GetKeyDown(KeyCode.Space) && isChangeScene)
        {
            SceneManager.LoadScene("Title");
        }
        PlayerHpProd();
        SetMemory_Protected();
        SetMemory_DFS();
        SetMemory_Instantiate();
        SpawnDummySystem();
    }

    private IEnumerator GameOver()
    {
        yield return new WaitForSeconds(1f);
        isChangeScene = true;
        WaveManager waveManager = null;
        waveManager = FindObjectOfType<WaveManager>();
        gameoverCanvas.gameObject.SetActive(true);
        playerkilledText.text = "WELL DONE";

    }

    private void PlayerHpProd()
    {
        if (!isPlayerDead)
        {
            float tempHP = playerControl.PlayerHp;
            playerHp.text = tempHP + " / 100";
            hpBar.transform.localScale = new Vector3(tempHP * 0.01f, hpBar.transform.localScale.y, hpBar.transform.localScale.z);
        }
    }

    public void StoreButtonDown()
    {
        if (questManager.nowQuest == 6)
            questManager.GetUpdateQuest();
        Debug.Log("Click Button");
        if (!isStoreActive)
        {
            ActiveStore();
            object_DFS.SetActive(true);
            object_Protected.SetActive(true);
            object_Instantiate.SetActive(true);
            //object_Instantiate.SetActive(true);
        }
        else
        {
            if (isStack_DFS) { }
            else object_DFS.SetActive(false);
            if (isStack_Protected) { }
            else object_Protected.SetActive(false);
            if (isOverflow_Instantiate)
            {
                object_Instantiate.SetActive(false);
            }
            else
                object_Instantiate.SetActive(true);

            isStoreButtonDown = false;
            InactiveStore();
        }

    }

    private void ActiveStore()
    {
        isStoreActive = true;
        Time.timeScale = 0;
        canvasStore.gameObject.SetActive(true);
    }

    private void InactiveStore()
    {
        isStoreActive = false;
        Time.timeScale = 1;
        canvasStore.gameObject.SetActive(false);
    }

    private void SetMemory_Protected()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!isOverflow_Protected)
            {
                if (!isStack_Protected)
                {
                    StartCoroutine("StackMemory_Protected");
                    StopCoroutine("DeleteMemory_Protected");
                }
                else
                {
                    StartCoroutine("DeleteMemory_Protected");
                    StopCoroutine("StackMemory_Protected");
                }
            }
            //Debug.Log("Input Q");

        }
        progressbar_Protected.transform.localScale =
         new Vector3(progressbar_Protected.transform.localScale.x, memory_Protected * 0.01f, progressbar_Protected.transform.localScale.z);
    }

    private IEnumerator StackMemory_Protected()
    {
        isStack_Protected = true;
        object_Protected.SetActive(true);
        progressbar_Protected.gameObject.GetComponent<Image>().color = Color.yellow;
        while (true)
        {
            if (memory_Protected >= 100)
            {
                object_Protected.SetActive(false);
                StartCoroutine("OverFlowMemory_Protected");
                StopCoroutine("StackMemory_Protected");
            }
            else
                //Debug.Log("StackMemory");
                memory_Protected += 1;
            yield return new WaitForSeconds(1f / object_Protected.GetComponent<Item_>().stackMemoryPerSec);
        }
    }

    private IEnumerator DeleteMemory_Protected()
    {
        isStack_Protected = false;
        object_Protected.SetActive(false);
        progressbar_Protected.gameObject.GetComponent<Image>().color = Color.white;

        while (true)
        {
            if (memory_Protected <= 0) { }
            else
                //Debug.Log("Delete");
                memory_Protected -= 1f;
            yield return new WaitForSeconds(1f / object_Protected.GetComponent<Item_>().deleteMemoryPerSec * 1.25f);
        }
    }

    private IEnumerator OverFlowMemory_Protected()
    {
        if (questManager.nowQuest == 4)
        {
            Debug.Log("UpdateQuest");
            questManager.GetUpdateQuest();
        }
        isOverflow_Protected = true;
        isStack_Protected = false;
        progressbar_Protected.gameObject.GetComponent<Image>().color = Color.red;
        while (memory_Protected > 0)
        {
            memory_Protected -= 1f;
            yield return new WaitForSeconds(1f / object_Protected.GetComponent<Item_>().deleteMemoryPerSec * 1.5f);
        }
        isOverflow_Protected = false;
    }

    private void SetMemory_DFS()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isOverflow_DFS)
            {
                if (!isStack_DFS)
                {
                    StartCoroutine("StackMemory_DFS");
                    StopCoroutine("DeleteMemory_DFS");
                }
                else
                {
                    StartCoroutine("DeleteMemory_DFS");
                    StopCoroutine("StackMemory_DFS");
                }
            }
            //Debug.Log("Input Q");

        }
        progressbar_DFS.transform.localScale =
         new Vector3(progressbar_DFS.transform.localScale.x, memory_DFS * 0.01f, progressbar_DFS.transform.localScale.z);
    }

    private IEnumerator StackMemory_DFS()
    {
        object_DFS.SetActive(true);
        isStack_DFS = true;
        progressbar_DFS.gameObject.GetComponent<Image>().color = Color.yellow;
        while (true)
        {
            if (memory_DFS >= 100)
            {
                object_DFS.SetActive(false);
                StartCoroutine("OverFlowMemory_DFS");
                StopCoroutine("StackMemory_DFS");
            }
            else
                //Debug.Log("StackMemory");
                memory_DFS += 1;
            yield return new WaitForSeconds(1f / object_DFS.GetComponent<Item_>().stackMemoryPerSec);
        }
    }

    private IEnumerator DeleteMemory_DFS()
    {
        object_DFS.SetActive(false);
        isStack_DFS = false;
        progressbar_DFS.gameObject.GetComponent<Image>().color = Color.white;
        while (true)
        {
            if (memory_DFS <= 0) { }
            else
                //Debug.Log("Delete");
                memory_DFS -= 1f;
            yield return new WaitForSeconds(1f / object_DFS.GetComponent<Item_>().deleteMemoryPerSec * 1.25f);
        }
    }

    private IEnumerator OverFlowMemory_DFS()
    {
        if (questManager.nowQuest == 4)
        {
            Debug.Log("UpdateQuest");
            questManager.GetUpdateQuest();
        }
        isOverflow_DFS = true;
        isStack_DFS = false;
        progressbar_DFS.gameObject.GetComponent<Image>().color = Color.red;
        while (memory_DFS > 0)
        {
            memory_DFS -= 1f;
            yield return new WaitForSeconds(1f / object_DFS.GetComponent<Item_>().deleteMemoryPerSec * 1.5f);
        }
        isOverflow_DFS = false;
    }

    public void DecreasePlayerGold(int price)
    {
        PlayerGold -= price;
    }

    private void SetMemory_Instantiate()
    {

        if (!isStoreActive || isStoreButtonDown)
        {
            if (!isOverflow_Instantiate)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    StartCoroutine("StackMemory_Instantiate");
                }
                if (!isStack_Instantiate && !isDeleting_Instantiate)
                {
                    StartCoroutine("DeleteMemory_Instantiate");
                }
            }
        }
        //Debug.Log("Input Q");

        progressbar_Instantiate.transform.localScale =
         new Vector3(progressbar_Instantiate.transform.localScale.x, memory_Instantiate * 0.01f, progressbar_Instantiate.transform.localScale.z);
    }

    private IEnumerator StackMemory_Instantiate()
    {
        if (isStoreButtonDown) { Debug.Log("Skip Stack"); }
        else
        {
            isStack_Instantiate = true;
            isDeleting_Instantiate = false;
            StopCoroutine("DeleteMemory_Instantiate");
            //object_Instantiate.SetActive(true);
            progressbar_Instantiate.gameObject.GetComponent<Image>().color = Color.yellow;

            //Debug.Log("StackMemory");

            // while (true)
            // {
            //     if (memory_Instantiate >= 100)
            //     {
            //         object_Instantiate.SetActive(false);
            //         StartCoroutine("OverFlowMemory_Instantiate");
            //         StopCoroutine("StackMemory_Instantiate");
            //     }
            //     else
            //         //Debug.Log("StackMemory");
            //         memory_Instantiate += 1;
            //     yield return new WaitForSeconds(1f / object_Instantiate.GetComponent<Item_>().stackMemoryPerSec);
            // }
            if (object_Instantiate != null)
                memory_Instantiate += object_Instantiate.GetComponent<Item_>().stackMemoryPerSec;
            if (memory_Instantiate >= 100)
            {
                memory_Instantiate = 100;
                object_Instantiate.SetActive(false);
                StartCoroutine("OverFlowMemory_Instantiate");
                StopCoroutine("DeleteMemory_Instantiate");
                StopCoroutine("StackMemory_Instantiate");
            }
            yield return new WaitForSeconds(1f);
            isStack_Instantiate = false;
        }
    }

    private IEnumerator DeleteMemory_Instantiate()
    {
        //Debug.Log("Delete Memory");
        //isStack_Instantiate = false;
        //object_Instantiate.SetActive(false);
        isDeleting_Instantiate = true;
        progressbar_Instantiate.gameObject.GetComponent<Image>().color = Color.white;

        while (true)
        {
            if (memory_Instantiate <= 0) { }
            else
                //Debug.Log("Delete");
                memory_Instantiate -= 1f;
            if (object_Instantiate != null)
                yield return new WaitForSeconds(1f / object_Instantiate.GetComponent<Item_>().deleteMemoryPerSec * 1.25f);
        }
    }

    private IEnumerator OverFlowMemory_Instantiate()
    {
        if (questManager.nowQuest == 4)
        {
            Debug.Log("UpdateQuest");
            questManager.GetUpdateQuest();
        }
        //Debug.Log("OverFlow");
        isOverflow_Instantiate = true;
        progressbar_Instantiate.gameObject.GetComponent<Image>().color = Color.red;
        while (memory_Instantiate > 0)
        {
            memory_Instantiate -= 1f;
            yield return new WaitForSeconds(1f / object_Instantiate.GetComponent<Item_>().deleteMemoryPerSec * 1.5f);
        }
        isOverflow_Instantiate = false;
        object_Instantiate.SetActive(true);
    }

    public void SetActiveObject_Protected()
    {
        object_Protected.SetActive(true);
    }
    public void SetInactiveObject_Protected()
    {
        object_Protected.SetActive(false);
    }

    public void SetActiveObject_DFS()
    {
        object_DFS.SetActive(true);
    }
    public void SetInactiveObject_DFS()
    {
        object_DFS.SetActive(false);
    }
    private void SpawnDummySystem()
    {
        if (isDummyDead)
        {
            isDummyDead = false;
            Invoke("SpawnDummy", 1);
        }
    }

    private void SpawnDummy()
    {
        GameObject _dummy = dummy;
        _dummy = Instantiate(_dummy, dummyTransform.position, Quaternion.identity);
    }
}
