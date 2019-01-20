using System.Collections;
using UnityEngine;

public class BreathVisualizer : ModuleBase
{
    public ParticleSystem particles;
    public ParticleSystem.EmissionModule emission;

    public BreathDetector detector;

    public TMPro.TextMeshProUGUI text;

    public float particleEmissionWhenBreathing = 50;

    public Light light;

    public float maxLightIntensity = 0.5f;
    private float targetLight = 0;

    public void Start()
    {
        detector = GameManager.instance.modules.Get<BreathDetector>();
        emission = particles.emission;
        //  StartCoroutine(DelayedParticleDeactivate());
    }

    IEnumerator DelayedParticleDeactivate()
    {
        while (true)
        {
            yield return new WaitForSeconds(4);
            if (detector.breathState != BreathDetector.BREATH_STATE.BREATHING)
            {
                particles.gameObject.SetActive(false);
                yield return new WaitForEndOfFrame();
                particles.gameObject.SetActive(true);
            }



        }


    }
    float lightVelocity;
    public float lightDamp = 1;
    public override void DoUpdate()
    {
        //   if (!GameManager.instance.modules.Get<Narrator>().IsPlaying())
        if (!GameManager.instance.currentState != GameManager.instance.introduction)
        {
            switch (detector.breathState)
            {
                case BreathDetector.BREATH_STATE.BREATHING:
                    targetLight = maxLightIntensity;
                    //  emission.rateOverTime = particleEmissionWhenBreathing;
                    text.text = "Breathing:\n" + detector.DbValue;
                    // particles.gameObject.SetActive(true);

                    break;
                case BreathDetector.BREATH_STATE.NOT_BREATHING:
                    // emission.rateOverTime = 0;
                    targetLight = 0;

                    text.text = "Not breathing: " + detector.DbValue;
                    break;
            }
        }
        light.intensity = Mathf.SmoothDamp(light.intensity, targetLight, ref lightVelocity, lightDamp);
    }
}
