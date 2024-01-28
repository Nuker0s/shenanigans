using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Synth4 : MonoBehaviour
{
    public float frequency = 440;
    public float gain = 0.01f;
    public float phase;
    private float increment;
    public float sampfreq;
    public AnimationCurve LFO1;
    public bool button = false;
    [SerializeField]
    public List<Wavecontainer> wavecontainers = new List<Wavecontainer>();
    public Wave currwave = null;
    public float time = 1;
    
    private void OnAudioFilterRead(float[] data, int channels)
    {
        
        increment = frequency * 2 * Mathf.PI / sampfreq;
        for (int i = 0; i < data.Length; i += channels)
        {
            
            phase += increment;
            
            if (currwave!=null)
            {
                data[i] = currwave.Process(phase,time) * gain;
            }
           
            
            if (channels == 2)
            {
                data[i + 1] = data[i];
            }
        }
        if (phase > 30000)
        {
            phase = increment;
        }
        /*frequency += deltatime * 20;
        if (frequency>880)
        {
            frequency = 440;
        }*/


    }
    private void Update()
    {
        if (button)
        {
            button = false;
            StartCoroutine(playsequence());
        }
    }
    public IEnumerator playsequence() 
    {
        foreach (Wavecontainer wavecontainer in wavecontainers) 
        {
            currwave = wavecontainer.wave;
            frequency = wavecontainer.frequency;
            time = wavecontainer.time;
            
            yield return new WaitForSeconds(wavecontainer.duration);
        }
    }
    


}
