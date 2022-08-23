using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class RotateController 
{
    float YAxis;
    PlayerController _playerController;
    public RotateController(PlayerController playerController)
    {
        _playerController = playerController;
    }
    public void TickFixed()
    {
        YAxis = _playerController.transform.rotation.y;
        _playerController.transform.DORotate(new Vector3(0, 10, 0), 0.2f, RotateMode.WorldAxisAdd).SetEase(Ease.Linear);
    }
}
