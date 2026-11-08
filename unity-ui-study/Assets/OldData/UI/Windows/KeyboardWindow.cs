using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class KeyboardWindow : GenericWindow
{
    public int maxCharacters = 7;
    public TMP_Text inputField;

    public GameObject rootKeyboard;

    private Button[] keys;

    private string inputText = string.Empty;
    private string inputTextWithCursor = "_";

    private bool isCursorVisible;
    private float blinkTimer;
    private float blinkInterval = 0.5f;



    protected override void Awake()
    {
        base.Awake();
        keys = rootKeyboard.GetComponentsInChildren<Button>();
        foreach (var button in keys)
        {
            TMP_Text text = button.GetComponentInChildren<TMP_Text>();
            string key = text.text;
            button.onClick.AddListener(() => OnKeys(key));
        }
    }

    private void Update()
    {
        blinkTimer += Time.deltaTime;
        if (blinkTimer > blinkInterval)
        {
            blinkTimer = 0f;
            isCursorVisible = !isCursorVisible;
            UpdateInputField();
        }
    }

    public override void Open()
    {
        blinkTimer = 0f;
        isCursorVisible = true;
        SetText(String.Empty);

        base.Open();
    }

    private void SetText(string text)
    {
        if (text.Length > maxCharacters)
        {
            return;
        }
        inputText = text;
        inputTextWithCursor = inputText.Length < maxCharacters ? $"{inputText}_" : inputText;
        UpdateInputField();
    }

    public void UpdateInputField()
    {
        inputField.text = isCursorVisible ? inputTextWithCursor : inputText;
    }

    public void OnCancel()
    {
        SetText(string.Empty);
    }

    public void OnDelete()
    {
        if (inputText.Length > 0)
        {
            SetText(inputText.Substring(0, inputText.Length - 1));
        }
    }

    public void OnAccept()
    {
        windowManager.Open(Windows.Difficulty);
    }

    public void OnKeys(string keys)
    {
        SetText($"{inputText}{keys}");
    }


    public void OnKeyDown()
    {
        if (inputText.Length < maxCharacters)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                inputText += "Q";
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                inputText += "W";
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                inputText += "E";
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                inputText += "R";
            }
            if (Input.GetKeyDown(KeyCode.T))
            {
                inputText += "T";
            }
            if (Input.GetKeyDown(KeyCode.Y))
            {
                inputText += "Y";
            }
            if (Input.GetKeyDown(KeyCode.U))
            {
                inputText += "U";
            }
            if (Input.GetKeyDown(KeyCode.I))
            {
                inputText += "I";
            }
            if (Input.GetKeyDown(KeyCode.O))
            {
                inputText += "O";
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                inputText += "P";
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                inputText += "A";
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                inputText += "S";
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                inputText += "D";
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                inputText += "F";
            }
            if (Input.GetKeyDown(KeyCode.G))
            {
                inputText += "G";
            }
            if (Input.GetKeyDown(KeyCode.H))
            {
                inputText += "H";
            }
            if (Input.GetKeyDown(KeyCode.J))
            {
                inputText += "J";
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                inputText += "K";
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                inputText += "L";
            }
            if (Input.GetKeyDown(KeyCode.Z))
            {
                inputText += "Z";
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                inputText += "X";
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                inputText += "C";
            }
            if (Input.GetKeyDown(KeyCode.V))
            {
                inputText += "V";
            }
            if (Input.GetKeyDown(KeyCode.B))
            {
                inputText += "B";
            }
            if (Input.GetKeyDown(KeyCode.N))
            {
                inputText += "N";
            }
            if (Input.GetKeyDown(KeyCode.M))
            {
                inputText += "M";
            }
        }

    }
}

//using UnityEngine;
//using TMPro;
//using UnityEngine.UI;
//using Unity.IO.LowLevel.Unsafe;
//using Unity.VisualScripting;
//using System;
//using NUnit.Framework;

//public class KeyboardWindow : GenericWindow
//{
//    public int maxCharacters = 7; // 7글자 제한
//    public TMP_Text inputField;
//    public GameObject rootKeyboard;
//    public Button[] keys;

//    private string inputText = string.Empty; // inputField는 남겨두고 쓸 TEXT
//    private string inputTextWithCursor = "_"; // 커서용

//    private bool isCursorVisible;
//    private float blinkTimer;
//    private float blinkInterval = 0.5f;

//    // private bool isInitialState = true;

//    protected override void Awake()
//    {
//        base.Awake();
//        keys = rootKeyboard.GetComponentsInChildren<Button>();
//        foreach (var button in keys)
//        {
//            TMP_Text text = button.GetComponentInChildren<TMP_Text>();
//            string key = text.text;
//            button.onClick.AddListener(() => OnKeys(key));
//        }
//    }

//    private void Update()
//    {
//        blinkTimer += Time.deltaTime;
//        if (blinkTimer > blinkInterval)
//        {
//            blinkTimer = 0f;
//            isCursorVisible = !isCursorVisible;
//            UpdateInputField();
//        }
//    }

//    public override void Open()
//    {
//        blinkTimer = 0f; // 초기화
//        isCursorVisible = true; // false여도 상관없음
//        SetText(String.Empty);

//        base.Open();
//    }

//    private void SetText(string text)
//    {
//        if (text.Length > maxCharacters)
//        {
//            return;
//        }
//        inputText = text;
//        inputTextWithCursor = inputText.Length < maxCharacters ? $"{inputText}_" : inputText;
//        UpdateInputField();
//    }

//    public void UpdateInputField()
//    {
//        inputField.text = isCursorVisible ? inputTextWithCursor : inputText;
//    }

//    public void OnCancel()
//    {
//        SetText(string.Empty);
//        //inputField.text = string.Empty;
//    }

//    public void OnDelete()
//    {
//        if (inputText.Length > 0)
//        {
//            SetText(inputText.Substring(0, inputText.Length - 1));
//        }
//    }

//    public void OnAccept()
//    {
//        windowManager.Open(Windows.Start);
//    }

//    public void OnKeys(string keys)
//    {
//        SetText($"{inputText}{keys}");
//    }
//}

//// if (isInitialState)
//// {
////     inputField.text = "";
////     isInitialState = false;
//// }

//// if (inputField.text.Length < maxCharacters)
//// {
////     inputField.text += keys;
////     
//// }

//// public void OnKeyDowns(string keys)
//// {
////     if(Input.GetKeyDown(keys))
////     {
////         inputField.text += keys;
////     }
//// }