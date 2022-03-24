using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundWaveCollide : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        gameObject.GetComponentInParent<SoundWave>().PullTrigger(other);
    }
}
