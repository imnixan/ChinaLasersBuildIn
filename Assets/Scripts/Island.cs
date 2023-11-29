using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Island : MonoBehaviour
{
    private MirrorPlacer mp;

    private void Start()
    {
        mp = GetComponentInParent<MirrorPlacer>();
    }

    private void OnMouseDown()
    {
        mp.PlaceHere(transform);
    }
}
