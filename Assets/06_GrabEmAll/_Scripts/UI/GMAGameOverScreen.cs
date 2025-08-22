using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UISystem;

namespace DivoPOC.GrabEmAll
{
    public class GMAGameOverScreen : UISystem.Screen
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
            
        }
        private void OnDisable()
        {
           
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
        [ContextMenu("Restart Game")]
        private void RestartGame()
        {
            /*ViewController.Instance.HideScreen(ScreenName.GMAGameOverScreen);
            ActionManager.OnGameRestart?.Invoke();*/

            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);
        }
        private void SetScoreOnGameComplete(string score)
        {
            string textData = $"Game Over You have Earn  <color=Yellow>{score}</color> POINTS";
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

