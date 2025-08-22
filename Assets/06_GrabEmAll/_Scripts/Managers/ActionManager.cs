using System;
using System.Collections.Generic;
using UnityEngine;

namespace DivoPOC.GrabEmAll
{
    public class ActionManager : MonoBehaviour
    {
        internal static Action OnGameStart;
        internal static Action OnGameOver;
        internal static Action OnGameRestart;
        internal static Action OnReadySetGoCountDown;

        internal static Action OnGamePause;
        internal static Action OnGameResume;

        internal static Action OnResetLeaderBoard;
        internal static Action OnOpenLeaderBoard;
        internal static Action OnClosedLeaderBoard;
        internal static Action<string> SetPlayerScoreTxt;
        internal static Action<string> SetScoreOnGameComplete;
        internal static Action<string> SetCurrentYearTxt;
        internal static Action<string> SetGameTimmerTxt;
       
        internal static Action<AchievementsController> SetAchievementsAtHalt;
        internal static Action<Queue<AchievementsController>, int> SetCurrentYearAchievements;
        internal static Action<Vector3> OnInitializePointsTxt;
        internal static Action OnStopRendererTrackAchievements;
        internal static Action OnResumeRendererTrackAchievements;
        internal static Action OnStopRender3DAnimations;
        internal static Action OnResumeRender3DAnimations;
        internal static Action<AchievementsController> OnRemovedTrackedAchievement;
        internal static Action LoadNewYearData;
        internal static Action<float, float, float> OnPerformUIHaptics;
        internal static Action OnPlayerScored;
        internal static Action<int> OnUpdatePLayerLiveUI;
        internal static Action OnDiscardCurrentYearAchievements;
        internal static Action<int> OnUpdatePlayerLives;
        internal static Func<int> GetPlayerLives;
        internal static Func<int> GetPlayerScores;
        internal static Action<List<HintData>> OnLoadNewYearHints;
        internal static Action<HintStatus> OnResetHintBtn;
        internal static Action LoadandOpenHintUI;
        internal static Action OnGameTutorialStart;
        internal static Action OnResumeCurrentTutorial;
        internal static Action OnReachedWaitingPoint;
        internal static Action<TutorialObjectType> OnChooseTutorialObject;
        internal static Action<bool, TutorialObjectType> OnCompleteOneTrip;
        internal static Action OnSkipTutorial;
        internal static Action OnStartDescriptionUIAnimation;
        internal static Action<float, float> StartNewYearStartAnimation;
        internal static Func<int> GetLastYear;
        internal static Func<int> GetCurrenYear;
        internal static Action UpdateCurrentYear;
        internal static Action<bool> GamePointsHeightAdjustment;
        internal static Action OnResetIdealTimmer;
        internal static Action OnUpdateIdealTimmer;

        internal static Action OnResetYearManager;
        internal static Action OnResetSpwanManager;

        internal static Action<bool> OnSetTutorialPF;
        internal static Func<bool> GetTutorialPF;
        #region Pool Management Action
        //Not Used 
        internal static Action<AchievementsController> OnEnqueueAchievements;
        internal static Func<Achievements, AchievementsController> OnDequeueAchievements;

       #endregion Pool Management Action
    }
}

