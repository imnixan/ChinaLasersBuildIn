using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Island : MonoBehaviour
{
    private MirrorPlacer mp;
    private ParticleSystem shine;

    private void Start()
    {
        mp = GetComponentInParent<MirrorPlacer>();
        shine = GetComponentInChildren<ParticleSystem>();
    }

    private void OnMouseDown()
    {
        mp.PlaceHere(transform);
    }

    private void Shine()
    {
        Diamond diamond = GetComponentInChildren<Diamond>();
        if (diamond)
        {
            SetInactive();
        }
        else
        {
            shine.Play();
        }
    }

    private void PutOut()
    {
        shine.Stop();
    }

    public void SetInactive()
    {
        shine.Stop();
        OnDisable();
        Destroy(this);
    }

    private void OnEnable()
    {
        MirrorPlacer.ShineIsland += Shine;
        MirrorPlacer.PutOutIsland += PutOut;
    }

    private void OnDisable()
    {
        MirrorPlacer.ShineIsland -= Shine;
        MirrorPlacer.PutOutIsland -= PutOut;
    }
}
