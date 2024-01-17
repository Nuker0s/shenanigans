using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;


public class SynthTest2 : MonoBehaviour
{
    public float gain;
    public float frequency = 440.0f;
    public float increment;
    public float incmult = 2;
    public float phase;
    public float phasediv = 1;
    public float sampfreq = 48000.0f;
    public AnimationCurve c1;
    public string toread = "A";
    public float lettertime = 0.3f;
    public float spacetime = 0.5f;
    public AudioSource aus;
    private Vector3 ones = new Vector3(1, 1, 1);
    public List<Lettersounds> letters = new List<Lettersounds>();
    public float octave = 4;
    public bool test;
    public float testtime;
    public int wavetype;

    private void OnAudioFilterRead(float[] data, int channels)
    {
        //phase = 0;
        increment = frequency * incmult * Mathf.PI / sampfreq;
        for (int i = 0; i < data.Length; i += channels)
        {
            phase += increment;
            if (phase > math.PI * 1000) phase = 0;

            switch (wavetype)
            {
                default: break;

                case 0:
                    data[i] = gain * Mathf.Sin(phase / phasediv);
                    break;
                case 1:
                    data[i] = gain * Mathf.PerlinNoise1D(phase / phasediv);
                    break;
                case 2:
                    data[i] = gain * (math.sin(phase) < 0 ? 0.5f : 0.1f);
                    break;
                case 3:
                    data[i] = gain * 0.5f * (((math.sin(phase) + 0.5f) % 1f) - 0.5f) * 2;
                    break;

            }


            if (channels == 2)
            {
                data[i + 1] = data[i];
            }
        }
    }
    
    private void Update()
    {
        if (test) 
        {
            test = false;
            textread();
        }
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
        
        foreach (var letter in toread)
        {
            frequency = 0;
            Lettersounds tosay = null;
            foreach (var item in letters)
            {
                if (item.Letter == ("" + letter).ToUpper())
                {
                    tosay = item;
                    break;
                }

            }
            float octoffset = (32 * octave) / 12;
            if (tosay != null)
            {
                /*for (int i = 0; i < 3; i++)
                {
                    frequency = 32 * octave + octoffset * tosay.Freqs[i];
                    yield return new WaitForSeconds(tosay.Times[i]*testtime);
                }*/
                for (int i = 0; i < 3; i++)
                {
                    //float target = 32 * octave + octoffset * tosay.Freqs[i + 1];
                    float target = 440 * math.pow(2, tosay.Freqs[i] / 12);
                    frequency = target;
                    aus.pitch = tosay.Pitches[i];
                    wavetype = (int)tosay.Wavetypes[i];
                    yield return new WaitForSeconds(tosay.Times[i] * testtime);
                    /*while (frequency <= target-1)
                    {

                        //frequency = math.lerp(frequency, target, tosay.Times[i] * testtime);
                        yield return null;
                    }*/

                }
                frequency = 0;
                wavetype = 0;
                aus.pitch = 1;
                
            }
            else
            {
                yield return new WaitForSeconds(spacetime);
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
        public Vector3 Wavetypes;

        public Lettersounds(string letter, Vector3 freqs)
        {
            Letter = letter;
            Freqs = freqs;
        }
        public Lettersounds(string letter, Vector3 freqs, Vector3 pitches, Vector3 times, Vector3 wawetype)
        {
            Letter = letter;
            Freqs = freqs;
            Pitches = pitches;
            Times = times;
            Wavetypes = wawetype;   


        }
    }
}
