using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LaserPathFinder : MonoBehaviour
{
    private TigerLaser tigerLaser;
    private List<Vector3> laserPoints;
    public Vector3[] points;
    private float drawSpeed;
    public static event UnityAction PathFinished;
    private AudioClip ricoshetSound,
        diamondSound;

    public void StartDraw(
        TigerLaser tigerLaser,
        float drawSpeed,
        Vector3[] points,
        AudioClip ricoshet,
        AudioClip diamond
    )
    {
        this.ricoshetSound = ricoshet;
        this.diamondSound = diamond;
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
                transform.position.z < -19
                || transform.position.z > 1
                || transform.position.x < -6
                || transform.position.x > 13.5
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
        PathFinished?.Invoke();
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Diamond"))
        {
            Diamond diamond = collider.GetComponent<Diamond>();
            diamond.ShineDiamond();
            if (PlayerPrefs.GetInt("Sound") == 0)
            {
                AudioSource.PlayClipAtPoint(diamondSound, Vector3.zero);
            }
        }
        else if (collider.CompareTag("Mirror"))
        {
            if (PlayerPrefs.GetInt("Sound") == 0)
            {
                AudioSource.PlayClipAtPoint(ricoshetSound, Vector3.zero);
            }
        }
    }
}
