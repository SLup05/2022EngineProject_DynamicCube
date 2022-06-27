using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestControl : MonoBehaviour
{
    private float speed = 15;
    private bool isColWithPlane = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!isColWithPlane)
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        else
        {

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Plane"))
        {
            isColWithPlane = true;
            //            Debug.Log("StopChest");
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
