
using System.Collections;
using UISystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DivoPOC.HomeScreen
{
    public class HSGameSelectionScreen : UISystem.Screen
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
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
        public void WoakAGoalButtonClick()
        {
            ActionManager.OnPlayCustomSound?.Invoke("ButtonClick", 1f);
            SceneManager.LoadScene(1, LoadSceneMode.Single);
            ShowLoading(1);
        }
        public void StadiumUpgradeButtonClick()
        {
            ActionManager.OnPlayCustomSound?.Invoke("ButtonClick", 1f);
            //SceneManager.LoadScene(2, LoadSceneMode.Single);
            ShowLoading(2);
        }
        public void OrganazingTheClubButtonClick()
        {
            ActionManager.OnPlayCustomSound?.Invoke("ButtonClick", 1f);
            //SceneManager.LoadScene(3, LoadSceneMode.Single);
            ShowLoading(3);
        }
        public void GrabEmAllButtonClick()
        {
            ActionManager.OnPlayCustomSound?.Invoke("ButtonClick", 1f);
            //SceneManager.LoadScene(3, LoadSceneMode.Single);
            ShowLoading(4);
        }

        public void HoverScreenEnable(UnityEngine.GameObject gameObject)
        {
            gameObject.SetActive(true);
            //ActionManagerNew.OnTriggerHapticFeedback?.Invoke(.1f, .1f);
            ActionManager.OnPlayCustomSound?.Invoke("ButtonHover", 1f);
        }
        public void HoverScreenDisable(UnityEngine.GameObject gameObject)
        {
            gameObject.SetActive(false);
        }

        void ShowLoading(int index)
        {
            StartCoroutine(LoadSceneAsyncWithUI(index));
        }
        private IEnumerator LoadSceneAsyncWithUI(int sceneIndex)
        {
            ViewController.Instance.ChangeScreen(ScreenName.HSLoadingScreen);
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
            while (!operation.isDone)
            {
                float progress = Mathf.Clamp01(operation.progress / 0.95f);
                yield return null;
            }
        }
    }
}
