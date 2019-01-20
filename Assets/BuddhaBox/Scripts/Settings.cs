using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{

    public enum MODE {BREATH_DETECTION, AUTO_PLAY, DEBUG};

    public MODE mode = MODE.BREATH_DETECTION;

    public float introductionDuration = 3; // in seconds
    public float finishingDuration = 5; // in seconds

    public float SecondsBetweenRecheckingBreathsPerMinute = 10;
    public float SecondsPerBreathForIntensity2 = 2;
    public float SecondsPerBreathForIntensity3 = 4;

    public float SecondsBetweenStatesInAutoplay = 60;

    public float TotalSecondsBeforeFinishingExperience = 240;

}
