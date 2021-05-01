using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class PlayerMove : MonoBehaviour
{
    [Header("アイテム取得時に鳴らすSE")] public AudioClip CursorMove3;
    // Start is called before the first frame update


    int x_MoveCount = 1;//初期位置
    int z_MoveCount = 1;
    Vector3 thisObjPosition;
    Vector3 saveThisObjPosition;

    private float time;//同時押し対策

    void Start()
    {
        time = 1.0f;
    }


    void Update()
    {
        time += TIme.deltaTIme;
        if( time >= 0.1f)
        {
            thisObjPosition = this.gameObject.transform.position;

            if (Input.GetKeyDown(KeyCode.LeftArrow) && x_MoveCount > -1)
            {
                //Debug.Log("下");
                saveThisObjPosition = this.gameObject.transform.position;
                thisObjPosition.x -= 1;
                this.gameObject.transform.position = thisObjPosition;
                x_MoveCount -= 1;
                time = 0.0f;
            }

            if (Input.GetKeyDown(KeyCode.RightArrow) && x_MoveCount < 4)
            {
                saveThisObjPosition = this.gameObject.transform.position;
                thisObjPosition.x += 1;
                this.gameObject.transform.position = thisObjPosition;
                x_MoveCount += 1;
                time = 0.0f;
            }

            if (Input.GetKeyDown(KeyCode.UpArrow) && z_MoveCount < 3)
            {
                saveThisObjPosition = this.gameObject.transform.position;
                thisObjPosition.z += 1;
                this.gameObject.transform.position = thisObjPosition;
                z_MoveCount += 1;
                time = 0.0f;
            }

            if (Input.GetKeyDown(KeyCode.DownArrow) && z_MoveCount > -2)
            {
                saveThisObjPosition = this.gameObject.transform.position;
                //thisObjPosition.z -= 1;
                this.gameObject.transform.DOLocalMove(new Vector3(0, 0, -1.0f),0.1f).SetRelative();
                this.gameObject.transform.position = thisObjPosition;
                z_MoveCount -= 1;
                time = 0.0f;
            }

        }


    }

    void OnTriggerStay(Collider other)
    {
        //衝突してほしいゲームオブジェクトでなければ抜ける
        //if (other.gameObject.tag != "Piece")
        //{
        //    return;
        //}

        //other.transform.position = saveThisObjPosition;
        other.transform.DOMove((saveThisObjPosition),0.1f);
    }
}
