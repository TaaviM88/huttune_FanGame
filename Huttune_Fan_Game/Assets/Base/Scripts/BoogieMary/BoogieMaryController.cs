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

    BoogieMaryStoryState storyState = BoogieMaryStoryState.Sleeping;
    BoogieMaryMoveState moveState = BoogieMaryMoveState.None;
    BoogieMaryActionState actionState = BoogieMaryActionState.None;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(playerLocation.position);
    }
}
