using System;
using UnityEngine;
using UnityEngine.UI;

public class StratWindow : GenericWindow
{
    public Button continueButton;
    public Button newGameButton;
    public Button optionButton;

    public bool canContinue;

    protected override void Awake()
    {
        base.Awake();

        continueButton.onClick.AddListener(OnClickContinueButton);
        newGameButton.onClick.AddListener(OnClickNewGameButton);
        optionButton.onClick.AddListener(OnClickOptionButton);
    }

    public override void Open()
    {
        base.Open();
        
        continueButton.gameObject.SetActive(canContinue);
        firstSeleted = canContinue ? continueButton.gameObject : newGameButton.gameObject;
    }

    private void OnClickContinueButton()
    {
        windowManager.Open(Windows.Difficulty);
    }


      private void OnClickNewGameButton()
    {
        windowManager.Open(Windows.Keyboard);
        Debug.Log("newGameButton");
    }


    private void OnClickOptionButton()
    {
        Debug.Log("optionButton");
    }

}
