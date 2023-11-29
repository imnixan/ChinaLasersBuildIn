using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class SkyRotate : MonoBehaviour
{
    [SerializeField]
    private float rotSpeed;

    private void FixedUpdate()
    {
        transform.Rotate(0, rotSpeed, 0);
    }
}
