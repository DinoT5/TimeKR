using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayonEnter : MonoBehaviour
{

    public AudioSource audioSource;

    private void Start()
    {
        audioSource.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            audioSource.enabled = true;
            audioSource.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            audioSource.enabled = false;
            audioSource.Stop();
        }
    }

}


