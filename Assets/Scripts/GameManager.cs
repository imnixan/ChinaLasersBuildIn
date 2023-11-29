using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private FieldManager[] gameFields;

    [SerializeField]
    private TextMeshProUGUI levelText;

    [SerializeField]
    private Transform leftCloud,
        rightCloud;

    [SerializeField]
    private RectTransform topHud,
        botHud,
        loseScreen,
        winScreen;

    [SerializeField]
    private LineRenderer line;

    [SerializeField]
    private Button placeButton;

    private TigerLaser tigerLaser;

    FieldManager currentField;
    private AudioSource musicPlayer;

    [SerializeField]
    private AudioClip laser,
        ricoshet,
        diamond;

    private bool gameReady;
    private int levelIndex;

    private void Start()
    {
        musicPlayer = GetComponent<AudioSource>();
        if (PlayerPrefs.GetInt("Music") == 0 && !musicPlayer.isPlaying)
        {
            musicPlayer.Play();
        }
        Application.targetFrameRate = 300;
        levelIndex = PlayerPrefs.GetInt("CurrentLevel");
        currentField = Instantiate(gameFields[levelIndex]);
        currentField.Init(placeButton.GetComponentInChildren<TextMeshProUGUI>());
        placeButton.onClick.AddListener(currentField.GetComponent<MirrorPlacer>().PlaceMirror);
        tigerLaser = currentField.GetComponentInChildren<TigerLaser>();
        tigerLaser.Init(line, laser, ricoshet, diamond);
        LaserPathFinder.PathFinished += OnPathFinished;
        placeButton.interactable = true;
        levelText.text = $"level {levelIndex + 1}";
        StartAnim();
    }

    private void StartAnim()
    {
        Sequence startAnim = DOTween.Sequence();
        startAnim
            .Append(leftCloud.DOLocalMoveX(-10, 0.5f))
            .Join(rightCloud.DOLocalMoveX(10, 0.5f))
            .Append(topHud.DOAnchorPosY(0, 0.5f))
            .Join(botHud.DOAnchorPosY(0, 0.5f))
            .AppendCallback(() =>
            {
                leftCloud.gameObject.SetActive(false);
                rightCloud.gameObject.SetActive(false);
                gameReady = true;
            })
            .Restart();
    }

    public void Fire()
    {
        if (gameReady)
        {
            tigerLaser.Fire();
        }
    }

    public void Restart()
    {
        if (gameReady)
        {
            Sequence restartAnim = DOTween.Sequence();
            if (loseScreen.anchoredPosition.y == 0)
            {
                restartAnim.Append(loseScreen.DOAnchorPosY(2000, 0.5f));
            }
            restartAnim
                .PrependCallback(() =>
                {
                    gameReady = false;
                    leftCloud.gameObject.SetActive(true);
                    rightCloud.gameObject.SetActive(true);
                })
                .Append(topHud.DOAnchorPosY(500, 0.5f))
                .Join(botHud.DOAnchorPosY(-500, 0.5f))
                .Append(leftCloud.DOLocalMoveX(-2.5f, 0.5f))
                .Join(rightCloud.DOLocalMoveX(2.5f, 0.5f))
                .AppendCallback(() =>
                {
                    Destroy(currentField.gameObject);
                    OnDisable();
                    Start();
                })
                .Restart();
        }
    }

    private void OnPathFinished()
    {
        topHud.DOAnchorPosY(500, 0.5f);
        botHud.DOAnchorPosY(-500, 0.5f);
        if (currentField.shinedDiamonds == currentField.totalDiamonds)
        {
            Debug.Log("WINT");
            winScreen.DOAnchorPosY(0, 0.5f);
            PlayerPrefs.SetInt(levelIndex.ToString(), 1);
            PlayerPrefs.Save();
        }
        else
        {
            loseScreen.DOAnchorPosY(0, 0.5f);
            Debug.Log("LOSE");
        }
    }

    private void OnDisable()
    {
        LaserPathFinder.PathFinished -= OnPathFinished;
    }

    public void Next()
    {
        levelIndex++;
        winScreen.DOAnchorPosX(2000, 0.5f);
        if (levelIndex <= 5)
        {
            PlayerPrefs.SetInt("CurrentLevel", levelIndex);
            PlayerPrefs.Save();
            Sequence nextAnim = DOTween.Sequence();
            Restart();
        }
        else
        {
            Menu();
        }
    }

    public void Menu()
    {
        SceneManager.LoadScene("GameMenu");
    }
}
