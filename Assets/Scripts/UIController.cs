using System;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject welcomeScreen;
    public GameObject inGameUI;
    public GameObject pauseScreen;

    public Text highscoreText;

    private const string _highscoreComponentName = "Highscore";

    private void Start()
    {
        var textComponents = welcomeScreen.GetComponents<Text>();
        foreach (var component in textComponents)
        {
            if (_highscoreComponentName == component.name)
            {
                highscoreText = component;
            }
        }
        if (highscoreText == null)
        {
            throw new NullReferenceException("Highscore text not found!");
        }
    }

    public void SetHighscore(int score)
    {
        highscoreText.text = $"HIGHSCORE: {score}";
    }

    public void ShowWelcomeScreen()
    {
        welcomeScreen.SetActive(true);
    }
    
    public void HideWelcomeScreen()
    {
        welcomeScreen.SetActive(false);
    }

    public void ShowInGameUI()
    {
        inGameUI.SetActive(true);
    }
    
    public void HideInGameUI()
    {
        inGameUI.SetActive(false);
    }
    
    public void ShowPauseScreen()
    {
        pauseScreen.SetActive(true);
    }
    
    public void HidePauseScreen()
    {
        pauseScreen.SetActive(false);
    }
}
