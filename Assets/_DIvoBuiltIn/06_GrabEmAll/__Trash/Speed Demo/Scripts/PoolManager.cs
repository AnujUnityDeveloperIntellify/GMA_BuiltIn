using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace SpeedVariation
{
    public enum YearEnum
    {
        Y2024,Y2023,Y2022, Y2021, Y2020, Y2019, Y2018, Y2017, Y2016, Y2015, Y2014
    }
    public class PoolManager : MonoBehaviour
    {
        [SerializeField] private TextMeshPro _yearTxt;
        [SerializeField] private TextMeshPro _speedTxt;
        [SerializeField] private Transform haltPoint;
        [SerializeField] List<DivoPOC.GrabEmAll.TrackPoints> TrackPoints = new List<DivoPOC.GrabEmAll.TrackPoints>();
        [SerializeField] private List<Material> YearMat;
        [SerializeField] private List<BallController> ballHolder = new List<BallController>(); 
        private Queue<BallController> allBallQueue = new Queue<BallController>();
        private Queue<BallController> currentYearBalls = new Queue<BallController>();
        private int currentYear = 0;
        private float basedSpeed = 4.5f;
        private float speedIncrementRate = 0.4f;
        private float trackDistance;
        private float YearlapsDuration = 28f;
        private void Awake()
        {
            InitializedPooledObject();
        }
        private void Start()
        {
            Precalculate();
        }
        private void OnEnable()
        {
            ActionManager.OnSetToHalt += SetToHalt;
            ActionManager.OnCalculateNewYearSpeed += CalculateNewYearSpeed;
        }

        private void OnDisable()
        {
            ActionManager.OnSetToHalt -= SetToHalt;
            ActionManager.OnCalculateNewYearSpeed -= CalculateNewYearSpeed;
        }

        private void SetToHalt(BallController ball)
        {
            allBallQueue.Enqueue(ball);
            ball.transform.position = haltPoint.position;
            ball.gameObject.SetActive(false);
        }
        private void InitializedPooledObject()
        {
            allBallQueue.Clear();
            foreach (var ball in ballHolder) 
            {
                SetToHalt(ball);
            }
        }
        private void Precalculate()
        {
            trackDistance = Vector3.Distance(TrackPoints[0].SpwanPoints.position, TrackPoints[0].TargetPoints.position);
            currentYear = 0;
        }
        private void CalculateNewYearSpeed()
        {
            Material mat = YearMat[currentYear % YearMat.Count];
            float currentSpeed = basedSpeed + currentYear * speedIncrementRate;
            float distanceCovered = trackDistance;
            float timeTakenToTravel = distanceCovered / currentSpeed;
            float spawnInterval = timeTakenToTravel * 0.3f;
            YearlapsDuration = 30 - (1 * timeTakenToTravel);
            int numberOfObject = Mathf.FloorToInt(YearlapsDuration/spawnInterval);
            //Debug.LogError($"Year Start {currentYear}: Speed = {currentSpeed}, Travel Time = {timeTakenToTravel}s, Spawn Interval = {spawnInterval}s, Balls = {numberOfObject} ");
            Debug.LogError($"Year Start {currentYear}, Spawn Interval = {spawnInterval}s, Balls = {numberOfObject}, Year Laps = {YearlapsDuration}, Ball TAT = {timeTakenToTravel}");
            UpdateYearandSpeedUI(currentYear, currentSpeed);
            currentYear++;
            currentYearBalls.Clear();

            for (int i = 0; i < numberOfObject && allBallQueue.Count > 0; i++)
            {
                BallController ball = allBallQueue.Dequeue();
                currentYearBalls.Enqueue(ball);
            }
            StartCoroutine(SpawnYearBalls(spawnInterval,currentSpeed, mat));
        }
        private IEnumerator SpawnYearBalls(float spawnInterval,float currentSpeed,Material mat)
        {
            while (currentYearBalls.Count > 0)
            {
                BallController ball = currentYearBalls.Dequeue();
                ball.gameObject.SetActive(true);
                yield return null;
                DivoPOC.GrabEmAll.TrackPoints randomTrackPoint = TrackPoints[UnityEngine.Random.Range(0, TrackPoints.Count)];
                ball.transform.position = randomTrackPoint.SpwanPoints.position;
                ball.Init(randomTrackPoint.TargetPoints, currentSpeed,mat);
                yield return new WaitForSeconds(spawnInterval);
            }
            Debug.LogError("=====> Complete Year");
        }
        private void UpdateYearandSpeedUI(int currentYear ,float currentspeed)
        {
            _speedTxt.text = $"{currentspeed:F2} m/s";
            YearEnum year = (YearEnum)currentYear;
            string x = year.ToString().Substring(1);
            _yearTxt.text = x;
        }
    }
}

