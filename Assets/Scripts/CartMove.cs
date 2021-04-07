using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class CartMove : MonoBehaviour
{
    //レイのベクトルを格納(斜め下45°)
	Vector3 northRay = new Vector3(0,-5.0f,5.0f);	//北
	Vector3 southRay = new Vector3(0,-5.0f,-5.0f);	//南
	Vector3 eastRay =  new Vector3(5,-5.0f,0);		//東
	Vector3 westRay =  new Vector3(-5,-5.0f,0);		//西

    float cartSpeed = 5.0f;//初期スピード

    // レイの衝突情報を格納する
    RaycastHit northRaycastHit = default;
    RaycastHit southRaycastHit = default;
    RaycastHit eastRaycastHit = default;
    RaycastHit westRaycastHit = default;

    // レイの距離
    [SerializeField]
    float distance = 1;

    // レイキャストに使用するマスク ※現在未使用
    [SerializeField]
    LayerMask mask = default;

    /// <summary>
    /// 更新
    /// </summary>
    void update()
    {
        // レイを生成
        Ray northRay = new Ray(transform.position, new Vector3(0,-5.0f,5.0f));
        Ray southRay = new Ray(transform.position, new Vector3(0,-5.0f,-5.0f));
        Ray eastRay = new Ray(transform.position, new Vector3(5,-5.0f,0));
        Ray westRay = new Ray(transform.position, new Vector3(-5,-5.0f,0));


        //レイが衝突したか判定する
        if (Physics.Raycast(northRay,out northRaycastHit, distance,mask,QueryTriggerInteraction.Collide))
        {
            // 衝突したオブジェクトの名前をログに表示する
            //Debug.Log(northRaycastHit.collider.gameObject.name);
        }
        if (Physics.Raycast(southRay,out southRaycastHit, distance,mask,QueryTriggerInteraction.Collide))
        {
            // 衝突したオブジェクトの名前をログに表示する
            //Debug.Log(northRaycastHit.collider.gameObject.name);
        }
         if (Physics.Raycast(eastRay,out eastRaycastHit, distance,mask,QueryTriggerInteraction.Collide))
        {
            // 衝突したオブジェクトの名前をログに表示する
            //Debug.Log(northRaycastHit.collider.gameObject.name);
        }
         if (Physics.Raycast(westRay,out westRaycastHit, distance,mask,QueryTriggerInteraction.Collide))
        {
            // 衝突したオブジェクトの名前をログに表示する
            //Debug.Log(northRaycastHit.collider.gameObject.name);
        }

        // レイを可視化する
        Debug.DrawRay(northRay.origin, northRay.direction * distance,Color.red);
        Debug.DrawRay(southRay.origin, southRay.direction * distance,Color.red);
        Debug.DrawRay(westRay.origin, westRay.direction * distance,Color.red);
        Debug.DrawRay(eastRay.origin, eastRay.direction * distance,Color.red);
    }

    // Start is called before the first frame update
    void Start()
    {
        DOVirtual.DelayedCall (3f, ()=>FromSouthVertical());//3秒遅延
    }

    //*****************************************************************************
    //進行メソッド
    //*****************************************************************************

    //垂直 南から
    void FromSouthVertical()
    {   //   このオブジェクトを                          この座標に             このスピードで
        this.gameObject.transform.DOLocalMove(new Vector3(0, 0, 0.9f), cartSpeed)
            .SetRelative()          //相対移動
            .SetEase(Ease.Linear)   //一定の速さ
			.OnComplete(() =>       //移動完了後にこのメソッドを実行
			{
                FromSouth();
			});
    }

    //垂直 北から
    void FromNorthVertical()
    {
        this.gameObject.transform.DOLocalMove(new Vector3(0, 0, -0.9f), cartSpeed)
            .SetRelative()          //相対移動
            .SetEase(Ease.Linear)  //一定の速さ
			.OnComplete(() =>
			{
                FromNorth();
			});
    }

    // ━ 東から
    void FromEastHorizontal()
    {
        this.gameObject.transform.DOLocalMove(new Vector3(-0.9f, 0, 0f), cartSpeed)
            .SetRelative()          //相対移動
            .SetEase(Ease.Linear)  //一定の速さ
			.OnComplete(() =>
			{
                Debug.Log("test");
                FromEast();
			});
    }

    // ━ 西から
    void FromWestHorizontal()
    {
        this.gameObject.transform.DOLocalMove(new Vector3(0.9f, 0, 0f), cartSpeed)
            .SetRelative()          //相対移動
            .SetEase(Ease.Linear)  //一定の速さ
			.OnComplete(() =>
			{
                Debug.Log("test");
                FromWest();
			});
    }

    // ┏ 東から
    void FromEastRightToDown()
    {
        var sequence = DOTween.Sequence();

        sequence.Append(
                    this.gameObject.transform.DOLocalMove(new Vector3(-0.6f, 0f, 0f), cartSpeed/2)
                        .SetRelative()
                        .SetEase(Ease.Linear))
                .Append(
                    this.gameObject.transform.DOLocalMove(new Vector3(0f, 0, -0.4f), cartSpeed/2)
                        .SetRelative()          //相対移動
                        .SetEase(Ease.Linear)  //一定の速さ
                        )
                .AppendCallback(() =>
                {
                    FromNorth();
                    Debug.Log("test");
                }
                );
    }

    // ┏ 南から
    void FromSouthRightToDown()
    {
        var sequence = DOTween.Sequence();

        sequence.Append(
                    this.gameObject.transform.DOLocalMove(new Vector3(0f, 0f, 0.6f), cartSpeed/2)
                        .SetRelative()
                        .SetEase(Ease.Linear))
                .Append(
                    this.gameObject.transform.DOLocalMove(new Vector3(0.4f, 0, 0f), cartSpeed/2)
                        .SetRelative()          //相対移動
                        .SetEase(Ease.Linear)  //一定の速さ
                        )
                .AppendCallback(() =>
                {
                    FromWest();
                }
                );
    }

    // ┗ 東から
    void FromEastRightToUp()
    {
        var sequence = DOTween.Sequence();

        sequence.Append(
                    this.gameObject.transform.DOLocalMove(new Vector3(-0.6f, 0f, 0f), cartSpeed/2)
                        .SetRelative()
                        .SetEase(Ease.Linear))
                .Append(
                    this.gameObject.transform.DOLocalMove(new Vector3(0f, 0, 0.4f), cartSpeed/2)
                        .SetRelative()          //相対移動
                        .SetEase(Ease.Linear)  //一定の速さ
                        )
                .AppendCallback(() =>
                {
                    FromSouth();
                    Debug.Log("test");
                }
                );
    }

    // ┗ 北から
    void FromNorthRightToUp()
    {
        var sequence = DOTween.Sequence();

        sequence.Append(
                    this.gameObject.transform.DOLocalMove(new Vector3(0, 0f, -0.6f), cartSpeed/2)
                        .SetRelative()
                        .SetEase(Ease.Linear))
                .Append(
                    this.gameObject.transform.DOLocalMove(new Vector3(0.4f, 0, 0), cartSpeed/2)
                        .SetRelative()          //相対移動
                        .SetEase(Ease.Linear)  //一定の速さ
                        )
                .AppendCallback(() =>
                {
                    FromWest();
                    Debug.Log("test");
                }
                );
    }


    // ┛ 西から
    void FromWestLeftToUp()
    {
        var sequence = DOTween.Sequence();

        sequence.Append(
                    this.gameObject.transform.DOLocalMove(new Vector3(0.6f, 0f, 0f), cartSpeed/2)
                        .SetRelative()
                        .SetEase(Ease.Linear))
                .Append(
                    this.gameObject.transform.DOLocalMove(new Vector3(0f, 0, 0.4f), cartSpeed/2)
                        .SetRelative()          //相対移動
                        .SetEase(Ease.Linear)  //一定の速さ
                        )
                .AppendCallback(() =>
                {
                    FromSouth();
                });
    }

    // ┛ 北から
    void FromNorthLeftToUp()
    {
        var sequence = DOTween.Sequence();

        sequence.Append(
                    this.gameObject.transform.DOLocalMove(new Vector3(0f, 0f,-0.6f), cartSpeed/2)
                        .SetRelative()
                        .SetEase(Ease.Linear))
                .Append(
                    this.gameObject.transform.DOLocalMove(new Vector3(-0.4f, 0, 0f), cartSpeed/2)
                        .SetRelative()          //相対移動
                        .SetEase(Ease.Linear)  //一定の速さ
                        )
                .AppendCallback(() =>
                {
                    FromEast();
                });
    }

    // ┓ 南から
    void FromSouthLeftToDown()
    {
        Debug.Log("FromSouthLeftToDownメソッド");
        var sequence = DOTween.Sequence();

        sequence.Append(
                    this.gameObject.transform.DOLocalMove(new Vector3(0, 0f, 0.6f), cartSpeed/2)
                        .SetRelative()
                        .SetEase(Ease.Linear))
                .Append(
                    this.gameObject.transform.DOLocalMove(new Vector3(-0.4f, 0, 0f), cartSpeed/2)
                        .SetRelative()          //相対移動
                        .SetEase(Ease.Linear)  //一定の速さ
                        )
                .AppendCallback(() =>
                {
                    FromEast();
                });
    }

    // ┓ 西から
    void FromWestLeftToDown()
    {
        var sequence = DOTween.Sequence();

        sequence.Append(
                    this.gameObject.transform.DOLocalMove(new Vector3(0.6f, 0f, 0f), cartSpeed/2)
                        .SetRelative()
                        .SetEase(Ease.Linear))
                .Append(
                    this.gameObject.transform.DOLocalMove(new Vector3(0f, 0, -0.4f), cartSpeed/2)
                        .SetRelative()          //相対移動
                        .SetEase(Ease.Linear)  //一定の速さ
                        )
                .AppendCallback(() =>
                {
                    FromNorth();
                });

    }


    //*****************************************************************************
    //判定メソッド
    //*****************************************************************************

    //北から
    void FromNorth()
    {
        //北から進めないピースならゲームオーバー
        if(southRaycastHit.collider.gameObject.CompareTag("Piece_Horizontal")
        || southRaycastHit.collider.gameObject.CompareTag("Piece_LeftToDown")
        || southRaycastHit.collider.gameObject.CompareTag("Piece_RightToDown")
        || northRaycastHit.collider.gameObject.CompareTag("Ground")
        || northRaycastHit.collider.gameObject.CompareTag("Player")
        )

        {
            //ゲームオーバーメソッドへ
            Debug.Log("GameOver");
        }

        if(southRaycastHit.collider.gameObject.CompareTag("Piece_Vertical"))
        {
            Debug.Log("Verticalなので直進");
            FromNorthVertical();
        }

        if(southRaycastHit.collider.gameObject.CompareTag("Piece_LeftToUp"))
        {
            Debug.Log("北から左折");
            FromNorthLeftToUp();
        }
        if(southRaycastHit.collider.gameObject.CompareTag("Piece_RightToUp"))
        {
            Debug.Log("北から右折");
            FromNorthRightToUp();
        }
    }

    //南から
    void FromSouth()
    {
        //南から進めないピースならゲームオーバー
        if(northRaycastHit.collider.gameObject.CompareTag("Piece_Horizontal")
        || northRaycastHit.collider.gameObject.CompareTag("Piece_LeftToUp")
        || northRaycastHit.collider.gameObject.CompareTag("Piece_RightToUp")
        || northRaycastHit.collider.gameObject.CompareTag("Ground")
        || northRaycastHit.collider.gameObject.CompareTag("Player")
        )
        {
            //ゲームオーバーメソッドへ
            Debug.Log("GameOver");
        }

        if(northRaycastHit.collider.gameObject.CompareTag("Piece_Vertical"))
        {
            Debug.Log("Verticalなので直進");
            FromSouthVertical();
        }

        if(northRaycastHit.collider.gameObject.CompareTag("Piece_LeftToDown"))
        {
            Debug.Log("南から左折");
            FromSouthLeftToDown();
        }
        if(northRaycastHit.collider.gameObject.CompareTag("Piece_RightToDown"))
        {
            Debug.Log("南から右折");
            FromSouthRightToDown();
        }
    }

    //東から
    void FromEast()
    {
        Debug.Log("test");
        //東から進めないピースならゲームオーバー
        if(westRaycastHit.collider.gameObject.CompareTag("Piece_Vertical")
        || westRaycastHit.collider.gameObject.CompareTag("Piece_LeftToUp")
        || westRaycastHit.collider.gameObject.CompareTag("Piece_LeftToDown")
        || northRaycastHit.collider.gameObject.CompareTag("Ground")
        || northRaycastHit.collider.gameObject.CompareTag("Player")
        )
        {
            Debug.Log("GameOver");
        }

        //東から西
        if(westRaycastHit.collider.gameObject.CompareTag("Piece_Horizontal"))
        {
            Debug.Log("Horizontalなので直進");
            FromEastHorizontal();
        }

        //東から南
        if(westRaycastHit.collider.gameObject.CompareTag("Piece_RightToDown"))
        {
            Debug.Log("東から南");
            FromEastRightToDown();
        }

        //東から北
        if(westRaycastHit.collider.gameObject.CompareTag("Piece_RightToUp"))
        {
            Debug.Log("東から北");
            FromEastRightToUp();
        }
    }

    //西から
    void FromWest()
    {
        //西から進めないピースならゲームオーバー
        if(eastRaycastHit.collider.gameObject.CompareTag("Piece_Vertical")
        || eastRaycastHit.collider.gameObject.CompareTag("Piece_RightToUp")
        || eastRaycastHit.collider.gameObject.CompareTag("Piece_RightToDown")
        || northRaycastHit.collider.gameObject.CompareTag("Ground")
        || northRaycastHit.collider.gameObject.CompareTag("Player")
        )
        {
            Debug.Log("GameOver");
        }

        //西から東
        if(eastRaycastHit.collider.gameObject.CompareTag("Piece_Horizontal"))
        {
            Debug.Log("Horizontalなので直進");
            FromWestHorizontal();
        }

        //西から南
        if(eastRaycastHit.collider.gameObject.CompareTag("Piece_LeftToDown"))
        {
            Debug.Log("西から南");
            FromWestLeftToDown();
        }

        //西から北
        if(eastRaycastHit.collider.gameObject.CompareTag("Piece_LeftToUp"))
        {
            Debug.Log("西から北");
            FromWestLeftToUp();
        }
    }
}
