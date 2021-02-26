using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour, IUnityAdsListener
{
    string placement = "Rewarded_Android";


    void Start()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize("4028415", true);

    }   

    public void ShowAd(string p)
    {
        Advertisement.Show(p);
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if(showResult == ShowResult.Finished)
        {
            FindObjectOfType<PlayerController>().extraLife();
        }
        else if(showResult == ShowResult.Failed)
        {

        }
    }

    public void OnUnityAdsDidStart(string placementId)
    {
    }

    public void OnUnityAdsReady(string placementId)
    {
    }
    public void OnUnityAdsDidError(string message)
    {
    }
}
