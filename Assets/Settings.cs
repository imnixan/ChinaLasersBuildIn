using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class Settings : MonoBehaviour
{
    [SerializeField]
    private Image soundIcon,
        musicIcon;

    private AudioSource musicMenu;
    private RectTransform rt;
    private Color green,
        red;

    private void Start()
    {
        green = Color.green / 2;
        green.a = 1;
        red = Color.red;
        red.a = 1;
        rt = GetComponent<RectTransform>();
        musicMenu = GetComponent<AudioSource>();
        soundIcon.color = PlayerPrefs.GetInt("Sound") == 0 ? green : red;

        musicIcon.color = PlayerPrefs.GetInt("Music") == 0 ? green : red;

        if (PlayerPrefs.GetInt("Music") == 0)
        {
            musicMenu.Play();
        }
    }

    public void ChangeSound()
    {
        PlayerPrefs.SetInt("Sound", PlayerPrefs.GetInt("Sound") == 0 ? 1 : 0);
        PlayerPrefs.Save();
        soundIcon.color = PlayerPrefs.GetInt("Sound") == 0 ? green : red;
    }

    public void ChangeMusic()
    {
        PlayerPrefs.SetInt("Music", PlayerPrefs.GetInt("Music") == 0 ? 1 : 0);
        PlayerPrefs.Save();
        musicIcon.color = PlayerPrefs.GetInt("Music") == 0 ? green : red;
        if (PlayerPrefs.GetInt("Music") == 0)
        {
            musicMenu.Play();
        }
        else
        {
            musicMenu.Pause();
        }
    }

    public void Close()
    {
        rt.DOAnchorPosX(2000, 0.5f);
    }
}
