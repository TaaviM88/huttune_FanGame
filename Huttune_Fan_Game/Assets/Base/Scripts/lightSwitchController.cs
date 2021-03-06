using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightSwitchController : MonoBehaviour,IInteractable
{
    public RoomManager roomManager;
    Animator anime;
    float timeLeft = 0.5f;
    float originalTimeLeft;
    bool canBeFlipped = true;
    // Start is called before the first frame update
    void Start()
    {
        anime = GetComponent<Animator>();
        originalTimeLeft = timeLeft;
    }

    // Update is called once per frame
    void Update()
    {
        SwitchCooldown();

    }

    private void SwitchCooldown()
    {
        if (!canBeFlipped)
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
            }
            else
            {
                canBeFlipped = true;
                timeLeft = originalTimeLeft;
            }
        }
    }

    public void Interact()
    {
        if(canBeFlipped)
        {
            anime.SetBool("Light_Off", roomManager.Toggle());
            canBeFlipped = false;
        }
        
       
    }

}
