using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateBase : MonoBehaviour
{

    public bool hasFocus = false;

    public virtual void GainFocus()
    {
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
