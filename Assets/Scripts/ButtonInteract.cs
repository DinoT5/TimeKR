using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonInteract : MonoBehaviour
{
    public GameObject lighttToActivate;
    private Animator animator;


    void Start()
    {
        if (lighttToActivate != null)
        {
            lighttToActivate.SetActive(false);
        }
        animator = GetComponent<Animator>();
        animator.enabled = false;



    }

    public void OpenLight()
    {
        if (lighttToActivate != null)
        {
            lighttToActivate.SetActive(true);
        }
    }

    public void CloseLight()
    {
        if (lighttToActivate != null)
        {
            lighttToActivate.SetActive(false);
        }
    }
    public void ActivateAnimator()
    {
        if (animator != null)
        {
            animator.enabled = true;
            animator.SetBool("Stopping", false);

        }
    }

    // Function where you want to deactivate the animator
    public void DeactivateAnimator()
    {
        if (animator != null)
        {
            animator.SetBool("Stopping", true);

        }
    }
}
