using UnityEngine;

public class UpgradeManager : MonoBehaviour
{

    [SerializeField] float priceRateIncrease = 30f; // % of price to increase per upgrade

    float fireRate;
    float currentAttackUpgrade = 0.5f;
    int fireRateCost = 10;
    [SerializeField] TMPro.TextMeshProUGUI fireRateText;
    [SerializeField] TMPro.TextMeshProUGUI fireRateCostText;
    float damage;
    float currentDamageUpgrade = 0.2f;
    int damageCost = 10;
    [SerializeField] TMPro.TextMeshProUGUI damageText;
    [SerializeField] TMPro.TextMeshProUGUI damageCostText;
    int sheildTimeInMilliseconds;
    int currentShieldTimeUpgrade = 100;
    int shieldTimeCost = 10;
    [SerializeField] TMPro.TextMeshProUGUI shieldTimeText;
    [SerializeField] TMPro.TextMeshProUGUI shieldTimeCostText;

    [SerializeField] PlayerAttackController playerAttackController;
    [SerializeField] PlayerMovementController playerMovementController;

    void Start()
    {
        fireRate = playerAttackController.GetDefaultFireRate();
        fireRateText.text = fireRate.ToString();
        damage = playerAttackController.GetDefaultDamage();
        damageText.text = damage.ToString();
        sheildTimeInMilliseconds = playerAttackController.GetShieldTimeInMilliseconds();
        shieldTimeText.text = sheildTimeInMilliseconds.ToString();

        fireRateCost = PlayerPrefs.GetInt("FireRateCost", 10);
        fireRateCostText.text = fireRateCost.ToString();

        damageCost = PlayerPrefs.GetInt("DamageCost", 10);
        damageCostText.text = damageCost.ToString();

        shieldTimeCost = PlayerPrefs.GetInt("ShieldTimeCost", 10);
        shieldTimeCostText.text = shieldTimeCost.ToString();

        currentAttackUpgrade = PlayerPrefs.GetFloat("CurrentAttackUpgrade", 0.5f);
        currentDamageUpgrade = PlayerPrefs.GetFloat("CurrentDamageUpgrade", 0.5f);
        currentShieldTimeUpgrade = PlayerPrefs.GetInt("CurrentShieldTimeUpgrade", 100);        
    }

    public void UpgradeFireRate()
    {
        if (!GameManager.GetInstance().Buy(fireRateCost)) return;

        fireRate += currentAttackUpgrade;
        fireRateText.text = fireRate.ToString();
        playerAttackController.SetDefaultFireRate(fireRate);

        fireRateCost += (int) (fireRateCost * (priceRateIncrease/100f));
        fireRateCostText.text = fireRateCost.ToString();

        currentAttackUpgrade += 0.05f;

        PlayerPrefs.SetFloat("DefaultFireRate", fireRate);
        PlayerPrefs.SetInt("FireRateCost", fireRateCost);
        PlayerPrefs.SetFloat("CurrentAttackUpgrade", currentAttackUpgrade);

    }

    public void UpgradeDamage()
    {
        if (!GameManager.GetInstance().Buy(damageCost)) return;
        damage += currentDamageUpgrade;
        damageText.text = damage.ToString();
        playerAttackController.SetDefaultDamage(damage);

        damageCost += (int) (damageCost * (priceRateIncrease/100f));
        damageCostText.text = damageCost.ToString();

        currentDamageUpgrade += 0.05f;

        PlayerPrefs.SetFloat("DefaultDamage", damage);
        PlayerPrefs.SetInt("DamageCost", damageCost);
        PlayerPrefs.SetFloat("CurrentDamageUpgrade", currentDamageUpgrade);
    }

    public void UpgradeShieldTime()
    {
        if (!GameManager.GetInstance().Buy(shieldTimeCost)) return;
        sheildTimeInMilliseconds += currentShieldTimeUpgrade;
        shieldTimeText.text = sheildTimeInMilliseconds.ToString();
        playerAttackController.SetDefaultShieldTimeInMilliseconds(sheildTimeInMilliseconds);

        shieldTimeCost += (int) (shieldTimeCost * (priceRateIncrease/100f));
        shieldTimeCostText.text = shieldTimeCost.ToString();

        currentShieldTimeUpgrade += 50;

        PlayerPrefs.SetInt("DefaultShieldTimeInMilliseconds", sheildTimeInMilliseconds);
        PlayerPrefs.SetInt("ShieldTimeCost", shieldTimeCost);
        PlayerPrefs.SetInt("CurrentShieldTimeUpgrade", currentShieldTimeUpgrade);
    }
}
