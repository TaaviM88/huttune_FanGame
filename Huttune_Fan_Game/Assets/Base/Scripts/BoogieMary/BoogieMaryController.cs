
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class BoogieMaryController : MonoBehaviour
{
    NavMeshAgent agent;
    public Animator anime;
    public Transform raycastStartPoint;
    public Transform playerLocation;
    public float doorOpenRange = 1;
    public float TripOverCooldown = 5;
    Transform lastknowLocation;
    Transform cheeseLocation;
    Vector3 target;
    BoogieMaryStoryState storyState = BoogieMaryStoryState.Sleeping;
    BoogieMaryMoveState moveState = BoogieMaryMoveState.None;
    BoogieMaryActionState actionState = BoogieMaryActionState.None;

    bool canMove = true;
    bool isWalking = false;
    bool reachedTarget = false;
    bool isTrippingTimerOn = false;
    bool canAttack = true;
    public bool isSleepping = true;

    float agentOriginalSpeed = 0;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agentOriginalSpeed = agent.speed;
    }

    public void Update()
    {
        anime.SetFloat("MoveSpeed", agent.speed);

        if (!canMove)
        {
            return;
        }

        if(!isTrippingTimerOn)
        {
            StartCoroutine(TripOver());
        }
    }

    IEnumerator TripOver()
    {

        print("Kokeillaan kaatuisko Mary");
        bool tripOver = UnityEngine.Random.Range(0f, 1f) > 0.9;

        if(tripOver)
        {
            canMove = false;
            agent.speed = 0;
            //anime.SetFloat("MoveSpeed", agent.speed);
            anime.SetTrigger("BoogieMaryTrip");
            agent.isStopped = true;
        }

        isTrippingTimerOn = true;
        yield return new WaitForSeconds(TripOverCooldown);
        
        if(tripOver)
        {
            canMove = true;
            agent.isStopped = false;
            agent.speed = agentOriginalSpeed;
        }

        isTrippingTimerOn = false;
        canAttack = true;
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
        if (Physics.Raycast(raycastStartPoint.position, transform.forward, out hit, doorOpenRange))
        {
            DoorScript door = hit.collider.gameObject.GetComponent<DoorScript>();
            if(door != null)
            {
                if(door.isLocked)
                {
                    return;
                }
                else
                {
                    door.Interact();
                }
            }
                
        }
    }

    private void MoveToTarget(Vector3 newtarget)
    {
        agent.SetDestination(newtarget);
        transform.LookAt(newtarget, Vector3.up);

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            reachedTarget = true;
            agent.isStopped = true;
            agent.speed = 0;
            Attack();
        }
        else
        {

            if (agent.isStopped)
            {
                agent.speed = agentOriginalSpeed;
                agent.isStopped = false;
            }
        }
        
    }

    public void Attack()
    {
        if(canAttack)
        {
            anime.SetTrigger("BoogieMaryAttack");
            canAttack = false;
        }

        
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
