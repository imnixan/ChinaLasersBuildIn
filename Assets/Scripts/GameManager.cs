using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private FieldManager[] gameFields;

    [SerializeField]
    private LineRenderer line;

    [SerializeField]
    private Button placeButton;

    private TigerLaser tigerLaser;

    FieldManager currentField;

    private void Start()
    {
        Application.targetFrameRate = 300;
        currentField = Instantiate(gameFields[PlayerPrefs.GetInt("CurrentLevel")]);
        currentField.Init(placeButton.GetComponentInChildren<TextMeshProUGUI>());
        placeButton.onClick.AddListener(currentField.GetComponent<MirrorPlacer>().PlaceMirror);
        tigerLaser = currentField.GetComponentInChildren<TigerLaser>();
        tigerLaser.Init(line);
    }

    public void Fire()
    {
        tigerLaser.Fire();
    }

    public void Restart()
    {
        Destroy(currentField.gameObject);

        Start();
    }
}
