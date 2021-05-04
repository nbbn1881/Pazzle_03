using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowChar : MonoBehaviour
{
    private UnityEngine.AI.NavMeshAgent agent;
    // 追いかけるキャラクター
    [SerializeField]
    private GameObject player;
    private Animator animator;
    //　到着したとする距離
    [SerializeField]
    private float arrivedDistance = 0f;
    //　追いかけ始める距離
    [SerializeField]
    private float followDistance = 1f;

    void Start()
    {
        animator = GetComponent <Animator> ();
	    agent = GetComponent <UnityEngine.AI.NavMeshAgent> ();
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(player.transform.position);

        //　到着している時
        //if(agent.remainingDistance < arrivedDistance) {
            //agent.Stop();
            //　Unity5.6バージョン以降の停止
            //agent.isStopped = true;
            //animator.SetFloat("Speed", 0f);
        //　到着していない時で追いかけ出す距離になったら
        /*} else if(agent.remainingDistance > followDistance) {*/
            //agent.Resume();
            //　Unity5.6バージョン以降の再開
            agent.isStopped = false;
            animator.SetFloat(6.0f, agent.desiredVelocity.magnitude);
        /*}*/
    }
}
