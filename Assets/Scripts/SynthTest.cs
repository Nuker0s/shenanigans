using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
public class SynthTest : MonoBehaviour
{
    public float gain;
    public float frequency = 440.0f;
    public float increment;
    public float phase;
    public float sampfreq = 48000.0f;
    public AudioSource aus;
    public AnimationCurve c1;
    public float lettertime = 0.3f;
    public float spacetime = 0.5f;
    public Slider incslider;
    public Slider gainslider;
    public Slider phasediv;
    public Slider Freqslider;
    public Slider pitchslider;

    private void OnAudioFilterRead(float[] data, int channels)
    {
        //phase = 0;
        increment = frequency * incslider.value * Mathf.PI / sampfreq;
        for (int i = 0; i < data.Length; i += channels)
        {
            phase += increment;
            if (phase > math.PI * 10000) phase = 0;

            data[i] = gain * Mathf.Sin(Mathf.PerlinNoise1D(phase / phasediv.value));
            if (channels == 2)
            {
                data[i + 1] = data[i];
            }
        }
    }
    private void Update()
    {
        gain = gainslider.value;
        frequency = Freqslider.value;
        aus.pitch = pitchslider.value;
    }
    private void Start()
    {
        //StartCoroutine(soundloop());
    }
   
}

