using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class ChangeTheSky : MonoBehaviour
{
    public int notesAmount;
    
    public Material skies;
    public float exposure;

    public float index = 0.000000001f;
    float minExposure = 1;

    // Start is called before the first frame update
    void Start()
    {
        notesAmount = 0;

        skies.SetFloat("_Exposure", exposure);
        exposure = 4;
    }

    // Update is called once per frame
    void Update()
    {
        notesAmount = GameObject.Find("Player").GetComponent<PlayerEngine_v2>().notesAmount;

        SetSkyDarkness();
    }

    public void SetSkyDarkness()
    {
        if (notesAmount == 0)
        {
            skies.SetFloat("_Exposure", exposure);
            exposure = 4f;
        }
        else if (notesAmount == 1)
        {
            skies.SetFloat("_Exposure", exposure);
            exposure = 3f;
        }
        else if (notesAmount == 2)
        {
            skies.SetFloat("_Exposure", exposure);
            exposure = 2f;
        }
        else if (notesAmount == 3)
        {
            skies.SetFloat("_Exposure", exposure);
            exposure = 1f;
        }
        else
        {
            skies.SetFloat("_Exposure", exposure);
            exposure = 0.5f;
        }
    }

    public void GettingDarker()
    {
        PlayerEngine_v2 playerScript = GameObject.FindWithTag("Player").GetComponent<PlayerEngine_v2>();

        if (playerScript.isCollected && playerScript.notesAmount >= 3 && exposure >= minExposure)
        {
            skies.SetFloat("_Exposure", exposure);
            exposure -= index;
        }
    }

    public void SetDefault() //just to test if it works
    {
            skies.SetFloat("_Exposure", exposure);
            exposure =5;
    }
}
