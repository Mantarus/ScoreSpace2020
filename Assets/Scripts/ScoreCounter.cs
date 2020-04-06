using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    public GameController gameController;
    public GameObject truck;
    public GameObject man;
    public Text scoreText;
    public Text totalText;
    
    public float distanceMultiplier;
    public float speedMultiplier;
    public float bonusMultiplier;

    private Rigidbody _truckRb;
    private Rigidbody _manRb;
    private BalanceChecker _manChecker;

    private bool _bonus;
    private bool _calculate = true;
    
    private float _initialZPosition;
    private float _score = 0;
    private float _maxSpeed;
    private float _totalDistance;
    private float _bonusDistance;
    private string _highscorePref = "highscore";

    private void Start()
    {
        _initialZPosition = truck.transform.position.z;
        _truckRb = truck.GetComponent<Rigidbody>();
        _manRb = man.GetComponent<Rigidbody>();
        _manChecker = man.GetComponent<BalanceChecker>();

        totalText.text = "";
    }

    private void FixedUpdate()
    {
        if (_manChecker.IsAlive())
        {
            var speed = _truckRb.velocity.z;
            if (speed > _maxSpeed) _maxSpeed = speed;

            var distanceDelta = Mathf.Max(speed * Time.fixedDeltaTime, 0);
            var scoreIncrement = distanceDelta * distanceMultiplier;
            _score += scoreIncrement;

            _totalDistance = truck.transform.position.z - _initialZPosition;
        }
        else
        {
            _bonus = true;
            if (_manRb.IsSleeping()) _calculate = false;
            if (man.transform.position.y < 0) _calculate = false;
            if (_calculate)
            {
                var bonusDistanceDelta = Mathf.Max(_manRb.velocity.z * Time.fixedDeltaTime, 0);
                var scoreIncrement = bonusDistanceDelta * bonusMultiplier;
                _score += scoreIncrement;
                
                _bonusDistance = man.transform.position.z - _totalDistance;
            }
        }
        
        UpdateScore();
    }

    private void UpdateScore()
    {
        if (_calculate)
        {
            scoreText.text = $"Score: {(int)_score}";
            if (_bonus) scoreText.text += " BONUS!";
        }
        else
        {
            PlayerPrefs.SetInt(_highscorePref, Mathf.Max((int)_score, PlayerPrefs.GetInt(_highscorePref)));
            totalText.text = $"Score: {(int)_score}\n" +
                             $"Total: {(int)_totalDistance}\n" +
                             $"Bonus: {(int)_bonusDistance}\n" +
                             $"Max Speed: {(int)_maxSpeed}\n" +
                             $"Your best score so far: {PlayerPrefs.GetInt(_highscorePref)}\n" +
                             "Press R to start again!";
            scoreText.text = "";
            gameController.EndGame();
        }
    }
}
