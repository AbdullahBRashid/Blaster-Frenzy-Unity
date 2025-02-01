using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    [SerializeField] GameManager gameManager;

    [SerializeField] GameObject bulletPrefab;

    // Default Values
    // FireRate
    [SerializeField] float defaultFireRate = 10f;
    public float GetDefaultFireRate() { return defaultFireRate; }
    public void SetDefaultFireRate(float fireRate) { defaultFireRate = fireRate; }
    // Damage
    [SerializeField] float defaultDamage = 1.5f;
    public float GetDefaultDamage() { return defaultDamage; }
    public void SetDefaultDamage(float damage) { defaultDamage = damage; }
    // Shield
    [SerializeField] int defaultShieldTimeInMilliseconds = 2500;
    public int GetDefaultShieldTimeInMilliseconds() { return defaultShieldTimeInMilliseconds; }
    public void SetDefaultShieldTimeInMilliseconds(int shieldTimeInMilliseconds) { defaultShieldTimeInMilliseconds = shieldTimeInMilliseconds; }

    // Current Values
    // FireRate
    [SerializeField] float fireRate;
    public float GetFireRate() { return fireRate; }
    public void SetFireRate(float fireRate) { this.fireRate = fireRate; }

    // Damage
    [SerializeField] float damage;
    public float GetDamage() { return damage; }
    public void SetDamage(float damage) { this.damage = damage; }

    // Shield
    [SerializeField] int shieldTimeInMilliseconds;
    public int GetShieldTimeInMilliseconds() { return shieldTimeInMilliseconds; }
    public void SetShieldTimeInMilliseconds(int shieldTimeInMilliseconds) { this.shieldTimeInMilliseconds = shieldTimeInMilliseconds; }

    // Sheild Settings
    [SerializeField] GameObject shieldObject;
    [SerializeField] bool sheildActive = false;


    public void ActivateShield() {
        if (sheildActive) {
            CancelInvoke(nameof(DeactivateShield));
        }
        shieldObject.SetActive(true);
        sheildActive = true;
        Invoke(nameof(DeactivateShield), shieldTimeInMilliseconds/1000f + 1);
    }

    public void DeactivateShield() {
        sheildActive = false;
        shieldObject.SetActive(false);
    }

    public bool IsShieldActive() {
        return sheildActive;
    }

    // Shooting Handling
    [SerializeField] Transform bulletSpawnPoint;
    bool isShooting;

    public void StartShooting() {
        isShooting = true;
        InvokeRepeating(nameof(Shoot), 0f, 3f / fireRate);
    }

    public void StopShooting() {
        isShooting = false;
        CancelInvoke(nameof(Shoot));
    }

    void Awake() {
        defaultDamage = PlayerPrefs.GetFloat("DefaultDamage", defaultDamage);
        defaultFireRate = PlayerPrefs.GetFloat("DefaultFireRate", defaultFireRate);
        defaultShieldTimeInMilliseconds = PlayerPrefs.GetInt("DefaultShieldTimeInMilliseconds", defaultShieldTimeInMilliseconds);
    }

    void Update()
    {
        // Shoot while the player is touching the screen
        if ((Input.touchCount > 0 || Input.GetMouseButton(0) ) && gameManager.IsWaveStarted()) {
            if (!isShooting) {
                StartShooting();
            }
        } else {
            if (isShooting) {
                StopShooting();
            }
        }
    }

    public void Init() {
        fireRate = defaultFireRate;
        damage = defaultDamage;
        shieldTimeInMilliseconds = defaultShieldTimeInMilliseconds;
    }

    void Shoot() {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        bullet.GetComponent<BulletController>().SetDamage(damage);
    }

    public void Die () {
        Init();
        gameManager.GameOver();
        StopShooting();
    }

    public void IncreaseFireRate(float fireRate) {
        StopShooting();
        this.fireRate += fireRate;
        StartShooting();
    }

    public void AddCurrentDamage(int damage) {
        this.damage += damage;
    }

    
}