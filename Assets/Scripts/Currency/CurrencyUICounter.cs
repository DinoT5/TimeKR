//Author: Benjamin Bertolli

using UnityEngine;
using TMPro;

public class CurrencyUICounter : CurrencyDependent
{
    [SerializeField] private TextMeshProUGUI TMPro_TextField;

    private void UpdateUI()
    {
        TMPro_TextField.text = TotalCurrency.ToString();
    }

    private void Update()
    {
        UpdateUI(); //This is so bad hopefully can optimise...
    }

}
