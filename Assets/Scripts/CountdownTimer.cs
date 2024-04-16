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
    public bool viewingCountdown { get; private set; }

    private void Awake()
    {
        instance = this;
        viewingCountdown = false;
    }
    public static CountdownTimer GetInstance()
    {
        return instance;
    }
    private void Start()
    {
        countdownPanel.SetActive(false);
    }

    void Update()
    {
        remainingTime -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            countdownPanel.SetActive(true);
            viewingCountdown = true;
        }
        else if (Input.GetKeyUp(KeyCode.Tab))
        {
            countdownPanel.SetActive(false);
            viewingCountdown = false;
        }

    }

}
