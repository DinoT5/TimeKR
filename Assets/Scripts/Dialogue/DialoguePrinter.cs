using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialoguePrinter : MonoBehaviour
{
    public static DialoguePrinter Instance { get; private set; }
    
    // Start is called before the first frame update
    [SerializeField] private TMP_Text _dialogueTextMesh;
    [SerializeField] private GameObject _itemUsePanel;
    [SerializeField] private TMP_Text _dialogueTextTimerRunOut;
    private TMP_Text _currentDialogueTextMesh;

    public void PrintDialogueLine(string lineToPrint, float charSpeed, Action finishedCallback)
    {
        _currentDialogueTextMesh = _dialogueTextMesh;
        _itemUsePanel.SetActive(true);
        StartCoroutine(CO_PrintDialogueLine(lineToPrint, charSpeed, finishedCallback));
    }
    public void PrintTimerRunOutLine(string lineToPrint, float charSpeed, Action finishedCallback)
    {
        _currentDialogueTextMesh = _dialogueTextTimerRunOut;
        StartCoroutine(CO_PrintDialogueLine(lineToPrint, charSpeed, finishedCallback));
    }
    private IEnumerator CO_PrintDialogueLine(string lineToPrint, float charSpeed, Action finishedCallback)
    {
        _currentDialogueTextMesh.SetText(string.Empty);

        for (int i = 0; i < lineToPrint.Length; i++)
        {
            var character = lineToPrint[i];
            _currentDialogueTextMesh.SetText(_currentDialogueTextMesh.text + character);

            yield return new WaitForSeconds(charSpeed);
        }
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));
        _currentDialogueTextMesh.SetText(string.Empty);
        finishedCallback?.Invoke();
        _itemUsePanel.SetActive(false);

        EventBus.Instance.ResumeGameplay();
        yield return null;

    }
    void Awake()
    {
        Instance = this;
        _itemUsePanel.SetActive(false);
    }


}
