using UnityEngine;

public class SceneInitializer : MonoBehaviour
{

    [SerializeField] GameObject leftWall;
    [SerializeField] GameObject rightWall;
    [SerializeField] GameObject topWall;

    [SerializeField] CoinsSO coinsSO;

    void Awake()
    {
        // Calculate the weights of the coins
        coinsSO.CalculateWeights();

        // Set the position of the left wall according to screen size minus collider width
        leftWall.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height / 2, 0));
        leftWall.transform.position = new Vector3(leftWall.transform.position.x - 0.5f, leftWall.transform.position.y, 0);

        // Set the position of the right wall according to screen size minus collider width
        rightWall.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height / 2, 0));
        rightWall.transform.position = new Vector3(rightWall.transform.position.x + 0.5f, rightWall.transform.position.y, 0);
        
        // Set the position of the top wall according to screen size minus collider height
        topWall.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height, 0));
        topWall.transform.position = new Vector3(topWall.transform.position.x, topWall.transform.position.y + 0.5f, 0);
    }
}
