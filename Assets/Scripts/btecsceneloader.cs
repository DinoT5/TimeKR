using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class btecsceneloader : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject MapPanel;
    public GameObject visualCue;
    [SerializeField] private ScreenFader _fader;
    private void Start()
    {
        visualCue.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        visualCue.SetActive(true);
        if (Input.GetKey(KeyCode.E))
        {
            _fader.FadeToBlack(0.3f, FinishedFadingToBlack);
            
        }
    }
    private void OnTriggerExit(Collider Player)
    {
        visualCue.SetActive(false);
    }
    private void FinishedFadingToBlack()
    {
        MapPanel.SetActive(true);
        _fader.FadeToBlack(0.3f, null);
    }
}
