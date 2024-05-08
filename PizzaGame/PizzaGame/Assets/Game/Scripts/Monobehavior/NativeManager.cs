using System.Collections.Generic;
using System.IO;
using UnityEngine;
#if UNITY_IOS || UNITY_TVOS
using System.Runtime.InteropServices;
#endif
public class NativeManager : SingletonMonoBehaviour<NativeManager>
{
#if UNITY_IOS || UNITY_TVOS

    public class NativeAPI
    {
        [DllImport("__Internal")]
        public static extern void showScore(string score);
    }
#endif


    public void SendDataScore(string score)
    {
        //    Debug.Log("SENDAndroidJavaClass_FAILL");
#if UNITY_EDITOR
#elif UNITY_ANDROID
        try
        {
            AndroidJavaClass jc = new AndroidJavaClass("com.unity.mynativeapp.SharedClass");
            jc.CallStatic("showScore", score);
             Debug.Log("SENDAndroidJavaClass_TRY");
        }
        catch
        {
                 Debug.Log("SENDAndroidJavaClass_FAILL");
        }
#elif UNITY_IOS || UNITY_TVOS
        NativeAPI.showScore(score);
#endif
    }
}

