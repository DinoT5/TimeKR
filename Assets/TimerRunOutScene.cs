using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerRunOutScene : MonoBehaviour
{
    [SerializeField] private ScreenFader _fader;
    private void Awake()
    {
        _fader.FadeToBlack(1f, FinishedFadingToBlack);
    }

    private void FinishedFadingToBlack()
    {
        _fader.FadeToBlack(2f, FinishedFadingFromBlack);
        DialoguePrinter.Instance.PrintTimerRunOutLine("Time has run out\r\nyou make your way back to the apartment", 0.06f, () => _fader.FadeFromBlack(1f, FinishedFadingFromBlack));

    }
    private void FinishedFadingFromBlack()
    {
        
        StartCoroutine(WaitAndFadeFromBlack());
    }
    private IEnumerator WaitAndFadeFromBlack()
    {
        yield return new WaitForSeconds(2);

        SceneManager.LoadScene("Battle Mode");
    }


}
