using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class CartMove : MonoBehaviour
{

	Vector3 northRay = new Vector3(0,-5.0f,5.0f);	//北
	Vector3 southRay = new Vector3(0,-5.0f,-5.0f);	//南
	Vector3 eastRay =  new Vector3(5,-5.0f,0);		//東
	Vector3 westRay =  new Vector3(-5,-5.0f,0);		//西

    float cartSpeed = 7.0f;//初期スピード

    // レイの衝突情報を格納する
    RaycastHit northRaycastHit = default;
    RaycastHit southRaycastHit = default;
    RaycastHit eastRaycastHit = default;
    RaycastHit westRaycastHit = default;


    public void OnCallChangeFace()
    {
        //エラー回避用
    }


    // レイの距離
    [SerializeField]
    float distance = 1;

    // レイキャストに使用するマスク
    [SerializeField]
    LayerMask mask = default;

    /// <summary>
    /// 更新
    /// </summary>
    void Update()
    {
        // transformを取得
        Transform myTransform = this.transform;

        //座標を取得
        Vector3 pos = myTransform.position;
        pos.x += 0f;    // x座標へ0.01加算
        pos.y += 1f;    // y座標へ0.01加算
        pos.z += 0f;    // z座標へ0.01加算

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
        DOVirtual.DelayedCall (3f, ()=>CartStart());//3秒遅延
    }

    void CartStart()
    {
        Debug.Log("CartStart");
        this.gameObject.transform.DOLocalMove(new Vector3(0, 0, 0.8f), cartSpeed)
            .SetRelative()          //相対移動
            .SetEase(Ease.Linear)  //一定の速さ
			.OnComplete(() =>
			{
                FromSouth();//
			});
    }

    //*****************************************************************************
    //進行メソッド
    //*****************************************************************************

    //垂直 南から
    void FromSouthVertical()
    {
        Debug.Log("FromSouthVertical");
        this.gameObject.transform.DOLocalMove(new Vector3(0, 0, 1.0f), cartSpeed)
            .SetRelative()          //相対移動
            .SetEase(Ease.Linear)  //一定の速さ
			.OnComplete(() =>
			{
                FromSouth();//
			});
    }

    //垂直 北から
    void FromNorthVertical()
    {
        Debug.Log("FromNorthVertical");
        this.gameObject.transform.DOLocalMove(new Vector3(0, 0, -1.0f), cartSpeed)
            .SetRelative()          //相対移動
            .SetEase(Ease.Linear)  //一定の速さ
			.OnComplete(() =>
			{
                FromNorth();
			});
    }

    //水平 東から
    void FromEastHorizontal()
    {
        Debug.Log("FromEastHorizontal");
        this.gameObject.transform.DOLocalMove(new Vector3(-1.0f, 0, 0f), cartSpeed)
            .SetRelative()          //相対移動
            .SetEase(Ease.Linear)  //一定の速さ
			.OnComplete(() =>
			{
                FromEast();
			});
    }

    //水平 西から
    void FromWestHorizontal()
    {
        Debug.Log("FromWestHorizontal");
        this.gameObject.transform.DOLocalMove(new Vector3(1.0f, 0, 0f), cartSpeed)
            .SetRelative()          //相対移動
            .SetEase(Ease.Linear)  //一定の速さ
			.OnComplete(() =>
			{
                FromWest();
			});
    }

    //左と下 南から
    void FromSouthLeftToDown()
    {
        Debug.Log("FromSouthLeftToDown");
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
                .OnComplete(() =>
                {
                    FromEast();
                });
    }

    ///左と下 西から
    void FromWestLeftToDown()
    {
        Debug.Log("FromWestLeftToDown");
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
                .OnComplete(() =>
                {
                    FromNorth();
                }
                );
    }

    ///左と上 西から
    void FromWestLeftToUp()
    {
        Debug.Log("FromWestLeftToUp");
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
                .OnComplete(() =>
                {
                    FromSouth();
                }
                );
    }

    ///左と上 北から
    void FromNorthLeftToUp()
    {
        Debug.Log("FromNorthLeftToUp");
        var sequence = DOTween.Sequence();

        sequence.Append(
                    this.gameObject.transform.DOLocalMove(new Vector3(0f, 0f, -0.6f), cartSpeed/2)
                        .SetRelative()
                        .SetEase(Ease.Linear))
                .Append(
                    this.gameObject.transform.DOLocalMove(new Vector3(-0.4f, 0, 0f), cartSpeed/2)
                        .SetRelative()          //相対移動
                        .SetEase(Ease.Linear)  //一定の速さ
                        )
                .OnComplete(() =>
                {
                    FromEast();
                }
                );
    }


    ///右と下 東から
    void FromEastRightToDown()
    {
        Debug.Log("FromEastRightToDown");
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
                .OnComplete(() =>
                {
                    FromNorth();
                }
                );
    }

    ///右と下 南から
    void FromSouthRightToDown()
    {
        Debug.Log("FromSouthRightToDown");
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
                .OnComplete(() =>
                {
                    FromWest();
                }
                );
    }

    ///右と上 北から
    void FromNorthRightToUp()
    {
        Debug.Log("FromNorthRightToU");
        var sequence = DOTween.Sequence();

        sequence.Append(
                    this.gameObject.transform.DOLocalMove(new Vector3(0f, 0f, -0.6f), cartSpeed/2)
                        .SetRelative()
                        .SetEase(Ease.Linear))
                .Append(
                    this.gameObject.transform.DOLocalMove(new Vector3(0.4f, 0, 0f), cartSpeed/2)
                        .SetRelative()          //相対移動
                        .SetEase(Ease.Linear)  //一定の速さ
                        )
                .OnComplete(() =>
                {
                    FromWest();
                }
                );
    }

    ///右と上 東から
    void FromEastRightToUp()
    {
        Debug.Log("FromEastRightToUp");
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
                .OnComplete(() =>
                {
                    FromSouth();
                }
                );
    }




    //*****************************************************************************
    //判定メソッド
    //*****************************************************************************

    //北から
    void FromNorth()
    {
        Debug.Log("FromNorth()");
        //北から進めないピースならゲームオーバー
        if(southRaycastHit.collider.gameObject.CompareTag("Piece_Horizontal")
        || southRaycastHit.collider.gameObject.CompareTag("Piece_LeftToDown")
        || southRaycastHit.collider.gameObject.CompareTag("Piece_RightToDown")
        )

        {
            //ゲームオーバーメソッドへ
            Debug.Log("GameOver");
            //TODO:シーン移動
        }

        if(southRaycastHit.collider.gameObject.CompareTag("Piece_Vertical"))
        {
            Debug.Log("FromNorthVerticalへ");
            FromNorthVertical();
        }

        if(southRaycastHit.collider.gameObject.CompareTag("Piece_LeftToUp"))
        {
            Debug.Log("FromNorthLeftToUpへ");
            FromNorthLeftToUp();
        }
        if(southRaycastHit.collider.gameObject.CompareTag("Piece_RightToUp"))
        {
            Debug.Log("FromNorthRightToUpへ");
            FromNorthRightToUp();
        }
    }

    //南から
    void FromSouth()
    {
        Debug.Log("FromSouth()");
        //南から進めないピースならゲームオーバー
        if(northRaycastHit.collider.gameObject.CompareTag("Piece_Horizontal")
        || northRaycastHit.collider.gameObject.CompareTag("Piece_LeftToUp")
        || northRaycastHit.collider.gameObject.CompareTag("Piece_RightToUp")
        )

        {
            //ゲームオーバーメソッドへ
            Debug.Log("GameOver");
            //TODO:シーン移動
        }

        if(northRaycastHit.collider.gameObject.CompareTag("Piece_Vertical"))
        {
            Debug.Log("FromSouthVerticalへ");
            FromSouthVertical();
        }

        if(northRaycastHit.collider.gameObject.CompareTag("Piece_LeftToDown"))
        {
            Debug.Log("FromSouthLeftToDownへ");
            FromSouthLeftToDown();
        }
        if(northRaycastHit.collider.gameObject.CompareTag("Piece_RightToDown"))
        {
            Debug.Log("FromSouthRightToDownへ");
            FromSouthRightToDown();
        }
    }

    //東から
    void FromEast()
    {
        Debug.Log("FromEast()");
        //東から進めないピースならゲームオーバー
        if(westRaycastHit.collider.gameObject.CompareTag("Piece_Vertical")
        || westRaycastHit.collider.gameObject.CompareTag("Piece_LeftToUp")
        || westRaycastHit.collider.gameObject.CompareTag("Piece_LeftToDown")
        )
        {
            Debug.Log("GameOver");
            //TODO:シーン移動
        }

        //東から西
        if(westRaycastHit.collider.gameObject.CompareTag("Piece_Horizontal"))
        {
            Debug.Log("FromEastHorizontalへ");
            FromEastHorizontal();
        }

        //東から南
        if(westRaycastHit.collider.gameObject.CompareTag("Piece_RightToDown"))
        {
            Debug.Log("FromEastRightToDownへ");
            FromEastRightToDown();
        }

        //東から北
        if(westRaycastHit.collider.gameObject.CompareTag("Piece_RightToUp"))
        {
            Debug.Log("FromEastRightToUpへ");
            FromEastRightToUp();
        }
        Debug.Log("FromEast終了");
    }

    //西から
    void FromWest()
    {
        Debug.Log("FromWest()");
        //西から進めないピースならゲームオーバー
        if(eastRaycastHit.collider.gameObject.CompareTag("Piece_Vertical")
        || eastRaycastHit.collider.gameObject.CompareTag("Piece_RightToUp")
        || eastRaycastHit.collider.gameObject.CompareTag("Piece_RightToDown")
        )
        {
            Debug.Log("GameOver");
            //TODO:シーン移動
        }

        //西から東
        if(eastRaycastHit.collider.gameObject.CompareTag("Piece_Horizontal"))
        {
            Debug.Log("FromWestHorizontalへ");
            FromWestHorizontal();
        }

        //西から南
        if(eastRaycastHit.collider.gameObject.CompareTag("Piece_LeftToDown"))
        {
            Debug.Log("FromWestLeftToDownへ");
            FromWestLeftToDown();
        }

        //西から北
        if(eastRaycastHit.collider.gameObject.CompareTag("Piece_LeftToUp"))
        {
            Debug.Log("FromWestLeftToUpへ");
            FromWestLeftToUp();
        }
    }
}
