using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class GenericWindow : MonoBehaviour 
{
    protected WindowManager windowManager;


    public GameObject firstSeleted;
    private EventSystem eventSystem;


    protected virtual void Awake()
    {
        eventSystem = EventSystem.current;
    }


    public void Init(WindowManager windowManager)
    {
        this.windowManager = windowManager;
    }

    
    public void OnFocus()
    {
        eventSystem.SetSelectedGameObject(firstSeleted);
    }

    public virtual void Open()
    {
        gameObject.SetActive(true);
        OnFocus();
    }

    public virtual void Close()
    {
        gameObject.SetActive(false);
    }
}
