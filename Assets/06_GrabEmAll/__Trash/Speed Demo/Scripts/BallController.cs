using Meta.XR.MRUtilityKit.SceneDecorator;
using Oculus.Voice.Bindings.Android;
using TMPro;
using UnityEngine;

namespace SpeedVariation
{
    public class BallController : MonoBehaviour
    {
        private Transform TargetPoint;
        private float moveSpeed;
        private bool isMoving;
        private MeshRenderer mr;
        private void Awake()
        {
            TargetPoint = null;
            moveSpeed = 0.0f;
            isMoving = false;
            mr= transform.GetComponent<MeshRenderer>();
        }
        private void Update()
        {
            if (!isMoving)
                return;

            transform.position = Vector3.MoveTowards(transform.position, TargetPoint.position, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, TargetPoint.position) < 0.01f)
            {
                ReachedToTarget();
            }
        }
        internal void StopMoving()
        {
            isMoving = false;
        }
        internal void Init(Transform target, float speed,Material mat)
        {
            if(target !=null)
            { 
                TargetPoint = target;
                moveSpeed = speed;
                isMoving = true;
                mr.material = mat;
            }
        }
        private void ReachedToTarget()
        {
            StopMoving();
            ActionManager.OnSetToHalt?.Invoke(this);
        }
    }
}

