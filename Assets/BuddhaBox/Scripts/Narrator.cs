using UnityEngine;

public class Narrator : ModuleBase
{
    public AudioSource audiosource;
    public AudioClip[] clips;

    public float minimumPeriodBetweenNarration = 20;
    private float clock = 30000;
    public override void DoUpdate()
    {
        base.DoUpdate();
        clock += Time.deltaTime;
    }

    public bool IsPlaying()
    {
        return audiosource.isPlaying;
    }

    public void Play(int number)
    {
        if (clock > minimumPeriodBetweenNarration)
        {
            if(!audiosource.isPlaying){
            clock = 0;
            audiosource.clip = clips[number - 1];
            audiosource.Play();
            Debug.Log("Playing " + number);
            } else
            {
                Debug.Log("Already playing. can't play: " + number);

            }
        } else
        {
            Debug.Log("too soon to play: " + number);
        }
    }
}
