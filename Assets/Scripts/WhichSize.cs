using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WhichSize : MonoBehaviour
{
    [SerializeField]
    private int fieldSize;

    private InputField inputField;

    private void Start()
    {
        inputField = GetComponentInChildren<InputField>();

        inputField.characterValidation = InputField.CharacterValidation.Integer;
        inputField.text = fieldSize.ToString();
    }

    public void OnEndEdit()
    {
        fieldSize = int.Parse(inputField.text);
    }

    public int FieldSize
    {
        get
        {
            return fieldSize;
        }
    }
}
