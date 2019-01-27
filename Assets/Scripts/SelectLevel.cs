using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectLevel : MonoBehaviour
{
    string level = "Level1";
    [SerializeField]
    GameObject popUp;
    GameManager gameManager;
    Button SelectLevelButton;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void Play()
    {
        gameManager.IsExploration = false;
        SceneManager.LoadScene(level);
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().ToString());
    }

    public void Exit()
    {
        Application.Quit();
    }
}
