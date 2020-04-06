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

    public void Pause()
    {
        _paused = true;
        _lastTimeScale = Time.timeScale;
        Time.timeScale = 0;
    }

    public void Unpause()
    {
        _paused = false;
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
