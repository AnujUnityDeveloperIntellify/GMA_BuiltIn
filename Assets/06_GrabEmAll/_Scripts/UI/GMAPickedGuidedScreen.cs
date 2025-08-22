using UISystem;
using UnityEngine;
using UnityEngine.UI;

namespace DivoPOC.GrabEmAll
{
    public class GMAPickedGuidedScreen : UISystem.Screen
    {
        [SerializeField] private Button continiueBtn;
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
            continiueBtn.onClick.AddListener(ContiniueTutorial);
            skipBtn.onClick.AddListener(SkipTutorial);
        }
        [ContextMenu("Continue")]
        private void ContiniueTutorial()
        {
            ViewController.Instance.HideScreen(ScreenName.GMAPickedGuidedScreen);
            ActionManager.OnResumeCurrentTutorial?.Invoke();
        }
        [ContextMenu("Skip")]
        private void SkipTutorial()
        {
            ViewController.Instance.HideScreen(ScreenName.GMAPickedGuidedScreen);
            ActionManager.OnSkipTutorial?.Invoke();
        }

        #endregion Custom Methods
    }
}

