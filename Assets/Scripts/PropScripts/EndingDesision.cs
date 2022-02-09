using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingDesision : MonoBehaviour
{
    public static bool isBadEnding;
    public static EndingDesision _instance;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
    }

    private void Start()
    {
        DontDestroyOnLoad(this);
    }
}
