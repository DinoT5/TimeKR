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
    public AudioSource src;
    public AudioClip _countdownSound;

    private bool soundPlayed = false;


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
            if (!isTabPressed)
            {
                isTabPressed = true;
                countdownPanel.SetActive(true);
                countdownAnimator.SetBool("ShowCountdown", true);
                viewingCountdown = true;

                if (!soundPlayed)
                {
                    src.PlayOneShot(_countdownSound);
                    soundPlayed = true;
                }
            }
        }
        else if (Input.GetKeyUp(KeyCode.Tab))
        {
            isTabPressed = false;
            countdownPanel.SetActive(false);
            countdownAnimator.SetBool("ShowCountdown", false);
            viewingCountdown = false;
            soundPlayed = false;

        }
    }


}
