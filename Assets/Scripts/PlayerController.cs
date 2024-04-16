using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public float groundDist;

    public LayerMask terrainLayer;
    public Rigidbody rb;
    public SpriteRenderer sr;
    public Animator animator;
    public bool canMove;

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }
    void Start()
    {
        canMove = true;
    }
    private void OnEnable()
    {
        EventBus.Instance.onOpenInventory += () => canMove = true;
        EventBus.Instance.onCloseInventory += () => canMove = false;

    }
    private void OnDisable()
    {
        EventBus.Instance.onOpenInventory -= () => canMove = true;
        EventBus.Instance.onCloseInventory -= () => canMove = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (canMove == false)
        {
            return;
        }
        if (DialogueManager.GetInstance().dialogueIsPlaying == true || CountdownTimer.GetInstance().viewingCountdown == true)
        {
            return;
        }
        RaycastHit hit;
        Vector3 castPos = transform.position;
        castPos.y += 1;
        if (Physics.Raycast(castPos, -transform.up, out hit, Mathf.Infinity, terrainLayer))
        {
            if (hit.collider != null)
            {
                Vector3 movePos = transform.position;
                movePos.y = hit.point.y + groundDist;
                transform.position = movePos;
            }
        }

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector3 moveDir = new Vector3(x, 0, y);
        rb.velocity = moveDir * speed;
        if (x != 0 && x < 0)
        {
            sr.flipX = true;
        }
        else if (x != 0 && x > 0)
        {
            sr.flipX = false;
        }
        if (x == 0)
        {
            animator.SetBool("isMoving", false);
        }
        else
            animator.SetBool("isMoving", true);

    }
}
