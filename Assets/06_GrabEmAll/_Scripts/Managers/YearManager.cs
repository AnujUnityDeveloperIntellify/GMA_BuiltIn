using UnityEngine;
using System.Collections.Generic;
using System;

namespace DivoPOC.GrabEmAll
{
    public class YearManager : MonoBehaviour
    {
        #region Variables
        [Header("<b><size=15><color=Green>Year Scriptable Object")]
        [Space(5)]
        [SerializeField] private AchievmentsHolderSO YearAchievementsHolder;
        [Space(8)]
        [Header("<b><size=15><color=Green>Pooled Achievements")]
        [Space(5)]
        [SerializeField] private List<AchievementsController> AllPoolObjects;
        [SerializeField] private Transform OutterPoint;
        [SerializeField] private int StartYearNumber;
        [SerializeField] private int LastYearNumber;
        [Space(8)]
        [Header("<b><size=15><color=Green>Power Boosters")]
        [Space(5)]
        [SerializeField] private List<AchievementsController> AllPowerUpBooster;
        [Space(8)]
        [Header("<b><size=15><color=Green>Hint Boosters")]
        [Space(5)]
        [SerializeField] private List<AchievementsController> AllHintBooster;
        private Queue<AchievementsController> powerBoosterQueue = new Queue<AchievementsController>();
        private Queue<AchievementsController> hintBoosterQueue = new Queue<AchievementsController>();
        private Queue<AchievementsController> yearAchievementsObjects = new Queue<AchievementsController>();
        private int currentYearNumber;
        //private yearDescription currentYearDescription;
        internal static int currentYearIndex;
        [SerializeField] private Queue<AchievementsController> poolQueue = new Queue<AchievementsController>();
        #endregion Variables

        #region Unity Methods

        private void Awake()
        {
            currentYearNumber = StartYearNumber;
        }
        void Start()
        {
            currentYearIndex = 0;
            InitializedPoolObjects(AllPoolObjects);
            InitializedPoolObjects(AllPowerUpBooster);
            InitializedPoolObjects(AllHintBooster);
        }
        private void OnEnable()
        {
            ActionManager.LoadNewYearData += ReadandSetNextYearData;
            ActionManager.SetAchievementsAtHalt += HaltAchievements;
            ActionManager.GetLastYear += GetLastYear;
            ActionManager.GetCurrenYear += GetCurrentYear;
            ActionManager.UpdateCurrentYear += UpdateNextYearRound;
            ActionManager.OnResetYearManager += ResetYearManager;
        }
        private void OnDisable()
        {
            ActionManager.SetAchievementsAtHalt -= HaltAchievements;
            ActionManager.LoadNewYearData -= ReadandSetNextYearData;
            ActionManager.GetLastYear -= GetLastYear;
            ActionManager.GetCurrenYear -= GetCurrentYear;
            ActionManager.UpdateCurrentYear -= UpdateNextYearRound;
            ActionManager.OnResetYearManager -= ResetYearManager;
        }
        #endregion Unity Methods

        #region Custom Methods

        private void ResetYearManager()
        {
            poolQueue.Clear();
            powerBoosterQueue.Clear();
            hintBoosterQueue.Clear();
            yearAchievementsObjects.Clear();
            currentYearIndex = 0;
            InitializedPoolObjects(AllPoolObjects);
            InitializedPoolObjects(AllPowerUpBooster);
            InitializedPoolObjects(AllHintBooster);
        }
        private int GetLastYear()
        {
            return LastYearNumber;
        }
        private int GetCurrentYear()
        {
            return currentYearNumber;
        }
        private void UpdateNextYearRound()
        {
            currentYearNumber -= 1;
        }
        [ContextMenu("Read and Load")]
        private void ReadandSetNextYearData()
        {
            YearAchievmentsSO currentYearAchievementsSO = null;
            yearAchievementsObjects.Clear();
            AchievementsController DequeElement = null;
            if (currentYearIndex<YearAchievementsHolder.spwanContents.Count)
            {
                currentYearAchievementsSO = YearAchievementsHolder.spwanContents[currentYearIndex];
                //Debug.LogError("=========>Start "+ currentYearAchievementsSO.achievementYear);
                foreach (var x in currentYearAchievementsSO.yearAchievments)
                {
                    DequeElement = poolQueue.Dequeue();
                    DequeElement.SetAchievementData(x);
                    DequeElement.SetAchievementsCategoryType(x.ObjectTypeMaterial);
                    yearAchievementsObjects.Enqueue(DequeElement);
                    DequeElement = null;
                }
                LoadToSpwanAchievements(yearAchievementsObjects, currentYearAchievementsSO.achievementYear);
                currentYearIndex++;
            }
        }
        private void LoadToSpwanAchievements(Queue<AchievementsController> pooledObjects, int _year)
        {
            //Code Here if we want to Add some Extra Achievements to maitaining a increasing speed
            if (ActionManager.GetPlayerLives?.Invoke() < 3)
            {
                pooledObjects = InsertBooster(pooledObjects, powerBoosterQueue.Dequeue(), _year, Achievements.PowerBooster, AchievementStatus.PowerBoster);
            }
            pooledObjects = InsertBooster(pooledObjects, hintBoosterQueue.Dequeue(), _year, Achievements.HintBooster, AchievementStatus.HintBooster);
            ActionManager.SetCurrentYearAchievements?.Invoke(pooledObjects, _year);
        }
        private Queue<AchievementsController> InsertBooster(Queue<AchievementsController> pooledObjects, AchievementsController booster, int year,
                                                            Achievements type, AchievementStatus status)
        {
            booster.SetAchievementData(new TeamAchievementsSO(type, year, status));
            var tempList = new List<AchievementsController>(pooledObjects);
            switch(status)
            {
                case AchievementStatus.PowerBoster:
                    tempList.Insert(UnityEngine.Random.Range(0, tempList.Count), booster);
                    break;
                case AchievementStatus.HintBooster:
                    tempList.Insert(UnityEngine.Random.Range(0, 4), booster);
                    break;
            }
            return new Queue<AchievementsController>(tempList);
        }
        private void InitializedPoolObjects(List<AchievementsController> _poolObjects)
        {
            foreach (var x in _poolObjects)
            {
                ActionManager.SetAchievementsAtHalt?.Invoke(x);
            }
        }

        private void HaltAchievements(AchievementsController currentAchievements)
        {
            if (currentAchievements != null)
            {
                AddedRespectivePoolled(currentAchievements);
                currentAchievements.gameObject.transform.position = OutterPoint.position;
               // Debug.LogError(currentAchievements.name+" On Halt || pooled Count "+ poolQueue.Count);
                currentAchievements.gameObject.SetActive(false);
            }
        }
        private void AddedRespectivePoolled(AchievementsController currentAchievements)
        {
            switch (currentAchievements.achievementType)
            {
                case Achievements.None:
                case Achievements.Trophy:
                case Achievements.Golden_Boot:
                case Achievements.Goals:
                case Achievements.Coach:
                case Achievements.Key_Player:
                case Achievements.Red_Card:
                case Achievements.World_Ranking:
                case Achievements.Jersey:
                case Achievements.Sponsors:
                    poolQueue.Enqueue(currentAchievements);
                    currentAchievements.ResetAchievementData();
                    break;
                case Achievements.PowerBooster:
                    powerBoosterQueue.Enqueue(currentAchievements);
                    break;
                case Achievements.HintBooster:
                    hintBoosterQueue.Enqueue(currentAchievements);
                    break;
                default:
                    Debug.LogError("Default Pooled type");
                    break;
            }
        }
        #endregion Custom Methods
    }
}

