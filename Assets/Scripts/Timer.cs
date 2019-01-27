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
    public AudioClip win, lose;

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
        if (gameOverPanel != null)
        {
            Destroy(playerController);
            gameOverPanel.gameObject.SetActive(true);
        }
    }

    public void GoodEnd()
    {
        StopCoroutine(CountDown());
        GetComponent<AudioSource>().Stop();
        if (WinPanel != null)
        {
            Destroy(playerController);
            WinPanel.gameObject.SetActive(true);
        }
    }
}
