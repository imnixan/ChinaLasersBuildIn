using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPathFinder : MonoBehaviour
{
    private TigerLaser tigerLaser;
    private List<Vector3> laserPoints;
    public Vector3[] points;
    private float drawSpeed;

    public void StartDraw(TigerLaser tigerLaser, float drawSpeed, Vector3[] points)
    {
        this.points = points;
        this.drawSpeed = drawSpeed;
        this.tigerLaser = tigerLaser;
        gameObject.AddComponent<SphereCollider>().isTrigger = true;
        gameObject.AddComponent<Rigidbody>().useGravity = false;

        StartCoroutine(DrawLine(points));
    }

    IEnumerator DrawLine(Vector3[] points)
    {
        int index = 0;
        List<Vector3> uiLine = new List<Vector3>() { points[0], points[0] };
        while (index < points.Length)
        {
            Vector3 destination = points[index];
            transform.position = Vector3.MoveTowards(transform.position, destination, drawSpeed);
            if (
                transform.position.z < -18
                || transform.position.z > 0
                || transform.position.x < -5
                || transform.position.x > 12.5
            )
            {
                uiLine[uiLine.Count - 1] = transform.position;
                tigerLaser.UpdateLine(uiLine.ToArray());
                EndPath();
            }
            if (transform.position == destination)
            {
                if (!uiLine.Contains(destination))
                {
                    uiLine.Add(destination);
                }
                index++;
            }
            uiLine[uiLine.Count - 1] = transform.position;
            tigerLaser.UpdateLine(uiLine.ToArray());
            yield return new WaitForFixedUpdate();
        }
        EndPath();
    }

    private void EndPath()
    {
        Debug.Log("END");
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Diamond"))
        {
            Diamond diamond = collider.GetComponent<Diamond>();
            diamond.ShineDiamond();
        }
    }
}
