using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SelectLevel : MonoBehaviour
{
    string level;
    [SerializeField]
    GameObject popUp;
    GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }


    public void OpenAndSetPopUp(int _level)
    {
        level = "Level" + _level.ToString();
        popUp.SetActive(true);
    }

    public void Play()
    {
        gameManager.IsExploration = false;
        SceneManager.LoadScene(level);
    }

    public void Exploration()
    {
        gameManager.IsExploration = true;
        SceneManager.LoadScene(level);
    }
}
