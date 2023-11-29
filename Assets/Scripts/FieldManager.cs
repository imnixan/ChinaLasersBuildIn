using UnityEngine;
using TMPro;

public class FieldManager : MonoBehaviour
{
    [SerializeField]
    private int totalDiamonds,
        shinedDiamonds;

    [SerializeField]
    private Vector2Int[] diamondsPositions;

    [SerializeField]
    private Transform[] lines;

    [SerializeField]
    private int mirrorsTotal;

    [SerializeField]
    private GameObject diamondPrefab;

    private MirrorPlacer mp;

    public void Init(TextMeshProUGUI counter)
    {
        mp = GetComponent<MirrorPlacer>();
        mp.Init(mirrorsTotal, counter);
        totalDiamonds = diamondsPositions.Length;
        InitDiamonds();
    }

    private void InitDiamonds()
    {
        foreach (var pos in diamondsPositions)
        {
            Transform island = lines[pos.x].GetChild(pos.y);
            Instantiate(diamondPrefab, island);
        }
    }

    public void AddShinedDiamond()
    {
        shinedDiamonds++;
        if (shinedDiamonds == totalDiamonds)
        {
            Debug.Log("Win!");
        }
    }
}
