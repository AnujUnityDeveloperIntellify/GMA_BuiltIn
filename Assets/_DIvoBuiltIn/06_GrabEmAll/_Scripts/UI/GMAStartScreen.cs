using UISystem;
using UnityEngine;
using UnityEngine.UI;

namespace DivoPOC.GrabEmAll
{
    public class GMAStartScreen : UISystem.Screen
    {
        [SerializeField] private Button startBtn;
        [SerializeField] private Button backBtn;
        [SerializeField] private Button skipBtn;
        public override void Awake()
        {
            base.Awake();
            Init();
        }

        public override void Show()
        {
            meshObject.SetActive(true);
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
        #region Custom Methods
        private void Init()
        {
            backBtn.onClick.AddListener(BackScreen);
            startBtn.onClick.AddListener(StartGame);
            skipBtn.onClick.AddListener(SkipTutorial);
        }
        [ContextMenu("Back")]
        private void BackScreen()
        {
            ViewController.Instance.ChangeScreen(ScreenName.GMALeaderBoardGuidedScreen);
            DivoPOC.ActionManager.OnPlayCustomTimerSound?.Invoke(GameManager.GMALeaderBoard_1_Audio, GameManager.customVolume);
        }
        [ContextMenu("Start")]
        private void StartGame()
        {
            ViewController.Instance.HideScreen(ScreenName.GMAGameStartScreen);
            ActionManager.OnGameTutorialStart?.Invoke();
        }
        [ContextMenu("Skip")]
        private void SkipTutorial()
        {
            ViewController.Instance.HideScreen(ScreenName.GMAGameStartScreen);
            ActionManager.OnSkipTutorial?.Invoke();
        }
        #endregion Custom Methods
    }
}

