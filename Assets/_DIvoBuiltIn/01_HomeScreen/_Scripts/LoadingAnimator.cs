using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DivoPOC.HomeScreen
{
    public class LoadingAnimator : UISystem.Screen
    {
        public Image targetImage; // Image component in UI
        public List<Sprite> gifFrames; // Drag your gif frame sprites here
        public float frameDelay = 0.1f; // Delay between frames

        private Coroutine playRoutine;

        public override void Awake()
        {
            base.Awake();
        }

        public override void Show()
        {
            meshObject.SetActive(true);
            PlayGif();
            base.Show();
        }

        public override void Hide()
        {
            meshObject.SetActive(false);
            StopGif();

            base.Hide();
        }

        public override void Redraw()
        {
            base.Redraw();
        }

        public void PlayGif()
        {
            if (gifFrames.Count == 0 || targetImage == null)
            {
                Debug.LogWarning("No GIF frames or Image target assigned.");
                return;
            }

            if (playRoutine != null)
                StopCoroutine(playRoutine);

            playRoutine = StartCoroutine(PlayGifCoroutine());
        }

        public void StopGif(bool resetToFirstFrame = true)
        {
            if (playRoutine != null)
            {
                StopCoroutine(playRoutine);
                playRoutine = null;
            }

            if (resetToFirstFrame && gifFrames.Count > 0)
            {
                targetImage.sprite = gifFrames[0];
            }
        }

        private IEnumerator PlayGifCoroutine()
        {
            int index = 0;
            while (true)
            {
                targetImage.sprite = gifFrames[index];
                index = (index + 1) % gifFrames.Count;
                yield return new WaitForSeconds(frameDelay);
            }
        }
    }
}