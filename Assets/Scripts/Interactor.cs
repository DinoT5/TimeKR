using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    public KeyCode interactKey = KeyCode.E; 
    public float interactionDistance = 2f; 
    public bool isInteractable = true; 
    public Animator animator; 
    public bool disableAfterInteract = true; 
    public Color interactedColor = Color.green; 

    private bool hasInteracted = false; 
    private GameObject player; 
    private Renderer theRenderer; 


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); 
        theRenderer = GetComponent<Renderer>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (isInteractable && !hasInteracted && Input.GetKeyDown(interactKey) && IsPlayerInRange())
        {
            Interact();
        }
    }

    
    private bool IsPlayerInRange()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        return distance <= interactionDistance;
    }

    
    public void Interact()
    {
        if (animator != null)
        {
            animator.SetTrigger("interact"); 
        }

        theRenderer.material = new Material(theRenderer.material); 
        theRenderer.material.color = interactedColor;

        if (disableAfterInteract)
        {
            isInteractable = false; 
            hasInteracted = true;  
        }
    }
}