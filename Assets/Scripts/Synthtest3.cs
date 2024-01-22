using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Synthtest3 : MonoBehaviour
{
    public float gain= 0.01f;
    public float frequency = 440.0f;
    public float incmult;
    public float increment = 0;
    public float phase = 0;
    public float sampfreq = 48000.0f;
    public float deltatime=0;
    public float variable = 0;
    // Start is called before the first frame update
    private void OnAudioFilterRead(float[] data, int channels)
    {
        
        increment = frequency * incmult * Mathf.PI / sampfreq;
        for (int i = 0; i < data.Length; i += channels)
        {
            phase += increment;
            float phasevar = phase;
            phasevar = math.sin(phasevar);
            data[i] = phasevar * gain;
            if (channels == 2)
            {
                data[i + 1] = data[i];
            }
        }
        if (phase>30000)
        {
            phase = increment;
        }
        /*frequency += deltatime * 20;
        if (frequency>880)
        {
            frequency = 440;
        }*/
        

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        deltatime = Time.deltaTime;
    }
}
