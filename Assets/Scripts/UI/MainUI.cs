using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainUI : MonoBehaviour
{
    [SerializeField] private GameObject settingsScreen;
    [SerializeField] private GameObject startScreen;
    
    /*--------Temporary--------*/
    [SerializeField] private GameObject aboutPanel;

    private void Awake()
    {
        aboutPanel.SetActive(false);
        settingsScreen.SetActive(false);
    }

    public void onClose()
    {
        aboutPanel.SetActive(false);
    }
    /*-------------------------*/
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (settingsScreen.activeInHierarchy)
            {
                settingsScreen.SetActive(false);
                startScreen.SetActive(true);
            }
        }
    }

    public void GameScreen()
    {
        SceneManager.LoadScene(1);
    }

    public void Settings()
    {
        if (!settingsScreen.activeInHierarchy)
        {
            startScreen.SetActive(false);
            settingsScreen.SetActive(true);
        }
    }

    public void Quit()
    {
        GameController.QuitGame();
    }
    
    // Temporary to give small description about game
    public void About()
    {
        aboutPanel.SetActive(true);
    }
}
