using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IntroUiController : UIController<IntroUiController>
{
    [SerializeField] private Button _staminaButton;
    [SerializeField] private Button _incomeButton;
    [SerializeField] private Button _speedButton;
    [SerializeField] private Button _tapToStartButton;
    [SerializeField] PlayerController _playerController;


    [Header("Shop")]
    [SerializeField] TMP_Text _incomeText;
    [SerializeField] TMP_Text _staminaText;
    [SerializeField] TMP_Text _speedText;
    [SerializeField] TMP_Text _moneyText;
    [SerializeField] private float _incomeUpValue;
    [SerializeField] private float _staminaUpValue;
    [SerializeField] private float _speedUpValue;
    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        
        _staminaButton.onClick.AddListener(StaminaButtonController);
        _incomeButton.onClick.AddListener(IncomeButtonController);
        _speedButton.onClick.AddListener(SpeedButtonController);
        _tapToStartButton.onClick.AddListener(StartButtonController);
        GameManager.Instance.OnGameStarted += Hide;
    }

    private void OnDisable()
    {
        _staminaButton.onClick.RemoveListener(StaminaButtonController);
        _incomeButton.onClick.RemoveListener(IncomeButtonController);
        _speedButton.onClick.RemoveListener(SpeedButtonController);
        _tapToStartButton.onClick.RemoveListener(StartButtonController);
        GameManager.Instance.OnGameStarted -= Hide;
    }
    private void Update()
    {
        if (_canvas.enabled)
        {
            _moneyText.text = $"{MoneyCount()}";
        }
        Debug.Log("speed="+GameManager.Instance.Speed);
        Debug.Log("stamina="+GameManager.Instance.Stamina);
    }

    private void SpeedButtonController()
    {
        _speedUpValue = System.Convert.ToInt32(_speedText.text);
        if (_speedUpValue < GameManager.Instance.Gold)
        {
            print("Paran Yeterli");
            GameManager.Instance.Speed = GameManager.Instance.SpeedStartValue - GameManager.Instance.SpeedIncreaseAmount;
            GameManager.Instance.SpeedStartValue = GameManager.Instance.Speed;
            if (GameManager.Instance.Speed <= 0.12f)
            {
                GameManager.Instance.Speed = 0.12f;
            }
            if (GameManager.Instance.GameState == GameStates.InGameWon)
            {
                GameManager.Instance.SpeedStartValue = 0.3f;
            }
            GameManager.Instance.Gold = GameManager.Instance.Gold - _speedUpValue;
        }
        else
        {
            print("paran yetersiz");
        }
    }

    private void IncomeButtonController()
    {
        _incomeUpValue = System.Convert.ToInt32(_incomeText.text);
        print(_incomeUpValue);
        if (_incomeUpValue < GameManager.Instance.Gold)
        {
            print("Paran Yeterli");
            GameManager.Instance.GoldIncreaseAmount = GameManager.Instance.BuyGoldIncreaseAmount + GameManager.Instance.GoldIncreaseAmount;
            GameManager.Instance.Gold = GameManager.Instance.Gold - _incomeUpValue;
        }
        else
        {
            print("paran yetersiz");
        }
    }

    public void StaminaButtonController()
    {
        _staminaUpValue = System.Convert.ToInt32(_staminaText.text);
        if (_staminaUpValue < GameManager.Instance.Gold)
        {
            print("Paran Yeterli");
            GameManager.Instance.Stamina = GameManager.Instance.StaminaIncreaseAmount + GameManager.Instance.StaminaStartValue;
            GameManager.Instance.StaminaStartValue = GameManager.Instance.Stamina;
            GameManager.Instance.Gold = GameManager.Instance.Gold - _staminaUpValue;
        }
        else
        {
            print("paran yetersiz");
        }
    }

    private void StartButtonController()
    {
        GameManager.Instance.InitializeGameStarted();
    }

    private float MoneyCount()
    {
        return GameManager.Instance.Gold;
    }
}
