using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class BasicSynth : MonoBehaviour
{
    public float gain;
    public float frequency = 440.0f;
    public float increment;
    public float phase;
    public float sampfreq = 48000.0f;
    public AnimationCurve c1;
    public Text toread;
    public float lettertime = 0.3f;
    public float spacetime = 0.5f;

    private void OnAudioFilterRead(float[] data, int channels)
    {
        //phase = 0;
        increment = frequency * 2 * Mathf.PI / sampfreq;
        for (int i = 0; i < data.Length; i+=channels)
        {
            phase += increment;
            if (phase > math.PI*1000) phase = 0;
                
            data[i] = gain * Mathf.Sin(c1.Evaluate(math.sin(phase)));
            if (channels == 2)
            {
                data[i + 1] = data[i];
            }
        }
    }
    private void Start()
    {
        StartCoroutine(soundloop());
    }
    public void textread() 
    {
        StartCoroutine(soundloop());
        StartCoroutine(soundloop());
    }
    public IEnumerator soundloop() 
    {
        string text = toread.text;
        foreach (var letter in text)
        {
            frequency = 0;
            foreach (var item in itemList)
            {
                if (item.Letter == (""+letter).ToUpper())
                {
                    frequency = item.Value;
                }
                
            }
            if (frequency==0)
            {
                yield return new WaitForSeconds(spacetime);
            }
            if(frequency != 0) 
            {
                yield return new WaitForSeconds(lettertime);
                frequency = 0;
            }
            
            

        }
        
    }
    List<Item> itemList = new List<Item>
        {
            new Item("E", 1200),
            new Item("F", 250),
            new Item("T", 900),
            new Item("W", 200),
            new Item("Y", 200),
            new Item("A", 800),
            new Item("I", 800),
            new Item("N", 800),
            new Item("O", 800),
            new Item("S", 800),
            new Item("G", 170),
            new Item("P", 170),
            new Item("H", 640),
            new Item("B", 160),
            new Item("R", 620),
            new Item("V", 120),
            new Item("D", 440),
            new Item("K", 120),
            new Item("L", 400),
            new Item("Q", 500),
            new Item("U", 340),
            new Item("J", 400),
            new Item("X", 400),
            new Item("C", 300),
            new Item("M", 300),
            new Item("Z", 200)
        };
    public class Item
    {
        public string Letter { get; set; }
        public int Value { get; set; }

        public Item(string letter, int value)
        {
            Letter = letter;
            Value = value;
        }
    }
}
