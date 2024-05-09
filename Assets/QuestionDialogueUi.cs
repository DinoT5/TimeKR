using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class QuestionDialogueUi : MonoBehaviour
{
    public GameObject _questionDialogueUI;
    public TextMeshProUGUI textMeshPro;
    public Button yesBtn;
    public Button noBtn;
    public GameObject visualCue;


    private void Awake()
    {
        textMeshPro = textMeshPro.GetComponent<TextMeshProUGUI>();
        yesBtn = yesBtn.GetComponent<Button>();
        noBtn = noBtn.GetComponent<Button>();
        visualCue.SetActive(false);


    }
    public void ShowQuestion(string questionText,Action yesAction,Action noAction)
    {
        textMeshPro.text = questionText;
        yesBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(yesAction));
        noBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(noAction));
    }
    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        visualCue.SetActive(true);
        if (Input.GetKey(KeyCode.E))
        {
            _questionDialogueUI.SetActive(true);
            ShowQuestion("Would you like to wait out the remaining time?", () =>
            {
                SceneManager.LoadScene("Battle Mode");
            }, () =>
            {
                _questionDialogueUI.SetActive(false);
            });
            

        }
    }
    private void OnTriggerExit(Collider other)
    {
        visualCue.SetActive(false);

    }



}