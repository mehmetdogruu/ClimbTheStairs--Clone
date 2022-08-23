using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class StairsController : MonoBehaviour
{
    private Queue<GameObject> _stairsPoolObjects;
    public List<GameObject> StairsList;

    [SerializeField] private GameObject _stairtsSpawn;
    [SerializeField] private GameObject _stairsStart;
    [SerializeField] private GameObject _stairsPrefab;
    [SerializeField] private float _poolSize;

    [Header("Color")]
    public Material _modelMat;
    public Color _whiteColor = Color.white;
    public Color _redColor = Color.red;

    private void Awake()
    {
        InstantiateStairs();
    }

    private void InstantiateStairs()
    {
        var stairsPool = new GameObject("StairsPool");
        _stairsPoolObjects = new Queue<GameObject>();
        StairsList = new List<GameObject>();
        for (int i = 0; i < _poolSize; i++)
        {
            var stairs = Instantiate(_stairsPrefab,stairsPool.transform);
            stairs.SetActive(false);
            _stairsPoolObjects.Enqueue(stairs);
            StairsList.Add(stairs);
        }
    }
    private GameObject GetPooledObject()
    {
        var stair = _stairsPoolObjects.Dequeue();
        stair.SetActive(true);
        _stairsPoolObjects.Enqueue(stair);
        return stair;
    }
    public void StairsSpawn()
    {
        var Stairs = GetPooledObject();
        Stairs.transform.localScale = Vector3.zero;
        Stairs.transform.DOScale(Vector3.one, 0.2f);
        Stairs.transform.DOMove(_stairtsSpawn.transform.position, .2f);
        Stairs.transform.position = _stairsStart.transform.position;
        Stairs.transform.rotation = _stairsStart.transform.rotation;
    }
}
