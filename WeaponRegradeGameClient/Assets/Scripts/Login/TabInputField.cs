using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TabInputField : MonoBehaviour
{
    public TMP_InputField[] inputFields; 


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            TMP_InputField currentInputField = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<TMP_InputField>();

            if (currentInputField != null)
            {
                int currentIndex = System.Array.IndexOf(inputFields, currentInputField);
                int nextIndex = (currentIndex + 1) % inputFields.Length; 

           
                inputFields[nextIndex].Select();
                inputFields[nextIndex].ActivateInputField();
            }
        }
    }
}