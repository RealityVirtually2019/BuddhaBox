using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateBase : MonoBehaviour
{
    protected GameManager gm;

    public bool hasFocus = false;

   
    public virtual void GainFocus()
    {
        gm = GameManager.instance;

        hasFocus = true;
    }

    public virtual void DoUpdate()
    {

    }
    public virtual void LoseFocus()
    {
        hasFocus = false;
    }
}
