using System.Collections.Generic;
using UnityEngine;

namespace DivoPOC.GrabEmAll
{
    public class PooledManager : MonoBehaviour
    {
        #region Variables
        private Dictionary<Achievements, Queue<AchievementsController>> PoolDict = new Dictionary<Achievements, Queue<AchievementsController>>();
        
        #region Pooled Object References

        [SerializeField] private List<AchievementsController> trophy_AchievemetsPooled;
        [Space(6)]
        [SerializeField] private List<AchievementsController> boot_AchievemetsPooled;
        [Space(6)]
        [SerializeField] private List<AchievementsController> goal_AchievemetsPooled;
        [Space(6)]
        [SerializeField] private List<AchievementsController> coach_AchievemetsPooled;

        [Space(8)]

        [SerializeField] private List<AchievementsController> YearPooledList;
        [Space(8)]
        private int currentYearIndex = 0;
        [SerializeField] private AchievmentsHolderSO YearAchievementsHolder;
        #endregion Pooled Object References
        /*
                [Header("Show Objects")]
                [SerializeField] private List<AchievementsController> Show_Trophy_AchievemetsPooled = new List<AchievementsController>();
                [SerializeField] private List<AchievementsController> Show_Boot_AchievemetsPooled = new List<AchievementsController>();
                [SerializeField] private List<AchievementsController> Show_Goal_AchievemetsPooled = new List<AchievementsController>();
                [SerializeField] private List<AchievementsController> Show_Coach_AchievemetsPooled = new List<AchievementsController>();
                */
        #endregion Variables

        #region Unity Methods

        private void Awake()
        {
            InitialisedPooledDict();
        }
        void Start()
        {

        }
        private void OnEnable()
        {
            ActionManager.OnEnqueueAchievements += EnqueueAchievements;
            ActionManager.OnDequeueAchievements += DequeueAchievements;
        }
        private void OnDisable()
        {
            ActionManager.OnDequeueAchievements -= DequeueAchievements;
            ActionManager.OnEnqueueAchievements -= EnqueueAchievements;
        }
        #endregion Unity Methods

        #region Custom Methods
        
        private void InitialisedPooledDict()
        {
            PoolDict.Clear();
            LoadAchievementToDictionary(trophy_AchievemetsPooled);
            LoadAchievementToDictionary(boot_AchievemetsPooled);
            LoadAchievementToDictionary(goal_AchievemetsPooled);
            LoadAchievementToDictionary(coach_AchievemetsPooled);

           /* ShowObjects(Show_Trophy_AchievemetsPooled, PoolDict[Achievements.Trophy]);
            ShowObjects(Show_Boot_AchievemetsPooled, PoolDict[Achievements.Boots]);
            ShowObjects(Show_Goal_AchievemetsPooled, PoolDict[Achievements.Goals]);
            ShowObjects(Show_Coach_AchievemetsPooled, PoolDict[Achievements.Coach]);*/
        }
        [ContextMenu("Read Next Year")]
        private void ReadNextYearData()
        {
            YearAchievmentsSO currentYearAchievementsSO = null;
            if (currentYearIndex < YearAchievementsHolder.spwanContents.Count)
            {
                currentYearAchievementsSO = YearAchievementsHolder.spwanContents[currentYearIndex];
                if (currentYearAchievementsSO != null)
                {
                   // Debug.LogError("Load "+ currentYearAchievementsSO.year.ToString());
                    ShowObjects(YearPooledList, ReadYearData(currentYearAchievementsSO));
                }
            }
            currentYearIndex++;
        }
        private Queue<AchievementsController> ReadYearData(YearAchievmentsSO yearAchievements)
        {
            Queue<AchievementsController> poolQueue = new Queue<AchievementsController>();
            poolQueue.Clear();
            AchievementsController DequeElement = null;
            foreach (var x in yearAchievements.yearAchievments)
            {
                DequeElement = ActionManager.OnDequeueAchievements?.Invoke(x.achievementsType);
                DequeElement.SetAchievementData(x);
                poolQueue.Enqueue(DequeElement);
                DequeElement = null;
            }
            return poolQueue;
        }
        private void EnqueueAchievements(AchievementsController achievementsObj)
        {
            if (achievementsObj != null)
            {
                PoolDict[achievementsObj.achievementType].Enqueue(achievementsObj);
            }
        }
        private AchievementsController DequeueAchievements(Achievements achievementsType)
        {
            AchievementsController dequeueObject = null;
            dequeueObject = PoolDict[achievementsType].Dequeue();
            return dequeueObject;
        }
        private void LoadAchievementToDictionary(List<AchievementsController> _AchievementsHolder)
        {
            if(_AchievementsHolder!=null && _AchievementsHolder.Count>0)
            {
                foreach(var x in _AchievementsHolder)
                {
                    if (!PoolDict.ContainsKey(x.achievementType))
                    {
                        PoolDict[x.achievementType] = new Queue<AchievementsController>();
                    }
                    PoolDict[x.achievementType].Enqueue(x);
                }
            }
        }

        //------------
        private void ShowObjects(List<AchievementsController> showList,Queue<AchievementsController> queueTemp)
        {
            showList.Clear();
            foreach(var x in queueTemp)
            {
                showList.Add(x);
            }
        }
        
        #endregion Custom Methods

    }
}