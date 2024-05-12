using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        ScreenFader.Instance.FadeToScene("SampleScene", 1.5f);
        PlayerPrefs.SetInt("EnemiesSpawned", 6);
    }

    public void Options()
    {
        ScreenFader.Instance.FadeToScene("Options", 1.5f);
    }

    public void Credits()
    {
        ScreenFader.Instance.FadeToScene("Credits", 1.5f);
    }

    public void Controls()
    {
        ScreenFader.Instance.FadeToScene("Controls", 1.5f);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
