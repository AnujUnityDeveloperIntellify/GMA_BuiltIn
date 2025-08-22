using TMPro;
using UISystem;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace DivoPOC.GrabEmAll
{
    public class GMAGameCompleteScreen : UISystem.Screen
    {
        [SerializeField] private Button lobbyBtn;
        [SerializeField] private Button RestartBtn;
        [SerializeField] private TextMeshProUGUI EarnCoinTxt;
        public override void Awake()
        {
            base.Awake();
            Init();
        }
        private void OnEnable()
        {
            ActionManager.SetScoreOnGameComplete += SetScoreOnGameComplete;
        }
        private void OnDisable()
        {
            ActionManager.SetScoreOnGameComplete -= SetScoreOnGameComplete;
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
            lobbyBtn.onClick.AddListener(BackToLobby);
            RestartBtn.onClick.AddListener(RestartGame);
        }
        [ContextMenu("Home Screen")]
        private void BackToLobby()
        {
            //Application.Quit();
            ActionManager.OnSetTutorialPF?.Invoke(false);
            SceneManager.LoadScene(0);
        }
        private void RestartGame()
        {
            /*ViewController.Instance.HideScreen(ScreenName.GMAGameCompleteScreen);
            ActionManager.OnGameRestart?.Invoke();*/

            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);
        }
        private void SetScoreOnGameComplete(string score)
        {
            string textData = $"YOU’VE COMPLETED THE JOURNEY BY COLLECTING <color=Yellow>{score}</color> POINTS";
            EarnCoinTxt.text = textData;
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

