using UnityEngine;
using System;
using System.Collections;
using System.Data.SqlTypes;

namespace DivoPOC.GrabEmAll
{
    public class HapticManager : MonoBehaviour
    {
        #region Variables
        [Range(0, 1)]
        [SerializeField] private float amplitude;
        [Range(0, 1)]
        [SerializeField] private float frequency;
        [Range(0, 2)]
        [SerializeField] private float duration;

        [SerializeField] private OVRInput.Controller leftController;
        [SerializeField] private OVRInput.Controller rightController;
        #endregion Variables

        #region Unity Methods

        void Start()
        {

        }
        private void OnEnable()
        {
            ActionManager.OnPerformUIHaptics += UIHaptic;
        }
        private void OnDisable()
        {
            ActionManager.OnPerformUIHaptics -= UIHaptic;
        }

        #endregion Unity Methods

        #region Custom Methods
       
        private IEnumerator StartHaptic(float frequency, float amplitude, float duration)
        {
            OVRInput.SetControllerVibration(frequency, amplitude, leftController);
            OVRInput.SetControllerVibration(frequency, amplitude, rightController);
            yield return new WaitForSecondsRealtime(duration);
            StopHaptic();
        }
        private void StopHaptic()
        {
            OVRInput.SetControllerVibration(0, 0, leftController);
            OVRInput.SetControllerVibration(0, 0, rightController);
        }
        private void UIHaptic(float _frequency, float _amplitude, float _duration)
        {
            StartCoroutine(StartHaptic(_frequency, _amplitude,_duration));
        }
        #endregion Custom Methods

    }
}

