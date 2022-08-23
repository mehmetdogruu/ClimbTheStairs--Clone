using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public event Action OnReadyToRun;
    public event Action OnGameStarted;
    public event Action OnTimeRunningOut;
    public event Action OnGameLost;
    public event Action OnGameWon;

    [SerializeField] private float _gold;
    [SerializeField] private float _goldIncreaseAmount;
    [SerializeField] private float _buyGoldIncreaseAmount;
    [SerializeField] private float _stamina;
    [SerializeField] private float _staminaStartValue;
    [SerializeField] private float _staminaIncreaseAmount;
    [SerializeField] private float _speed;
    [SerializeField] private float _speedStartValue;
    [SerializeField] private float _speedIncreaseAmount;

    public GameStates GameState { get; private set; }

    public float Stamina { get => _stamina; set => _stamina = value; }
    public float StaminaIncreaseAmount { get => _staminaIncreaseAmount; set => _staminaIncreaseAmount = value; }
    public float StaminaStartValue { get => _staminaStartValue; set => _staminaStartValue = value; }
    public float Gold { get => _gold; set => _gold = value; }
    public float GoldIncreaseAmount { get => _goldIncreaseAmount; set => _goldIncreaseAmount = value; }
    public float BuyGoldIncreaseAmount { get => _buyGoldIncreaseAmount; set => _buyGoldIncreaseAmount = value; }
    public float Speed { get => _speed; set => _speed = value; }
    public float SpeedIncreaseAmount { get => _speedIncreaseAmount; set => _speedIncreaseAmount = value; }
    public float SpeedStartValue { get => _speedStartValue; set => _speedStartValue = value; }



    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
    }

    public void InitializeReadyToClimb()
    {
        GameState = GameStates.InReadyToClimb;
        CharacterAnimationController.instance.TriggerIdle();
        OnReadyToRun?.Invoke();
    }
    public void InitializeGameStarted()
    {
        GameState = GameStates.InGameStarted;
        OnGameStarted?.Invoke();
    }
    public void InitializeTimeRunningOut()
    {
        OnTimeRunningOut?.Invoke();
    }
    public void InitaliazeGameLost()
    {
        GameState = GameStates.InGameLost;
        OnGameLost?.Invoke();
    }
    public void InitializeGameWon()
    {
        GameState = GameStates.InGameWon;
        OnGameWon?.Invoke();
        Gold = 0;
        Speed= 0.3f;
        StaminaStartValue = 13;   
    }


}
