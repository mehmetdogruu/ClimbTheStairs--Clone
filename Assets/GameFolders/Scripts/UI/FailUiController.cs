using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FailUiController : UIController<FailUiController>
{
    [SerializeField] private Button _retryButton;
    [SerializeField] private GameObject _highScoreObject;
    [SerializeField] private TMP_Text _highScoreText;

    private void OnEnable()
    {
        _retryButton.onClick.AddListener(RetryButtonPressed);
    }
    private void Start()
    {
        GameManager.Instance.OnGameLost += Show;
        GameManager.Instance.OnGameLost += SetHighScore;
    }

    private void OnDisable()
    {
        _retryButton.onClick.RemoveListener(RetryButtonPressed);
        GameManager.Instance.OnGameLost -= Show;
        GameManager.Instance.OnGameLost -= SetHighScore;
    }


    private void RetryButtonPressed()
    {
        GameManager.Instance.InitializeReadyToClimb();
        IntroUiController.Instance.Show();
        HideInstant();
    }
    public void SetHighScore()
    {
        _highScoreText.text = $"{PlayerPrefs.GetFloat("HighestScore")}";
        _highScoreObject.transform.position = new Vector3(_highScoreObject.transform.position.x, PlayerPrefs.GetFloat("HighScoreObjectYPosition"), _highScoreObject.transform.position.z);
    }
}
