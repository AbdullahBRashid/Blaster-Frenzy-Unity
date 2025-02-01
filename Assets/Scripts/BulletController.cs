using UnityEngine;

public class BulletController : MonoBehaviour
{
    float damage;
    [SerializeField] float speed = 3f;

    GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.GetInstance();
    }

    void Update()
    {
        if (!gameManager.IsWaveStarted() || gameManager.IsGameOver()) {
            Destroy(gameObject);
        }
        transform.Translate(speed * Time.deltaTime * Vector2.up);

        if (transform.position.y > 7.5f) {
            Destroy(gameObject);
        }
    }

    public void SetDamage(float damage) {
        this.damage = damage;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            return;
        }
        else if (other.CompareTag("BallHitbox"))
        {
            other.GetComponent<BallHitBoxController>().parent.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
