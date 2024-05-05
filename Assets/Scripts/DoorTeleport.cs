using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTeleport : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform _teleportDestination;
    [SerializeField] private ScreenFader _fader;
    public GameObject visualCue;

    void Start()
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
    // Update is called once per frame
    void Update()
    {

    }
    private void TeleportPlayer()
    {
        if (_teleportDestination != null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            GameObject mainCamera = Camera.main.gameObject;
            if (player != null && mainCamera != null)
            {
                Vector3 offset = _teleportDestination.position - player.transform.position;

                player.transform.position += offset;

                mainCamera.transform.position += offset;
            }
            else
            {
                Debug.LogError("Player GameObject not found.");
            }
        }
        else
        {
            Debug.LogError("Teleport destination not set.");
        }
    }
    private void FinishedFadingToBlack()
    {
        _fader.FadeToBlack(2f, FinishedFadingFromBlack);
        

    }
    private void FinishedFadingFromBlack()
    {
        TeleportPlayer();
        StartCoroutine(WaitAndFadeFromBlack());
    }

    private IEnumerator WaitAndFadeFromBlack()
    {
        yield return new WaitForSeconds(2);

        _fader.FadeFromBlack(1f, null);
    }
    private void OnTriggerExit(Collider other)
    {
        visualCue.SetActive(false);

    }
}
