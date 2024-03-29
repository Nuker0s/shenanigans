using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;


public class BasicSynth : MonoBehaviour
{
    public float gain;
    public float frequency = 440.0f;
    public float increment;
    public float incmult;
    public float phase;
    public float phasediv;
    public float sampfreq = 48000.0f;
    public AnimationCurve c1;
    public Text toread;
    public float lettertime = 0.3f;
    public float spacetime = 0.5f;
    public Slider gainslider;
    private Vector3 ones = new Vector3(1,1,1);
    public List<Lettersounds>  letters = new List<Lettersounds>();

    private void OnAudioFilterRead(float[] data, int channels)
    {
        //phase = 0;
        increment = frequency * incmult * Mathf.PI / sampfreq;
        for (int i = 0; i < data.Length; i+=channels)
        {
            phase += increment;
            if (phase > math.PI*1000) phase = 0;
                
            data[i] = gain * Mathf.Sin(Mathf.PerlinNoise1D(phase/phasediv));
            if (channels == 2)
            {
                data[i + 1] = data[i];
            }
        }
    }
    private void Update()
    {
        gain = gainslider.value;
    }
    private void Start()
    {
        //StartCoroutine(soundloop());
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
            Lettersounds tosay;
            foreach (var item in letters)
            {
                if (item.Letter == (""+letter).ToUpper())
                {
                    tosay = item;
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



    /*public IEnumerator soundloop()
    {
        string text = toread.text;
        foreach (var letter in text)
        {
            frequency = 0;
            Lettersounds tosay;
            foreach (var item in letters)
            {
                if (item.Letter == ("" + letter).ToUpper())
                {
                    frequency = item.Value;
                }

            }
            if (frequency == 0)
            {
                yield return new WaitForSeconds(spacetime);
            }
            if (frequency != 0)
            {
                yield return new WaitForSeconds(lettertime);
                frequency = 0;
            }



        }

    }*/


    /*
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
    }*/
    [System.Serializable]
    public class Lettersounds
        {
        [SerializeField]
        public string Letter;

        public Vector3 Freqs;
        public Vector3 Pitches; 
        public Vector3 Times; 

        public Lettersounds(string letter, Vector3 freqs)
        {
            Letter = letter;
            Freqs = freqs;
        }
        public Lettersounds(string letter, Vector3 freqs, Vector3 pitches,Vector3 times)
        {
            Letter = letter;
            Freqs = freqs;
            Pitches = pitches;
            Times = times;

            

        }
    }
}
