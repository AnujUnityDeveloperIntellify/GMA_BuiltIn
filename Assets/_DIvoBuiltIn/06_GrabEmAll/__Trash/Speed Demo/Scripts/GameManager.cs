using TMPro;
using System.Collections;
using UnityEngine;
using System;
using UnityEngine.UI;

namespace SpeedVariation
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Button startBtn;
        [SerializeField] private TextMeshPro _timmerTxt;
        [SerializeField] private TextMeshPro _timeUpTxt;
        [SerializeField] private GameObject _StartUIGO;
        private float timeInMinites = 5f;
        private Coroutine countdownCoroutine;
        void Start()
        {
            startBtn.onClick.AddListener(GameStart);
        }
        private void OnEnable()
        {

        }
        private void OnDisable()
        {

        }
        [ContextMenu("Game Start")]
        private void GameStart()
        {
            StartCountdown(timeInMinites, () => { GameComplete(); });
            _StartUIGO.SetActive(false);
        }
        private void GameComplete()
        {
            _timeUpTxt.gameObject.SetActive(true);
            _timmerTxt.gameObject.SetActive(false);
        }
        private void StartCountdown(float minutes, Action onComplete = null)
        {
            if (countdownCoroutine != null)
                StopCoroutine(countdownCoroutine);
            countdownCoroutine = StartCoroutine(CountdownCoroutine(minutes * 60f, onComplete));
        }
        private IEnumerator CountdownCoroutine(float seconds, Action onComplete)
        {
            _timmerTxt.gameObject.SetActive(true);
            float secondsLeft = seconds;
            int debugCountdown = 30;
            ActionManager.OnCalculateNewYearSpeed?.Invoke();
            while (secondsLeft > 0)
            {
                yield return new WaitForSeconds(1f);
                if (debugCountdown <= 0 && secondsLeft > 0)
                {
                    debugCountdown = 30;
                    ActionManager.OnCalculateNewYearSpeed?.Invoke();
                }
                secondsLeft--;
                debugCountdown--;
                UpdateTimerDisplay(secondsLeft);
            }
            UpdateTimerDisplay(0);
            onComplete?.Invoke();
        }
        private void UpdateTimerDisplay(float timeLeft)
        {
            int minutes = Mathf.FloorToInt(timeLeft / 60f);
            int seconds = Mathf.FloorToInt(timeLeft % 60f);
            _timmerTxt.text = $"{minutes:00}:{seconds:00}";
        }
    }
}

