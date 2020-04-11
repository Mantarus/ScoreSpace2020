using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public UIController uiController;
    public AudioSource music;
    
    private bool _gameStarted = false;
    private bool _gameEnded = false;
    private bool _paused = false;
    
    private float _lastTimeScale = 1;

    private void Start()
    {
        Time.timeScale = 0;
        
        uiController.HideInGameUI();
        uiController.HidePauseScreen();
        uiController.HideTotalScoreScreen();
        
        uiController.SetHighscore(PlayerPrefs.GetInt("highscore"));
        uiController.ShowWelcomeScreen();
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
        music.Pause();
    }

    private void Unpause()
    {
        uiController.HidePauseScreen();
        uiController.ShowInGameUI();
        
        _paused = false;
        Time.timeScale = _lastTimeScale;
        music.UnPause();
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
        music.Play();
    }

    public void EndGame()
    {
        Time.timeScale = 1;
        _gameEnded = true;
        uiController.HideInGameUI();
        uiController.ShowTotalScoreScreen();
        music.Stop();
    }
}
