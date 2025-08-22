using System;
using UnityEngine;

namespace DivoPOC
{
    public class ActionManager : MonoBehaviour
    {
        #region Audio Actions
        internal static Action<String, float> OnPlayCustomSound; // Play one-shot clips
        internal static Action<String, float> OnPlayCustomTimerSound; //Play time remaining audio sound
        internal static Action<String, float> OnPlayBackgroundMusic; // Play background music for a mini-game
        internal static Action OnStopBackgroundMusic; // Pause background music
        internal static Action OnStopCustomTimerSound; //Stop time remaining audio sound
        internal static Func<string, float> GetClipLength; // Get the Clip Length
        #endregion Audio Actions
    }
}