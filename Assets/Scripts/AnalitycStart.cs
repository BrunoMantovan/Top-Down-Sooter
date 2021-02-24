using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;

public class AnalitycStart : MonoBehaviour
{
    public static AnalitycStart instance;

    private void Awake()
    {
        instance = this;

        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        GameAnalytics.Initialize();
    }
}
