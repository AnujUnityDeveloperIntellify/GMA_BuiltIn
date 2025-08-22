using UISystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DivoPOC.GrabEmAll
{
    public class GMAQuitScreen : UISystem.Screen
    {
        [SerializeField] private Button yes;
        [SerializeField] private Button no;
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

        private void Init()
        {
            yes.onClick.AddListener(ClickYes);
            no.onClick.AddListener(ClickNo);
        }
        [ContextMenu("YES")]
        private void ClickYes()
        {
            //Application.Quit();
            Time.timeScale = 1f;
            ActionManager.OnSetTutorialPF?.Invoke(false);
            SceneManager.LoadScene(0);
        }
        [ContextMenu("NO")]
        private void ClickNo()
        {
            ViewController.Instance.HideScreen(ScreenName.GMAQuitScreen);
            ActionManager.OnGameResume?.Invoke();
        }


        public void HoverScreenEnable(UnityEngine.GameObject gameObject)
        {
            DivoPOC.ActionManager.OnPlayCustomSound?.Invoke("ButtonHover", 1f);
            ActionManager.OnPerformUIHaptics?.Invoke(0.1f, 0.1f, 0.1f);
            gameObject.SetActive(true);
        }
        public void HoverScreenDisable(UnityEngine.GameObject gameObject)
        {
            gameObject.SetActive(false);
        }
    }
}
