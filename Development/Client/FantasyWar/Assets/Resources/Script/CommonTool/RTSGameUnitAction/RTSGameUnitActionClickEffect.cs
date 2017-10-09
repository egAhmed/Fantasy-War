using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSGameUnitActionClickEffect : MonoBehaviour {

    private ParticleSystem[] particles;
    //
    private void Awake()
    {
        particles = gameObject.GetComponentsInChildren<ParticleSystem>();
    }

    public void play()
    {
        if (particles!=null) {
            foreach(ParticleSystem p in particles)
            {
                p.Play();
            }
        }
    }

    public void stop()
    {
        if (particles != null)
        {
            foreach (ParticleSystem p in particles)
            {
                p.Stop();
            }
        }
    }
}
