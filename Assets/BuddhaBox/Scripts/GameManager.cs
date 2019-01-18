using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public ModuleManager modules;

    public StateWaiting waiting;
    public StateIntroduction introduction;

    public GameStateBase currentState;

    public void Awake()
    {
        GameManager.instance = this;
        currentState = waiting;
        waiting.GainFocus();
    }

    public void SetCurrentState(GameStateBase newState)
    {
        if(currentState != null)
        {
            currentState.LoseFocus();
        }
        currentState = newState;
        currentState.GainFocus();
    }

    public void Update()
    {
        currentState.DoUpdate();
    }
    
}
