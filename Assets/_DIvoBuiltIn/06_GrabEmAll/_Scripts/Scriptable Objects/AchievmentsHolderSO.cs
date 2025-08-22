using UnityEngine;
using System.Collections.Generic;
namespace DivoPOC.GrabEmAll
{
    [CreateAssetMenu(fileName = "AchievmentsHolderSO", menuName = "Scriptable Objects/Grab Em All/AchievmentsHolderSO")]
    public class AchievmentsHolderSO : ScriptableObject
    {
        public List<YearAchievmentsSO> spwanContents;
    }
}
