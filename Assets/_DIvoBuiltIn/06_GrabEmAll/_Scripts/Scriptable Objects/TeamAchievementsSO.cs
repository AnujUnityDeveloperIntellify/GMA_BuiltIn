using UnityEngine;

namespace DivoPOC.GrabEmAll
{
    [CreateAssetMenu(fileName = "TeamAchievementsSO", menuName = "Scriptable Objects/Grab Em All/TeamAchievementsSO")]
    public class TeamAchievementsSO : ScriptableObject
    {
        public Achievements achievementsType;
        public int achievementYear;
        public AchievementStatus achievementStatus;
        public Material ObjectTypeMaterial;

        public TeamAchievementsSO(Achievements _achievementType, int _year, AchievementStatus _achievementStatus)
        {
            achievementStatus = _achievementStatus;
            achievementYear = _year;
            achievementsType = _achievementType;
        }
        //public GameObject prefabGO;
    }
}
