//Author: Benjamin Bertolli

using System;
using UnityEngine;
using TMPro;

public enum eCurrencyFormats
{
    Basic = 0,
    Time = 1
}

public class CurrencyUICounter : CurrencyDependent
{

    [SerializeField] private TextMeshProUGUI TMPro_TextField;
    [SerializeField] private eCurrencyFormats CurrencyFormat = eCurrencyFormats.Time; 

    private void UpdateUI()
    {

        switch (CurrencyFormat)
        {

            case eCurrencyFormats.Basic:
                TMPro_TextField.text = TotalCurrency.ToString();
                break;

            case eCurrencyFormats.Time:
                TMPro_TextField.text = GenerateTextStringInTimeFormat();
                break;

            default:
                TMPro_TextField.text = TotalCurrency.ToString();
                break;

        }

    }

    private string GenerateTextStringInTimeFormat()
    {

        string TimeFormattedText;
        float TimeAsPercent = TotalCurrency * 0.01f;
        int TotalHours = Convert.ToInt32(TimeAsPercent);
        float TotalMinutesAsPercent = TimeAsPercent - TotalHours;
        int TotalMinutes = (int) (TotalMinutesAsPercent * 100 * 0.6);

        //Calculation was negative so add 1hr to put it back to positive
        if(TotalMinutes < 0)
        {

            TotalMinutes += 60;

        }

        TimeFormattedText = TotalHours.ToString() + "hr : " + TotalMinutes.ToString() + "mins";

        return TimeFormattedText;

    }

    private void Update()
    {
        UpdateUI(); //This is so bad hopefully can optimise...
    }

}
