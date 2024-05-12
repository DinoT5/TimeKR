using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Footsteps : MonoBehaviour
{
    public static Footsteps instance;
    public AudioSource audioS;
    public List<AudioClip> concreteFootsteps;


    private void Awake()
    {
        instance = this;
    }
    public void PlayFootstep() 
    {
        int randomNum = Random.Range (0, concreteFootsteps.Count);
        audioS.PlayOneShot(concreteFootsteps[randomNum]);
    }
}
