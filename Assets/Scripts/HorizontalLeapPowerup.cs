using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class HorizontalLeapPowerup : Powerup
{
    private float forwardForce;
    // POLYMORPHISM
    public override void UsePowerup() {
        forwardForce = player.jumpForce * 0.5f;
        player.Move(player.GetCameraForward() * forwardForce, ForceMode.Impulse);
        if (player.isOnGround) {
            player.Jump(1f);
        }
        base.UsePowerup();
    }
}
