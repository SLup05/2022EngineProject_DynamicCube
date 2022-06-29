using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Instantiate : Item_
{
    public GameObject bullet = null;
    public GameObject empty = null;
    //private float attackpoint = 0;
    public Camera camera = null;
    // Start is called before the first frame update
    void Start()
    {
        attackpoint = 6;
        stackMemoryPerSec = 15f;
        deleteMemoryPerSec = 15f;
        speicalPharse = "발사 탄수 증가";
    }

    // Update is called once per frame
    void Update()
    {
        Ray CamRay = camera.ScreenPointToRay(Input.mousePosition);

        RaycastHit raycastHit;
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("CLick");
            if (Physics.Raycast(CamRay, out raycastHit, 10000))
            {
                if (raycastHit.transform.tag != "UI")
                {
                    Debug.Log("Ray Hit");
                    Vector3 tempPos = raycastHit.point;
                    StartCoroutine("ShootBullet", tempPos);
                }
            }
            // GameObject _bullet = bullet;
            // Instantiate(_bullet, transform.position, Quaternion.identity);
            // Vector3 mousePos = Input.mousePosition;
            // Vector3 targetPos = camera.WorldToScreenPoint(mousePos);
            // _bullet.GetComponent<Instantiate_Bullet>().SetTargetPos(new Vector3(targetPos.x, 1, targetPos.z));
        }
    }

    private IEnumerator ShootBullet(Vector3 _tempPos)
    {
        for (int i = 0; i < specialCount + 1; i++)
        {
            GameObject _bullet = bullet;
            //GameObject _empty = empty;

            //Instantiate(_empty, new Vector3(tempPos.x, 1, tempPos.z), Quaternion.identity);

            //Debug.Log("Click " + new Vector3(tempPos.x, 1, tempPos.z));

            _bullet = Instantiate(_bullet, transform.position, Quaternion.identity);
            _bullet.transform.SetParent(null);
            _bullet.GetComponent<Instantiate_Bullet>().SetAttackPoint(attackpoint);
            _bullet.GetComponent<Instantiate_Bullet>().SetTargetPos(new Vector3(_tempPos.x, 1, _tempPos.z));
            yield return new WaitForSeconds(0.15f);
        }
    }
    protected virtual void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {

            //Debug.Log("get hit");
            other.gameObject.SendMessage("Hit", attackpoint);
        }
    }
    public void CheckSpecialUpgrade()
    {
        if (specialCount == 1)
        { }
        else if (specialCount > 0)
        {
        }
    }
}
