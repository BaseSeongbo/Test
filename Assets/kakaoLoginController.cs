using UnityEngine;
using UnityEngine.UI;

public class kakaoLoginController : MonoBehaviour
{
#if UNITY_ANDROID
    AndroidJavaObject unityActivity;
    AndroidJavaObject myClass;

    public AppController appController;
#endif
    public Text text;
    private void Start()
    {
        unityActivity = appController.unityActivity;
        myClass = new AndroidJavaObject("com.based.android.kakao");
        myClass.Call("Initkakao", unityActivity, "5709e85e8dde707c6c2e55dfeed3e3ce");
    }
    public void LoginBtn()
    {
        text.text = myClass.Call<string>("Login", unityActivity);
    }
    public void LogoutBtn()
    {
        myClass.Call("LogOut", unityActivity);
    }
    public void UnlinkBtn()
    {
        myClass.Call("UnLink", unityActivity);
    }
    public void UserInfoBtn()
    {
        myClass.Call("UserInfo", unityActivity);
    }
}
