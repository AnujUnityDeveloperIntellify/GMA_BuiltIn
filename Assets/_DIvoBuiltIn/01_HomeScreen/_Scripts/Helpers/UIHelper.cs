
using UISystem;
using UnityEngine;
using UnityEngine.UI;

namespace DivoPOC.HomeScreen
{
    public class UIHelper : MonoBehaviour
    {

        #region Unity Methods

        private void Update()
        {
            // Check X button on right controller
            bool isXPressed = IsXButtonPressed();

            // Handle X button press
            if (isXPressed && !wasXPressed)
            {
                Debug.Log("X Button Pressed!");
                HandleXButtonPress();
            }
            // Handle X button release
            else if (!isXPressed && wasXPressed)
            {
                Debug.Log("X Button Released!");
                HandleXButtonRelease();
            }
            wasXPressed = isXPressed;
        }
        #endregion

        #region FillImage

        public void fillInImageHover(Image imageToFill)
        {
            ActionManager.OnPlayCustomSound?.Invoke("ButtonHover", 0.8f);
           // ActionManagerNew.OnTriggerHapticFeedback?.Invoke(.1f, .1f);


            //fill In the image on hover
            LeanTween.value(gameObject, imageToFill.fillAmount, 1f, 0.1f).setOnUpdate((float val) =>
            {
                imageToFill.fillAmount = val;
            });
        }
        public void fillOutImageHover(Image imageToFill)
        {
            //fill out the image on remove hover
            LeanTween.value(gameObject, imageToFill.fillAmount, 0f, 0.1f).setOnUpdate((float val) =>
            {
                imageToFill.fillAmount = val;
            });
        }

        #endregion

        #region Press X Handle
        private bool wasXPressed = false;
        private bool IsXButtonPressed()
        {
            if (UnityEngine.XR.InputDevices.GetDeviceAtXRNode(UnityEngine.XR.XRNode.LeftHand).TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryButton, out bool buttonValue))
                return buttonValue;
            return false;
        }

        private void HandleXButtonPress()
        {
            ViewController.Instance.ChangeScreen(ScreenName.HSLeftThumbTutorialScreen);
        }

        private void HandleXButtonRelease()
        {
            // Your custom A button release logic
        }
        #endregion
    }
}