using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SelectLevel : MonoBehaviour
{
    string level;
    [SerializeField]
    GameObject popUp;
    

    public void OpenAndSetPopUp(int _level)
    {
        level = "Level" + _level.ToString();
        popUp.SetActive(true);
    }

    public void Play()
    {
        SceneManager.LoadScene(level);
    }
}
