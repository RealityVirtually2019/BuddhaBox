using UnityEngine;

public class Narrator : ModuleBase
{
    public AudioSource audiosource;
    public AudioClip[] clips;

    public float minimumPeriodBetweenNarration = 20;
    private float clock = 20;
    public override void DoUpdate()
    {
        base.DoUpdate();
        clock += Time.deltaTime;
    }

    public void Play(int number)
    {
        if (clock > minimumPeriodBetweenNarration)
        {
            clock = 0;
            audiosource.clip = clips[number - 1];
            audiosource.Play();
        }
    }
}
