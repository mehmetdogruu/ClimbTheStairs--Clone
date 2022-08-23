using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] StairsController _stairsController;
    [SerializeField] PointController _pointController;

    [SerializeField] GameObject scoreTable;
    [SerializeField] GameObject _wonPosition;
    [SerializeField] GameObject _explosionEffect;
    public GameObject _sweatingEffect;
    [SerializeField] GameObject _model;
    [SerializeField] Material _playerMat;

    private RotateController _rotateConroller;
    private float _spawnTime;
 
    private void Start()
    {
        SetStartColor();
        GameManager.Instance.StaminaStartValue = 13;
        GameManager.Instance.Stamina = GameManager.Instance.StaminaStartValue;
        GameManager.Instance.GoldIncreaseAmount = 0.5f;
        GameManager.Instance.SpeedStartValue = 0.3f;
        GameManager.Instance.Speed = GameManager.Instance.SpeedStartValue;
        GameManager.Instance.SpeedIncreaseAmount = 0.05f;
        _sweatingEffect.SetActive(false);

        GameManager.Instance.OnReadyToRun += SetStartColor;
    }
    private void Awake()
    {
        _rotateConroller = new RotateController(this);
    }
    private void FixedUpdate()
    {
        scoreTable.transform.position = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z);
        if (GameManager.Instance.GameState==GameStates.InGameStarted)
        {
            if (Input.GetMouseButton(0))
            {
                if (GameManager.Instance.Stamina > 0)
                {
                    GameManager.Instance.Stamina -= Time.fixedDeltaTime;
                    _spawnTime += Time.fixedDeltaTime;
                    CharacterAnimationController.instance.TriggerRun();
                    _rotateConroller.TickFixed();

                    if (_spawnTime > GameManager.Instance.Speed)
                    {
                        _stairsController.StairsSpawn();
                        StartCoroutine(_pointController.PointsRoutine());
                        float lerpY = Mathf.Lerp(transform.position.y, transform.position.y + 0.05f, 3);
                        transform.position = new Vector3(transform.position.x, lerpY, transform.position.z);
                        _spawnTime = 0;
                    }
                }
            }
            else
            {
                CharacterAnimationController.instance.TriggerIdle();
            }
            if (GameManager.Instance.Stamina <= 0)
            {
                CharacterAnimationController.instance.TriggerIdle();
                GameManager.Instance.InitaliazeGameLost();
                SetStartColor();
                _model.SetActive(false);
            }
            if (GameManager.Instance.Stamina < 5)
            {
                GameManager.Instance.InitializeTimeRunningOut();
                _stairsController._modelMat.color = Color.Lerp(_stairsController._modelMat.color, _stairsController._redColor, 0.02f);
                _sweatingEffect.SetActive(true);
            }
            if (GameManager.Instance.Stamina >= 5)
            {
                _sweatingEffect.SetActive(false);
                _stairsController._modelMat.color = Color.Lerp(_stairsController._modelMat.color, _stairsController._whiteColor, 0.02f);
                _model.SetActive(true);
            }
            GameManager.Instance.Stamina += 0.2f * Time.deltaTime;
        }       
    }
    public void SetStartColor()
    {
        _playerMat.color = Color.white;
    }
}
