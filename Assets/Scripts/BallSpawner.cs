using UnityEngine;
using System;

public class BallSpawner : MonoBehaviour
{

    [SerializeField] GameObject ballPrefab;
    [SerializeField] float spawnRate = 8f; // Seconds per ball spawn

    [SerializeField] Transform leftSpawn;
    [SerializeField] Transform rightSpawn;

    [SerializeField] float entrySpeed = 8;

    [SerializeField] float minHealth;
    [SerializeField] float maxHealth;

    public void SetSpawnRate(float spawnRate) {
        this.spawnRate = spawnRate;
    }

    public void StartSpawning(float time = 0f) {
        InvokeRepeating(nameof(SpawnBall), time, spawnRate);
    }

    public void StopSpawning() {
        CancelInvoke(nameof(SpawnBall));
    }

    void SpawnBall() {
        GameManager.GetInstance().IncrementBallsSpawned();
        GameManager.GetInstance().AddBallsOnScreen();

        float random = UnityEngine.Random.Range(0, 10);
        Vector3 spawnPosition = random > 5 ? leftSpawn.position : rightSpawn.position;
        GameObject ball = Instantiate(ballPrefab, spawnPosition, Quaternion.identity);

        // Random grade and health
        int grade = UnityEngine.Random.Range(1, 5);
        int minHealth = 1;
        float multipler = 0.5f;
        int wave = GameManager.GetInstance().GetWaveNumber();
        if (wave > 3) {
            multipler = 1f;
            minHealth = 3;
        } if (wave > 10) {
            multipler = 2f;
            minHealth = 10;
        } if (wave > 30) {
            multipler = 3f;
            minHealth = 50;
        } if (wave > 100) {
            multipler = 4f;
            minHealth = 200;
        }
        int maxHealth =  (int) (8 * (int) Math.Pow(2, grade) * multipler);
        int health = UnityEngine.Random.Range(minHealth, maxHealth) + 1;
        ball.GetComponent<BallController>().SetGrade(grade);
        ball.GetComponent<BallController>().SetHealth(health);

        ball.transform.position = new Vector3(ball.transform.position.x, ball.transform.position.y, UnityEngine.Random.Range(-100, 100) / 50);

        // Disable circle collider initially
        ball.GetComponent<BallController>().circleCollider.enabled = false;

        // Set initial gravity scale of ball to 0
        ball.GetComponent<Rigidbody2D>().gravityScale = 0;

        // Set initial velocity of ball to move towards center of screen horizontally
        int direction = ball.transform.position.x > 0 ? -1 : 1;
        ball.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(entrySpeed * direction, 0);

        // Assign random value to fallX within the screen width
        ball.GetComponent<BallController>().fallX = 0;
    }
}
