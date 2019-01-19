using UnityEngine;

public class InputHandler : ModuleBase
{
    GameManager gm;

    public bool autoPlay = false;
    public float secondsPerIntensity = 5;

    public float intensityClock = 0;

    private void Start()
    {
        gm = GameManager.instance;
    }


    private int CheckBreathingRate()
    {
        return 0;
    }

    public override void DoUpdate()
    {
        if (autoPlay)
        {
            if (gm.currentState != gm.introduction && gm.currentState != gm.finishing)
            {
                intensityClock += Time.deltaTime;

            }
            if (intensityClock > secondsPerIntensity)
            {
                intensityClock = 0;
                gm.SetCurrentState((gm.currentState as StateIntensityBase).nextState);
            }
        }
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
