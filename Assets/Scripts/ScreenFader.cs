using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{
    public static ScreenFader Instance { get;private set;}
    [SerializeField]private Image _fader;
    private bool _isBusy;
    public PlayerMovement _playerMovement;
    public void FadeToBlack(float duration, Action finishedCallback)
    {
        if (_isBusy) return;
        _playerMovement.canMove = false;
        StartCoroutine(CO_FadeToBlack(duration,finishedCallback));
    }
    public void FadeFromBlack(float duration, Action finishedCallback)
    {
        if (_isBusy) return;
        StartCoroutine(CO_FadeFromBlack(duration,finishedCallback));
    }

    private void Awake()
    {
        Instance = this;

    }
    public void FadeToScene(string sceneName, float duration)
    {
        if (_isBusy) return;
        StartCoroutine(CO_FadeToScene(sceneName, duration));
    }
    private IEnumerator CO_FadeToScene(string sceneName, float duration)
    {
        _isBusy = true;

        yield return StartCoroutine(CO_FadeToBlack(duration, null));

        SceneManager.LoadScene(sceneName);

        yield return StartCoroutine(CO_FadeFromBlack(duration, null)); // No callback needed here

        _isBusy = false;
    }


    private IEnumerator CO_FadeToBlack(float duration, Action finishedCallback)
    {
        _isBusy = true;
        while (_fader.color.a < 1)
        {
            _fader.color = new Color(0,0,0, _fader.color.a + (Time.deltaTime / duration ));
            
            yield return null;
        }
        _fader.color = new Color(0, 0, 0, 1);
        _isBusy = false;
        finishedCallback?.Invoke();
        yield return null;
    }
    private IEnumerator CO_FadeFromBlack(float duration, Action finishedCallback)
    {
        _isBusy = true;
        while (_fader.color.a > 0)
        {
            _fader.color = new Color(0, 0, 0, _fader.color.a - (Time.deltaTime / duration));
            
            yield return null;
        }
        _fader.color = new Color(0, 0, 0, 0);
        _isBusy = false;
        finishedCallback?.Invoke();
        _playerMovement.canMove = true;
        yield return null;
    }

}
