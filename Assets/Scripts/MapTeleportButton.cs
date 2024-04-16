using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapTeleportButton : MonoBehaviour
{
    // Start is called before the first frame update

    public Vector3 teleportLocation;
    public string sceneToLoad;
    public GameObject MapPanel;
    [SerializeField] private ScreenFader _fader;



    public Transform Player;

    private void Awake()
    {
       
        GameObject player = GameObject.FindWithTag("Player");
        Player = Player.transform;
    }

    public void Teleport()
    {
        
        Player.gameObject.transform.position = teleportLocation;

        _fader.FadeFromBlack(4f,FinishedFadingFromBlack);
        MapPanel.SetActive(false);

    }
    public void TeleportScene()
    {
        PlayerPrefs.SetFloat("TeleportX", teleportLocation.x);
        PlayerPrefs.SetFloat("TeleportY", teleportLocation.y);
        PlayerPrefs.SetFloat("TeleportZ", teleportLocation.z);

        SceneManager.LoadScene(sceneToLoad);
    }
    
    public void CloseMap()
    {
        MapPanel.SetActive(false);
        _fader.FadeFromBlack(4f, null);
    }

    private void FinishedFadingFromBlack()
    {
        MapPanel.SetActive(false);
        _fader.FadeFromBlack(4f, null);


    }
}
