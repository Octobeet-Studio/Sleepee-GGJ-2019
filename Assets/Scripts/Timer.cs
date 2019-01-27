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
    Text opsText;
    PlayerControllerLinear playerController;

    private void Awake()
    {
        playerController = FindObjectOfType<PlayerControllerLinear>();
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
            UrinaLevel.fillAmount = t / timer;
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
        if (opsText != null)
        {
            Destroy(playerController);
            opsText.gameObject.SetActive(true);
        }
    }

    public void GoodEnd()
    {
        Debug.Log("Yeah");
    }
}
