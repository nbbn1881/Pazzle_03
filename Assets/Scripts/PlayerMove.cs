using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class PlayerMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    int x_MoveCount = 1;//初期位置
    int z_MoveCount = 1;
    Vector3 thisObjPosition;
    Vector3 saveThisObjPosition;

    void Update()
    {

        if (Input.anyKey == false)
        {
            return;
        }

        thisObjPosition = this.gameObject.transform.position;

        if (Input.GetKeyDown(KeyCode.LeftArrow) && x_MoveCount > -1)
        {
            saveThisObjPosition = this.gameObject.transform.position;//移動前の位置を保存してからポジションを変更
            this.gameObject.transform.DOLocalMove(new Vector3(-1, 0, 0), 0.1f).SetRelative();
            this.gameObject.transform.position = thisObjPosition;
            x_MoveCount -= 1;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && x_MoveCount < 4)
        {
            saveThisObjPosition = this.gameObject.transform.position;
            this.gameObject.transform.DOLocalMove(new Vector3(1, 0, 0), 0.1f).SetRelative();
            //thisObjPosition.x += 1;
            this.gameObject.transform.position = thisObjPosition;
            x_MoveCount += 1;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && z_MoveCount < 3)
        {
            saveThisObjPosition = this.gameObject.transform.position;
            this.gameObject.transform.DOLocalMove(new Vector3(0, 0, 1), 0.1f).SetRelative();
            //thisObjPosition.z += 1;
            this.gameObject.transform.position = thisObjPosition;
            z_MoveCount += 1;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) && z_MoveCount > -2)
        {
            saveThisObjPosition = this.gameObject.transform.position;
            this.gameObject.transform.DOLocalMove(new Vector3(0, 0, -1), 0.1f).SetRelative();
            //thisObjPosition.z -= 1;
            this.gameObject.transform.position = thisObjPosition;
            z_MoveCount -= 1;
        }
    }

    void OnTriggerStay(Collider other)
    {
        //衝突してほしいゲームオブジェクトでなければ抜ける
        if (other.gameObject.tag != "Piece")
        {
            return;
        }

        //other.transform.position = saveThisObjPosition;
        other.transform.DOMove((saveThisObjPosition),0.1f);
    }
}
