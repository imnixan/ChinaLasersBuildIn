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
    private Button[] levelButtons;

    private void Awake()
    {
        settingsScreen.anchoredPosition = new Vector2(2000, 0);
        Application.targetFrameRate = 300;
        Screen.orientation = ScreenOrientation.Portrait;
    }

    private void Start()
    {
        for (int i = 1; i < 6; i++)
        {
            if (!PlayerPrefs.HasKey(i.ToString()))
            {
                levelButtons[i].interactable = false;
                locks[i - 1].SetActive(true);
            }
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
        SceneManager.LoadScene("GameField");
    }
}
