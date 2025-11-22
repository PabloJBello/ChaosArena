using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject settingsScreen;
    [SerializeField] private Text winnerText;
    //[SerializeField] private AudioClip pauseSound;  // Sound played when game paused

    private void Awake()
    {
        winnerText.gameObject.SetActive(false);
        pauseScreen.SetActive(false);
        settingsScreen.SetActive(false);
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // If currently on the settings screen, close it and return to pause menu
            if (settingsScreen.activeInHierarchy)
            {
                Settings();
            }
            // If currently paused (pause screen active), unpause the game
            else if (pauseScreen.activeInHierarchy)
            {
                PauseGame(false);
            }
            // Otherwise, pause the game
            else
            {
                PauseGame(true);
            }
        }
    }
    
    public void PauseGame(bool status)
    {
        pauseScreen.SetActive(status);
        settingsScreen.SetActive(false); // Make sure settings is hidden when unpausing

        /* Need Sound Manager script to control sound */
        // SoundManager.instance.PlaySound(gameOverSound);
        
        if (status)
            Time.timeScale = 0;
        else 
            Time.timeScale = 1;
    }

    public void Settings()
    {
        if (settingsScreen.activeInHierarchy)
            settingsScreen.SetActive(false);
        else
            settingsScreen.SetActive(true);
    }
    
    public void MainMenu()
    {
        PauseGame(false);
        SceneManager.LoadScene(0);
    }
    
    public void Quit()
    {
        GameController.QuitGame();
    }

    public void ShowWinnerText()
    {
        Debug.Log("Show winner");
        winnerText.gameObject.SetActive(true);
        StartCoroutine(EndGame());
    }
    
    private IEnumerator EndGame()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(0);
    }
}
