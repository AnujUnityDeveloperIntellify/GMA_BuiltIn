using UISystem;

namespace DivoPOC.HomeScreen
{
    public class HSRightThumbTutorialScreen : Screen
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
            ViewController.Instance.HideScreen(ScreenName.HSRightThumbTutorialScreen);
            ViewController.Instance.ChangeScreen(ScreenName.HSGameSecetionScreen);
        }
        public void NextScreen()
        {
            ActionManager.OnPlayCustomSound?.Invoke("ButtonClick", 1f);
            ViewController.Instance.ChangeScreen(ScreenName.HSExitATutorialScreen);
        }
        public void PreviousScreen()
        {
            ActionManager.OnPlayCustomSound?.Invoke("ButtonClick", 1f);
            ViewController.Instance.ChangeScreen(ScreenName.HSLeftThumbTutorialScreen);
        }
    }
}