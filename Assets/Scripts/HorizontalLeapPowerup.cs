using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalLeapPowerup : Powerup
{
    private float forwardForce;
    public override void UsePowerup() {
        forwardForce = player.jumpForce * 0.5f;
        player.Move(player.GetCameraForward() * forwardForce, ForceMode.Impulse);
        if (player.isOnGround) {
            player.Jump(1f);
        }
        base.UsePowerup();
    }
}
