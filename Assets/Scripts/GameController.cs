using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private bool _paused = false;
    private float _lastTimeScale = 1;

    private void Start()
    {
        Time.timeScale = 1;
    }
    
    void Update()
    {
        CheckPause();
        CheckRestart();
    }

    private void CheckPause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _paused = !_paused;

            if (_paused) Pause();
            else Unpause();
        }
    }

    private void Pause()
    {
        _lastTimeScale = Time.timeScale;
        Time.timeScale = 0;
    }

    private void Unpause()
    {
        Time.timeScale = _lastTimeScale;
    }

    private void CheckRestart()
    {
        if (_paused && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
