using System;
using System.Collections;
using UISystem;
using UnityEngine;
using static DivoPOC.ActionManager;
using static OVRPlugin;

namespace DivoPOC.GrabEmAll
{
    #region Public Enums
    internal enum HintStatus
    {
        None,Active,Inactive
    }
    internal enum GrabEmAllStatus
    {
        None,
        Start,
        GameComplete,
        IsPlaying,
        IsPause,
        GameOver
    }

    #endregion Public Enums

    public class GameManager : MonoBehaviour
    {
        #region Variables
        [SerializeField] private GameObject ParentPoolingObject;
        internal static GrabEmAllStatus gameStatus;
        internal static HintStatus hintStatus;
        private const float timerInMinutes = 5f;
        internal const float yearLapsDuration = 30f;
        internal const float backgroundVolume = 0.25f; 
        internal const float customVolume = 1.0f;
        internal static float currentTimeInSecondLeft = 0;
        private Coroutine gameTimmer;
        private int currentPlayerScore;
        private int currentPlayerLives = 3;
        private static bool isTutorialHappened = false;

        #region Game Tags
        internal static string achievementTag = "Team Achievements";
        internal static string playerHandTag = "Player Hand";
        #endregion Game Tags

        #region Audio Keys

        internal const string backgroundAudio = "BackgroundMusic";
        internal const string grabCorrectAchievementAudio = "GrabCorrect";
        internal const string GMAWelcomeAudio = "Welcome";
        internal const string GMALeaderBoard_1_Audio = "LeaderBoardOne";
        internal const string GMALeaderBoard_2_Audio = "LeaderBoardTwo";
        internal const string GMALeaderBoard_3_Audio = "LeaderBoardThree";
        internal const string GMALeaderBoard_4_Audio = "LeaderBoardFour";
        internal const string GMALeaderBoard_5_Audio = "LeaderBoardFive";
        internal const string GMATutorialStart_Audio = "TutorialStart";
        internal const string GMAPickedGuided_Audio = "PickedGuided";
        internal const string GMAChooseCorrectGuided_Audio = "ChooseCorrect";
        internal const string GMAGameComplete_Audio = "GameComplete";
        internal const string GMAGameLost_Audio = "GameLost";
        internal const string GMAReady_Audio = "Ready";
        internal const string GMASet_Audio = "Set";
        internal const string GMAGo_Audio = "Go";
        internal const string GMALifeLost_Audio = "LifeLost";
        internal const string GMAOneLifeLeft_Audio = "OneLifeLeft";
        internal const string GMACollectPowerUp_Audio = "CollectPowerUp";
        internal const string GMANextYearAnimation_Audio = "NextYearAnimation";
        internal const string GMAIdealTimmer_VocalAudio = "IdealTimmerVocalAudio";
        internal const string GMAIdealTimmer_BeepAudio = "IdealTimmerBeepAudio";
        internal const string GMAWaitForHint_Audio = "WaitForHint";

        #endregion Audio Keys

        #endregion Variables

        #region Unity Methods

        private void Awake()
        {
            Init();
        }
        void Start()
        {

        }
        private void OnEnable()
        {
            gameStatus = GrabEmAllStatus.None;
            ActionManager.OnGameStart += GameStart;
            ActionManager.OnGamePause += GamePause;
            ActionManager.OnGameResume += GameResume;
            ActionManager.OnPlayerScored += UpdatePlayerScore;
            ActionManager.GetPlayerScores += GetPlayerScore;
            ActionManager.OnUpdatePlayerLives += UpdatePlayerLives;
            ActionManager.GetPlayerLives += GetPlayerLives;
            ActionManager.OnGameOver += GameOver;
            ActionManager.OnGameRestart += GameReset;
            ActionManager.OnSetTutorialPF += SetTutorialPF;
            ActionManager.GetTutorialPF += GetTutorialPF;
        }
        private void OnDisable()
        {
            ActionManager.OnGameStart -= GameStart;
            ActionManager.OnGamePause -= GamePause;
            ActionManager.OnGameResume -= GameResume;
            ActionManager.OnPlayerScored -= UpdatePlayerScore;
            ActionManager.GetPlayerScores -= GetPlayerScore;
            ActionManager.OnUpdatePlayerLives -= UpdatePlayerLives;
            ActionManager.GetPlayerLives -= GetPlayerLives;
            ActionManager.OnGameOver -= GameOver;
            ActionManager.OnGameRestart -= GameReset;
            ActionManager.OnSetTutorialPF -= SetTutorialPF;
            ActionManager.GetTutorialPF -= GetTutorialPF;
        }
        private bool isAPressed = false;
        private bool isXPressed = false;
        private void Update()
        {
            // Check right-hand primary (A)
            bool aButtonDown = IsAButtonPressed();
            if (aButtonDown && !isAPressed)
            {
                isAPressed = true;
                OnPressedAButton();
            }
            else if (!aButtonDown && isAPressed)
            {
                // reset flag when released
                isAPressed = false;
            }

            // Check left-hand primary (X)
            bool xButtonDown = IsXButtonPressed();
            if (xButtonDown && !isXPressed)
            {
                isXPressed = true;
                OnPressedXButton();
            }
            else if (!xButtonDown && isXPressed)
            {
                isXPressed = false;
            }
        }
        private bool IsAButtonPressed()
        {
            var rightHand = UnityEngine.XR.InputDevices.GetDeviceAtXRNode(UnityEngine.XR.XRNode.RightHand);
            return rightHand.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryButton, out bool value) && value;
        }

        private bool IsXButtonPressed()
        {
            var leftHand = UnityEngine.XR.InputDevices.GetDeviceAtXRNode(UnityEngine.XR.XRNode.LeftHand);
            return leftHand.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryButton, out bool value) && value;
        }
        #endregion Unity Methods

        #region Tutorial PF
        private void SetTutorialPF(bool value)
        {
            isTutorialHappened = value;
        }
        private bool GetTutorialPF()
        {
            return isTutorialHappened;
        }
        
        #endregion Tutorial PF

        #region Custom Methods
        private void Init()
        {
            currentPlayerLives = 3;
            Application.targetFrameRate = 72;
            currentPlayerScore = 0;
            hintStatus = HintStatus.None;
            OnPlayCustomTimerSound?.Invoke(backgroundAudio, backgroundVolume);
        }
        private void GameStart()
        {
            ParentPoolingObject.SetActive(true);
            ActionManager.GamePointsHeightAdjustment?.Invoke(false);
            StartGameTimmer();
            ActionManager.OnOpenLeaderBoard?.Invoke();
            ActionManager.SetPlayerScoreTxt?.Invoke(currentPlayerScore.ToString());
        }
        private void GameComplete()
        {
            //Debug.LogError(" Game Complete" );
            OnPlayCustomTimerSound?.Invoke(GameManager.GMAGameComplete_Audio, GameManager.customVolume);
            ActionManager.SetScoreOnGameComplete?.Invoke(currentPlayerScore.ToString());
            ActionManager.OnClosedLeaderBoard?.Invoke();
            ViewController.Instance.ChangeScreen(ScreenName.GMAGameCompleteScreen);
        }    
        private void GameOver()
        {
            ActionManager.OnStopRender3DAnimations?.Invoke();
            ParentPoolingObject.SetActive(false);
            gameStatus = GrabEmAllStatus.GameOver;
            ActionManager.OnClosedLeaderBoard?.Invoke();
            StopCoroutine(gameTimmer);
            ActionManager.OnDiscardCurrentYearAchievements?.Invoke();
            OnPlayCustomTimerSound?.Invoke(GameManager.GMAGameLost_Audio, GameManager.customVolume);
            ViewController.Instance.ChangeScreen(ScreenName.GMAGameOverScreen);
        }
        
        #region Game Timmer
        private void StartGameTimmer()
        {
            gameStatus = GrabEmAllStatus.IsPlaying;
            gameTimmer = StartCoroutine(CountdownCoroutine(timerInMinutes * 60));
        }
        private IEnumerator CountdownCoroutine(float totalSeconds)
        {
            GetNextYearData();
            ActionManager.SetCurrentYearTxt?.Invoke(ActionManager.GetCurrenYear?.Invoke().ToString());
            SetCountDownTime("");
            string currentCountDownTime = "";
            float perYearDuration = 0f;
            while (totalSeconds > 0)
            {
                if (gameStatus == GrabEmAllStatus.IsPlaying)
                {
                    totalSeconds -= 1f;
                    currentTimeInSecondLeft = totalSeconds;
                    perYearDuration += 1;
                    ActionManager.OnUpdateIdealTimmer?.Invoke();
                    int minutes = Mathf.FloorToInt(totalSeconds / 60f);
                    int seconds = Mathf.FloorToInt(totalSeconds % 60f);
                    currentCountDownTime = $"{minutes:D2} : {seconds:D2}";
                    SetCountDownTime(currentCountDownTime);
                    if (perYearDuration >= yearLapsDuration)
                    {
                        perYearDuration = 0f;
                        GetNextYearData();
                        if (ActionManager.GetCurrenYear?.Invoke() != ActionManager.GetLastYear?.Invoke())
                        {
                            ActionManager.OnResetHintBtn?.Invoke(HintStatus.Inactive);
                            ActionManager.StartNewYearStartAnimation?.Invoke(0.25f, 0.8f);
                        }
                    }
                }
                yield return new WaitForSeconds(1f);
            }
            gameStatus = GrabEmAllStatus.GameComplete;
            GameComplete();
        }
        private void SetCountDownTime(string value)
        {
            ActionManager.SetGameTimmerTxt?.Invoke(value);
        }
        #endregion Game Timmer

        #region User Inputs

        private void OnPressedAButton()
        {
            if (gameStatus == GrabEmAllStatus.IsPlaying)
            {
                ViewController.Instance.ChangeScreen(ScreenName.GMAQuitScreen);
                ActionManager.OnGamePause?.Invoke();
            }
        }
        private void OnPressedXButton()
        {
            if (gameStatus == GrabEmAllStatus.IsPlaying)
            {
                if(hintStatus == HintStatus.Active)
                {
                    ActionManager.OnResetHintBtn?.Invoke(HintStatus.Inactive);
                    ActionManager.LoadandOpenHintUI?.Invoke();
                }
                else
                {
                    OnPlayCustomTimerSound?.Invoke(GMAWaitForHint_Audio, customVolume);
                }
            }
            
        }
        #endregion User Inputs

        #region Game Pause & Play
        private void GamePause()
        {
            //Pause the Time and Game 
            gameStatus = GrabEmAllStatus.IsPause;
            Time.timeScale = 0f;
            ActionManager.OnStopRendererTrackAchievements?.Invoke();
            ActionManager.OnStopRender3DAnimations?.Invoke();
        }
        private void GameResume()
        {
            if (gameStatus == GrabEmAllStatus.IsPause)
            {
                //Start the Time and Game
                gameStatus = GrabEmAllStatus.IsPlaying;
                Time.timeScale = 1f;
                ActionManager.OnResumeRendererTrackAchievements?.Invoke();
                ActionManager.OnResumeRender3DAnimations?.Invoke();
            }
        }
        #endregion Game Pause & Play

        #region Application Pause & Resume
#if !UNITY_EDITOR

        void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
            {
                ActionManager.OnSetTutorialPF?.Invoke(false);
            }
            else
            {
                ActionManager.OnSetTutorialPF?.Invoke(true);
            }
        }


#endif

        #endregion Application Pause & Resume

        #region Game Year logic
        private void GetNextYearData()
        {
            //ActionManager.OnResetIdealTimmer?.Invoke();
            ActionManager.LoadNewYearData?.Invoke();
        }
        #endregion Game Year logic

        #region Game Player Points
        private void UpdatePlayerScore()
        { currentPlayerScore++; }
        private int GetPlayerScore()
        { return currentPlayerScore; }

        #endregion Game Player Points

        #region Game Player Lives
        private void UpdatePlayerLives(int value)
        {
            currentPlayerLives += value;
            if (currentPlayerLives == 0)
            {
                //Show Game Over UI
                ActionManager.OnGameOver?.Invoke();
            }
            else
            {
                if(value<0 && currentPlayerLives == 1)
                {
                    OnPlayCustomTimerSound?.Invoke(GameManager.GMAOneLifeLeft_Audio, GameManager.customVolume);
                }
                ActionManager.OnUpdatePLayerLiveUI?.Invoke(value);
            }
        }
        private int GetPlayerLives()
        { return currentPlayerLives; }

        #endregion Game Player Lives

        #region  Game Reset

        private void GameReset()
        {
            gameTimmer = null;
            currentPlayerLives = 3;
            currentPlayerScore = 0;
            ActionManager.OnResetIdealTimmer?.Invoke();
            ActionManager.OnResetLeaderBoard?.Invoke();
            ActionManager.OnResetHintBtn?.Invoke(HintStatus.None);
            OnPlayCustomTimerSound?.Invoke(backgroundAudio, backgroundVolume);
            ActionManager.OnResetYearManager?.Invoke();
            ActionManager.OnResetSpwanManager?.Invoke();
            ActionManager.OnReadySetGoCountDown?.Invoke();
        }
        #endregion Game Reset

        #endregion Custom Methods

    }
}

