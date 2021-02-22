using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : MonoBehaviour
{
    public List<Transform> waypoints = new List<Transform>();
    public float moveSpeed = 3;
    public float cooldownMin = 3;
    public float cooldownMax = 6;
    bool isMoving = false;
    bool canPlaySound = true;
    Vector3 targetPosition;
    float t;
    float cooldown;
    float soundsCooldown;
    PlayRandomSound sounds;
    // Start is called before the first frame update
    void Start()
    {
        sounds = GetComponent<PlayRandomSound>();
        WanderToNextPoint();        
    }

    // Update is called once per frame
    void Update()
    {
        if(isMoving)
        {
            if(Vector3.Distance(transform.position,targetPosition) < 0.01)
            {
                StartRandomCooldown();
                isMoving = false;
                return;
            }

            t += Time.deltaTime * moveSpeed;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * moveSpeed);
            PlaySound();
        }
    }

    private void PlaySound()
    {
        if (sounds.IsPlayingSound()) 
        {
            return;
        }

        if(canPlaySound)
        {
            sounds.RandomSound();
            canPlaySound = false;
        }
        else
        {
            canPlaySound = UnityEngine.Random.Range(0f, 1f) > 0.9;
        }
    }

    private void StartRandomCooldown()
    {
        cooldown = UnityEngine.Random.Range(cooldownMin, cooldownMax);
        t = 0;
        StartCoroutine(Cooldown());
    }

    public void WanderToNextPoint()
    {
        targetPosition = GetRandomPointFromList(); 
    }

    private Vector3 GetRandomPointFromList()
    {
        int rndIndex = UnityEngine.Random.Range(0, waypoints.Count - 1);
        isMoving = true;
        return waypoints[rndIndex].position;
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldown);
        WanderToNextPoint();
    }
}
