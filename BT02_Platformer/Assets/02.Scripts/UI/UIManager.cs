using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    public static InGameUI InGameUI;

    protected override void Awake()
    {
        base.Awake();
        InGameUI = GetComponentInChildren<InGameUI>();
    }
}
