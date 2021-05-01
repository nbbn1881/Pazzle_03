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


    // ボタンの制限を解除するコルーチン
    // private IEnumerator EnableButton()
    // {
    //     // 1秒後に解除
    //     yield return waitOneSecond;
    //     buttonEnabled = true;
    // }


    void Update()
    {

        // 制限中は動作させない
        // if (buttonEnabled == false)
        // {
        //     return;
        // }

        // 制限されていない場合
        // else
        // {
            // Debug.Log("Clicked !!");

            // ボタンを制限する
            // buttonEnabled = false;

            // // 一定時間経過後に解除
            // StartCoroutine(EnableButton());
            thisObjPosition = this.gameObject.transform.position;

            if (Input.GetKeyDown(KeyCode.LeftArrow) && x_MoveCount > -1)
            {
                //Debug.Log("下");
                saveThisObjPosition = this.gameObject.transform.position;
                thisObjPosition.x -= 1;
                this.gameObject.transform.position = thisObjPosition;
                x_MoveCount -= 1;
            }

            if (Input.GetKeyDown(KeyCode.RightArrow) && x_MoveCount < 4)
            {
                saveThisObjPosition = this.gameObject.transform.position;
                thisObjPosition.x += 1;
                this.gameObject.transform.position = thisObjPosition;
                x_MoveCount += 1;
            }

            if (Input.GetKeyDown(KeyCode.UpArrow) && z_MoveCount < 3)
            {
                saveThisObjPosition = this.gameObject.transform.position;
                thisObjPosition.z += 1;
                this.gameObject.transform.position = thisObjPosition;
                z_MoveCount += 1;
            }

            if (Input.GetKeyDown(KeyCode.DownArrow) && z_MoveCount > -2)
            {
                saveThisObjPosition = this.gameObject.transform.position;
                //thisObjPosition.z -= 1;
                this.gameObject.transform.DOLocalMove(new Vector3(0, 0, -1.0f),0.1f).SetRelative();
                this.gameObject.transform.position = thisObjPosition;
                z_MoveCount -= 1;
            }
        // }
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
