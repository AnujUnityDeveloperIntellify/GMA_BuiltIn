using TMPro;
using UnityEngine;
using System;
using static DivoPOC.ActionManager;

namespace DivoPOC.GrabEmAll
{
    public class GameAnimationManager : MonoBehaviour
    {
        #region Variables
        [Header("<b><size=15><color=Green>Ready Set Go Text")]
        [Space(6)]
        [SerializeField] private TextMeshPro ReadySetGOTxt;
        [Space(10)]
        [Header("<b><size=15><color=Green>Earn Points Text")]
        [Space(6)]
        [SerializeField] private Transform earnPointsTxt;
        [SerializeField] private Transform scoreDisplayPoint;
        private Vector3 earnPointsTxtStartingScale;
        [Space(10)]
        [Header("<b><size=15><color=Green>New Year Animation Contents")]
        [Space(6)]
        [SerializeField] private TextMeshPro nextYearTxt;
        [SerializeField] private Transform yearFrontPoint;
        [SerializeField] private Transform yearDisplayPoint;
        private Vector3 yearTxtOpenScale = new Vector3(0.7f, 0.7f, 0.7f);
        private Vector3 yearTxtHideScale = new Vector3(0.25f, 0.25f, 0.25f);
        [Space(10)]
        [Header("<b><size=15><color=Green>3D Animated Meshes")]
        [Space(6)]
        [SerializeField] private MeshRenderer[] _3DAnimationsRenderers;
        float moveScaleDuration;
        private ITweenManager _tweenManager;

        #endregion Variables

        #region Unity Methods

        private void Awake()
        {
            _tweenManager = new TweenManager();
            earnPointsTxtStartingScale = earnPointsTxt.localScale;
        }
        void Start()
        {

        }
        private void OnEnable()
        {
            ActionManager.OnReadySetGoCountDown += ReadySetGO_UI;
            ActionManager.OnInitializePointsTxt += InitializedEarnCoinTxt;
            ActionManager.StartNewYearStartAnimation += ShowNewYear;
            ActionManager.OnStopRender3DAnimations += Pause3DAnimation;
            ActionManager.OnResumeRender3DAnimations += Resume3DAnimations;
        }
        private void OnDisable()
        {
            ActionManager.OnReadySetGoCountDown -= ReadySetGO_UI;
            ActionManager.OnInitializePointsTxt -= InitializedEarnCoinTxt;
            ActionManager.StartNewYearStartAnimation -= ShowNewYear;
            ActionManager.OnStopRender3DAnimations -= Pause3DAnimation;
            ActionManager.OnResumeRender3DAnimations -= Resume3DAnimations;
        }
        #endregion Unity Methods

        #region Ready Set Go Game UI

        private void ReadySetGO_UI()
        {
            ReadySetGOTxt.text = "";
            ReadySetGOTxt.gameObject.SetActive(true);
            ReadySetGOAnimation();
        }
        private void ReadySetGOAnimation()
        {
            OnPlayCustomTimerSound?.Invoke(GameManager.GMAReady_Audio,GameManager.customVolume);
            _tweenManager.ScaleObject(ReadySetGOTxt.gameObject, Vector3.one * 0.2f, Vector3.one, 0.8f, true, LeanTweenType.easeOutBack,
                () =>{ReadySetGOTxt.text = "Ready";},
                () =>{Invoke(nameof(SetAnimation), 0.2f);});
        }
        private void SetAnimation()
        {
            OnPlayCustomTimerSound?.Invoke(GameManager.GMASet_Audio, GameManager.customVolume);
            _tweenManager.ScaleObject(ReadySetGOTxt.gameObject, Vector3.one * 0.2f, Vector3.one, 0.8f, true, LeanTweenType.easeOutBack,
               () =>{ReadySetGOTxt.text = "Set";},
               () =>{Invoke(nameof(GoAnimation), 0.2f);});
        }
        private void GoAnimation()
        {
            OnPlayCustomTimerSound?.Invoke(GameManager.GMAGo_Audio, GameManager.customVolume);
            _tweenManager.ScaleObject(ReadySetGOTxt.gameObject, Vector3.one * 0.2f, Vector3.one, 0.8f, true, LeanTweenType.easeOutBack,
               () =>{ReadySetGOTxt.text = "Go";}, 
               () =>{
                   ReadySetGOTxt.gameObject.SetActive(false);
                   ActionManager.OnGameStart?.Invoke();
                   Destroy(ReadySetGOTxt.gameObject);
                   ReadySetGOTxt = null;
                   });
        }

        #endregion Ready Set Go Game UI

        #region Earn Coin Animation

        private void InitializedEarnCoinTxt(Vector3 startPoint)
        {
            _tweenManager.MoveObject(earnPointsTxt.gameObject, startPoint, scoreDisplayPoint.position, 0.5f, true, 
                () =>{earnPointsTxt.gameObject.SetActive(true);}, 
                () => 
                {
                    _tweenManager.ScaleObject(earnPointsTxt.gameObject, earnPointsTxt.localScale, Vector3.zero, 0.2f, true, null,
                        () =>
                            {
                                ActionManager.SetPlayerScoreTxt?.Invoke(ActionManager.GetPlayerScores?.Invoke().ToString());
                                ResetEarnCoinTxt();
                            });
                });
        }
        private void ResetEarnCoinTxt()
        {
            earnPointsTxt.position = Vector3.zero;
            earnPointsTxt.gameObject.SetActive(false);
            earnPointsTxt.localScale = earnPointsTxtStartingScale;
        }

        #endregion Earn Coin Animation 

        #region New Year Animation

        private void ShowNewYear(float _moveandScaleDuration, float _haltDuration)
        {
            ResetYearTxt();
            nextYearTxt.gameObject.SetActive(true);
            nextYearTxt.text = ActionManager.GetCurrenYear?.Invoke().ToString();
            YearAnimation(_moveandScaleDuration, _haltDuration);
        }
        private void YearAnimation(float _moveandScaleDuration, float _haltDuration)
        {
            moveScaleDuration = _moveandScaleDuration;
            float haltDuration = _haltDuration;
            _tweenManager.MoveAndScaleObject(nextYearTxt.gameObject, yearDisplayPoint.position, yearFrontPoint.position, yearTxtHideScale, yearTxtOpenScale, moveScaleDuration, true,
                LeanTweenType.easeInOutCirc, LeanTweenType.notUsed,null,
                () =>{
                    ActionManager.UpdateCurrentYear?.Invoke();
                    string newYear = ActionManager.GetCurrenYear?.Invoke().ToString();
                    nextYearTxt.text = newYear;
                    Invoke(nameof(MoveObjectToBack), haltDuration);});
        }
        private void MoveObjectToBack()
        {
            _tweenManager.MoveAndScaleObject(nextYearTxt.gameObject, yearFrontPoint.position, yearDisplayPoint.position, yearTxtOpenScale, yearTxtHideScale, moveScaleDuration, true,
                LeanTweenType.easeInOutCirc, LeanTweenType.notUsed, null,
                () => 
                {
                    ResetYearTxt();
                    nextYearTxt.gameObject.SetActive(false);
                    ActionManager.SetCurrentYearTxt?.Invoke(ActionManager.GetCurrenYear?.Invoke().ToString());
                });
        }
      
        private void ResetYearTxt()
        {
            nextYearTxt.transform.localScale = yearTxtHideScale;
            nextYearTxt.text = "";
        }

        #endregion New Year Animation

        #region Pause & Resume Animations 

        private void Pause3DAnimation()
        {
            foreach(var _3Dmr in _3DAnimationsRenderers)
            {
                if(_3Dmr.gameObject.activeInHierarchy)
                {
                    ToggleRenderer(_3Dmr, false);
                }
            }
        }
        private void Resume3DAnimations()
        {
            foreach (var _3Dmr in _3DAnimationsRenderers)
            {
                if (_3Dmr.gameObject.activeInHierarchy)
                {
                    ToggleRenderer(_3Dmr, true);
                }
            }
        }
        private void ToggleRenderer(MeshRenderer mr, bool isActive)
        {
            mr.enabled = isActive;
        }

        #endregion Pause & Resume Animations  

        #region Custom Methods
       
        #endregion Custom Methods
    }
}
