using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    public KeyCode interactKey = KeyCode.E; // Key to press to interact with the object
    public float interactionDistance = 2f; // Maximum distance at which the player can interact with the object
    public bool isInteractable = true; // Whether the object is currently interactable
    public Animator animator; // Reference to the animator component
    public bool disableAfterInteract = true; // Whether to disable the object after it has been interacted with
    public Color interactedColor = Color.green; // Color to change the object to when it is interacted with

    private bool hasInteracted = false; // Whether the object has been interacted with
    private GameObject player; // Reference to the player game object
    private Renderer theRenderer; // Reference to the renderer component


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // Find the player game object
        theRenderer = GetComponent<Renderer>(); // Get the renderer component
    }

    // Update is called once per frame
    void Update()
    {
        if (isInteractable && !hasInteracted && Input.GetKeyDown(interactKey) && IsPlayerInRange())
        {
            Interact();
        }
    }

    // Check if the player is within interaction distance of the object
    private bool IsPlayerInRange()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        return distance <= interactionDistance;
    }

    // Interact with the object
    public void Interact()
    {
        if (animator != null)
        {
            animator.SetTrigger("interact"); // Trigger the "interact" animation
        }

        theRenderer.material = new Material(theRenderer.material); // Create a new material instance
        theRenderer.material.color = interactedColor;

        if (disableAfterInteract)
        {
            isInteractable = false; // Disable interaction with the object
            hasInteracted = true; // Mark the object as interacted with
        }
    }
}