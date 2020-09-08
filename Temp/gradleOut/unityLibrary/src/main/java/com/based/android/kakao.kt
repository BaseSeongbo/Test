package com.based.android

import android.content.Context
import android.widget.Toast
import com.kakao.sdk.auth.LoginClient
import com.kakao.sdk.auth.model.OAuthToken
import com.kakao.sdk.common.KakaoSdk
import com.kakao.sdk.common.util.Utility
import com.kakao.sdk.user.UserApiClient

class kakao {
    fun Initkakao(context: Context, appKey: String) {
        KakaoSdk.init(context, appKey);
    }
    fun GetKeyHash(context: Context): String {
        return Utility.getKeyHash(context)
    }
    fun Login(context: Context):String?{
        var tokenString:String? = null
        val callback: (OAuthToken?, Throwable?) -> Unit = { token, error ->
            if (error != null) {
                Toast.makeText(context, "로그인 실패", Toast.LENGTH_LONG).show()
            } else if (token != null) {
                Toast.makeText(context, "로그인 성공", Toast.LENGTH_LONG).show()
                tokenString = token.accessToken
            }
        }
        if (LoginClient.instance.isKakaoTalkLoginAvailable(context)) {
            LoginClient.instance.loginWithKakaoTalk(context, callback = callback)
        } else {
            LoginClient.instance.loginWithKakaoAccount(context, callback = callback)
        }
        return tokenString
    }
    fun UnLink(context: Context){
        UserApiClient.instance.unlink { error ->
            if (error != null) {
                Toast.makeText(context, "연결 끊기 실패", Toast.LENGTH_LONG).show()
            }
            else {
                Toast.makeText(context, "연결 끊기 성공. SDK에서 토큰 삭제 됨", Toast.LENGTH_LONG).show()
            }
        }
    }
    fun LogOut(context: Context){
        UserApiClient.instance.logout { error ->
            if (error != null) {
                Toast.makeText(context, "로그아웃 실패. SDK에서 토큰 삭제됨", Toast.LENGTH_LONG).show()
            }
            else {
                Toast.makeText(context, "로그아웃 성공. SDK에서 토큰 삭제됨", Toast.LENGTH_LONG).show()
            }
        }
    }
    fun UserInfo(context: Context){
        UserApiClient.instance.accessTokenInfo { tokenInfo, error ->
            if (error != null) {
                Toast.makeText(context, "토큰 정보 보기 실패", Toast.LENGTH_LONG).show()
            }
            else if (tokenInfo != null) {
                Toast.makeText(context, "\"토큰 정보 보기 성공\" +\n" +
                        "                        \"\\n회원번호: ${tokenInfo.id}\" +\n" +
                        "                        \"\\n만료시간: ${tokenInfo.expiresIn} 초", Toast.LENGTH_LONG).show()
            }
        }
    }
}