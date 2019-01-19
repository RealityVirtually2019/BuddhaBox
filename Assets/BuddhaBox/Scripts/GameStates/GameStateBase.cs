using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateBase : MonoBehaviour
{
    protected GameManager gm;

    public bool hasFocus = false;

    public bool hasNarration = false;
    public int narrationID = 0;
   
    public virtual void GainFocus()
    {
        gm = GameManager.instance;

        if (hasNarration)
        {
            gm.modules.Get<Narrator>().Play(narrationID);
        }

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
