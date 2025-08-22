using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;
using UISystem;

namespace DivoPOC.GrabEmAll
{
    public enum Achievements
    {
        None,
        Trophy,
        Golden_Boot,
        Goals,
        Coach,
        Key_Player,
        Red_Card,
        World_Ranking,
        Jersey,
        Sponsors,
        PowerBooster,
        HintBooster
    }
    public enum AchievementYear
    {
        None, Year2015, Year2016, Year2017, Year2018, Year2019, Year2020, Year2021, Year2022, Year2023, Year2024, Year2025 }
    public enum AchievementStatus
    {
        None,
        Correct,
        Incorrect,
        PowerBoster,
        HintBooster
    }

    [System.Serializable]
    public class TrackPoints
    {
        public Transform SpwanPoints;
        public Transform TargetPoints;
    }
    public class SpwanManager : MonoBehaviour
    {
        #region Variables
        [Header("<b><size=15><color=Green>Track Data")]
        [Space(5)]
        [SerializeField] private List<TrackPoints> trackPoints;
        private Queue<AchievementsController> CurrentYearAchievements = new Queue<AchievementsController>();
        private List<AchievementsController> trackAchievements = new List<AchievementsController>();
        private float currentSpeed;
        private int currentYear;
        private Coroutine spwanCoroutine;
        private float YearlapsDuration ;
        private float trackDistance;
        private Queue<AchievementsController> currentYearAchievementsQueue;
        private Action OnReachedLastAchievement;
        [Space(10)]
        [Header("<b><size=15><color=Green>Achievement Speed Movevement Data")]
        [Space(5)]
        [SerializeField] private float speedMultiplier = 1.0f; // Adjust in Inspector (e.g., 0.8 for slower, 1.2 for faster)
        [SerializeField] private float minSpeed = 3f; // Minimum speed for noobs
        [SerializeField] private float maxSpeed = 6.5f; // Maximum speed for VR comfort
        [SerializeField] private float spawnDistance = 20f; // Distance to spawn achievements from player
        #endregion Variables

        #region Unity Methods

        private void Awake()
        {
            this.currentYearAchievementsQueue = new Queue<AchievementsController>();
            this.currentYearAchievementsQueue.Clear();
        }
        void Start()
        {
            this.YearlapsDuration = GameManager.yearLapsDuration;
            this.trackAchievements.Clear();
        }
        private void OnEnable()
        {
            ActionManager.SetCurrentYearAchievements += SetCurrentYearAchievements;
            ActionManager.OnStopRendererTrackAchievements += StopRendererTrackAchievements;
            ActionManager.OnResumeRendererTrackAchievements += ResumeRendererTrackAchievements;
            ActionManager.OnRemovedTrackedAchievement += RemoveTrackedAchievements;
            ActionManager.OnDiscardCurrentYearAchievements += StopAchievementSpwaning;
            ActionManager.LoadandOpenHintUI += GetCurrentHints;
            ActionManager.OnResetSpwanManager += ResetSpwanManager;
        }
        private void OnDisable()
        {
            ActionManager.SetCurrentYearAchievements -= SetCurrentYearAchievements;
            ActionManager.OnStopRendererTrackAchievements -= StopRendererTrackAchievements;
            ActionManager.OnResumeRendererTrackAchievements -= ResumeRendererTrackAchievements;
            ActionManager.OnRemovedTrackedAchievement -= RemoveTrackedAchievements;
            ActionManager.OnDiscardCurrentYearAchievements -= StopAchievementSpwaning;
            ActionManager.LoadandOpenHintUI -= GetCurrentHints;
            ActionManager.OnResetSpwanManager -= ResetSpwanManager;
        }
        #endregion Unity Methods

        #region Custom Methods
        private void ResetSpwanManager()
        {
            this.trackAchievements.Clear();
            this.currentYearAchievementsQueue = new Queue<AchievementsController>();
            this.currentYearAchievementsQueue.Clear();
        }
        private void StopRendererTrackAchievements()
        {
            foreach(var achievement in this.trackAchievements)
            {
                achievement.PauseAchievement();
                achievement.ToggleCoinRenderer(false);
            }
        }
        private void ResumeRendererTrackAchievements()
        {
            foreach (var achievement in this.trackAchievements)
            {
                achievement.ResumeAchievement();
                achievement.ToggleCoinRenderer(true);
            }
        }

        private void SetCurrentYearAchievements(Queue<AchievementsController> _totalObjects, int _currentYear)
        {
            trackDistance = Vector3.Distance(trackPoints[0].SpwanPoints.position, trackPoints[0].TargetPoints.position);
            this.OnReachedLastAchievement = null;
            this.CurrentYearAchievements.Clear();
            this.CurrentYearAchievements = new Queue<AchievementsController>(_totalObjects);
            this.currentYear = _currentYear;
            this.currentSpeed = GetSpeedForNextYearObjects(this.CurrentYearAchievements,YearlapsDuration);
            float timeTakenbyOneObject = (trackDistance /this.currentSpeed);
            float timeElaps = this.YearlapsDuration - (timeTakenbyOneObject * 0.4f);
            StartsAchievementsMotion(this.CurrentYearAchievements, timeElaps, timeTakenbyOneObject);
            //Debug.LogError($"time Elaps : {timeElaps} time take By Object {timeTakenbyOneObject} || {this.currentYear}");
        }
        private void StopAchievementSpwaning()
        {
            StopCoroutine(spwanCoroutine);
            spwanCoroutine = null;
            for (int i = 0; i < trackAchievements.Count; i++)
            {
                trackAchievements[i].ForcedSetToHalt();
            }
        }

        private void GetCurrentHints()
        {
            List<HintData> correctHintsSprites = new List<HintData>();
            List<AchievementsController> yearAchievments = new List<AchievementsController>(this.currentYearAchievementsQueue);
            correctHintsSprites = GetCorrect8Sprites(yearAchievments);
            ActionManager.OnLoadNewYearHints?.Invoke(correctHintsSprites);
        }

        private List<HintData> GetCorrect8Sprites(List<AchievementsController> yearAchievments)
        {
            List<HintData> allCorrectHints = new List<HintData>();
            allCorrectHints.Clear();
            HintData tempContainer;
            foreach (var x in yearAchievments)
            {
                if (x.achievementStatus == AchievementStatus.Correct)
                {
                    Material mat = x.categoryMR.material;
                    Texture2D baseMapTexture = (Texture2D)mat.GetTexture("_BaseMap");
                    Sprite sprite = Sprite.Create(baseMapTexture, new Rect(0, 0, baseMapTexture.width, baseMapTexture.height), new Vector2(0.5f, 0.5f));
                    tempContainer = new HintData();
                    tempContainer.hintSprite = sprite;
                    tempContainer.hintType = x.achievementType.ToString().Replace("_"," ") ;
                    allCorrectHints.Add(tempContainer);
                }
            }
            return allCorrectHints;
        }
        private void StartsAchievementsMotion(Queue<AchievementsController> _upcommingAchievement, float timeElaps, float timeTakenByObject)
        {
            if (spwanCoroutine != null)
            {
                StopCoroutine(spwanCoroutine);
                spwanCoroutine = null;
            }
            this.currentYearAchievementsQueue = null;
            this.currentYearAchievementsQueue = new Queue<AchievementsController>(_upcommingAchievement);
            spwanCoroutine = StartCoroutine(SpawnOverTime(timeElaps,timeTakenByObject));
        }
        private IEnumerator SpawnOverTime(float timeElaps, float timeTakenByObject)
        {
            if (this.currentYearAchievementsQueue == null || this.currentYearAchievementsQueue.Count == 0) yield break;
            int count = this.currentYearAchievementsQueue.Count;
            float interval = timeElaps / count;
            TrackPoints randomTrackPoint = null;
            AchievementsController dequeueElement = null;
            for(int i=0; i < count;i++)
            {
                this.OnReachedLastAchievement = this.currentYearAchievementsQueue.Count == 1 ? ReachedLastAchievement : null;
                randomTrackPoint = GetRandomTrackPoint();
                dequeueElement = this.currentYearAchievementsQueue.Dequeue();
                if (dequeueElement != null)
                {
                    this.trackAchievements.Add(dequeueElement);
                    dequeueElement.gameObject.SetActive(true);
                    yield return null;
                    dequeueElement.Init(randomTrackPoint, currentSpeed,this.OnReachedLastAchievement);
                }
                dequeueElement = null;
                if(this.currentYearAchievementsQueue.Count>0)
                {
                    yield return new WaitForSeconds(interval);
                }
            }
            //Debug.LogError("======>Complete " + currentYear.ToString());
        }
        private TrackPoints GetRandomTrackPoint()
        {
            return trackPoints[UnityEngine.Random.Range(0,trackPoints.Count)];
        }
        private float GetSpeedForNextYearObjects(Queue<AchievementsController> upcommingAchievement, float totalTime)
        {
            float distance = trackDistance; // Distance from player to spawn achievements
            if (upcommingAchievement == null || upcommingAchievement.Count == 0)
            {
                Debug.LogWarning("No achievements to spawn. Using fallback speed.");
                return minSpeed * speedMultiplier; // Apply multiplier to fallback
            }

            int count = upcommingAchievement.Count;
            int yearIndex = YearManager.currentYearIndex; // 12 for Year2025, 1 for Year2014
            if (yearIndex == 0)
            {
                Debug.LogWarning($"Invalid year {currentYear}. Using fallback speed.");
                return minSpeed * speedMultiplier;
            }

            // Fixed target travel time like static function for continuous spawning
            float targetTravelTime = 6.67f; // Approximate travel time at base speed for continuous flow (like static at ~3 u/s for 20 units)
            float baseSpeed = distance / targetTravelTime; // Dynamic to distance

            // Scale speed by reverse year progression (2025 lowest, 2014 highest)
            float speedIncrementPerYear = 0.3f * (distance / 20f); // Scale increment based on distance to match original ratios
            float speed = baseSpeed + yearIndex * speedIncrementPerYear; // Invert progression like static

            // Apply speed multiplier
            speed *= speedMultiplier;

            // Scale min/max based on distance to keep travel times consistent
            float effectiveMinSpeed = minSpeed; //* (distance / 20f);
            float effectiveMaxSpeed = maxSpeed;//* (distance / 20f);

            // Clamp to ensure playability
            speed = Mathf.Clamp(speed, effectiveMinSpeed, effectiveMaxSpeed);

            Debug.LogError($"Year {currentYear}: {count} objects, finalSpeed {speed:F2}u/s");
            return speed;
        }
        private void RemoveTrackedAchievements(AchievementsController _achievementsController)
        {
            this.trackAchievements.Remove( _achievementsController );
        }
        private void ReachedLastAchievement()
        {
           
        }
        /* private float GetSpeedForNextYearObjects(Queue<AchievementsController> upcommingAchievement, float totalTime)
         {
             float speed = 0f;

             // Code Here to Calculate a Variable Moving Speed for Year Achievements
             // For Test
             speed = 5f;
             return speed;
         }*/
        #endregion Custom Methods

    }
}

