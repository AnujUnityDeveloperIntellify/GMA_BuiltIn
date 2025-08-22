using UISystem;
using UnityEngine;
using UnityEngine.UI;

namespace DivoPOC.GrabEmAll
{
    public class GMAChooseCorrectGuidedScreen : UISystem.Screen
    {
        public override void Awake()
        {
            base.Awake();
            Init();
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

        }

        #endregion Custom Methods
    }
}

