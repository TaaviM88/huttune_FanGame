using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnumManager : MonoBehaviour
{
    public PlayerMoveState moveState { get; set; }
    public PlayerStandState standState { get; set; }
    public PlayerActionState actionState { get; set;}
}
