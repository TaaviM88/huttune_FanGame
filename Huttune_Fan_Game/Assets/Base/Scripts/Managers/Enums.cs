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

public enum WashingMachinePuzzleState
{
    NoPower,
    PowerOn,
    DetergentOn,
    laundryOn,
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