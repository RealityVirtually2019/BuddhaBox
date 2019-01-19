using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : ModuleBase
{
    bool startSignaled = false;

  //  public enum STATES 

    public bool StartSignaled()
    {
        return startSignaled;
    }

    public override void DoUpdate()
    {
        startSignaled = Input.GetKeyDown(KeyCode.Space);
      
        base.DoUpdate();
    }
}
