using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private GameObject visualCue;
    [SerializeField] private TextAsset inkJSON;
    [SerializeField] private TextAsset secondInkJSON;
    private bool playerInRange;
    private bool firstStoryEnd;

    private void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(false);
        //firstStoryEnd = false;
    }
    private void Update()
    {
        if (playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
           
            visualCue.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (firstStoryEnd == true)
                {
                    OnFirstStoryEnd();
                }
                else
                {

                    DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
                    firstStoryEnd = true;
                }
            }
        }
        else
        {
            visualCue.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }
    public void OnFirstStoryEnd()
    {
        DialogueManager.GetInstance().EnterDialogueMode(secondInkJSON);
    }
}
