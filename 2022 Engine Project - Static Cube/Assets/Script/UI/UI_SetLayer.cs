using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_SetLayer : MonoBehaviour
{
    public int Layer;
    // Start is called before the first frame update
    void Start()
    {
        if (Layer == 10)
            transform.SetAsLastSibling();
        else if (Layer == -1)
            transform.SetAsFirstSibling();
        else
            transform.SetSiblingIndex(Layer);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
