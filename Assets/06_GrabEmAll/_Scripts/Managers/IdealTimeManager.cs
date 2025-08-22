using UnityEngine;
using static DivoPOC.ActionManager;


namespace DivoPOC.GrabEmAll
{
    public class IdealTimeManager : MonoBehaviour
    {
        [Header("<b><size=15><color=Green>Threshold Ideal Timmer")]
        [Space(6)]
        [Range(0, 30)]
        [SerializeField] private float ThresholdIdelTime;
        private float currentIdealTime;

        private void OnEnable()
        {
            ActionManager.OnResetIdealTimmer += ResetIdealTimmer;
            ActionManager.OnUpdateIdealTimmer += IncreamentIdealTimmer;
        }

        private void OnDisable()
        {
            ActionManager.OnResetIdealTimmer -= ResetIdealTimmer;
            ActionManager.OnUpdateIdealTimmer -= IncreamentIdealTimmer;
        }
        private void ResetIdealTimmer()
        {
            this.currentIdealTime = 0f;
        }
        private void IncreamentIdealTimmer()
        {
            float idealTimmerVocalLength = GetClipLength(GameManager.GMAIdealTimmer_VocalAudio);
            this.currentIdealTime += 1f;

            if (Mathf.Abs(ThresholdIdelTime - this.currentIdealTime) == 8)
            {
                OnPlayCustomTimerSound?.Invoke(GameManager.GMAIdealTimmer_VocalAudio, GameManager.customVolume);
            }
            if (Mathf.Abs(ThresholdIdelTime - this.currentIdealTime) < (8 - idealTimmerVocalLength))
            {
                OnPlayCustomTimerSound?.Invoke(GameManager.GMAIdealTimmer_BeepAudio, GameManager.customVolume);
                if (this.currentIdealTime == ThresholdIdelTime)
                {
                    IdealTimmerFinished();
                }
            }
        }
        private void IdealTimmerFinished()
        {
            ActionManager.OnGameOver?.Invoke();
        }
    }
}

