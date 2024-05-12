using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public void MainMenu()
    {
        ScreenFader.Instance.FadeToScene("MainMenuScene", 1.5f);
    }
}