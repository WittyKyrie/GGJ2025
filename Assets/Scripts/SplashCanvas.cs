using MoreMountains.Feedbacks;
using UnityEngine;

public class SplashCanvas : MonoBehaviour
{
    public void StartGame()
    {
        GetComponent<MMF_Player>().PlayFeedbacks();
    }

    public void AboutUs()
    {
        
    }

    public void Exit()
    {
        Application.Quit();
    }
}
