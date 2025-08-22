using UISystem;
using UnityEditor;
using UnityEngine;

namespace DivoPOC.GrabEmAll
{
    public class TutorialObjectController : MonoBehaviour
    {
        #region Variables
        private Transform target;
        private float speed;
        private bool isMoving;
        [Header("<b><size=15><color=Green>Coin Object")]
        [Space(6)]
        [SerializeField] private GameObject coinObject;
        [Space(10)]
        [Header("<b><size=15><color=Green>Particle Effects")]
        [Space(6)]
        [SerializeField] private ParticleSystem wrongPS;
        [SerializeField] private ParticleSystem rightPS;
        [Space(6)]
        private TutorialObjectType tutorialObjectType;
        private ParticleSystem lastPS;
        private Transform TutorialMidVisiblePoint;
        private bool hasReachedToMid;
        private BoxCollider _Collider;
        private Transform objTransform;
        private Vector3 playerPos;
        private Vector3 stopPos;
        #endregion Variables

        #region Unity Methods

        private void Awake()
        {
            this.isMoving = false;
            this.target = null;
            hasReachedToMid = false;
            _Collider = GetComponent<BoxCollider>();
            objTransform = GetComponent<Transform>();
        }
        void Start()
        {
            
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
            if (isMoving)
            {
                playerPos = new Vector3(objTransform.position.x, 0f, objTransform.position.z);
                if (Vector3.Distance(objTransform.position, target.position) < 0.1f)
                {
                    OnSkipandReachedTarget();
                }
                else if (Vector3.Distance(playerPos, stopPos) < 0.1 && !hasReachedToMid)
                {
                    hasReachedToMid = true;
                    ActionManager.OnReachedWaitingPoint?.Invoke();
                    StopObjectMoving();
                }
                objTransform.position = Vector3.MoveTowards(objTransform.position, target.position, speed * Time.deltaTime);
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(GameManager.playerHandTag))
            {
                OnTutorialObjectCatched();
                DisableCurrentUI();
            }
        }
        #endregion Unity Methods

        #region Custom Methods
        private void DisableCurrentUI()
        {
            switch(tutorialObjectType)
            {
                case TutorialObjectType.IncorrectObject:
                    ViewController.Instance.HideScreen(ScreenName.GMAChooseCorrectGuidedScreen);
                    ActionManager.OnPerformUIHaptics?.Invoke(0.1f, 0.1f, 0.1f);
                    ActionManager.OnResumeCurrentTutorial?.Invoke();
                    break;
                case TutorialObjectType.CorrectObject:
                    // Continue Guided Tutorial
                    ActionManager.OnPerformUIHaptics?.Invoke(0.1f, 0.1f, 0.1f);
                    ViewController.Instance.HideScreen(ScreenName.GMAChooseCorrectGuidedScreen);
                    ActionManager.OnResumeCurrentTutorial?.Invoke();
                    break;
            }
        }
        internal void Init(TrackPoints trackPoints, float movingSpeed)
        {
            //gameObject.SetActive(true);
            //Debug.LogError("TP " + trackPoints.SpwanPoints.name + trackPoints.TargetPoints.name);
            this.target = trackPoints.TargetPoints;
            if (this.target != null)
            {
               // Debug.LogError("TP " + this.target.name);
                this.objTransform.position = trackPoints.SpwanPoints.position;
                this.speed = movingSpeed;
                this.isMoving = true;
                coinObject.SetActive(true);
                ToggleCollider(false);
            }
        }
        internal void SetTutorialObjectData(TutorialObjectType data)
        {
            tutorialObjectType = data;
        }
        internal void ResetTutorialObjectData()
        {
            tutorialObjectType = TutorialObjectType.None;
        }
        internal void SetWaitingPoints(Transform midVisible)
        {
            this.stopPos = new Vector3(midVisible.position.x, 0f, midVisible.position.z);
            this.TutorialMidVisiblePoint = midVisible;
            hasReachedToMid = false;
        }

        private void StopObjectMoving()
        {
            isMoving = false;
            ToggleCollider(true);
        }
        internal void StartObjectMoving()
        {
            isMoving = true;
        }
        private void PlayParticleEffect(ParticleSystem particleEffect)
        {
            if (particleEffect != null)
            {
                coinObject.SetActive(false);
                particleEffect.gameObject.SetActive(true);
                particleEffect.Play();
                lastPS = particleEffect;
            }
/*#if UNITY_EDITOR
            EditorApplication.isPaused = true;
#endif*/
            Invoke(nameof(ResetPS), 0.5f);
        }
        private void OnSkipandReachedTarget()
        {
            this.isMoving = false;
            this.target = null;
            //Debug.LogError("2");
            SetToHalt();
        }
        private void OnTutorialObjectCatched()
        {
            ActionManager.OnChooseTutorialObject?.Invoke(tutorialObjectType);
            this.isMoving = false;
            this.target = null;
            //Debug.LogError("On Object Catched "+ tutorialObjectType);
            //ActionManager.OnChooseObject?.Invoke(tutorialObjectType);
            switch (tutorialObjectType)
            {
                case TutorialObjectType.CorrectObject:
                    PlayParticleEffect(rightPS);
                    ActionManager.OnPerformUIHaptics?.Invoke(0.1f, 0.1f, 0.1f);
                    DivoPOC.ActionManager.OnPlayCustomSound?.Invoke(GameManager.grabCorrectAchievementAudio, GameManager.backgroundVolume);
                    break;
                case TutorialObjectType.IncorrectObject:
                    PlayParticleEffect(wrongPS);
                    ActionManager.OnPerformUIHaptics?.Invoke(0.1f, 0.1f, 0.1f);
                    break;
                case TutorialObjectType.OnlyPickedTest:
                    PlayParticleEffect(rightPS);
                    ActionManager.OnPerformUIHaptics?.Invoke(0.1f, 0.1f, 0.1f);
                    DivoPOC.ActionManager.OnPlayCustomSound?.Invoke(GameManager.grabCorrectAchievementAudio, GameManager.backgroundVolume);
                    break;
            }

        }
        private void SetToHalt()
        {
            //Debug.LogError("Reached to Target " + tutorialObjectType);
            ActionManager.OnCompleteOneTrip?.Invoke(false, tutorialObjectType);
        }
       
        private void ResetPS()
        {
            if (lastPS != null)
            {
                lastPS.Stop();
                lastPS.Clear();
                lastPS.gameObject.SetActive(false);
            }
            ActionManager.OnCompleteOneTrip?.Invoke(true, tutorialObjectType);
        }
        internal void ToggleCollider(bool isActive)
        {
            _Collider.enabled = isActive;
        }
        #endregion Custom Methods

    }
}

