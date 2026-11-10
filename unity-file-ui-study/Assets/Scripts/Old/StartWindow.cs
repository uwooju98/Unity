using UnityEngine;
using UnityEngine.UI;

public class StartWindow : GenericWindow
{
    public Button contienuButton;
    public Button newGameButton;
    public Button optionButton;

    public bool canContinue = false;

    protected override void Awake()
    {
        base.Awake();

        contienuButton.onClick.AddListener(OnClickContinue);
        newGameButton.onClick.AddListener(OnClickNewGame);
        //newGameButton.onClick.AddListener(() => Debug.Log("New Game"));
        optionButton.onClick.AddListener(OnClickOption);
    }

    public override void Open()
    {
        contienuButton.gameObject.SetActive(canContinue);
        firstSeleted = canContinue ? contienuButton.gameObject : newGameButton.gameObject;

        base.Open();
    }

    public void OnClickContinue()
    {
        manager.Open(Windows.GameOver);
    }

    public void OnClickNewGame()
    {
        manager.Open(Windows.Keyboard);
    }

    public void OnClickOption()
    {
        Debug.Log("OnClickOption");
    }
}
