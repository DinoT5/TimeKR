using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{

    CharacterController cc;
    AudioSource audioS;


    private void Start()
    {
        cc = GetComponent<CharacterController>();
        audioS = GetComponent<AudioSource>();

    }

    void Update()
    {
        if (cc.isGrounded == true && cc.velocity.magnitude > 2f && audioS.isPlaying == false)
        {

        }

        if (cc.isGrounded && audioS.isPlaying == false)

        {
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
                audioS.Play();

            audioS.volume = Random.Range(0.8f, 1);
            audioS.pitch = Random.Range(0.8f, 1.1f);
        }
    }

}
