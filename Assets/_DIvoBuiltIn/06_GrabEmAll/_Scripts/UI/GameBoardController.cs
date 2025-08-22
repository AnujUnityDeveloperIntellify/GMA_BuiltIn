using System.Collections.Generic;
using TMPro;
using UISystem;
using UnityEngine;
using UnityEngine.UI;

namespace DivoPOC.GrabEmAll
{
    public class GameBoardController : MonoBehaviour
    {
        #region Variables
        [Header("<b><size=15><color=Green>Board Canvas")]
        [Space(5)]
        [SerializeField] private List<CanvasGroup> leaderBordCanvas;
        [Space(8)]
        [Header("<b><size=15><color=Green>Board Fields")]
        [Space(5)]
        [SerializeField] private TextMeshProUGUI gameTimmerTxt;
        [SerializeField] private TextMeshProUGUI gameCurrentYearTxt;
        [SerializeField] private TextMeshProUGUI playerScoreTxt;
        [Space(8)]
        [Header("<b><size=15><color=Green>Player Lives")]
        [Space(5)]
        [SerializeField] private List<Image> PlayerLivesHeart;
        [Space(8)]
        [Header("<b><size=15><color=Green>Hint Object")]
        [Space(5)]
        [SerializeField] private GameObject hintObject;
        private Stack<Image> aliveHeart = new Stack<Image>();
        private Stack<Image> deadHeart = new Stack<Image>();
        private ITweenManager _tweenManager;
        #endregion Variables

        #region Unity Methods

        void Start()
        {
            
        }
        private void Awake()
        {
            Init();
        }
        private void OnEnable()
        {
            ActionManager.SetCurrentYearTxt += SetCurrentYearTxt;
            ActionManager.SetGameTimmerTxt += SetGameTimmerTxt;
            ActionManager.SetPlayerScoreTxt += SetPlayerScoreTxt;
            ActionManager.OnOpenLeaderBoard += OpenLeaderBoard;
            ActionManager.OnClosedLeaderBoard += ClosedLeaderBoard;
            ActionManager.OnUpdatePLayerLiveUI += UpdateLiveUI;
            ActionManager.OnResetHintBtn += ToggelHintStatus;
            ActionManager.OnResetLeaderBoard += ResetGameBoard;
        }
        private void OnDisable()
        {
            ActionManager.SetCurrentYearTxt -= SetCurrentYearTxt;
            ActionManager.SetGameTimmerTxt -= SetGameTimmerTxt;
            ActionManager.SetPlayerScoreTxt -= SetPlayerScoreTxt;
            ActionManager.OnOpenLeaderBoard -= OpenLeaderBoard;
            ActionManager.OnClosedLeaderBoard -= ClosedLeaderBoard;
            ActionManager.OnUpdatePLayerLiveUI -= UpdateLiveUI;
            ActionManager.OnResetHintBtn -= ToggelHintStatus;
            ActionManager.OnResetLeaderBoard -= ResetGameBoard;
        }
        #endregion Unity Methods

        #region Custom Methods
        private void SetCurrentYearTxt(string currentYear)
        {
            gameCurrentYearTxt.text = currentYear;
        }
        private void SetPlayerScoreTxt(string playerScore)
        {
            playerScoreTxt.text = playerScore;
        }
        private void SetGameTimmerTxt(string gameTimmer)
        {
            gameTimmerTxt.text = gameTimmer;
        }
        private void OpenLeaderBoard()
        {
            foreach( var canvas in leaderBordCanvas)
            {
                canvas.gameObject.SetActive(true);
                TweenCanvasGroupAlpha(canvas, 0f, 1f, 0.6f);
            }
        }
        private void ClosedLeaderBoard()
        {
            foreach (var canvas in leaderBordCanvas)
            {
                canvas.gameObject.SetActive(false);
                TweenCanvasGroupAlpha(canvas, 1f, 0f, 0.6f);
            }
        }
        private void Init()
        {
            _tweenManager = new TweenManager();
            ClosedLeaderBoard();
            aliveHeart.Clear();
            deadHeart.Clear();
            foreach( var heart in PlayerLivesHeart)
            {
                aliveHeart.Push(heart);
            }
        }

        #region Player Lives
        private void UpdateLiveUI(int value)
        {
            var fromStack = value > 0 ? deadHeart : aliveHeart;
            var toStack = value > 0 ? aliveHeart : deadHeart;
            var startFill = value > 0 ? 0f : 1f;
            var endFill = value > 0 ? 1f : 0f;

            Image live = fromStack.Pop();
            if (live != null)
            {
                HeartFilling(live, startFill, endFill, 1f, LeanTweenType.linear);
                toStack.Push(live);
            }
        }
        private void HeartFilling(Image img, float startFill, float endFill, float duration, LeanTweenType easeType)
        {
            if (img == null) return;
            img.fillAmount = startFill;
            LeanTween.value(img.gameObject, startFill, endFill, duration)
                .setEase(easeType)
                .setOnUpdate((float val) =>
                {
                    img.fillAmount = val;
                });
        }
        private void TweenCanvasGroupAlpha(CanvasGroup canvasGroup, float startAlpha, float endAlpha, float duration)
        {
            canvasGroup.alpha = startAlpha;
            LeanTween.value(canvasGroup.gameObject, startAlpha, endAlpha, duration)
                .setOnUpdate((float val) => {
                    canvasGroup.alpha = val;
                })
                .setEase(LeanTweenType.easeInOutSine);
        }

        #endregion Player Lives

        #region Reset Game Board

        private void ResetGameBoard()
        {
            SetCurrentYearTxt("");
            SetGameTimmerTxt("");
            SetPlayerScoreTxt("");
            aliveHeart.Clear();
            deadHeart.Clear();
            foreach (var heart in PlayerLivesHeart)
            {
                heart.fillAmount = 1;
                aliveHeart.Push(heart);
            }
        }

        #endregion Reset Game Board

        #region Hint Booster

        private void ToggelHintStatus(HintStatus _status)
        {
            switch (_status) 
            {
                case HintStatus.Active:
                    GameManager.hintStatus = _status;
                    if (!hintObject.activeInHierarchy)
                    {
                        _tweenManager.ScaleObject(hintObject, Vector3.zero, Vector3.one, 1f, true, () =>
                        {
                            hintObject.SetActive(true);
                        }, null);
                    }
                    break;

                case HintStatus.Inactive:
                    GameManager.hintStatus = _status;
                    if(hintObject.activeInHierarchy)
                    {
                        hintObject.SetActive(false);
                    }
                    break;

                case HintStatus.None:
                    GameManager.hintStatus = _status;
                    hintObject.transform.localScale = Vector3.zero;  
                    hintObject.SetActive(false);
                    break;
            }
        }
        #endregion Hint Booster

        #endregion Custom Methods
    }
}

