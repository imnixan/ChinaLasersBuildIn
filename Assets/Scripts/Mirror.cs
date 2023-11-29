using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Mirror : MonoBehaviour, IPointerClickHandler
{
    public int angle;

    private void Start()
    {
        Rotate();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Rotate();
    }

    private void Rotate()
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
