using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    public GameController gameController;
    public UIController uiController;
    
    public GameObject truck;
    public GameObject man;
    
    public float distanceMultiplier;
    public float speedMultiplier;
    public int bonusMultiplier;

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
    private const string HighscorePref = "highscore";

    private void Start()
    {
        _initialZPosition = truck.transform.position.z;
        _truckRb = truck.GetComponent<Rigidbody>();
        _manRb = man.GetComponent<Rigidbody>();
        _manChecker = man.GetComponent<BalanceChecker>();
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
            if (_manRb.IsSleeping() || _manRb.velocity.magnitude < 0.1) _calculate = false;
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
            uiController.SetScore(_score, _bonus);
        }
        else
        {
            var highScore = Mathf.Max((int)_score, PlayerPrefs.GetInt(HighscorePref));
            PlayerPrefs.SetInt(HighscorePref, highScore);
            uiController.SetTotalScore(_score, _totalDistance, _bonusDistance, _maxSpeed, highScore);
            gameController.EndGame();
        }
    }
}
