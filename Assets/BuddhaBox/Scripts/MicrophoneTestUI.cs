using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Linq;

public class MicrophoneTestUI : MonoBehaviour
{
    public Text text;
    public SpectrumTest spectrum;

    public float dbForBreathing = 5;
    public float dbForNotBreathing = 1;

    public enum BREATH_STATE {UNKNOWN, BREATHING, NOT_BREATHING}
    public BREATH_STATE breathState = BREATH_STATE.UNKNOWN;

    public enum BREATH_METSASTATE { RED, YELLO, BLUE }
    public BREATH_METSASTATE breathMetaState = BREATH_METSASTATE.RED;

    public Queue<float> decibleHistory = new Queue<float>();
    public float historyMax = 50;
    float average = 0;
    public int count = 0;
    int t = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ++t;
        if (t>=600)
        {
            float qq = count / 2;
            if (qq > 4)
            {
                breathMetaState = BREATH_METSASTATE.RED;
            }
            else if (qq<2)
            {
                breathMetaState = BREATH_METSASTATE.BLUE;
            }
            else
            {
                breathMetaState = BREATH_METSASTATE.YELLO;
            }
            Debug.Log(qq + " breathes per 12 sec. status: " + breathMetaState);
            t = 0;
            count = 0;
        }

        decibleHistory.Enqueue(spectrum.DbValue);
        average = decibleHistory.Sum() / decibleHistory.Count();

        if(decibleHistory.Count() > historyMax)
        {
            decibleHistory.Dequeue();
        }
        if(average > dbForBreathing)
        {
            if (breathState != BREATH_STATE.BREATHING)
            {
                ++count;
            }
            breathState = BREATH_STATE.BREATHING;
        } else if (average < dbForBreathing)
        {
            breathState = BREATH_STATE.NOT_BREATHING;
        }
        //text.text = "State: "+breathState+ "\nDB: " + spectrum.DbValue;
    }
}
