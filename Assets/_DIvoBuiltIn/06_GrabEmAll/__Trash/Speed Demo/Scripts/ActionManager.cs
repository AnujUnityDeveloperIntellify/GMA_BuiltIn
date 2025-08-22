using UnityEngine;
using System;

namespace SpeedVariation
{
    public class ActionManager : MonoBehaviour
    {
        internal static Action<BallController> OnSetToHalt;
        internal static Action OnCalculateNewYearSpeed;
    }
}

