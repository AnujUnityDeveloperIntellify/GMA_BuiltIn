using UISystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace DivoPOC.GrabEmAll
{
    public class UIHelper : MonoBehaviour
    {
        #region FillImage

        public void fillInImageHover(Image imageToFill)
        {
            DivoPOC.ActionManager.OnPlayCustomSound?.Invoke("ButtonHover", 1f);
            ActionManager.OnPerformUIHaptics?.Invoke(0.1f, 0.1f, 0.1f);

            //Start tween from current fillAmount to 1(fully filled) in 0.2 seconds
            LeanTween.value(gameObject, imageToFill.fillAmount, 1f, 0.1f).setIgnoreTimeScale(true).setOnUpdate((float val) =>
            {
                imageToFill.fillAmount = val;
            });
        }
        public void fillOutImageHover(Image imageToFill)
        {
            //Start tween from current fillAmount to 1(fully filled) in 0.2 seconds
            LeanTween.value(gameObject, imageToFill.fillAmount, 0f, 0.1f).setIgnoreTimeScale(true).setOnUpdate((float val) =>
            {
                imageToFill.fillAmount = val;
            });
        }

        #endregion
    }
}