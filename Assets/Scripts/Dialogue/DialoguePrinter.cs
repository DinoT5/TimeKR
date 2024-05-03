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

    
    
    public void PrintDialogueLine(string lineToPrint, float charSpeed, Action finishedCallback)
    {
        _itemUsePanel.SetActive(true);
        StartCoroutine(CO_PrintDialogueLine(lineToPrint, charSpeed, finishedCallback));
    }
    private IEnumerator CO_PrintDialogueLine(string lineToPrint, float charSpeed, Action finishedCallback)
    {
        _dialogueTextMesh.SetText(string.Empty);

        for (int i = 0; i < lineToPrint.Length; i++)
        {
            var character = lineToPrint[i];
            _dialogueTextMesh.SetText(_dialogueTextMesh.text + character);

            yield return new WaitForSeconds(charSpeed);
        }
        finishedCallback?.Invoke();
        _itemUsePanel.SetActive(false);
        yield return null;
    }
    void Awake()
    {
        Instance = this;
        _itemUsePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
