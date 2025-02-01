using UnityEngine;

public abstract class PowerUpController : MonoBehaviour
{
    // Function to be implemented by child classes
    public abstract void PowerUp(GameObject player);

    GameManager gameManager;

    void Start() {
        gameManager = GameManager.GetInstance();    
    }

    void Update() {
        if (gameManager.IsGameOver() || !gameManager.IsWaveStarted()) {
            Destroy(gameObject);
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PowerUp(other.gameObject);
            Destroy(gameObject);
        }
    }
}