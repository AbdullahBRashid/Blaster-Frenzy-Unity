using UnityEngine;

public class DamagePowerUp : PowerUpController
{
    [SerializeField] int increaseDamage = 1;

    public override void PowerUp(GameObject player) {
        player.GetComponent<PlayerAttackController>().AddCurrentDamage(increaseDamage);
    }
}