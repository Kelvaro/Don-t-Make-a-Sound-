using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundWaveCheck : MonoBehaviour
{

    public bool WaveCollided;
    // Start is called before the first frame update
    void Start()
    {
        WaveCollided = false;
    }

    public bool getRecentWave()
    {
        return WaveCollided;
    }

    public void setRecentWave(bool hit)
    {
        WaveCollided = hit;
    }

}
