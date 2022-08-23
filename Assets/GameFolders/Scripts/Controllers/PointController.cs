using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class PointController : MonoBehaviour
{

    [SerializeField] private GameObject _stairsPoint;
    [SerializeField] private GameObject _pointPrefab;
    [SerializeField] private float _poolSize;
    private TMP_Text _pointText;
    private Queue<GameObject> _stairsPointPoolObjects;
    private void Awake()
    {
        InstantiatePoint();
    }

    private void InstantiatePoint()
    {
        var pointPool = new GameObject("PointPool");
        _stairsPointPoolObjects = new Queue<GameObject>();
        for (int i = 0; i < _poolSize; i++)
        {
            var pointBonus = Instantiate(_pointPrefab, pointPool.transform);
            pointBonus.SetActive(false);
            _stairsPointPoolObjects.Enqueue(pointBonus);
        }
    }
    private GameObject GetPooledObject()
    {
        var point = _stairsPointPoolObjects.Dequeue();
        point.SetActive(true);
        _stairsPointPoolObjects.Enqueue(point);
        return point;
    }
    public IEnumerator PointsRoutine()
    {
        InGameUiController.Instance.CoinTextUpdate();
        var bonusPoints = GetPooledObject();
        _pointText = bonusPoints.GetComponent<TMP_Text>();
        _pointText.text = $" $ {GameManager.Instance.GoldIncreaseAmount}";
        bonusPoints.transform.localScale = Vector3.one;
        bonusPoints.transform.position = _stairsPoint.transform.position;

        yield return new WaitForSeconds(0.3f);

        bonusPoints.transform.DOScale(Vector3.zero, 0.5f);
    }

}
