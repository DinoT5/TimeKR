//Author: Benjamin Bertolli

using UnityEngine;

public class CurrencyDependent : MonoBehaviour
{
    [SerializeField] private bool UsePlayerCurrency = false;

    private protected int _TotalCurrency = 0;
    public int TotalCurrency { get => _TotalCurrency; set => _TotalCurrency = value; }

    private void OnEnable()
    {
        if (UsePlayerCurrency)
        {
            Actions.OnChangeCurrency += SetCurrency;
            Actions.OrderCurrencyUpdate.InvokeAction(0);
        }
    }

    private void OnDisable()
    {
        if (UsePlayerCurrency)
        {
            Actions.OnChangeCurrency -= SetCurrency;
        }
    }

    private protected void SetCurrency(int newCurrency)
    {
        _TotalCurrency = newCurrency;
    }
}