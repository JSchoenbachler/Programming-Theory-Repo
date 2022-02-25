using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class VerticalJumpPowerup : Powerup
{
    private float jumpMultiplier = 2f;
    // POLYMORPHISM
    public override void UsePowerup() {
        if (player.isOnGround) {
            player.Jump(jumpMultiplier);
            base.UsePowerup();
        }
    }
}
