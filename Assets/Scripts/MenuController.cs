using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private RectTransform settingsScreen,
        levelScreen,
        menuScreen;

    [SerializeField]
    private GameObject[] locks;

    [SerializeField]
    private Image loadingScreen;

    [SerializeField]
    private RectTransform rotateSign;

    [SerializeField]
    private Button[] levelButtons;

    private bool loading;

    private void Awake()
    {
        settingsScreen.anchoredPosition = new Vector2(2000, 0);
        Application.targetFrameRate = 300;
        Screen.orientation = ScreenOrientation.Portrait;
        loadingScreen.color = new Color(0, 0, 0, 0);
        loadingScreen.raycastTarget = false;
    }

    private void Start()
    {
        for (int i = 1; i < 6; i++)
        {
            bool levelOpened = PlayerPrefs.HasKey(i.ToString());
            levelButtons[i].interactable = levelOpened;
            locks[i - 1].SetActive(!levelOpened);
        }
    }

    private void FixedUpdate()
    {
        if (loading)
        {
            rotateSign.Rotate(0, 0, 0.5f);
        }
    }

    public void Settings()
    {
        if (menuScreen.anchoredPosition.x == 0)
        {
            settingsScreen.DOAnchorPosX(0, 0.5f);
        }
    }

    public void Play()
    {
        if (menuScreen.anchoredPosition.x == 0 && settingsScreen.anchoredPosition.x == 2000)
        {
            menuScreen.DOAnchorPosX(-2000, 0.5f);
            levelScreen.DOAnchorPosY(0, 0.5f);
        }
    }

    public void CloseLevels()
    {
        menuScreen.DOAnchorPosX(0, 0.5f);
        levelScreen.DOAnchorPosY(2000, 0.5f);
    }

    public void PlayLevel(int level)
    {
        PlayerPrefs.SetInt("CurrentLevel", level);
        PlayerPrefs.Save();
        loading = true;
        Sequence load = DOTween.Sequence();

        load.PrependCallback(() =>
            {
                loadingScreen.raycastTarget = true;
            })
            .Append(loadingScreen.DOColor(Color.white, 0.5f))
            .AppendInterval(0.5f)
            .AppendCallback(() =>
            {
                SceneManager.LoadSceneAsync("GameField");
            })
            .Restart();
    }

    public void Exit()
    {
        Application.Quit();
    }
}
