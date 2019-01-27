using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public bool IsExploration;

    private void Start()
    {
        DontDestroyOnLoad(this);
    }
}
