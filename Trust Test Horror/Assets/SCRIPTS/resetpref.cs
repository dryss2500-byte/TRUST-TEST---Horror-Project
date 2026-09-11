using UnityEngine;
using UnityEngine.SceneManagement;

public class resetpref : MonoBehaviour
{

    private bool once;

    private void Awake()
    {
        SceneManager.sceneLoaded += first_scene_loaded;
    }

    void first_scene_loaded(Scene scene, LoadSceneMode mode)
    {
        if (once) return;

        once = !once;

        if (scene == SceneManager.GetSceneAt(1)) reset_prefs(false); //outdoor scene 
    }

    private static void reset_prefs(bool settings)
    {
        PlayerPrefs.SetInt("apparition 1", 0);
        PlayerPrefs.SetInt("has 1section completed", 0);
        PlayerPrefs.SetInt("apparition 2", 0);
        /////
        ///

        if (!settings) return;

        PlayerPrefs.SetFloat("player_sens", 60f);
        PlayerPrefs.SetInt("fullscreen", 0);
        PlayerPrefs.SetInt("vsync", 0);
    }
}