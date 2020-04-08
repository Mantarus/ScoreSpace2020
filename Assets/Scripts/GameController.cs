using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public UIController uiController;
    
    private bool _gameStarted = false;
    private bool _gameEnded = false;
    private bool _paused = false;
    
    private float _lastTimeScale = 1;

    private void Start()
    {
        Time.timeScale = 0;
        uiController.ShowWelcomeScreen();
        uiController.SetHighscore(PlayerPrefs.GetInt("highscore"));
    }
    
    void Update()
    {
        if (_gameStarted)
        {
            CheckPause();
            CheckRestart();
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                StartGame();
            }
        }
    }

    private void CheckPause()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && _gameStarted && !_gameEnded)
        {
            _paused = !_paused;

            if (_paused) Pause();
            else Unpause();
        }
    }

    private void Pause()
    {
        uiController.HideInGameUI();
        uiController.ShowPauseScreen();
        
        _paused = true;
        _lastTimeScale = Time.timeScale;
        Time.timeScale = 0;
    }

    private void Unpause()
    {
        uiController.HidePauseScreen();
        uiController.ShowInGameUI();
        
        _paused = false;
        Time.timeScale = _lastTimeScale;
    }

    private void CheckRestart()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void StartGame()
    {
        Time.timeScale = 1;
        _gameStarted = true;
        uiController.HideWelcomeScreen();
        uiController.ShowInGameUI();
    }

    public void EndGame()
    {
        Time.timeScale = 1;
        _gameEnded = true;
    }
}
