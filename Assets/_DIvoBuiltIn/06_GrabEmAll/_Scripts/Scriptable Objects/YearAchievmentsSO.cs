using System.Collections.Generic;
using UnityEngine;

namespace DivoPOC.GrabEmAll
{
    [CreateAssetMenu(fileName = "YearAchievmentsSO", menuName = "Scriptable Objects/Grab Em All/YearAchievmentsSO")]
    public class YearAchievmentsSO : ScriptableObject
    {
        public List<TeamAchievementsSO> yearAchievments;
        //public AchievementYear year;
        public int achievementYear;
    }
}

