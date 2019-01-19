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
    public float[] spectrum;
    string microphoneName;
    float lastMicRestartTime;
    float micRestartWait = 20;

    void Start()
    {
        RestartMicrophone();
        numSamples = Mathf.ClosestPowerOfTwo(numSamples);

       //initialise arrays
        spectrum = new float[numSamples];
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

        microphoneName = null; //if type is Microphone, the default microphone will be used. If StereoMix, 'Stereo Mix' will be searched for in the list.

        audioSource.loop = true;
        audioSource.outputAudioMixerGroup = muteGroup;

        AudioClip clip1 = audioSource.clip = Microphone.Start(microphoneName, true, 5, 44100);
        audioSource.clip = clip1;

        while (!(Microphone.GetPosition(microphoneName) - 0 > 0)) { }
        audioSource.Play();
        lastMicRestartTime = Time.unscaledTime;
        //print("restarted mic");
    }


    void Update()
    {
        audioSource.GetSpectrumData(spectrum, sampleChannel, windowUsed); //get the spectrum data
        if ((Time.unscaledTime - lastMicRestartTime) > micRestartWait)
        {
            RestartMicrophone();
        }
    }
    
}
