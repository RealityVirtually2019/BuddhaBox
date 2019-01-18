using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateWaiting : GameStateBase
{

    public override void DoUpdate()
    {
        if (GameManager.instance.modules.Get<InputHandler>().StartSignaled())
        {
            GameManager.instance.SetCurrentState(GameManager.instance.introduction);
        }
    }
 
}
