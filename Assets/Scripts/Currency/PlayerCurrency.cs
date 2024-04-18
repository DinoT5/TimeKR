//Author: Benjamin Bertolli

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCurrency : MonoBehaviour
{

    private int CurrentCurrency = 0; //Player's currency
    private const int MaxCurrency = 10000; //Can't exceed this, UI supports 5 digits but can be increased
    private const int StartingCurrency = 150;
    private bool HasSetStartCurrency = false;

    private void OnEnable()
    {
        if (HasSetStartCurrency == false)
        {
            HasSetStartCurrency = true;
            ChangeCurrency(StartingCurrency);
        }

        Actions.OrderCurrencyUpdate += ChangeCurrency;
    }

    private void OnDisable()
    {
        Actions.OrderCurrencyUpdate -= ChangeCurrency;
    }

    private void ChangeCurrency(int changeBy)
    {

        int newTotal = Mathf.Clamp(CurrentCurrency + changeBy, 0, MaxCurrency);

        CurrentCurrency = newTotal;
        Actions.OnChangeCurrency.InvokeAction(newTotal);

    }

    public int GetCurrency()
    {
        return CurrentCurrency;
    }

    public bool HasEnoughCurrency(int cost)
    {
        return cost < CurrentCurrency;
    }

}
