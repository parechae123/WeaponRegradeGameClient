using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TabInputField : MonoBehaviour
{
    public TMP_InputField[] inputFields; // Tab-navigable TMP InputFields

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            TMP_InputField currentInputField = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<TMP_InputField>();

            if (currentInputField != null)
            {
                int currentIndex = System.Array.IndexOf(inputFields, currentInputField);
                int nextIndex = (currentIndex + 1) % inputFields.Length; // Calculate the index of the next TMP InputField.

                // Move the focus to the next TMP InputField
                inputFields[nextIndex].Select();
                inputFields[nextIndex].ActivateInputField();
            }
        }
    }
}