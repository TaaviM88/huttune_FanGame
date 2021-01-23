using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCapsuleColliderManager : MonoBehaviour
{
    CapsuleCollider capsuleCollider;
    CharacterController controller;
    public Cloth cloth;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        capsuleCollider.isTrigger = true;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
 
    }


    private void OnTriggerEnter(Collider other)
    {

        cloth = other.gameObject.GetComponent<Cloth>();
        if(cloth != null)
        {
            cloth.capsuleColliders[0] = capsuleCollider;
            capsuleCollider.isTrigger = false;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        //if(cloth != null)
        //{
        //    cloth.capsuleColliders[0] = null;
        //    cloth = null;
        //}

        capsuleCollider.isTrigger = true;
    }

}
