using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

public class BreathDetector : ModuleBase
{
    public float GetSecondsBetweenBreaths()
    {
        return averageBreathPeriod;
    }

    [SerializeField]
    public AudioMixerGroup muteGroup; //the AudioMixerGroup used for silent tracks (microphones). Don't change.

    #region SAMPLING PROPERTIES
    /// <summary>
    /// The AudioSource to take data from. Can be empty if sourceType is not AudioSource.
    /// </summary>
    [Tooltip("The AudioSource to take data from.")]
    public AudioSource audioSource;

    /// <summary>
    /// The audio channel to use when sampling.
    /// </summary>
    [Tooltip("The audio channel to use when sampling.")]
    public int sampleChannel = 0;
    /// <summary>
    /// The number of samples to use when sampling. Must be a power of two.
    /// </summary>
    [Tooltip("The number of samples to use when sampling. Must be a power of two.")]
    public int numSamples = 256;
    /// <summary>
    /// The FFTWindow to use when sampling.
    /// </summary>
    [Tooltip("The FFTWindow to use when sampling.")]
    public FFTWindow windowUsed = FFTWindow.BlackmanHarris;

    /// <summary>
    /// The lower bound of the freuqnecy range to sample from. Leave at 0 when unused.
    /// </summary>
    [Tooltip("The lower bound of the freuqnecy range to sample from. Leave at 0 when unused.")]
    public float frequencyLimitLow = 0;

    /// <summary>
    /// The uppwe bound of the freuqnecy range to sample from. Leave at 22050 when unused.
    /// </summary>
    [Tooltip("The upper bound of the freuqnecy range to sample from. Leave at 22050 (44100/2) when unused.")]
    public float frequencyLimitHigh = 22050;


    #endregion
    private float[] spectrum;
    string microphoneName;
    float lastMicRestartTime;
    float micRestartWait = 20;

    public float RmsValue;
    public float DbValue;

    public float dbForBreathing = 5;
    public float dbForNotBreathing = 1;

    public enum BREATH_STATE { UNKNOWN, BREATHING, NOT_BREATHING }
    public BREATH_STATE breathState = BREATH_STATE.UNKNOWN;

    public enum BREATH_METSASTATE { RED, YELLO, BLUE }
    public BREATH_METSASTATE breathMetaState = BREATH_METSASTATE.RED;

    public Queue<float> decibleHistory = new Queue<float>();
    public float historyMax = 50;
    float average = 0;

    private float secondsSinceLastBreath = 0;
    public Queue<float> breathPeriodHistory = new Queue<float>();
    public float breathPeriodHistoryQueueSize = 4;
    public float averageBreathPeriod = 0;

    private float isBreathingTimer = 0;
    public float minimumRequiredBreathTime = 0.2f;

    void Start()
    {
        RestartMicrophone();
        numSamples = 1024;

        spectrum = new float[numSamples];
        //Debug.Log(Time.fixedDeltaTime);
    }

    /// <summary>
    /// Restarts the Microphone recording.
    /// </summary>
    public void RestartMicrophone()
    {
        Microphone.End(microphoneName);

        //set up microphone input source if required
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) audioSource = gameObject.AddComponent<AudioSource>();

        if (Microphone.devices.Length == 0)
        {
            Debug.LogError("Error from SimpleSpectrum: Microphone or Stereo Mix is being used, but no Microphones are found!");
        }

        microphoneName = null;
        audioSource.loop = true;
        audioSource.outputAudioMixerGroup = muteGroup;
        AudioClip clip1 = audioSource.clip = Microphone.Start(microphoneName, true, 5, 44100);
        audioSource.clip = clip1;

        while (!(Microphone.GetPosition(microphoneName) - 0 > 0)) { }
        audioSource.Play();
        lastMicRestartTime = Time.unscaledTime;
    }


    float RefValue = 0.01f;

    void FixedUpdate()
    {
        audioSource.GetOutputData(spectrum, sampleChannel);

        int i;
        float sum = 0;
        for (i = 0; i < numSamples; i++)
        {
            sum += spectrum[i] * spectrum[i]; // sum squared samples
        }
        RmsValue = Mathf.Sqrt(sum / numSamples); // rms = square root of average
        DbValue = 20 * Mathf.Log10(RmsValue / RefValue); // calculate dB
        if (DbValue < -160) DbValue = -160; // clamp it to -160dB min
 
        CalculateBPM();
    }


    void CalculateBPM()
    {
        decibleHistory.Enqueue(DbValue);
        average = decibleHistory.Sum() / decibleHistory.Count();

        while (decibleHistory.Count() > historyMax)
        {
            decibleHistory.Dequeue();
        }

        secondsSinceLastBreath += Time.deltaTime;
        switch (breathState)
        {
            case BREATH_STATE.BREATHING:
                isBreathingTimer += Time.deltaTime;
                if (average < dbForNotBreathing)
                {
                    if(isBreathingTimer > minimumRequiredBreathTime){
                        BreathHappened();
                    } else
                    {
                        Debug.Log("Breath too short");
                    }
                    isBreathingTimer = 0;
                    breathState = BREATH_STATE.NOT_BREATHING;
                }
                break;
            case BREATH_STATE.UNKNOWN:

            case BREATH_STATE.NOT_BREATHING:
                if (average > dbForBreathing)
                {
                    breathState = BREATH_STATE.BREATHING;
                }
                break;
        }
    }


    private void BreathHappened()
    {
        breathPeriodHistory.Enqueue(secondsSinceLastBreath);
        secondsSinceLastBreath = 0;
        if (breathPeriodHistory.Count() > breathPeriodHistoryQueueSize)
        {
            breathPeriodHistory.Dequeue();
        }
        averageBreathPeriod = breathPeriodHistory.Sum() / breathPeriodHistory.Count();
    }


}
