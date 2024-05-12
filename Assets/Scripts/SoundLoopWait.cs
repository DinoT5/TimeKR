using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundLoopWait : MonoBehaviour
{
    public AudioSource soundEffect;
    private bool canReplay = true;

    public void ReplaySound()
    {
        if (canReplay)
        {
            soundEffect.Stop();

            soundEffect.Play();

            StartCoroutine(DelayReplay());
        }
    }

    IEnumerator DelayReplay()
    {
        canReplay = false;

        yield return new WaitForSeconds(6f);

        canReplay = true;
    }
}
