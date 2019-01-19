using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : ModuleBase
{
    GameManager gm;

    private void Start()
    {
         gm = GameManager.instance;
    }

    
    public override void DoUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            gm.SetCurrentState(gm.introduction);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
           gm.SetCurrentState(gm.intenstity1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            gm.SetCurrentState(gm.intenstity2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            gm.SetCurrentState(gm.intenstity3);
        }

        base.DoUpdate();
    }
}
