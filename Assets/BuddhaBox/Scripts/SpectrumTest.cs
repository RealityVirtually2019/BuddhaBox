using UnityEngine;
using UnityEngine.Audio;

public class SpectrumTest : MonoBehaviour
{

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
    public float PitchValue;

    void Start()
    {
        //RestartMicrophone();
        //numSamples = Mathf.ClosestPowerOfTwo(numSamples);
        numSamples = 1024;

        spectrum = new float[numSamples];
        Debug.Log(Time.fixedDeltaTime);
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

    int t = 0;
    int j = 0;
    float lm = -100;
    float lmin = 100;
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

        if (DbValue > lm)
        {
            lm = DbValue;
        }
        if (DbValue < lmin)
        {
            lmin = DbValue;
        }

        if ((Time.unscaledTime - lastMicRestartTime) > micRestartWait)
        {
            //RestartMicrophone();
        }

        ++t;
        if (t>=25)
        {
            t = 0;
            ++j;
            Debug.Log("time:" + j + " max:" + lm + " min:" + lmin);
            lm = -100;
            lmin = 100;
        }
    }
    
}
