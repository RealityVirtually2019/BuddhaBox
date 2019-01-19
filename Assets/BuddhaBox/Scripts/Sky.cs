using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sky : ModuleBase
{
    public Material skybox;

    public Color targetColor;
    public Color lastColor;

    public float changeDuration = 2;
    public float changeClock = 0;

    public Light sun;

    public void ChangeColor(Color color)
    {
        changeClock = 0;
        targetColor = color;
        lastColor = skybox.GetColor("_SkyTint");
       // skybox.SetColor("_SkyTint", color);
       // skybox.SetColor("_GroundColor", color);
    }

    public void ForceColor(Color color)
    {
        skybox.SetColor("_SkyTint", color);
        skybox.SetColor("_GroundColor", color);
        sun.color = color;

        changeClock = 1;

    }
    public override void DoUpdate()
    {
       if(changeClock < 1)
        {
            skybox.SetColor("_SkyTint", Color.Lerp(lastColor, targetColor, changeClock));
            skybox.SetColor("_GroundColor", Color.Lerp(lastColor, targetColor, changeClock));
            sun.color = Color.Lerp(lastColor, targetColor, changeClock);
            RenderSettings.fogColor = Color.Lerp(lastColor, targetColor, changeClock);


            changeClock += Time.deltaTime / changeDuration;

        }
    }


}
