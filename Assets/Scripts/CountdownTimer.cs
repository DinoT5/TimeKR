using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI countdownText;
    [SerializeField] float remainingTime;
    [SerializeField] private GameObject countdownPanel;
    private static CountdownTimer instance;
    private Animator countdownAnimator;
    private bool isTabPressed = false;
    public bool viewingCountdown { get; private set; }

    private void Awake()
    {
        instance = this;
        countdownAnimator = countdownPanel.GetComponent<Animator>();
        viewingCountdown = false;
    }
    public static CountdownTimer GetInstance()
    {
        return instance;
    }
    private void Start()
    {
        //countdownPanel.SetActive(false);
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            
            isTabPressed = true;

        }
        else if (Input.GetKeyUp(KeyCode.Tab))
        {
            
            isTabPressed = false;
        }
        if (isTabPressed)
        {
            countdownPanel.SetActive(true);
            countdownAnimator.SetBool("ShowCountdown", true);
            viewingCountdown = true;
        }
        else if (isTabPressed == false)
        {
            countdownPanel.SetActive(false);
            countdownAnimator.SetBool("ShowCountdown", false);
            viewingCountdown = false;
        }

    }

}
