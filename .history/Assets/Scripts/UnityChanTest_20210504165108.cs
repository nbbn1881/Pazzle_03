using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ユニティちゃんの動作テスト
public class UnityChanTest : MonoBehaviour
{
    Animator animator;

    // スタート時に呼ばれる
    void Start ()
    {
        animator = GetComponent<Animator>();
    }


    // フレーム毎に呼ばれる
    //void Update () {
        /*
        // 前進
        if (Input.GetKey("up")) {
            transform.position += transform.forward * 0.01f;
            animator.SetBool("is_running", true);
        } else {
            animator.SetBool("is_running", false);
        }

        // 左右回転
        if (Input.GetKey ("left")) {
            transform.Rotate(0, -1, 0);
        } else if (Input.GetKey("right")) {
            transform.Rotate(0, 1, 0);
        }
        */
    //}
}


