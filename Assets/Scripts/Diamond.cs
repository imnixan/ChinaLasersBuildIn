using System;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    [SerializeField]
    private Material redMat,
        shineMat;
    private ParticleSystem shine;
    private MeshRenderer diamondMesh;
    private FieldManager gm;

    private void Start()
    {
        shine = GetComponentInChildren<ParticleSystem>();
        diamondMesh = GetComponentInChildren<MeshRenderer>();
        gm = GetComponentInParent<FieldManager>();
    }

    public void ShineDiamond()
    {
        diamondMesh.material = shineMat;
        shine.Play();
        gm.AddShinedDiamond();
    }
}
