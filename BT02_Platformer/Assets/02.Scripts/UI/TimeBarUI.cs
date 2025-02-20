using TMPro;
using UnityEngine;

public class TimeBarUI : MonoBehaviour
{
    private TextMeshProUGUI timeText;

    public bool isCount = true;
    public float game_time = 0;
    public bool isTimeover = false;
    public float display_time = 0;

    float times = 0;

    private void Start()
    {
        if (true == isCount)
        {
            display_time = game_time;
        }
    }

    private void Update()
    {
        if(isTimeover == false)
        {
            times = Time.deltaTime;

            if (isCount)
            {
                display_time = game_time - times;
            }

            if(display_time <= 0.0f)
            {
                display_time = 0f;
                isTimeover = true;
            }
        }
        else
        {
            display_time = times;
            if(display_time >= game_time)
            {
                display_time = game_time;
                isTimeover = true;
            }
        }
    }
}
