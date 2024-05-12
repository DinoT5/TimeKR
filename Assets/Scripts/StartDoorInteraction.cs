using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDoorInteraction : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private ItemData _requiredItem;
    [SerializeField] private Transform _teleportDestination;
    [SerializeField] private ScreenFader _fader;
    public AudioSource SFX;
    public AudioClip _itemUsed;




    private void Awake()
    {
        
    }

    private void OnEnable()
    {
        EventBus.Instance.onItemUsed += OnItemUsed;
    }

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
                Actions.OrderCurrencyUpdate.InvokeAction(-5);
                DialoguePrinter.Instance.PrintDialogueLine("You used the Axe on the door.", 0.04f, () => _fader.FadeToBlack(1f, FinishedFadingToBlack));


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
        _fader.FadeToBlack(2f, FinishedFadingFromBlack);
        SFX.clip = _itemUsed;
        SFX.Play();

    }
    private void FinishedFadingFromBlack()
    {
        TeleportPlayer();

        StartCoroutine(WaitAndFadeFromBlack());
    }

    private IEnumerator WaitAndFadeFromBlack()
    {
        yield return new WaitForSeconds(2);

        _fader.FadeFromBlack(3f, null);
    }


}
