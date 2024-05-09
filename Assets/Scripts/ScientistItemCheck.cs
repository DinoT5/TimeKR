using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScientistItemCheck : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private ItemData Alcohol;
    [SerializeField] private ItemData Drugs;
    [SerializeField] private ItemData Tweezers;
    [SerializeField] private ItemData WildGarlic;
    [SerializeField] private TextAsset inkJSON_alcohol;
    [SerializeField] private TextAsset inkJSON_drugs;
    [SerializeField] private TextAsset inkJSON_tweezers;
    [SerializeField] private TextAsset inkJSON_garlic;

    [SerializeField] private bool alcoholCheck;
    [SerializeField] private bool drugCheck;
    [SerializeField] private bool tweezersCheck;
    [SerializeField] private bool garlicCheck;


    private void OnEnable()
    {
        EventBus.Instance.onItemUsed += OnItemUsed;
    }

    // Update is called once per frame
    private void OnDisable()
    {
        EventBus.Instance.onItemUsed -= OnItemUsed;

    }
    private void OnItemUsed(ItemData item)
    {

        if (Vector3.Distance(PlayerMovement.Instance.transform.position, transform.position) < 3)
        {

            if (item == Alcohol)
            {
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON_alcohol);
                alcoholCheck = true;
                if (item.DestroyAfterUse)
                {
                    InventoryView.Instance.GetSelectedSlot().itemData = null;
                }

            }
            else if (item == Drugs)
            {
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON_drugs);
                drugCheck = true;
                if (item.DestroyAfterUse)
                {
                    InventoryView.Instance.GetSelectedSlot().itemData = null;
                }
            }
            else if (item == Tweezers)
            {
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON_tweezers);
                tweezersCheck = true;
                if (item.DestroyAfterUse)
                {
                    InventoryView.Instance.GetSelectedSlot().itemData = null;
                }

            }
            else if (item == WildGarlic)
            {
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON_garlic);
                garlicCheck = true;
                if (item.DestroyAfterUse)
                {
                    InventoryView.Instance.GetSelectedSlot().itemData = null;
                }
            }
            CheckCure();
        }
    }

    void CheckCure()
    {
        if (alcoholCheck && drugCheck && garlicCheck && tweezersCheck)
        {
            PlayerPrefs.SetInt("Cure", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Cure", 0);
        }
    }
}
