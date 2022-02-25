using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Powerup : MonoBehaviour
{
    // Start is called before the first frame update
    protected PlayerScript player;
    public void GetPowerup(PlayerScript playerTmp) {
        player = playerTmp;
        player.hasPowerup = true;
        player.powerup = this;
    }
    public virtual void UsePowerup() {
        player.hasPowerup = false;
        player.powerup = null;
    }
}
