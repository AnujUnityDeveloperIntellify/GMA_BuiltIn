using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace UISystem
{

    public class ViewController : MonoBehaviour
    {
        public static ViewController Instance;
        Screen currentView;
        Screen previousView;
        [SerializeField] ScreenName initScreen;
        [SerializeField] List<ScreenView> screens = new List<ScreenView>();
        [SerializeField] List<PopupView> popups = new List<PopupView>();
        [SerializeField] NavBar navBar;
        [SerializeField] Popup toast;

        #region custom msg

        [HideInInspector] public string loadingMsg = "Loading...";
        [HideInInspector] public string tipMsg = string.Empty;
        [HideInInspector] public float tipShowTime = 3;
        [HideInInspector] public bool locationGif = false;


        #endregion

        [System.Serializable]
        public struct ScreenView
        {
            public Screen screen;
            public ScreenName screenName;
            public bool hasNavBar;
        }

        [System.Serializable]
        public struct PopupView
        {
            public Popup popup;
            public PopupName popupName;
        }

        void Awake() => Instance = this;

        void Start() => Init();

        public void ShowPopup(PopupName popupName)
        {
            for (int indexOfpopup = 0; indexOfpopup < popups.Count; indexOfpopup++)
                popups[indexOfpopup].popup.Disable();

            popups[GetPopupIndex(popupName)].popup.Show();
        }
        public void ShowTipPopupOnly(PopupName popupName, string msg, float sec, bool gifValue)
        {
            tipMsg = msg;
            tipShowTime = sec;
            locationGif = gifValue;
            popups[GetPopupIndex(popupName)].popup.Show();
        }

        public void HidePopup(PopupName popupName)
        {
            popups[GetPopupIndex(popupName)].popup.Hide();
        }

        public void ShowToast(string description, float delay = 3)
        {
            toast.Fill(description);
            toast.Show();
        }

        public void ChangeScreen(ScreenName screen)
        {


            if (currentView != null)
            {
                previousView = currentView;

                previousView.Hide();

                currentView = screens[GetScreenIndex(screen)].screen;

                currentView.Show();

            }
            else
            {
                Debug.Log(GetScreenIndex(screen) + " -- " + screen);

                currentView = screens[GetScreenIndex(screen)].screen;

                currentView.Show();

            }
        }
        public void HideScreen(ScreenName screen)
        {

            currentView.Hide();

        }
        public void HideSelectedScreen(ScreenName screen)
        {

            currentView = screens[GetScreenIndex(screen)].screen;
            currentView.Hide();
        }
        void Update()
        {
            /* if (Input.GetKeyDown (KeyCode.Escape)) {
                 for (int i = 0; i < popups.Count; i++) {
                     if (popups[i].popup.isActive) {
                         popups[i].popup.Hide ();
                         return;
                     }
                 }
             }*/

            //Temporary Code.
#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(0);
            }
#endif
        }

        int GetScreenIndex(ScreenName screen)
        {
            return screens.FindIndex(
                delegate (ScreenView screenView)
                {
                    return screenView.screenName.Equals(screen);
                });
        }

        int GetPopupIndex(PopupName popup)
        {
            return popups.FindIndex(
                delegate (PopupView popupView)
                {
                    return popupView.popupName.Equals(popup);
                });
        }

        public void RedrawView() => currentView.Redraw();

        private void Init()
        {
            for (int indexOfScreen = 0; indexOfScreen < screens.Count; indexOfScreen++)
            {

                screens[indexOfScreen].screen.Disable();
            }

            for (int indexOfpopup = 0; indexOfpopup < popups.Count; indexOfpopup++)
                popups[indexOfpopup].popup.Disable();

            if (initScreen != ScreenName.None)
            {
                ChangeScreen(initScreen);
            }
            // popups[GetPopupIndex(PopupName.LoadingPopup)].popup.Show();
        }

        public void DisableScreen()
        {
            for (int i = 0; i < screens.Count; i++)
            {
                screens[i].screen.gameObject.GetComponent<GraphicRaycaster>().enabled = false;

            }
        }
        public void EnableScreen()
        {
            for (int i = 0; i < screens.Count; i++)
            {
                screens[i].screen.gameObject.GetComponent<GraphicRaycaster>().enabled = true;

            }
        }



        // public void ShowPopup(string title, string description)
        // {
        //     toast.Show(title, description);
        // }

        // public void HidePopup()
        // {
        //     toast.Hide();
        // }

        // ViewManager.Instance.GetViewComponent<ViewHunting>().ToggleChipsPopup(true);
        public T GetScreen<T>(ScreenName sName) => (T)screens[GetScreenIndex(sName)].screen.GetComponent<T>();
        public T GetPopup<T>(PopupName sName) => (T)popups[GetPopupIndex(sName)].popup.GetComponent<T>();


        #region Convert Hascode to color

        public static Color GetColorFromHashCode(string _colorCode)
        {
            if (ColorUtility.TryParseHtmlString(_colorCode, out Color newColor))
            {
                return newColor; // Successfully parsed the color
            }
            Debug.LogError($"Invalid Color Code: {_colorCode}");
            return Color.white;
        }
        public static void SetRTLForChildren(GameObject parant, bool rtlValue)
        {
            //Debug.Log($"****SetRTLForChildren : parant {parant} = bool {rtlValue}");
            TextMeshProUGUI[] textComponents = parant.GetComponentsInChildren<TextMeshProUGUI>();
            foreach (TextMeshProUGUI text in textComponents)
            {
                //Debug.Log("**** :" + text.name);
                text.isRightToLeftText = rtlValue;
            }
        }

        #endregion
    }
}