using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; //import
using TMPro;
using UnityEngine.UI;

public class UI_DamageEffect : MonoBehaviour
{
    private Material material = null;

    public int getDamage;
    private TextMeshPro textMeshPro;
    private Color textColor;

    private float destroyCount = 0f;

    private float fadeSpeed = 2f;
    private int cnt = 0;
    void Start()
    {

        textMeshPro = GetComponent<TextMeshPro>();
        //Debug.Log(getDamage);
        textMeshPro.text = "-" + getDamage.ToString();
        material = GetComponent<Material>();
        DOTween.Init(false, false, LogBehaviour.Default).SetCapacity(100, 20);
        //Destroy(gameObject, 0.7f);
        //textMeshPro.material.DOFade(0, 1);
        transform.DOLocalMove(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z + 1), 0.7f).SetEase(Ease.InOutQuart);
        //Destroy(gameObject);

        textColor = textMeshPro.color;
    }

    void Update()
    {
        destroyCount += Time.deltaTime;
        textColor.a = Mathf.Lerp(textMeshPro.color.a, 0, Time.deltaTime * fadeSpeed);
        textMeshPro.color = textColor;
        if (destroyCount >= 1.5f)
            Destroy(gameObject);
    }
}
