import android.app.Activity
import android.content.Context
import android.widget.Toast
import com.nhn.android.naverlogin.OAuthLogin
import com.nhn.android.naverlogin.OAuthLoginHandler

    var oAuthLoginInstance: OAuthLogin? = null
    var context: Context? = null
    var num: String? = null
    fun Show(context: Context, text: String){
        Toast.makeText(context, text, Toast.LENGTH_LONG).show();
    }
    fun SetDate(mOAuthLoginInstance: OAuthLogin, mContext: Context){
        oAuthLoginInstance = mOAuthLoginInstance
        context = mContext
    }
    fun GetState():String?{
        return oAuthLoginInstance!!.getState(context).toString()
    }
fun GetSuccess():String?{
    return num
}

    fun Login(activity: Activity){
        oAuthLoginInstance!!.startOauthLoginActivity(activity, mOAuthLoginHandler)
    }

    private val mOAuthLoginHandler: OAuthLoginHandler = object : OAuthLoginHandler() {
        override fun run(success: Boolean) {
            if(success){
                num = "1"
            } else{
                num = "0"
            }
        }
    }