using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreathDetector : ModuleBase
{
   public float GetBPM()
    {
        return ((Mathf.Sin(Time.realtimeSinceStartup) + 1) * 15) + 10;
    }
}
