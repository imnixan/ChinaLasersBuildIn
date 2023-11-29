using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

public class TigerLaser : MonoBehaviour
{
    [SerializeField]
    private float drawSpeed;
    public LineRenderer line;
    private List<Vector3> laserPoints;
    private RaycastHit hit;
    private Vector3 lastPos;
    private Vector3 direction;
    private float yPos;
    private int ignoreMask = ~(1 << 8);

    public void Init(LineRenderer line)
    {
        yPos = transform.position.y;
        laserPoints = new List<Vector3>();
        this.line = line;
        line.gameObject.SetActive(false);
    }

    private void BuildLazer()
    {
        Debug.Log(direction);
        if (Physics.Raycast(lastPos, direction, out hit, Mathf.Infinity, ignoreMask))
        {
            Debug.Log(hit.collider.name);
            if (hit.collider.CompareTag("Diamond"))
            {
                hit.collider.gameObject.layer = 8;
                lastPos = hit.collider.transform.position;
                laserPoints.Add(lastPos);
                BuildLazer();
            }
            else if (hit.collider.CompareTag("Mirror"))
            {
                Mirror mirror = hit.collider.GetComponent<Mirror>();

                direction = Vector3.Reflect(direction, hit.normal);
                lastPos = hit.collider.transform.position;
                //lastPos = hit.point;
                laserPoints.Add(lastPos);

                BuildLazer();
            }
        }
        else
        {
            laserPoints.Add(lastPos + direction * 20);
            DrawLaser();
        }
    }

    private void DrawLaser()
    {
        line.gameObject.SetActive(true);
        line.positionCount = 0;
        GameObject testObject = new GameObject();
        testObject.transform.SetParent(transform);
        testObject.transform.position = transform.position;
        testObject
            .AddComponent<LaserPathFinder>()
            .StartDraw(this, drawSpeed, laserPoints.ToArray());
    }

    public void Fire()
    {
        if (!line.gameObject.activeSelf)
        {
            laserPoints.Clear();

            lastPos = transform.position;
            direction = transform.forward;
            laserPoints.Add(lastPos);
            BuildLazer();
        }
    }

    public void UpdateLine(Vector3[] points)
    {
        line.positionCount = points.Length;
        line.SetPositions(points);
    }
}
