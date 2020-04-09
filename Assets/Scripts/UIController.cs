using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject welcomeScreen;
    public GameObject inGameUI;
    public GameObject pauseScreen;
    public GameObject totalScoreScreen;

    public Text highscoreText;
    public Text scoreText;
    public Text speedText;
    public Text throttleText;
    public Text totalScoreText;

    public void SetHighscore(float score)
    {
        highscoreText.text = $"HIGHSCORE: {(int)score}";
    }

    public void SetScore(float score, bool bonus)
    {
        scoreText.text = $"Score: {(int)score}";
        if (bonus) scoreText.text += " BONUS!";
    }

    public void SetSpeed(float speed)
    {
        speedText.text = $"{(int)speed}mph";
    }

    public void SetThrottle(float throttle)
    {
        throttleText.text = $"PWR: {(int)throttle}%";
    }

    public void SetTotalScore(float score, float distance, float bonus, float maxSpeed, float highscore)
    {
        totalScoreText.text = $"Total score: {(int)score}\n" + 
                              $"Distance: {(int)distance}\n" + 
                              $"Bonus : {(int)bonus}\n" + 
                              $"Max Speed: {(int)maxSpeed}\n" + 
                              $"Your best score so far: {(int)highscore}\n" + 
                              "Press R to start again!";
    }

    public void DisableTruckUI()
    {
        speedText.gameObject.SetActive(false);
        throttleText.gameObject.SetActive(false);
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
    
    public void ShowTotalScoreScreen()
    {
        totalScoreScreen.SetActive(true);
    }
    
    public void HideTotalScoreScreen()
    {
        totalScoreScreen.SetActive(false);
    }
    
}
