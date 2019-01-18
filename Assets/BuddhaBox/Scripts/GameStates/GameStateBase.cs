using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateBase : MonoBehaviour
{
    protected GameManager gm;

    public bool hasFocus = false;

    public void Awake()
    {
        gm = GameManager.instance;
    }

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
