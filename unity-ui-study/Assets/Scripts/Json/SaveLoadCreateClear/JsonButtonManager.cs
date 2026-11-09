using UnityEngine;

public class JsonButtonManager : MonoBehaviour
{
    public static event System.Action OnSaved;
    public static event System.Action OnLoaded;
    public static event System.Action OnCreated;
    public static event System.Action OnClreaed;

    public void OnClickSave()
    {
        OnSaved?.Invoke();
        Debug.Log("세이브");
    }
    public void OnClickLoad()
    {
        OnLoaded?.Invoke();
        Debug.Log("로드");
    }
    public void OnClickCreate()
    {
        OnCreated?.Invoke();
        Debug.Log("생성");
    }
    public void OnClickClear()
    {
        OnClreaed?.Invoke();
        Debug.Log("클리어");
    }
}
