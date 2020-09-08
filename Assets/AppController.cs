
using UnityEngine;

public class AppController : MonoBehaviour
{
#if UNITY_ANDROID
    public AndroidJavaObject unityActivity;
#endif
    private void Awake()
    {
#if UNITY_ANDROID
        AndroidJavaClass unityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        unityActivity = unityClass.GetStatic<AndroidJavaObject>("currentActivity");
#endif
    }
}
