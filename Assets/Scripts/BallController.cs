using TMPro;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] float health;
    [SerializeField] float maxHealth;
    [SerializeField] TextMeshProUGUI healthText;

    [SerializeField] int grade;

    [SerializeField] float wallBounce = 1.5f;
    [SerializeField] float groundBounce = 5f;
    [SerializeField] float gravityScale = 0.2f;

    [SerializeField] public int fallX; // X position where ball will fall

    [SerializeField] Rigidbody2D rb;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] public CircleCollider2D circleCollider;


    [SerializeField] PowerUpsSO powerUps;
    [SerializeField] CoinsSO coins;

    GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.GetInstance();
        healthText.text = ((int) (health+1)).ToString();
    }

    void Update()
    {
        if (gameManager.IsGameOver()) {
            Destroy(gameObject);
        }

        if (transform.position.x >= fallX - 0.2 && transform.position.x <= fallX + 0.2)
        {
            rb.gravityScale = gravityScale;
            circleCollider.enabled = true;
        }

        if (transform.position.y >= 8) {
            Destroy(gameObject);
        }

        if (transform.position.y < -5) {
            Destroy(gameObject);
        }
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // If the ball collides with the ground, bounce it up
        if (other.gameObject.CompareTag("GroundCheck"))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Random.Range(groundBounce * 0.8f, groundBounce));
        }

        if (other.gameObject.CompareTag("TopCheck")) {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 2);
        }

        // If the ball collides with the left wall, bounce it to the right
        if (other.gameObject.CompareTag("LeftCheck"))
        {
            rb.linearVelocity = new Vector2(wallBounce, rb.linearVelocity.y);
        }

        // If the ball collides with the right wall, bounce it to the left
        if (other.gameObject.CompareTag("RightCheck"))
        {
            rb.linearVelocity = new Vector2(-wallBounce, rb.linearVelocity.y);
        }

        // If the ball collides with the player, do nothing
        if (other.gameObject.CompareTag("Player"))
        {
            if (!other.gameObject.GetComponent<PlayerAttackController>().IsShieldActive()) {
                other.gameObject.GetComponent<PlayerAttackController>().Die();  
            }
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
        healthText.text = ((int) (health+1f)).ToString();
    }

    void Die()
    {
        gameManager.AddScore((int) maxHealth / (int)grade);

        int waveNumber = gameManager.GetWaveNumber();


        if (Random.Range(1, (int) (10f / Mathf.Log(waveNumber*3, 3))) == 1) {
            GameObject coin = Instantiate(coins.coinObject, transform.position, Quaternion.identity);
            Coin coinTable = coins.GetRandomCoin();
            coin.GetComponent<CoinController>().SetColor(coinTable.color);
            coin.GetComponent<CoinController>().SetValue(coinTable.value);
        }

        if (grade == 1) {
            float randomChance = waveNumber * 5; // Large number = low chance
            if (Random.Range(3, (int) randomChance) == 3) {
                Instantiate(powerUps.powerUps[Random.Range(0, powerUps.powerUps.Length)], transform.position, Quaternion.identity);
            }
            gameManager.RemoveBallsOnScreen();
            Destroy(gameObject);
            return;
        }


        if (maxHealth % 2 == 1) {
            maxHealth++;
        }

        float upForce = 80f;

        // Spawn 2 balls with one grade lower and half health
        GameObject ball1 = Instantiate(gameObject, transform.position, Quaternion.identity);
        gameManager.AddBallsOnScreen();
        ball1.GetComponent<BallController>().SetGrade(grade - 1);
        ball1.GetComponent<BallController>().SetHealth(maxHealth / 2);
        ball1.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(rb.linearVelocity.x, 0);

        ball1.GetComponent<Rigidbody2D>().AddForceY(upForce);
        ball1.GetComponent<Rigidbody2D>().gravityScale = 0.2f;

        GameObject ball2 = Instantiate(gameObject, transform.position, Quaternion.identity);
        gameManager.AddBallsOnScreen();
        ball2.GetComponent<BallController>().SetGrade(grade - 1);
        ball2.GetComponent<BallController>().SetHealth(maxHealth / 2);
        ball2.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(-rb.linearVelocity.x, 0);

        ball2.GetComponent<Rigidbody2D>().AddForceY(upForce);
        ball2.GetComponent<Rigidbody2D>().gravityScale = 0.2f;

        gameManager.RemoveBallsOnScreen();
        Destroy(gameObject);
    }

    public void SetGrade(int grade)
    {
        this.grade = grade;
        float scale = 0.7f + grade * 0.3f;
        // Change scale of ball based on grade
        transform.localScale = new Vector3(scale, scale, 1);
    }

    public void SetHealth(float health)
    {
        this.health = health;
        maxHealth = health;
        healthText.text = ((int) (health+1f)).ToString();
        // Change color of ball based on health
        if (health > 75)
        {
            spriteRenderer.color = Color.red;
        }
        else if (health > 50)
        {
            spriteRenderer.color = Color.yellow;
        }
        else if (health > 25)
        {
            spriteRenderer.color = Color.green;
        }
        else
        {
            spriteRenderer.color = Color.white;
        }
    }
}
