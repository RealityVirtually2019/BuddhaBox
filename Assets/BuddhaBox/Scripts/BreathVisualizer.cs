using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreathVisualizer : ModuleBase
{
    public ParticleSystem particles;
    public ParticleSystem.EmissionModule emission;

    public BreathDetector detector;

    public TMPro.TextMeshProUGUI text;

    public float particleEmissionWhenBreathing = 50;

    public void Start()
    {
        detector = GameManager.instance.modules.Get<BreathDetector>();
        emission = particles.emission;
        StartCoroutine(DelayedParticleDeactivate());
    }

    IEnumerator DelayedParticleDeactivate()
    {
        while (true)
        {
            yield return new WaitForSeconds(4);
            if(detector.breathState != BreathDetector.BREATH_STATE.BREATHING){
                particles.gameObject.SetActive(false);
                yield return new WaitForEndOfFrame();
                particles.gameObject.SetActive(true);
            }
         


        }


    }

    public override void DoUpdate()
    {
        switch (detector.breathState)
        {
            case BreathDetector.BREATH_STATE.BREATHING:
                emission.rateOverTime = particleEmissionWhenBreathing;
                text.text = "Breathing: "+detector.DbValue;
                particles.gameObject.SetActive(true);

                break;
            case BreathDetector.BREATH_STATE.NOT_BREATHING:
                emission.rateOverTime = 0;
                text.text = "Not breathing: " + detector.DbValue;
                break;
        }
    }
}
