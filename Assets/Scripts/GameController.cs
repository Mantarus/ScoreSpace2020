using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject welcomeScreen;
    public GameObject inGameUi;
    public GameObject pauseScreen;
    public Text highscoreText;
    
    private bool _gameStarted = false;
    private bool _gameEnded = false;
    private bool _paused = false;
    private float _lastTimeScale = 1;

    private void Start()
    {
        Time.timeScale = 0;
        welcomeScreen.SetActive(true);
        highscoreText.text = $"HIGHSCORE: {PlayerPrefs.GetInt("highscore")}";
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

    public void Pause()
    {
        inGameUi.SetActive(false);
        pauseScreen.SetActive(true);
        
        _paused = true;
        _lastTimeScale = Time.timeScale;
        Time.timeScale = 0;
    }

    public void Unpause()
    {
        inGameUi.SetActive(true);
        pauseScreen.SetActive(false);
        
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
        inGameUi.SetActive(true);
        welcomeScreen.SetActive(false);
    }

    public void EndGame()
    {
        Time.timeScale = 1;
        _gameEnded = true;
    }
}
