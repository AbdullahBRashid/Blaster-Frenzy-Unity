using UnityEngine;

public class StartMenuManager : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject settingsMenu;

    [SerializeField] AudioSource musicSource;
    [SerializeField] TMPro.TextMeshProUGUI musicText;
    bool musicPlaying;

    public void StartGame() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }

    void Awake()
    {
        musicPlaying = PlayerPrefs.GetInt("Music", 1) == 1;
        if (!musicPlaying) {
            musicSource.mute = true;
            musicText.text = "Music: OFF";
        }
    }

    public void OpenSettings() {
        settingsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void CloseSettings() {
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void ToggleMusic() {
        if (musicPlaying) {
            musicSource.mute = true;
            musicPlaying = false;
            musicText.text = "Music: OFF";
            PlayerPrefs.SetInt("Music", 0);
        } else {
            musicSource.mute = false;
            musicPlaying = true;
            musicText.text = "Music: ON";
            PlayerPrefs.SetInt("Music", 1);
        }
    }

    public void QuitGame() {
        Application.Quit();
    }
}
