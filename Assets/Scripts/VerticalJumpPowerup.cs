using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalJumpPowerup : Powerup
{
    private float jumpMultiplier = 2f;
    public override void UsePowerup() {
        if (player.isOnGround) {
            player.Jump(jumpMultiplier);
            base.UsePowerup();
        }
    }
}
