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
    peeping
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