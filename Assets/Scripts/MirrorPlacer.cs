using UnityEngine;
using TMPro;

public class MirrorPlacer : MonoBehaviour
{
    [SerializeField]
    private GameObject mirrorPrefab;
    private TextMeshProUGUI mirrorsCount;

    private int currentMirrors;
    private bool placeState;

    public void Init(int maxMirrors, TextMeshProUGUI mirrorsCount)
    {
        this.mirrorsCount = mirrorsCount;
        currentMirrors = maxMirrors;
        mirrorsCount.text = "x" + currentMirrors;
    }

    public void PlaceHere(Transform island)
    {
        if (placeState)
        {
            Instantiate(mirrorPrefab, island);
            placeState = false;
            currentMirrors--;
            mirrorsCount.text = "x" + currentMirrors;
        }
    }

    public void PlaceMirror()
    {
        if (currentMirrors > 0)
        {
            placeState = true;
        }
    }
}
