using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    public static InGameUI InGameUI;
    public static TimeBarUI TimeBarUI;

    protected override void Awake()
    {
        base.Awake();
        InGameUI = GetComponentInChildren<InGameUI>();
        TimeBarUI = GetComponent<TimeBarUI>();
    }
}
