#region PlayerStates
public enum PlayerMoveState
{
    Idle,
    Walk,
    Run
}

public enum PlayerStandState
{
    Stand,
    Crouch
}

public enum PlayerActionState
{
    nothing,
    puzzle,
    reading,
    peeping,
    inspecting
}
#endregion

#region GameStates
public enum GameState
{
    On,
    Pause,
    Exit
}
#endregion

public enum DoorState
{
    Close,
    Open,
    Locked,
    Moving //Eli joku animaatio käynnissä
}

public enum DoorLockType
{
    None,
    Puzzle,
    Key
}
#region WashingMachine
public enum WashingMachinePuzzleState
{
    NoPower,
    PowerOn,
    DetergentOn,
    laundryNotAdded,
    laundryAdded,
    washProgram,
    washMaschineOn,
    washMachineOnFire,
    washMachinFireOut,
}   

public enum WashingMachineState
{
    Idle,
    LidOpen,
    DoorOpen,
    DoorClose,
    AnimationOn,
    OnFire,
}

public enum WashMachineLidState
{
    ClosedNoDetergentAdded,
    OpenNoDetergentAdded,
    OpenDetergentAdded,
    ClosedDetergentAdded,
}

public enum WashMachineDoorState
{
    ClosedNoLoundry,
    OpenNoLaundry,
    ClosedLaundryAdded,
    OpenLaundryAdded,
}
#endregion

#region boogieMary
public enum BoogieMaryStoryState
{
    Sleeping,
    Wandering,
    TurnOffLights,
    Shower,
    ShowerAttack,
    Eating
}
public enum BoogieMaryMoveState
{
    None,
    Wandering,
    ChasingPlayer,
    ChasingCheese,
    MoveToTarget,

}

public enum BoogieMaryActionState
{
    None,
    Attacking,
    Looking,
    OpeningDoor
}
#endregion