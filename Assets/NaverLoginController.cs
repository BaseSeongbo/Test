using UnityEngine;
using UnityEngine.UI;

public enum OAuthLoginState
{
    NEED_INIT,
    NEED_LOGIN,
    NEED_REFRESH_TOKEN,
    OK
}

public class NaverLoginController : MonoBehaviour
{
#if UNITY_ANDROID
    AndroidJavaClass myClass;
    AndroidJavaObject mContext;
    AndroidJavaObject mOAuthLoginInstance;
    AndroidJavaObject toast;
    public AppController appController;
#endif
    public Text token;
    string at;
    string rt;
    string expires;
    string type;
    string state;
    string errorCode;
    string errorDesc;
    string requestApi;

    private void Start()
    {
        mContext = appController.unityActivity;
        myClass = new AndroidJavaClass("com.nhn.android.naverlogin.OAuthLogin");
        toast = new AndroidJavaObject("MyToastKt");
        Init();
    }
    private void Init()
    {
#if UNITY_ANDROID
        string OAUTH_CLIENT_ID = "3DzAGlPKaAlllDBxmUr4";
        string OAUTH_CLIENT_SECRET = "9MMdSGRluy";
        string OAUTH_CLIENT_NAME = "네이버 아이디로 로그인";
        mOAuthLoginInstance = myClass.CallStatic<AndroidJavaObject>("getInstance");
        mOAuthLoginInstance.Call("init", mContext, OAUTH_CLIENT_ID, OAUTH_CLIENT_SECRET, OAUTH_CLIENT_NAME);
        toast.CallStatic("SetDate", mOAuthLoginInstance, mContext);
        UpdateText(true);
#endif
    }
    private void UpdateText(bool success)
    {
        if (success)
        {
            at = mOAuthLoginInstance.Call<string>("getAccessToken", mContext);
            rt = mOAuthLoginInstance.Call<string>("getRefreshToken", mContext);
            expires = mOAuthLoginInstance.Call<long>("getExpiresAt", mContext).ToString();
            type = mOAuthLoginInstance.Call<string>("getTokenType", mContext);
            state = toast.CallStatic<string>("GetState");
            errorCode = "";
            errorDesc = "";
        }
        else
        {
            errorCode = mOAuthLoginInstance.Call<AndroidJavaObject>("getLastErrorCode", mContext).Call<string>("getCode");
            errorDesc = mOAuthLoginInstance.Call<string>("getLastErrorDesc", mContext);
        }
    }
    string success = null;
    private void FixedUpdate()
    {
        success = toast.CallStatic<string>("GetSuccess");
        requestApi = success;
        if (success == "1")
        {
            UpdateText(true);
        }
        else if (success == "0")
        {
            UpdateText(false);
        }
        token.text = at;
    }
    public void Login()
    {
#if UNITY_ANDROID
        toast.CallStatic("Login", mContext);
#endif
    }
    private void Toast(string text)
    {
        toast.CallStatic("Show", mContext, text);
    }
    public void Logout()
    {
        mOAuthLoginInstance.Call("logout", mContext);
        UpdateText(true);
        Toast("로그아웃 되었습니다.");
    }
    public void DeleteTokenTask()
    {
        bool isSuccessDeleteToken = mOAuthLoginInstance.Call<bool>("logoutAndDeleteToken", mContext);
        if (!isSuccessDeleteToken)
        {
            UpdateText(false);
        }
        else
        {
            UpdateText(true);
            Toast("연결이 끊어졌습니다.");
        }
    }
    public void RefreshTokenTask()
    {
        at = mOAuthLoginInstance.Call<string>("refreshAccessToken", mContext);
        UpdateText(true);
        Toast("토큰 재발급");
    }
    public void RequestApiTask()
    {
        requestApi = mOAuthLoginInstance.Call<string>("requestApi", mContext, at, "BaseSeongbo.github.io");
        UpdateText(true);
    }
}
