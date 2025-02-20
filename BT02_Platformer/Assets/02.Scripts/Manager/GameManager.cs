using UnityEngine;

public enum GAME_STATE
{
    PLAYING,
    GAMEOVER,
    GAMECLEAR,
    END,
}

public class GameManager : MonoSingleton<GameManager>
{
    private static GAME_STATE state;
    public static GAME_STATE STATE
    {
        get
        {
            return state;
        }
        set
        {
            UIManager.InGameUI.SetActiveInGameUI(value);
            state = value;
        }
    }

    
}
