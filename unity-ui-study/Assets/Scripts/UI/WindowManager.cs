using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class WindowManager : MonoBehaviour
{
    public List<GenericWindow> windows;


    public Windows defaultWindow;
    public Windows CurrentWindow { get; private set; }


    public void Start()
    {
        foreach (var window in windows)
        {
            window.Init(this);
            window.gameObject.SetActive(false);

            CurrentWindow = defaultWindow;
            windows[(int)CurrentWindow].Open();
        }
    }


    public void Open(Windows id)
    {
        windows[(int)CurrentWindow].Close();
        CurrentWindow = id;
        windows[(int)CurrentWindow].Open();
    }
}
