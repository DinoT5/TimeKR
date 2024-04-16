using Ink.Parsed;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f;

    public Rigidbody playerRb;
    public Animator animator;
    SpriteRenderer spriteRenderer;
    Vector3 movement;
    public bool canMove;


    // Update is called once per frame
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        canMove = true;
    }
    private void OnEnable()
    {
        EventBus.Instance.onGameplayPaused += () => canMove = false;
        EventBus.Instance.onGameplayResumed += () => canMove = true;

    }
    private void OnDisable()
    {
        EventBus.Instance.onGameplayPaused -= () => canMove = false;
        EventBus.Instance.onGameplayResumed -= () => canMove = true;
    }
    void Update()
    {
        
        
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.z = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.z);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        if (canMove == false)
        {
            animator.SetFloat("Speed", 0f);
        }





    }
    private void FixedUpdate()
    {

        if (DialogueManager.GetInstance().dialogueIsPlaying == true || CountdownTimer.GetInstance().viewingCountdown == true)
        {
            return;
        }
        if (canMove == false)
        {
            return;
        }


        playerRb.MovePosition(playerRb.position + movement * speed * Time.fixedDeltaTime);
    }
}
