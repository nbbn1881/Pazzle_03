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
                    this.gameObject.transform.DOLocalMove(new Vector3(0, 0f, 0.5f), cartSpeed/2)
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
                    this.gameObject.transform.DOLocalMove(new Vector3(0.5f, 0f, 0f), cartSpeed/2)
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
        if(northRaycastHit.collider.gameObject.CompareTag("Piece_LeftToDown"))
        {
            Debug.Log("南から右折");
            FromSouthRightToDown();
        }
    }

    void FromEast()
    {
        //東から進めないピースならゲームオーバー
        if(northRaycastHit.collider.gameObject.CompareTag("Piece_Vertical")
        || northRaycastHit.collider.gameObject.CompareTag("Piece_LeftToUp")
        || northRaycastHit.collider.gameObject.CompareTag("Piece_LeftToDown")
        )
        {
            Debug.Log("GameOver");
        }

        if(northRaycastHit.collider.gameObject.CompareTag("Piece_Horizontal"))
        {
            Debug.Log("Horizontalなので直進");
            FromEastHorizontal();
        }

        if(northRaycastHit.collider.gameObject.CompareTag("Piece_RightToDown"))
        {
            Debug.Log("東から南");
            FromEastToNorth();
        }
    }




}
