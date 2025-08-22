using System.Collections.Generic;
using UISystem;
using UnityEngine;

namespace DivoPOC.GrabEmAll
{
    [System.Serializable]
    public class WaitingPoints
    {
        public Transform CenterPoints;
        public Transform LeftPoints;
        public Transform RightPoints;
    }
    public enum TutorialObjectType
    {
        None,
        OnlyPickedTest,
        CorrectObject,
        IncorrectObject
    }
    public enum GameTutorialState
    {
        None,
        GuideForPickedOnly,
        GuideToChooseCorrect,
        Complete
    }
    public enum GuidedUIType
    {
        None,
        PickedGuidedUI,
        ChooseCorrectUI
    }
    public class TutorialManager : MonoBehaviour
    {
        #region Variables
        [Header("<b><size=15><color=Green>Tutorial Object Holder")]
        [Space(6)]
        [SerializeField] private Transform TutorialObjectHolder;
        [Space(10)]
        [Header("<b><size=15><color=Green>Tutorial Track Points")]
        [Space(6)]
        [SerializeField] private TrackPoints CenterTrackPoint;
        [SerializeField] private TrackPoints LeftTrackPoint;
        [SerializeField] private TrackPoints RightTrackPoint;
        [SerializeField] private Vector3 finalTargetPositionOffset;
        [Space(10)]
        [Header("<b><size=15><color=Green>Tutorial Halt Holder")]
        [Space(6)]
        [SerializeField] private Transform TutorialHaltHolder;
        [Space(10)]
        [Header("<b><size=15><color=Green>Tutorial Waiting Point")]
        [Space(6)]
        [SerializeField] private WaitingPoints TutorialMidVisiblePoint;
        [SerializeField] private WaitingPoints TutorialNearPlayerVisiblePoint;
        [Space(10)]
        [Header("<b><size=15><color=Green>Tutorial Pooling Object")]
        [Space(6)]
        [SerializeField] private TutorialObjectController SinglePickedObject;
        [SerializeField] private TutorialObjectController RightTutorialObject;
        [SerializeField] private TutorialObjectController WrongTutorialObject;

        private GameTutorialState gameCurrentTutorialState;
        private static int currentTutorialObjectCount;
        private List<TutorialObjectController> currentTutorialObjects = new List<TutorialObjectController>();
        #endregion Variables

        #region Unity Methods

        void Start()
        {
            gameCurrentTutorialState = GameTutorialState.None;
            currentTutorialObjectCount = 0;
            ResetToHalt();
        }
        private void OnEnable()
        {
            ActionManager.OnGameTutorialStart += StartGameTutorial;
            ActionManager.OnResumeCurrentTutorial += ResumeCurrentTutorial;
            ActionManager.OnReachedWaitingPoint += TutorialObjectReachedWaitingPoint;
            ActionManager.OnCompleteOneTrip += CompleteCurrentTrip;
            ActionManager.OnChooseTutorialObject += ChooseTutorialObject;
            ActionManager.OnSkipTutorial += SkipTutorial;
        }
        private void OnDisable()
        {
            ActionManager.OnGameTutorialStart -= StartGameTutorial;
            ActionManager.OnResumeCurrentTutorial -= ResumeCurrentTutorial;
            ActionManager.OnReachedWaitingPoint -= TutorialObjectReachedWaitingPoint;
            ActionManager.OnCompleteOneTrip -= CompleteCurrentTrip;
            ActionManager.OnChooseTutorialObject -= ChooseTutorialObject;
            ActionManager.OnSkipTutorial -= SkipTutorial;
        }
        #endregion Unity Methods

        #region Custom Methods
        private void StartGameTutorial()
        {
            ActionManager.GamePointsHeightAdjustment?.Invoke(true);
            gameCurrentTutorialState = GameTutorialState.GuideForPickedOnly;
            StartOnlyPickedTutorial();
        }
        private void CompleteTutorial()
        {
            //Debug.LogError("Tutorial Complete");
            CenterTrackPoint.TargetPoints.parent.transform.position = finalTargetPositionOffset;
            ActionManager.OnReadySetGoCountDown?.Invoke();
            DestroyTutorial();
        }
        private void SkipTutorial()
        {
            CompleteTutorial();
        }
        private void DestroyTutorial()
        {
            Destroy(TutorialObjectHolder.gameObject);
            Destroy(this.gameObject);
        }
        private void SetTutorialToHalt(TutorialObjectController tutObject)
        {
            tutObject.transform.position = TutorialHaltHolder.position;
            tutObject.ResetTutorialObjectData();
            tutObject.gameObject.SetActive(false);
        }
        private void StartOnlyPickedTutorial()
        {
            currentTutorialObjectCount = 0;
            currentTutorialObjects.Clear();
            currentTutorialObjects.Add(SinglePickedObject);
            SinglePickedObject.gameObject.SetActive(true);
            SinglePickedObject.SetTutorialObjectData(TutorialObjectType.OnlyPickedTest);
            SinglePickedObject.SetWaitingPoints(TutorialMidVisiblePoint.CenterPoints);
            SinglePickedObject.Init(CenterTrackPoint, 4.5f);
        }
        private void StartChooseCorrectTutorial()
        {
            currentTutorialObjectCount = 0;
            currentTutorialObjects.Clear();
            currentTutorialObjects.Add(RightTutorialObject);
            currentTutorialObjects.Add(WrongTutorialObject);

            RightTutorialObject.gameObject.SetActive(true);
            WrongTutorialObject.gameObject.SetActive(true);

            RightTutorialObject.SetTutorialObjectData(TutorialObjectType.CorrectObject);
            WrongTutorialObject.SetTutorialObjectData(TutorialObjectType.IncorrectObject);

            RightTutorialObject.SetWaitingPoints(TutorialNearPlayerVisiblePoint.RightPoints);
            WrongTutorialObject.SetWaitingPoints(TutorialNearPlayerVisiblePoint.LeftPoints);

            RightTutorialObject.Init(RightTrackPoint, 4.5f);
            WrongTutorialObject.Init(LeftTrackPoint, 4.5f);
        }
        private void ResetToHalt()
        {
            SetTutorialToHalt(SinglePickedObject);
            SetTutorialToHalt(RightTutorialObject);
            SetTutorialToHalt(WrongTutorialObject);
        }
        private void TutorialObjectReachedWaitingPoint()
        {
            currentTutorialObjectCount++;
            if (currentTutorialObjectCount == currentTutorialObjects.Count)
            {
                switch (gameCurrentTutorialState)
                {
                    case GameTutorialState.GuideForPickedOnly:
                        DivoPOC.ActionManager.OnPlayCustomTimerSound?.Invoke(GameManager.GMAPickedGuided_Audio, GameManager.customVolume);
                        ShowTutorialPartUI(GuidedUIType.PickedGuidedUI);
                        break;
                    case GameTutorialState.GuideToChooseCorrect:
                        DivoPOC.ActionManager.OnPlayCustomTimerSound?.Invoke(GameManager.GMAChooseCorrectGuided_Audio, GameManager.customVolume);
                        ShowTutorialPartUI(GuidedUIType.ChooseCorrectUI);
                        break;
                }
            }
        }
        private void ShowTutorialPartUI(GuidedUIType uiScreen)
        {
            switch (uiScreen)
            {
                case GuidedUIType.PickedGuidedUI:
                    //Show How To Pick UI
                    ViewController.Instance.ChangeScreen(ScreenName.GMAPickedGuidedScreen);
                    break;
                case GuidedUIType.ChooseCorrectUI:
                    //Show UI To Choose Right
                    ViewController.Instance.ChangeScreen(ScreenName.GMAChooseCorrectGuidedScreen);
                    break;
            }
        }
        [ContextMenu("Resume ")]
        private void ResumeCurrentTutorial()
        {
            foreach (var tutObject in currentTutorialObjects)
            {
                tutObject.StartObjectMoving();
            }
        }
        private void ChooseTutorialObject(TutorialObjectType objectType)
        {
            switch (objectType)
            {
                case TutorialObjectType.OnlyPickedTest:
                    SinglePickedObject.ToggleCollider(false);
                    break;
                case TutorialObjectType.CorrectObject:
                case TutorialObjectType.IncorrectObject:
                    WrongTutorialObject.ToggleCollider(false);
                    RightTutorialObject.ToggleCollider(false);
                    break;
            }
        }
        private void CompleteCurrentTrip(bool isCompleteCurrentTutorial, TutorialObjectType objectType)
        {
            //Debug.LogError($"isCompleteCurrentTutorial {isCompleteCurrentTutorial}, || {gameCurrentTutorialState}");
            if (isCompleteCurrentTutorial)
            {
                switch (gameCurrentTutorialState)
                {
                    case GameTutorialState.GuideForPickedOnly:
                        SetCurrentTutorialObjectToHalt();
                        gameCurrentTutorialState = GameTutorialState.GuideToChooseCorrect;
                        StartChooseCorrectTutorial();
                        break;
                    case GameTutorialState.GuideToChooseCorrect:
                        SelectTutorialObject(objectType);
                        break;
                }
            }
            else
            {
                switch (gameCurrentTutorialState)
                {
                    case GameTutorialState.GuideForPickedOnly:
                        SetCurrentTutorialObjectToHalt();
                        StartOnlyPickedTutorial();
                        break;
                    case GameTutorialState.GuideToChooseCorrect:
                        SetCurrentTutorialObjectToHalt();
                        StartChooseCorrectTutorial();
                        break;
                }
            }
        }
        private void SetCurrentTutorialObjectToHalt()
        {
            foreach (var tutObject in currentTutorialObjects)
            {
                SetTutorialToHalt(tutObject);
            }
        }
        private void SelectTutorialObject(TutorialObjectType objectType)
        {
            switch (objectType)
            {
                case TutorialObjectType.OnlyPickedTest:
                    break;
                case TutorialObjectType.CorrectObject:
                    CompleteTutorial();
                    break;
                case TutorialObjectType.IncorrectObject:
                    break;

            }
        }
        #endregion Custom Methods

    }
}

