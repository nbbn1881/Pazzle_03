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

    float cartSpeed = 5.0f;

    // レイの衝突情報を格納する
    RaycastHit northRaycastHit = default;
    RaycastHit southRaycastHit = default;
    RaycastHit eastRaycastHit = default;
    RaycastHit westRaycastHit = default;





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
    {
        this.gameObject.transform.DOLocalMove(new Vector3(0, 0, 0.9f), cartSpeed)
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
        this.gameObject.transform.DOLocalMove(new Vector3(0, 0, -0.9f), cartSpeed)
            .SetRelative()          //相対移動
            .SetEase(Ease.Linear)  //一定の速さ
			.OnComplete(() =>
			{
                //FromNorth();
			});
    }

    //水平 東から
    void FromEastHorizontal()
    {
        this.gameObject.transform.DOLocalMove(new Vector3(0.9f, 0, 0f), cartSpeed)
            .SetRelative()          //相対移動
            .SetEase(Ease.Linear)  //一定の速さ
			.OnComplete(() =>
			{
                //FromWest();
			});
    }

    //水平 西から
    void FromWestHorizontal()
    {
        this.gameObject.transform.DOLocalMove(new Vector3(-0.9f, 0, 0f), cartSpeed)
            .SetRelative()          //相対移動
            .SetEase(Ease.Linear)  //一定の速さ
			.OnComplete(() =>
			{
                //FromEast();
			});
    }

    //左と下 南から
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
                }
                );
    }

    ///左と下 西から
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
                }
                );
    }

    ///右と下 東から
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
                }
                );
    }

    ///右と下 南から
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
            Debug.Log("南から左折");
            //FromNorthLeftToUp();
        }
        if(southRaycastHit.collider.gameObject.CompareTag("Piece_RightToUp"))
        {
            Debug.Log("南から右折");
            //FromNorthRightToUp();
        }
    }

    //南から
    void FromSouth()
    {
        //南から進めないピースならゲームオーバー
        if(northRaycastHit.collider.gameObject.CompareTag("Piece_Horizontal")
        || northRaycastHit.collider.gameObject.CompareTag("Piece_LeftToUp")
        || northRaycastHit.collider.gameObject.CompareTag("Piece_RightToUp")
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
        //東から進めないピースならゲームオーバー
        if(eastRaycastHit.collider.gameObject.CompareTag("Piece_Vertical")
        || eastRaycastHit.collider.gameObject.CompareTag("Piece_LeftToUp")
        || eastRaycastHit.collider.gameObject.CompareTag("Piece_LeftToDown")
        )
        {
            Debug.Log("GameOver");
        }

        //東から西
        if(eastRaycastHit.collider.gameObject.CompareTag("Piece_Horizontal"))
        {
            Debug.Log("Horizontalなので直進");
            FromEastHorizontal();
        }

        //東から南
        if(eastRaycastHit.collider.gameObject.CompareTag("Piece_RightToDown"))
        {
            Debug.Log("東から南");
            FromEastRightToDown();
        }

        //東から北
        if(eastRaycastHit.collider.gameObject.CompareTag("Piece_RightToUp"))
        {
            Debug.Log("東から北");
            //FromEastRightToUp();
        }
    }

    //西から
    void FromWest()
    {
        //西から進めないピースならゲームオーバー
        if(westRaycastHit.collider.gameObject.CompareTag("Piece_Vertical")
        || westRaycastHit.collider.gameObject.CompareTag("Piece_RightToUp")
        || westRaycastHit.collider.gameObject.CompareTag("Piece_RightToDown")
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
            //FromEastLeftToDown();
        }

        //西から北
        if(eastRaycastHit.collider.gameObject.CompareTag("Piece_LeftToUp"))
        {
            Debug.Log("西から北");
            //FromEastLeftToUp();
        }
    }




}
