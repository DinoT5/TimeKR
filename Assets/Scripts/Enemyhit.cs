using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyhit : MonoBehaviour
{
    public AudioSource audioS;
    public List<AudioClip> enemyHitSounds;
    public GameObject hitFX;
    public Transform hitSpawnpoint;


    public void Hit()
    {
        int randomNum = Random.Range(0, enemyHitSounds.Count);
        audioS.PlayOneShot(enemyHitSounds[randomNum]);
        GameObject hit = Instantiate(hitFX, hitSpawnpoint.position,hitSpawnpoint.rotation);
        Destroy(hit, 0.5f);
    }

}
