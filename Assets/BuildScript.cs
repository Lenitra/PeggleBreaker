using UnityEngine;
using UnityEditor;

public class BuildScript : MonoBehaviour
{
    private string key = "mykey";

    public void BuildApplication()
    {
        // Appliquer la clé de signature à PlayerSettings
        PlayerSettings.Android.keystoreName = "Build/user.keystore";
        PlayerSettings.Android.keystorePass = "thomas2001";
        PlayerSettings.Android.keyaliasName = "key";
        PlayerSettings.Android.keyaliasPass = "thomas2001";

        // Construire l'application
        // BuildPipeline.BuildPlayer(new string[] { "Assets/Scenes/MyScene.unity" }, "MyApplication.apk", BuildTarget.Android, BuildOptions.None);
    }
}
