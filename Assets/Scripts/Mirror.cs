using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Mirror : MonoBehaviour, IPointerClickHandler
{
    public int angle;
    private ParticleSystem ps;
    private bool initiated;

    private void Start()
    {
        SetAngle();
        ps = GetComponentInChildren<ParticleSystem>();
        initiated = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (initiated)
        {
            Rotate();
        }
    }

    private void Rotate()
    {
        ps.Play();
        SetAngle();
    }

    private void SetAngle()
    {
        if (angle == 315)
        {
            angle = 45;
        }
        else
        {
            angle = 315;
        }
        transform.eulerAngles = new Vector2(0, angle);
    }
}
