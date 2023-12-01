using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class Settings : MonoBehaviour
{
    [SerializeField]
    private GameObject soundIcon,
        musicIcon;

    [SerializeField]
    private RectTransform menuRt;

    private AudioSource musicMenu;
    private RectTransform rt;

    [SerializeField]
    private Image bgBlur;

    private void Start()
    {
        rt = GetComponent<RectTransform>();
        musicMenu = GetComponent<AudioSource>();
        soundIcon.SetActive(PlayerPrefs.GetInt("Sound") == 0);

        musicIcon.SetActive(PlayerPrefs.GetInt("Music") == 0);

        if (PlayerPrefs.GetInt("Music") == 0)
        {
            musicMenu.Play();
        }
    }

    public void ChangeSound()
    {
        PlayerPrefs.SetInt("Sound", PlayerPrefs.GetInt("Sound") == 0 ? 1 : 0);
        PlayerPrefs.Save();
        soundIcon.SetActive(PlayerPrefs.GetInt("Sound") == 0);
    }

    public void ChangeMusic()
    {
        PlayerPrefs.SetInt("Music", PlayerPrefs.GetInt("Music") == 0 ? 1 : 0);
        PlayerPrefs.Save();
        musicIcon.SetActive(PlayerPrefs.GetInt("Music") == 0);
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
        menuRt.DOAnchorPosX(0, 0.5f);
        bgBlur.DOColor(new Color(0, 0, 0, 0), 0.5f);
    }
}
