using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class CartMove : MonoBehaviour
{
    Vector3 thisObjPosition;
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine("Corou1");
        DOVirtual.DelayedCall (3f, ()=>Setup());
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Setup()
    {
        this.gameObject.transform.DOLocalMove(new Vector3(0, 0, 0.9f), 5f).SetRelative();
    }

    /*
    private IEnumerator Corou1()//コルーチン1
    {
        Debug.Log("コルーチン");
        yield return new WaitForSeconds(2.0f);
        Debug.Log("2秒待った");
        Setup();
    }*/
}
