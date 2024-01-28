using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;


[System.Serializable]
public class Wavecontainer
{
    public Wave wave;
    public float frequency = 440f;
    public float time = 1;
    public float duration = 1;
}

public abstract class Wave : ScriptableObject
{
    public abstract float Process(float freq, float time);
}

[CreateAssetMenu(fileName = "NewSineWave", menuName = "Waves/Sine Wave")]
public class SineWave : Wave
{
    public override float Process(float freq, float time)
    {
        return math.sin(freq * time);
    }
}

[CreateAssetMenu(fileName = "NewPerlinNoiseWave", menuName = "Waves/Perlin Noise Wave")]
public class PerlinNoiseWave : Wave
{
    public override float Process(float freq, float time)
    {
        return Mathf.PerlinNoise(2 * math.PI * freq * time, 0);
    }
}

[CreateAssetMenu(fileName = "NewSquareWave", menuName = "Waves/Square Wave")]
public class SquareWave : Wave
{
    public override float Process(float freq, float time)
    {
        return math.sign(math.sin(freq * time));
    }
}
