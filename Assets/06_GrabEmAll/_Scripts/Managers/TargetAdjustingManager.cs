using System.Collections;
using UnityEngine;

namespace DivoPOC.GrabEmAll
{
    /// <summary>
    /// Manages dynamic adjustment of target and waiting points based on XR Rig's center eye camera Y-position.
    /// Optimizes updates for both gameplay and tutorial states, ensuring targets move with player height
    /// and stay above a minimum Y, with reduced CPU overhead for VR performance.
    /// </summary>
    public class TargetAdjustingManager : MonoBehaviour
    {
        private const float DEFAULT_UPDATE_INTERVAL = 0.1f; // Default update interval in seconds
        private const float DEFAULT_Y_CHANGE_THRESHOLD = 0.01f; // Default minimum Y change threshold

        [Header("XR Configuration")]
        [SerializeField] private Transform centerEyeCamera; // Reference to XR Rig's center eye camera
        [SerializeField]
        [Tooltip("Offset from camera Y-position for target adjustment")]
        private float yOffset = 0.1f; // Adjustable offset in Inspector
        [SerializeField]
        [Tooltip("Minimum Y-position to prevent targets going below platform")]
        private float minY = 0f; // Minimum Y constraint

        [Header("Gameplay Integration")]
        [SerializeField] private Transform spwanPointsHolder; // Reference to SpwanManager for gameplay trackPoints
        [SerializeField] private Transform targetPointsHolder; // Reference to SpwanManager for gameplay trackPoints

        [Header("Tutorial Integration")]
        [SerializeField] private Transform midWaitingPoint;
        [SerializeField] private Transform nearPlayerWaitingPoint; // Waiting point near player in tutorial

        [Header("Year Change Position Settings")]
        [SerializeField] private Transform yearFrontPointPosition;

        [Header("Update Settings")]
        [SerializeField]
        [Tooltip("Interval between Y-position updates in seconds")]
        private float updateInterval = DEFAULT_UPDATE_INTERVAL; // Configurable update frequency
        [SerializeField]
        [Tooltip("Minimum Y-position change to trigger an update")]
        private float yChangeThreshold = DEFAULT_Y_CHANGE_THRESHOLD; // Configurable threshold for Y change

        private float lastCameraY; // Cached last recorded camera Y-position
        private Coroutine adjustmentCoroutine;
        private bool isTutorialActive; // Tracks current game state

        private void Awake()
        {
            // Validate required references once at start
            if (centerEyeCamera == null)
            {
                Debug.LogError($"{nameof(TargetAdjustingManager)}: CenterEyeCamera not assigned!");
                enabled = false;
                return;
            }
            if (spwanPointsHolder == null || targetPointsHolder == null)
            {
                Debug.LogError($"{nameof(TargetAdjustingManager)}: Neither SpwanManager nor TutorialManager assigned!");
                enabled = false;
                return;
            }
        }

        private void OnEnable()
        {
            ActionManager.GamePointsHeightAdjustment += ToggleTargetGameState;
        }

        private void OnDisable()
        {
            ActionManager.GamePointsHeightAdjustment -= ToggleTargetGameState;
            if (adjustmentCoroutine != null)
            {
                StopCoroutine(adjustmentCoroutine);
            }
        }
        private void ToggleTargetGameState(bool isActive)
        {
            SetGameState(isActive);
        }
        private void SetGameState(bool isTutorial)
        {
            isTutorialActive = isTutorial;
            if (adjustmentCoroutine != null)
            {
                StopCoroutine(adjustmentCoroutine);
            }
            lastCameraY = centerEyeCamera.position.y; // Reset last Y on state change
            adjustmentCoroutine = StartCoroutine(UpdateTargetPointsCoroutine());
        }

        private IEnumerator UpdateTargetPointsCoroutine()
        {
            while (true)
            {
                float currentCameraY = centerEyeCamera.position.y;
                if (Mathf.Abs(currentCameraY - lastCameraY) > yChangeThreshold)
                {
                    float targetY = Mathf.Max(currentCameraY + yOffset, minY);
                    UpdateTargetPositions(targetY);
                    lastCameraY = currentCameraY;
                    // Uncomment for debugging: Debug.Log($"{nameof(TargetAdjustingManager)}: Updated targets to Y={targetY}");
                }

                //yield return new WaitForSeconds(updateInterval);
                yield return new WaitForEndOfFrame();
            }
        }
        private void UpdateTargetPositions(float targetY)
        {
            if (isTutorialActive)
            {
                UpdateTutorialTargets(targetY);
            }
            else if (!isTutorialActive && (spwanPointsHolder != null || targetPointsHolder != null))
            {
                UpdateGameplayTargets(targetY);
            }
        }

        private void UpdateGameplayTargets(float targetY)
        {
            Vector3 previousSpwaningPos = spwanPointsHolder.position;
            previousSpwaningPos = new Vector3(previousSpwaningPos.x, targetY, previousSpwaningPos.z);
            spwanPointsHolder.position = previousSpwaningPos;

            Vector3 previousTargetPos = targetPointsHolder.position;
            previousTargetPos = new Vector3(previousTargetPos.x, targetY, previousTargetPos.z);
            targetPointsHolder.position = previousTargetPos;

            if (yearFrontPointPosition != null)
            {
                float offsetY = 0.6f;
                targetY += offsetY;
                Vector3 previousYearFrontPoint = yearFrontPointPosition.position;
                previousYearFrontPoint = new Vector3(previousYearFrontPoint.x, targetY, previousYearFrontPoint.z);
                yearFrontPointPosition.position = previousYearFrontPoint;
            }
        }

        private void UpdateTutorialTargets(float targetY)
        {
            if (midWaitingPoint != null)
            {
                Vector3 midPos = midWaitingPoint.transform.position;
                midPos.y = targetY;
                midWaitingPoint.transform.position = midPos;
            }
            if (nearPlayerWaitingPoint != null)
            {
                Vector3 nearPos = nearPlayerWaitingPoint.transform.position;
                nearPos.y = targetY;
                nearPlayerWaitingPoint.transform.position = nearPos;
            }
            UpdateGameplayTargets(targetY);
        }
    }
}
