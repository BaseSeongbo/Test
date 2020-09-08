package com.based.android

import android.app.Activity
import android.content.Context
import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import androidx.core.app.ActivityCompat.startActivityForResult
import com.google.android.gms.auth.api.signin.GoogleSignIn
import com.google.android.gms.auth.api.signin.GoogleSignInAccount
import com.google.android.gms.auth.api.signin.GoogleSignInClient
import com.google.android.gms.auth.api.signin.GoogleSignInOptions
import com.google.android.gms.common.api.ApiException
import com.google.android.gms.tasks.Task

class google :AppCompatActivity() {
    var RC_SIGN_IN:Int = 9000
    var context: Context? = null
    var mGoogleSignInClient: GoogleSignInClient? = null

    fun GoogleInit(context: Context){
        val gso =  GoogleSignInOptions.Builder(GoogleSignInOptions.DEFAULT_SIGN_IN).requestEmail().build()
        this.context = context
        mGoogleSignInClient = GoogleSignIn.getClient(context, gso)
    }
    fun UserInfo():GoogleSignInAccount?{
        return GoogleSignIn.getLastSignedInAccount(context)
    }
    fun Login(){
        var signInIntent: Intent = mGoogleSignInClient!!.signInIntent
        startActivityForResult(context as Activity,signInIntent, RC_SIGN_IN,null)
    }
    override fun onActivityResult(requestCode: Int, resultCode: Int, data: Intent?) {
        if (requestCode == RC_SIGN_IN) {
            val task = GoogleSignIn.getSignedInAccountFromIntent(data)
            handleSignInResult(task)
        }
        super.onActivityResult(requestCode, resultCode, data)
    }
    fun handleSignInResult(completedTask: Task<GoogleSignInAccount>) {
        try {
            val account = completedTask.getResult(ApiException::class.java)
        } catch (e: ApiException) {

        }
    }

}