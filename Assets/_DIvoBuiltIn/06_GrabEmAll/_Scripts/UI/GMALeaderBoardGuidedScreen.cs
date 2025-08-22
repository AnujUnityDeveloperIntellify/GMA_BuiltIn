using System.Collections;
using System.Collections.Generic;
using UISystem;
using UnityEngine;
using UnityEngine.UI;
using static DivoPOC.ActionManager;

namespace DivoPOC.GrabEmAll
{
    public class GMALeaderBoardGuidedScreen : UISystem.Screen
    {
        [SerializeField] private Button nextbtn;
        [SerializeField] private Button skipbtn;
        [SerializeField] private float waitingTime;
        [SerializeField] private List<CanvasGroup> contentHolder;
        private Coroutine FadeSequenceCoroutine = null;
        private CanvasGroup previous = null;
        public override void Awake()
        {
            base.Awake();
            Init();
        }

        public override void Show()
        {
            meshObject.SetActive(true);

            if (FadeSequenceCoroutine != null)
            {
                StopCoroutine(FadeSequenceCoroutine);
            }
            ActionManager.OnStartDescriptionUIAnimation?.Invoke();
            base.Show();
        }

        public override void Hide()
        {
            meshObject.SetActive(false);
            base.Hide();
        }

        public override void Redraw()
        {
            base.Redraw();
        }
        private void Start()
        {
            GameManager.gameStatus = GrabEmAllStatus.Start;
        }
        private void OnEnable()
        {
            ResetContentHolder();
            ActionManager.OnStartDescriptionUIAnimation += StartFadeDescriptionUI;
        }
        private void OnDisable()
        {
            ActionManager.OnStartDescriptionUIAnimation -= StartFadeDescriptionUI;
        }
        #region Custom Methods
        private void Init()
        {
            nextbtn.onClick.AddListener(Next);
            skipbtn.onClick.AddListener(Skip);
        }
        [ContextMenu("Next")]
        private void Next()
        {
            previous.alpha = 0;
            if (FadeSequenceCoroutine != null)
            {
                StopCoroutine(FadeSequenceCoroutine);
            }
            ViewController.Instance.ChangeScreen(ScreenName.GMAGameStartScreen);
            OnPlayCustomTimerSound?.Invoke(GameManager.GMATutorialStart_Audio, GameManager.customVolume);
        }
        [ContextMenu("Skip")]
        private void Skip()
        {
            previous.alpha = 0;
            if (FadeSequenceCoroutine != null)
            {
                StopCoroutine(FadeSequenceCoroutine);
            }
            Next();
            //ViewController.Instance.HideScreen(ScreenName.GMALeaderBoardGuidedScreen);
            //ActionManager.OnSkipTutorial?.Invoke();
        }
        private void StartFadeDescriptionUI()
        {
            StartFadeSequence(contentHolder, 0.2f);
        }
        private void StartFadeSequence(List<CanvasGroup> uiObjects, float fadeDuration)
        {
            FadeSequenceCoroutine = StartCoroutine(FadeSequence(uiObjects, fadeDuration));
        }
        private IEnumerator FadeSequence(List<CanvasGroup> uiObjects, float fadeDuration)
        {
            //Debug.LogError("Fade Sequence Started");
            float interval = 0f;
            previous = null;
            for (int i = 0; i < uiObjects.Count; i++)
            {
                if (previous != null)
                {
                    previous.alpha = 0;
                }
                interval = PlayAudioandSetInterval(i);
                CanvasGroup current = uiObjects[i];
                current.alpha = 0;
                LeanTween.alphaCanvas(current, 1f, fadeDuration);
                previous = current;
                interval += waitingTime;
                yield return new WaitForSeconds(interval);
            }
            FadeSequenceCoroutine = null;
        }
        private void ToggleNavigation(bool isActive)
        {
            nextbtn.gameObject.SetActive(isActive);
            skipbtn.gameObject.SetActive(isActive);
        }
        private void ResetContentHolder()
        {
            foreach (var canvasgroup in contentHolder)
            {
                canvasGroup.alpha = 0;
            }
        }
        private float PlayAudioandSetInterval(int contentHolder)
        {
            float clipLength = 0f;
            switch (contentHolder)
            {
                case 0:
                    clipLength = (float)GetClipLength?.Invoke(GameManager.GMALeaderBoard_1_Audio);
                    break;
                case 1:
                    clipLength = (float)GetClipLength?.Invoke(GameManager.GMALeaderBoard_2_Audio);
                    OnPlayCustomTimerSound?.Invoke(GameManager.GMALeaderBoard_2_Audio, GameManager.customVolume);
                    break;
                case 2:
                    clipLength = (float)GetClipLength?.Invoke(GameManager.GMALeaderBoard_3_Audio);
                    OnPlayCustomTimerSound?.Invoke(GameManager.GMALeaderBoard_3_Audio, GameManager.customVolume);
                    break;
                case 3:
                    clipLength = (float)GetClipLength?.Invoke(GameManager.GMALeaderBoard_4_Audio);
                    OnPlayCustomTimerSound?.Invoke(GameManager.GMALeaderBoard_4_Audio, GameManager.customVolume);
                    break;
                case 4:
                    clipLength = (float)GetClipLength?.Invoke(GameManager.GMALeaderBoard_5_Audio);
                    OnPlayCustomTimerSound?.Invoke(GameManager.GMALeaderBoard_5_Audio, GameManager.customVolume);
                    break;

            }
            return clipLength;
        }
        #endregion Custom Methods
    }
}

