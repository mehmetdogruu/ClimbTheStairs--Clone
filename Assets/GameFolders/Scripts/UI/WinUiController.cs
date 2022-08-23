using UnityEngine;
using UnityEngine.UI;

public class WinUiController : UIController<WinUiController>
{
    [SerializeField] private Button _nextButton;
    [SerializeField] GameObject _player;

    private void OnEnable()
    {
        _player.SetActive(false);
        _nextButton.onClick.AddListener(NextButtonPressed);
    }

    private void Start()
    {
        GameManager.Instance.OnGameWon -= Hide;
        GameManager.Instance.OnGameStarted += Hide;
        GameManager.Instance.OnGameStarted -= Show;
        GameManager.Instance.OnGameWon += Show;
        _player.SetActive(true);
    }

    private void OnDisable()
    {
        _nextButton.onClick.RemoveListener(NextButtonPressed);
        PlayerPrefs.DeleteAll();
        _player.SetActive(true);
    }

    private void NextButtonPressed()
    {
        GameManager.Instance.InitializeReadyToClimb();
        IntroUiController.Instance.Show();
        HideInstant();
        _player.SetActive(true);
    }
}
