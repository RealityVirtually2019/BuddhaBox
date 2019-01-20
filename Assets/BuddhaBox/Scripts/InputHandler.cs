using UnityEngine;

public class InputHandler : ModuleBase
{
    GameManager gm;


    public float intensityClock = 0;
    public float decisionClock = 0;

    private BreathDetector breathDetector;

    private void Start()
    {
        gm = GameManager.instance;
        breathDetector = gm.modules.Get<BreathDetector>();

    }


    private int CheckBreathingRate()
    {
        return 0;
    }

    public override void DoUpdate()
    {
        if (gm.currentState != gm.introduction && gm.currentState != gm.finishing)
        {
            intensityClock += Time.deltaTime;
            decisionClock += Time.deltaTime;

        }

        switch (gm.settings.mode)
        {
            case Settings.MODE.AUTO_PLAY:
              
                    if (intensityClock > gm.settings.SecondsBetweenStatesInAutoplay)
                    {
                        intensityClock = 0;
                        gm.SetCurrentState((gm.currentState as StateIntensityBase).nextState);
                    }
                break;
            case Settings.MODE.BREATH_DETECTION:
                if (decisionClock > gm.settings.SecondsBetweenRecheckingBreathsPerMinute)
                {
                    float bpm = breathDetector.GetBPM();
                    Debug.Log("Making mood decision based on breath per minute: " + bpm);
                    decisionClock = 0;

                    if (bpm > gm.settings.BreathsPerMinuteForIntensity1)
                    {
                        gm.SetCurrentState(gm.intenstity1);

                    }
                    else if (bpm > gm.settings.BreathsPerMinuteForIntensity2)
                    {
                        gm.SetCurrentState(gm.intenstity2);

                    }
                    else
                    {
                        gm.SetCurrentState(gm.intenstity3);

                    }
                }
                break;
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
