using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public ModuleManager modules;

    public Settings settings;

    public StateIntroduction introduction;
    public StateFinishing finishing;

    public StateIntensity1 intenstity1;
    public StateIntensity2 intenstity2;
    public StateIntensity3 intenstity3;

    public GameStateBase currentState;

    [HideInInspector]
    public Player player;

    public void Awake()
    {
        GameManager.instance = this;
      
    }

    public void Start()
    {
        player = modules.Get<Player>();
        currentState = introduction;
        currentState.GainFocus();
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
