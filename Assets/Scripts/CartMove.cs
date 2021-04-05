using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class CartMove : MonoBehaviour
{

	Vector3 northRay = new Vector3(0,-5.0f,5.0f);	//北
	Vector3 SouthRay = new Vector3(0,-5.0f,-5.0f);	//南
	Vector3 EastRay =  new Vector3(5,-5.0f,0);		//東
	Vector3 WestRay =  new Vector3(-5,-5.0f,0);		//西

	RaycastHit hit;

	[SerializeField]
	bool isEnable = false;

	void OnDrawGizmos()
	{
		if (isEnable == false)
			return;

		var scale = transform.lossyScale.x * 0.5f;

		var isHit = Physics.Raycast (transform.position, northRay, out hit, 10);
		if (isHit) {
			//Gizmos.DrawRay (transform.position, transform.forward * hit.distance);
			Gizmos.color = Color.red;
			Gizmos.DrawRay (transform.position, northRay * hit.distance);
		} else {
			Gizmos.DrawRay (transform.position, transform.forward * 10);
		}
	}




    // Start is called before the first frame update
    void Start()
    {
        DOVirtual.DelayedCall (3f, ()=>Setup());//3秒遅延
    }



    void Setup()
    {
        this.gameObject.transform.DOLocalMove(new Vector3(0, 0, 0.9f), 5f)//5秒かけて移動
            .SetRelative()          //相対移動
            .SetEase(Ease.Linear)  //一定の速さ
			.OnComplete(() =>
			{
				NextMove();
			});
    }

    void NextMove()
    {

    }


}
