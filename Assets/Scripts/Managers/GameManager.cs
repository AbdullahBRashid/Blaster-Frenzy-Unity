using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton
    static GameManager instance;
    public static GameManager GetInstance() {
        return instance;
    }

    [SerializeField] GameObject StartButton;

    // Handle Score
    int score = 0;
    [SerializeField] TMPro.TextMeshProUGUI scoreText;
    public int GetScore() { return score; }
    public void SetScore(int score) {
        this.score = score;
        scoreText.text = this.score.ToString();
    }
    public void AddScore(int score) {
        this.score += score;
        scoreText.text = this.score.ToString();
    }

    // Handle HighScore
    [SerializeField] TMPro.TextMeshProUGUI highScoreText;
    int highScore = 0;
    public int GetHighScore() { return highScore; }

    // Player settings
    [SerializeField] PlayerAttackController playerAttack;
    [SerializeField] PlayerMovementController playerMovement;

    // Game State
    [SerializeField] int ballsSpawned = 0;
    [SerializeField] int ballsOnScreen = 0;

    // Wave settings
    [SerializeField] int waveNumber = 1;
    [SerializeField] bool waveStarted = false;
    [SerializeField] TMPro.TextMeshProUGUI waveText;

    // Handle Score
    [SerializeField] int coins;
    [SerializeField] TMPro.TextMeshProUGUI coinsText;
    public void AddCoins(int coins) {
        this.coins += coins;
        PlayerPrefs.SetInt("Coins", this.coins);
        coinsText.text = "$" + this.coins.ToString();
    }
    public void SpendCoins(int coins) {
        this.coins -= coins;
        PlayerPrefs.SetInt("Coins", this.coins);
        coinsText.text = "$" + this.coins.ToString();
    }
    public bool CanBuy(int cost) {
        return coins >= cost;
    }
    public bool Buy(int cost) {
        if (CanBuy(cost)) {
            SpendCoins(cost);
            return true;
        }
        return false;
    }
    public int GetCoins() {
        return coins;
    }

    // Handle Menus
    [SerializeField] GameObject gameOverMenu;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject upgradeMenu;

    // Spawn Settings
    [SerializeField] BallSpawner spawner;
    
    // Wave Settings

    public void StopWave() {
        waveStarted = false;
        spawner.StopSpawning();
        upgradeMenu.SetActive(true);

        playerMovement.SetMove(false);

        playerAttack.StopShooting();
        playerAttack.transform.position = new Vector3(0, playerAttack.transform.position.y, playerAttack.transform.position.z);
        playerAttack.DeactivateShield();

        StartButton.SetActive(true);
    }

    void HideWaveText() {
        waveText.enabled = false;
    }

    public int GetWaveNumber() {
        return waveNumber;
    }

    public bool IsWaveStarted() {
        return waveStarted;
    }

    public void IncrementWaveNumber() {
        waveNumber++;
        waveText.text = "Wave " + waveNumber;
    }

    public void IncrementBallsSpawned() {
        if (!waveStarted) {
            waveStarted = true;
        }
        ballsSpawned++;
    }

    public int GetBallsSpawned() {
        return ballsSpawned;
    }


    // Game Start
    void Awake() {
        instance = this;
    }

    
    void Start() {
        waveNumber = PlayerPrefs.GetInt("WaveNumber", 1);

        scoreText.enabled = false;

        coins = PlayerPrefs.GetInt("Coins", 0);
        coinsText.text = "$" + coins.ToString();
        
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = "HighScore: " + highScore.ToString();

        waveText.text = "Wave\n" + waveNumber;
        waveText.enabled = false;
        waveStarted = false;
        
        gameOverMenu.SetActive(false);

        StartButton.SetActive(true);
    }

    void Update() {

        if (waveStarted && ballsSpawned % 10 == 0) {
            spawner.StopSpawning();
        }

        if (waveStarted && ballsOnScreen == 0 && ballsSpawned % 10 == 0) {
            StopWave();
            IncrementWaveNumber();
            PlayerPrefs.SetInt("WaveNumber", waveNumber);
        }
    }

    public void AddBallsOnScreen() {
        ballsOnScreen++;
    }
    public void RemoveBallsOnScreen() {
        ballsOnScreen--;
    }

    // Handle Game Running
    bool isGameOver = false;
    public bool IsGameOver() {
        return isGameOver;
    }

    public void GameOver() {
        if (score > highScore) {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
        score = 0;
        highScoreText.text = "HighScore: " + highScore.ToString();
        isGameOver = true;
        playerMovement.SetMove(false);
        gameOverMenu.SetActive(true);
        spawner.StopSpawning();
        playerAttack.StopShooting();
        ballsOnScreen = 0;

        upgradeMenu.SetActive(true);
    }

    public void StartWave() {
        playerAttack.Init();
        playerAttack.StartShooting();
        playerMovement.SetMove(true);

        spawner.StartSpawning();

        scoreText.text = score.ToString();
        scoreText.enabled = true;

        waveText.enabled = true;
        Invoke(nameof(HideWaveText), 2);

        upgradeMenu.SetActive(false);

        StartButton.SetActive(false);
    }

    public void RestartGame() {
        score = 0;
        scoreText.text = "0";
        scoreText.enabled = true;

        isGameOver = false;
        gameOverMenu.SetActive(false);

        waveStarted = false;
        ballsSpawned = 0;

        playerAttack.transform.position = new Vector3(0, playerAttack.transform.position.y, playerAttack.transform.position.z);

        StartWave();
    }

    
}