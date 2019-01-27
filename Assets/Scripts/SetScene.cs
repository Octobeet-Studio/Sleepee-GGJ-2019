using UnityEngine;
using System.Collections;

public class SetScene : MonoBehaviour
{
    GameManager gm;

    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        if (gm != null)
        {
            if (gm.IsExploration)
                RenderSettings.ambientLight = Color.white;
            else
                RenderSettings.ambientLight = Color.black;
        }
    }
}
