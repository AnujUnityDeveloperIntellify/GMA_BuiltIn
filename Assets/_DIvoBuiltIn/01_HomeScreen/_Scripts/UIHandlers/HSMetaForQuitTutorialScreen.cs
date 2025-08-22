using UISystem;

namespace DivoPOC.HomeScreen
{
    public class HSMetaForQuitTutorialScreen : Screen
    {
        public override void Awake()
        {
            base.Awake();
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

        public void CloseScreen()
        {
            ActionManager.OnPlayCustomSound?.Invoke("ButtonClick", 1f);
            ViewController.Instance.HideScreen(ScreenName.HSMetaForQuitTutorialScreen);
            ViewController.Instance.ChangeScreen(ScreenName.HSGameSecetionScreen);
        }
        public void StartScreen()
        {
            ActionManager.OnPlayCustomSound?.Invoke("ButtonClick", 1f);
            ViewController.Instance.HideScreen(ScreenName.HSMetaForQuitTutorialScreen);
            ViewController.Instance.ChangeScreen(ScreenName.HSGameSecetionScreen);
        }
        public void PreviousScreen()
        {
            ActionManager.OnPlayCustomSound?.Invoke("ButtonClick", 1f);
            ViewController.Instance.ChangeScreen(ScreenName.HSExitATutorialScreen);
        }
    }
}
