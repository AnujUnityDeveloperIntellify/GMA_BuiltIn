using UISystem;
using UnityEngine;
using UnityEngine.UI;

namespace DivoPOC.GrabEmAll
{
    public class GMAWelcomeScreen : UISystem.Screen
    {
        [SerializeField] private Button nextbtn;
        public override void Awake()
        {
            base.Awake();
            Init();
        }

        public override void Show()
        {
            if(!ActionManager.GetTutorialPF.Invoke())
            {
                ActionManager.OnSetTutorialPF?.Invoke(true);
                meshObject.SetActive(true);
                base.Show();
            }
            else
            {
                Hide();
                ActionManager.OnReadySetGoCountDown?.Invoke();
            }
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
            DivoPOC.ActionManager.OnPlayCustomTimerSound(GameManager.GMAWelcomeAudio, GameManager.customVolume);
        }
        #region Custom Methods
        private void Init()
        {
            nextbtn.onClick.AddListener(Next);
        }
        [ContextMenu("Next")]
        private void Next()
        {
            ViewController.Instance.ChangeScreen(ScreenName.GMALeaderBoardGuidedScreen);
            DivoPOC.ActionManager.OnPlayCustomTimerSound?.Invoke(GameManager.GMALeaderBoard_1_Audio, GameManager.customVolume);
        }
        #endregion Custom Methods
    }
}

