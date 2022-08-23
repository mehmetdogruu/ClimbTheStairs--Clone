using UnityEngine.UI;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class InGameUiController : UIController<InGameUiController>
{
    [SerializeField] private Image _coinImage;
    [SerializeField] private TMP_Text _coinText;
    [SerializeField] private Image _fillBar;

    private void Start()
    {
        GameManager.Instance.OnGameStarted += Show;
        GameManager.Instance.OnGameWon += Hide;
        GameManager.Instance.OnGameLost += Hide;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameStarted -= Show;
        GameManager.Instance.OnGameWon -= Hide;
        GameManager.Instance.OnGameLost -= Hide;
    }
    public void ProgressBarFiller(float counter)
    {
        _fillBar.DOFillAmount(counter, .25f); 
    }

    private void CoinImageScaleEffect()
    {
        _coinImage.transform.DOPunchScale(.1f * Vector3.one, .25f, 1).OnComplete(()=> _coinImage.transform.DOScale(Vector3.one,.25f));
    }

    public void CoinTextUpdate()
    {
        _coinText.text = $"{GameManager.Instance.Gold}";
        var before = GameManager.Instance.Gold;
        GameManager.Instance.Gold += GameManager.Instance.GoldIncreaseAmount;
        DOTween.To(() => before, x => before = x, GameManager.Instance.Gold, .25f).OnUpdate(() => _coinText.text = before.ToString("F1"));
        CoinImageScaleEffect();
    }
}
