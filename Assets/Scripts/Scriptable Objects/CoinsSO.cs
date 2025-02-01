using UnityEngine;


[System.Serializable]
public struct Coin {
    public int value;
    public float weight;
    public Color color;
}


[CreateAssetMenu(menuName = "005 Blaster Frenzy/Coins Holder")]
public class CoinsSO : ScriptableObject
{
    // Store different coins for referencing
    public Coin[] coins;
    
    public GameObject coinObject;

    public void CalculateWeights()
    {
        for (int i = 0; i < coins.Length; i++)
        {
            coins[i].weight = 1f / coins[i].value;
        }
    }

    public Coin GetRandomCoin() {
        float totalWeight = 0;
        foreach (Coin coin in coins) {
            totalWeight += coin.weight;
        }
        

        float randomWeight = Random.Range(0, totalWeight);
        float currentWeight = 0;
        foreach (Coin coin in coins) {
            currentWeight += coin.weight;
            if (randomWeight <= currentWeight) {
                return coin;
            }
            
        }

        return coins[0];
    }

}