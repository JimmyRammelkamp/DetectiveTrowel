using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    GameManager.GameStatus gameStatus;
    bool isAnimating;

    [Header("Hand Instances")]
    [SerializeField] GameObject LeftHand;
    [SerializeField] GameObject RightHand;

    [Header("Available Positions")]
    [SerializeField] Transform restPosition;

    void Start()
    {
        GameManager.instance.onStatusUpdated += UpdateHands;
    }

    private void UpdateHands()
    {
        gameStatus = GameManager.instance.GetStatus();
    }
}
