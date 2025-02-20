using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    [SerializeField] private Image titleImage;
    [SerializeField] private Button btn_restart;
    [SerializeField] private Button btn_next;

    [SerializeField] private Sprite title_gameover;
    [SerializeField] private Sprite title_clear;


    public void SetActiveInGameUI(GAME_STATE state)
    {
        switch (state)
        {
            case GAME_STATE.PLAYING:
                gameObject.SetActive(false);
                btn_next.interactable = false;
                btn_restart.interactable = false;
                break;
            case GAME_STATE.GAMEOVER:
                gameObject.SetActive(true);
                btn_next.interactable = false;
                btn_restart.interactable = true;
                titleImage.sprite = title_gameover;
                break;
            case GAME_STATE.GAMECLEAR:
                gameObject.SetActive(true);
                btn_next.interactable = true;
                btn_restart.interactable = false;
                titleImage.sprite = title_clear;
                break;
            default:
                break;
        }
    }

}
