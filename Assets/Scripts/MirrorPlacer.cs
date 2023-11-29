using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class MirrorPlacer : MonoBehaviour
{
    [SerializeField]
    private GameObject mirrorPrefab;
    private TextMeshProUGUI mirrorsCount;
    public static event UnityAction ShineIsland,
        PutOutIsland;
    private Button button;
    private int currentMirrors;
    private bool placeState;

    public void Init(int maxMirrors, TextMeshProUGUI mirrorsCount)
    {
        this.mirrorsCount = mirrorsCount;
        currentMirrors = maxMirrors;
        mirrorsCount.text = "x" + currentMirrors;
        button = mirrorsCount.GetComponentInParent<Button>();
    }

    public void PlaceHere(Transform choosedIsland)
    {
        if (placeState)
        {
            button.image.color = Color.white;
            Instantiate(mirrorPrefab, choosedIsland);
            placeState = false;
            currentMirrors--;
            if (currentMirrors == 0)
            {
                button.interactable = false;
            }
            mirrorsCount.text = "x" + currentMirrors;
            PutOutIsland?.Invoke();
        }
    }

    public void PlaceMirror()
    {
        if (placeState)
        {
            placeState = false;
            button.image.color = Color.white;
            PutOutIsland?.Invoke();
        }
        else if (currentMirrors > 0)
        {
            button.image.color = new Color(1, 0.6f, 0, 1);
            placeState = true;
            ShineIsland?.Invoke();
        }
    }
}
