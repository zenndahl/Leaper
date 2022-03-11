using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public event Action onTargetCaptured;
    public void TargetCaptured()
    {
        if(onTargetCaptured != null)
        {
            onTargetCaptured();
        }
    }

    public event Action<int> onAddTime;
    public void AddTime(int time)
    {
        if(onAddTime != null)
        {
            onAddTime(time);
        }
    }

    public event Action onGameOver;
    public void GameOver()
    {
        if (onGameOver != null)
        {
            onGameOver();
        }
    }
}
