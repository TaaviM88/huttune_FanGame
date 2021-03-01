
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class BoogieMaryController : MonoBehaviour
{
    NavMeshAgent agent;
    public Animator anime;

    public Transform playerLocation;
    Transform lastknowLocation;
    Transform cheeseLocation;
    Vector3 target;
    BoogieMaryStoryState storyState = BoogieMaryStoryState.Sleeping;
    BoogieMaryMoveState moveState = BoogieMaryMoveState.None;
    BoogieMaryActionState actionState = BoogieMaryActionState.None;

    bool canMove = true;
    bool isWalking = false;
    bool reachedTarget = false;
    public bool isSleepping = true;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

    }

    public void Upadate()
    {
        if(!canMove)
        {
            return;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!canMove)
        {
            return;
        }

        ProcessRaycast();

        switch (moveState)
        {
            case BoogieMaryMoveState.None:
                target = playerLocation.position;
                break;
            case BoogieMaryMoveState.Wandering:
                break;
            case BoogieMaryMoveState.ChasingPlayer:
                break;
            case BoogieMaryMoveState.ChasingCheese:
                break;
            case BoogieMaryMoveState.MoveToTarget:
                break;
            default:
                break;
        }

        MoveToTarget(target);
    }

    private void ProcessRaycast()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.forward, out hit, 5))
        { 
                hit.collider.gameObject.GetComponent<IInteractable>()?.Interact();
                
        }

        
    }

    private void MoveToTarget(Vector3 newtarget)
    {
        agent.SetDestination(newtarget);
        transform.LookAt(newtarget, Vector3.up);

        if (agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance <= agent.stoppingDistance)
        {
            print("Perillä");
            reachedTarget = true;
        }
    }

    public void Attack()
    {

    }

    public void AwakeMary()
    {

    }

    public void TriggerAnimation()
    {

    }

    public void StopMary()
    {

    }

    public void FlipOver()
    {

    }

}
