using UnityEngine;

public class CoinController : MonoBehaviour
{

    [SerializeField] Color color;
    [SerializeField] int value;

    GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.GetInstance();
    }

    void Update() {
        if (!gameManager.IsWaveStarted() && !gameManager.IsGameOver()) {
            gameManager.AddCoins(value);
            Destroy(gameObject);
        }
        if (gameManager.IsGameOver()) {
            Destroy(gameObject);
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameManager.AddCoins(value);
            Destroy(gameObject);
        }
    }

    public void SetColor(Color color) {
        this.color = color;
        gameObject.GetComponent<SpriteRenderer>().color = color;
    }

    public void SetValue(int value) {
        this.value = value;
    }
}