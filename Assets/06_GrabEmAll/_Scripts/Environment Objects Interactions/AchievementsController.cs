using Oculus.Platform;
using System;
using UnityEngine;
using static DivoPOC.ActionManager;

namespace DivoPOC.GrabEmAll
{
    public class AchievementsController : MonoBehaviour
    {
        #region Variables
        private Transform target;
        private float speed;
        private bool isMoving;
        [Header("<b><size=15><color=Green>Particle Effects")]
        [Space(6)]
        [SerializeField] private ParticleSystem wrongPS;
        [SerializeField] private ParticleSystem rightPS;
        [Space(6)]
        [Header("<b><size=15><color=Green>Achievement Category")]
        [Space(6)]
        [SerializeField] private GameObject coinObject;
        [SerializeField] internal MeshRenderer categoryMR;
        [SerializeField] private Material DefaultCategoryType;
        [Space(6)]
        [Header("<b><size=15><color=Green>Achievement Type")]
        [Space(6)]
        [SerializeField] internal Achievements achievementType;
        internal AchievementStatus achievementStatus;
        private int achievementYear;
        private ParticleSystem lastPS;
        private BoxCollider _collider;
        private Transform objTransform;
        private Action lastObjectOfYearReached;
        #endregion Variables

        #region Unity Methods

        private void Awake()
        {
            _collider = GetComponent<BoxCollider>();
            objTransform = GetComponent<Transform>();
        }
        void Start()
        {
            this.isMoving = false;
            this.target = null;
        }
        private void OnEnable()
        {

        }
        private void OnDisable()
        {
            
        }
        void Update()
        {
            if (target == null) return;
            if(isMoving)
            {
                objTransform.position = Vector3.MoveTowards(objTransform.position, target.position, speed * Time.deltaTime);
                if (Vector3.Distance(objTransform.position, target.position) < 0.1f)
                {
                    OnSkipandReachedTarget();
                }
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag(GameManager.playerHandTag))
            {
                OnAchievementCatched();
            }
        }
        #endregion Unity Methods

        #region Custom Methods
        internal void Init(TrackPoints trackPoints, float movingSpeed, Action _lastObjectofYearReached)
        {
            //gameObject.SetActive(true);
            this.target = trackPoints.TargetPoints;
            if(this.target != null)
            {
                this.objTransform.position = trackPoints.SpwanPoints.position;
                this.speed = movingSpeed;
                this.isMoving = true;
                ToggleCoin(true);
                this._collider.enabled = true;
                this.lastObjectOfYearReached = _lastObjectofYearReached ?? null;
            }
        }
        internal void SetAchievementData(TeamAchievementsSO data)
        {
            //Used Below code for Same Type Object Pooling
            achievementStatus = data.achievementStatus;
            achievementType = data.achievementsType;
            achievementYear = data.achievementYear;
        }
        internal void ResetAchievementData()
        {
            achievementStatus = AchievementStatus.None;
            achievementType = Achievements.None;
            achievementYear = -1;
        }
        private void PlayParticleEffect(ParticleSystem particleEffect)
        {
            if(particleEffect!=null)
            {
                ActionManager.OnRemovedTrackedAchievement?.Invoke(this);
                ToggleCoin(false);
                particleEffect.gameObject.SetActive(true);
                lastPS = particleEffect;
                particleEffect.Play();
            }
            Invoke(nameof(SetToHalt), 0.5f);
        }
        private void OnSkipandReachedTarget()
        {
            ActionManager.OnRemovedTrackedAchievement?.Invoke(this);
            SetToHalt();
            this.isMoving = false;
            this.target = null;
            this._collider.enabled = false;
        }
        private void OnAchievementCatched()
        {
            ActionManager.OnResetIdealTimmer?.Invoke();
            this.isMoving = false;
            this.target = null;
            this._collider.enabled = false; 
            switch (achievementStatus)
            {
                case AchievementStatus.Correct:
                    PlayParticleEffect(rightPS);
                    ActionManager.OnInitializePointsTxt?.Invoke(objTransform.position);
                    ActionManager.OnPlayerScored?.Invoke();
                    ActionManager.OnPerformUIHaptics?.Invoke(0.1f, 0.1f, 0.1f);
                    OnPlayCustomSound?.Invoke(GameManager.grabCorrectAchievementAudio, GameManager.backgroundVolume);
                    break;
                case AchievementStatus.Incorrect:
                    PlayParticleEffect(wrongPS);
                    ActionManager.OnUpdatePlayerLives?.Invoke(-1);
                    OnPlayCustomSound?.Invoke(GameManager.GMALifeLost_Audio, GameManager.backgroundVolume);
                    ActionManager.OnPerformUIHaptics?.Invoke(0.1f, 0.1f, 0.1f);
                    break;
                case AchievementStatus.PowerBoster:
                    PlayParticleEffect(rightPS);
                    ActionManager.OnUpdatePlayerLives?.Invoke(1);
                    OnPlayCustomSound?.Invoke(GameManager.GMACollectPowerUp_Audio, GameManager.backgroundVolume);
                    ActionManager.OnPerformUIHaptics?.Invoke(0.1f, 0.1f, 0.1f);
                    break;
                case AchievementStatus.HintBooster:
                    PlayParticleEffect(rightPS);
                    ActionManager.OnResetHintBtn?.Invoke(HintStatus.Active);
                    OnPlayCustomSound?.Invoke(GameManager.GMACollectPowerUp_Audio, GameManager.backgroundVolume);
                    ActionManager.OnPerformUIHaptics?.Invoke(0.1f, 0.1f, 0.1f);
                    break;
            }
           
        }
        private void SetToHalt()
        {
            this.lastObjectOfYearReached?.Invoke();
            ResetPS();
            ActionManager.SetAchievementsAtHalt?.Invoke(this);
        }
        internal void ForcedSetToHalt()
        {
            ActionManager.OnRemovedTrackedAchievement?.Invoke(this);
            this.isMoving = false;
            this.target = null;
            this._collider.enabled = false;
            ResetPS();
            ActionManager.SetAchievementsAtHalt?.Invoke(this);
        }
        private void ResetPS()
        {
            if(lastPS != null)
            {
                lastPS.Stop();
                lastPS.Clear();
                lastPS.gameObject.SetActive(false);
                lastPS = null;
            }
        }
        private void ToggleCoin(bool isActive)
        {
            coinObject.SetActive(isActive);
        }
        internal void SetAchievementsCategoryType(Material mat)
        {
            if (mat != null)
            {
                categoryMR.material = mat;
            }
            else
            {
                categoryMR.material = DefaultCategoryType;
            }
        }
        internal void ToggleCoinRenderer(bool isActive)
        {
            coinObject.SetActive(isActive);
        }
        internal void PauseAchievement()
        {
            this.isMoving = false;
        }
        internal void ResumeAchievement()
        {
            this.isMoving = true;
        }
        #endregion Custom Methods

    }
}

