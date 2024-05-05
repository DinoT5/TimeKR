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


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnItemUsed(ItemData item)
    {

        if (Vector3.Distance(PlayerMovement.Instance.transform.position, transform.position) < 3)
        {

            if (item == Alcohol)
            {
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON_alcohol);
            }
            else if (item == Drugs)
            {
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON_drugs);

            }
            else if (item == Tweezers)
            {
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON_tweezers);

            }
            else if (item == WildGarlic)
            {
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON_garlic);
            }
        }
    }

}
