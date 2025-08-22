using System.Collections.Generic;
using System.Linq;
using TMPro;
using UISystem;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace DivoPOC.GrabEmAll
{
    [System.Serializable]
    public class HintContainter
    {
        public Image hintImage;
        public TextMeshProUGUI hintTxt;
    }
    public class HintData
    {
        public Sprite hintSprite;
        public string hintType;
    }
    public class GMAYearDescriptionMenuScreen : UISystem.Screen
    {
        [SerializeField] private TextMeshProUGUI yearTxt;
        [SerializeField] private Button closedBtn;
        [SerializeField] private List<HintContainter> hintImageHolders;
        [SerializeField] private Sprite blankSprite;
        public override void Awake()
        {
            base.Awake();
            Init();
        }
        private void OnEnable()
        {
            ActionManager.OnLoadNewYearHints += SetNewYearsSprites;
        }
        private void OnDisable()
        {
            ActionManager.OnLoadNewYearHints -= SetNewYearsSprites;
        }
        public override void Show()
        {
            meshObject.SetActive(true);
            base.Show();
        }

        public override void Hide()
        {
            meshObject.SetActive(false);
            base.Hide();
        }

        public override void Redraw()
        {
            base.Redraw();
        }
        #region Custom Methods
        private void Init()
        {
            closedBtn.onClick.AddListener(ClosedMenu);
        }
        [ContextMenu("Closed")]
        private void ClosedMenu()
        {
            ViewController.Instance.HideScreen(ScreenName.GMAYearDescriptionMenuScreen);
            ActionManager.OnGameResume?.Invoke();
        }
        #endregion Custom Methods

        #region Year Hints

        private void SetNewYearsSprites(List<HintData> _allCorrectHints)
        {
            _allCorrectHints = _allCorrectHints.Take(4).ToList();
            yearTxt.text = ActionManager.GetCurrenYear?.Invoke().ToString();
            var randomIndexes = GetDistinctRandomNumbers(0, 8, 4);
            int hintPointer = 0;
            for (int i = 0; i < hintImageHolders.Count; i++)
            {
                bool hasHint = randomIndexes.Contains(i) && hintPointer < _allCorrectHints.Count;
                hintImageHolders[i].hintImage.sprite = hasHint ? _allCorrectHints[hintPointer].hintSprite : blankSprite;
                hintImageHolders[i].hintTxt.text = hasHint ? _allCorrectHints[hintPointer++].hintType : "";
            }
            ActionManager.OnGamePause?.Invoke();
            ViewController.Instance.ChangeScreen(ScreenName.GMAYearDescriptionMenuScreen);
        }
        private List<int> GetDistinctRandomNumbers(int min, int max, int n)
        {
            HashSet<int> numbers = new HashSet<int>();
            while (numbers.Count < n)
            {
                int randomNum = UnityEngine.Random.Range(min, max);
                numbers.Add(randomNum);
            }
            return new List<int>(numbers);
        }
        #endregion Year Hints
    }
}