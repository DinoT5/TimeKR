using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectionForcer : MonoBehaviour
{
        [SerializeField] private Button _previousSelection;
        public GameObject defaultSelectedObject; 


    void Start()
    {
        _previousSelection.Select();
    }


}
