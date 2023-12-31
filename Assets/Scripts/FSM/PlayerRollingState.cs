using UnityEngine;

public class PlayerRollingState : PlayerState
{
    public override void OnEnter()
    {
        playerHealthSystem.enabled = false;
        playerMovement.enabled = true;
        playerShooting.enabled = false;
        boxCollider.enabled = false;
    }

    public override void OnExit()
    {
        playerHealthSystem.enabled = true;
        playerMovement.enabled = true;
        playerShooting.enabled = true;
        boxCollider.enabled = true;
    }

    public PlayerRollingState(PlayerMovement playerMovement, PlayerHealth playerHealthSystem, PlayerShooting playerShooting, string name, StateMachine stateMachine,BoxCollider boxCollider) : base(playerMovement, playerHealthSystem, playerShooting, name, stateMachine,boxCollider)
    {
    }
}