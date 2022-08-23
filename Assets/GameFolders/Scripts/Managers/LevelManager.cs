using System.Collections;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject _highScoreObject;
    [SerializeField] private TMP_Text _currentScoreText;
    [SerializeField] private TMP_Text _highScoreText;
    [SerializeField] private Transform winPos;
    [SerializeField] private GameObject _player;

    [SerializeField] private PlayerController _playerController;
    [SerializeField] private StairsController _stairsController;

    private void Start()
    {
        GameManager.Instance.OnGameLost += LevelEnd;
        GameManager.Instance.OnGameWon += LevelEnd;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameLost -= LevelEnd;
        GameManager.Instance.OnGameWon -= LevelEnd;
    }
    private float PlayerYPosition()
    {
        return _playerController.transform.localPosition.y;
    }

    public void Update()
    {
        if (PlayerYPosition()>=10)
        {
            GameManager.Instance.InitializeGameWon();
            CharacterAnimationController.instance.TriggerIdle();
        }
        IncreaseScore();
        InGameUiController.Instance.ProgressBarFiller(PlayerYPosition() / 10);
    }

    public void IncreaseScore()
    {

            var currentYPos = Mathf.Floor(PlayerYPosition() * 5f);
            var lastYPos = PlayerYPosition();

            PlayerPrefs.SetFloat("HighestScore", currentYPos);
            PlayerPrefs.SetFloat("HighScoreObjectYPosition", lastYPos);
            _currentScoreText.text = $"{currentYPos}";
    }

    private void LevelEnd()
    {
        StartCoroutine(LevelEndCo());
    }

    private IEnumerator LevelEndCo()
    {
        for (int i = 0; i < _stairsController.StairsList.Count; i++)
        {
            _stairsController.StairsList[i].GetComponent<Rigidbody>().isKinematic = false;
            _stairsController.StairsList[i].GetComponent<Rigidbody>().useGravity = true;
            _stairsController.StairsList[i].GetComponent<Rigidbody>().freezeRotation = false;
        }
        yield return new WaitForSeconds(1);

        _highScoreText.text = $"{PlayerPrefs.GetFloat("HighestScore")}";
        _highScoreObject.transform.position = new Vector3(_highScoreObject.transform.position.x, PlayerPrefs.GetFloat("HighScoreObjectYPosition"), _highScoreObject.transform.position.z);
        if (GameManager.Instance.GameState==GameStates.InGameLost ) _highScoreObject.transform.position = new Vector3(_highScoreObject.transform.position.x, PlayerPrefs.GetFloat("HighScoreObjectYPosition"), _highScoreObject.transform.position.z);

        _playerController.transform.position = Vector3.zero;
        _playerController.transform.rotation = Quaternion.identity;

        GameManager.Instance.InitializeReadyToClimb();

        for (int i = 0; i < _stairsController.StairsList.Count; i++)
        {
            _stairsController.StairsList[i].SetActive(false);
            _stairsController.StairsList[i].GetComponent<Rigidbody>().useGravity = false;
            _stairsController.StairsList[i].GetComponent<Rigidbody>().isKinematic = true;
            _stairsController.StairsList[i].GetComponent<Rigidbody>().freezeRotation = true;
        }
        GameManager.Instance.Stamina = GameManager.Instance.StaminaStartValue;
    }
}
