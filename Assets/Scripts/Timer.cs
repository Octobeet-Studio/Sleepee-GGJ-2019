using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField]
    float timer;
    [SerializeField]
    Image UrinaLevel;
    [SerializeField]
    GameObject gameOverPanel;
    [SerializeField]
    GameObject WinPanel;
    [SerializeField]
    List<AudioClip> alertAudioClips;
    public AudioClip win, lose, menuMusic;
    public AudioSource MainMusic;

    PlayerControllerLinear playerController;
    AudioSource audioSource;

    private void Awake()
    {
        playerController = FindObjectOfType<PlayerControllerLinear>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        UrinaLevel.fillAmount = 0;
        StartTimer();
    }

    public void StartTimer()
    {
        StartCoroutine(CountDown());
    }

    IEnumerator ScrollingImage()
    {
        float t = 0;
        while(UrinaLevel.fillAmount < 1)
        {
            t += Time.deltaTime;
            Debug.Log(t);
            int prevIndex = Mathf.FloorToInt(UrinaLevel.fillAmount / 0.25f);
            UrinaLevel.fillAmount = t / timer;
            int index = Mathf.FloorToInt(UrinaLevel.fillAmount / 0.25f);
            if ( index > prevIndex && alertAudioClips.Count > 0 && prevIndex < alertAudioClips.Count-1)
            {
                print("INDEX: " + prevIndex);
                audioSource.clip = alertAudioClips[prevIndex];
                audioSource.Play();
            }
            yield return null;
        }
    }

    IEnumerator CountDown()
    {
        StartCoroutine(ScrollingImage());
        //UrinaLevel.fillAmount = 0.5f;
        yield return new WaitForSeconds(timer);
        BadendGame();
    }

    void BadendGame()
    {
        audioSource.Stop();
        StartCoroutine(BadMusic());
        if (gameOverPanel != null)
        {
            Destroy(playerController);
            gameOverPanel.gameObject.SetActive(true);
        }
    }

    IEnumerator BadMusic()
    {
        while (MainMusic.isPlaying)
        {
            yield return null;
        }
        MainMusic.clip = lose;
        MainMusic.Play();
        MainMusic.loop = true;
    }

    public void GoodEnd()
    {
        audioSource.Stop();
        MainMusic.Stop();
        MainMusic.clip = win;
        MainMusic.Play();
        StartCoroutine(music());
        StopCoroutine(CountDown());
        if (WinPanel != null)
        {
            Destroy(playerController);
            WinPanel.gameObject.SetActive(true);
        }
    }

    IEnumerator music()
    {
        yield return new WaitForSeconds(win.length + 0.2f);
        MainMusic.loop = true;
        MainMusic.clip = menuMusic;
        MainMusic.Play();
    }
}
