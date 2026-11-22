using UnityEngine;
using UnityEngine.UI;

public class GamePlayingClockUI : MonoBehaviour
{
    [SerializeField] private Image timerImage;

    private void Update()
    {
        if (GameManager.Instance.IsGamePlaying())
        {
            float timerNormalized = GameManager.Instance.GetGamePlayingTimerNormalized();
            timerImage.fillAmount = timerNormalized;
        }
    }
}
