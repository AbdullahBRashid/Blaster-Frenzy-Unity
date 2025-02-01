using UnityEngine;

public class FireRatePowerUp : PowerUpController
{
    [SerializeField] float increaseFireRate = 1f;

    public override void PowerUp(GameObject player) {
        player.GetComponent<PlayerAttackController>().IncreaseFireRate(increaseFireRate);
    }
}