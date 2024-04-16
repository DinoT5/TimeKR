using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public GameObject interactableObject; 
    public string sceneToLoad; 

    private Interactor interactable;

    private void Start()
    {
        interactable = interactableObject.GetComponent<Interactor>();
        if (interactable == null)
        {
            Debug.LogError("The interactableObject does not have an InteractableObject script attached.");
        }
    }

    private void Update()
    {
        if (interactable.isInteractable == false)
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}