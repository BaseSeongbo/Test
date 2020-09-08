using UnityEngine;
using UnityEngine.UI;

public class GoogleLoginController : MonoBehaviour
{
    public AppController appController;
    AndroidJavaObject obj;
    AndroidJavaObject context;

    public Text text;

    string personName;
    string personGivenName;
    string personFamilyName;
    string personEmail;
    string personID;

    private void Start()
    {
        obj = new AndroidJavaObject("com.based.android.google");
        context = appController.unityActivity;
        obj.Call("GoogleInit",context);
    }
    private void FixedUpdate()
    {
        AndroidJavaObject userinfo =  obj.Call<AndroidJavaObject>("UserInfo");
        if(userinfo != null)
        {
            personName = userinfo.Call<string>("getDisplayName");
            personGivenName = userinfo.Call<string>("getGivenName");
            personFamilyName = userinfo.Call<string>("getFamilyName");
            personEmail = userinfo.Call<string>("getEmail");
            personID = userinfo.Call<string>("getId");
            text.text = $"Name{personName}, GivenName{personGivenName}, FamilyName{personFamilyName}, Email{personEmail}, ID{personID}";
        }
    }
    public void Login()
    {
        obj.Call("Login");
    }
}
