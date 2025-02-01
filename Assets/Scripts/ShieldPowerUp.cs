using UnityEngine;

public class SheildPowerUp : PowerUpController
{
    public override void PowerUp(GameObject player) {
        player.GetComponent<PlayerAttackController>().ActivateShield();
    }
}