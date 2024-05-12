using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallwayInteraction : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private ItemData _requiredItem;
    [SerializeField] private Transform _teleportDestination;
    [SerializeField] private ScreenFader _fader;
    public AudioSource src;
    public AudioClip _doorEnter;


    public DialogueTrigger _dialogueKey;

    private void Awake()
    {

    }

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

            if (item == _requiredItem)
            {
                InventoryView.Instance.CloseInventory();
                if (item.DestroyAfterUse)
                {
                    InventoryView.Instance.GetSelectedSlot().itemData = null;
                }
                _dialogueKey.enabled = false;
                Actions.OrderCurrencyUpdate.InvokeAction(0);
                DialoguePrinter.Instance.PrintDialogueLine("You used the Key on the door.", 0.04f, () => _fader.FadeToBlack(1f, FinishedFadingToBlack));


            }
        }
    }
    private void TeleportPlayer()
    {
        if (_teleportDestination != null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            if (player != null && mainCamera != null)
            {
                Vector3 offset = _teleportDestination.position - player.transform.position;

                player.transform.position += offset;

                mainCamera.transform.position += offset;
            }
            else
            {
                Debug.LogError("Player GameObject not found.");
            }
        }
        else
        {
            Debug.LogError("Teleport destination not set.");
        }

        //_fader.FadeFromBlack(4f, FinishedFadingFromBlack);

    }
    private void FinishedFadingToBlack()
    {
        src.clip = _doorEnter;
        src.Play();
        _fader.FadeToBlack(2f, FinishedFadingFromBlack);


    }
    private void FinishedFadingFromBlack()
    {
        TeleportPlayer();

        StartCoroutine(WaitAndFadeFromBlack());
    }

    private IEnumerator WaitAndFadeFromBlack()
    {
        yield return new WaitForSeconds(2);

        _fader.FadeFromBlack(2f, null);
    }
}
