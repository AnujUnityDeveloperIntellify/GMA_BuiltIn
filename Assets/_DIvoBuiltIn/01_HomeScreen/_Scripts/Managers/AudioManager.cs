using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DivoPOC
{
    public class AudioManager : MonoBehaviour
    {
        #region Variables
        [SerializeField] private AudioConfigSO audioConfig;
        [SerializeField] private AudioSource backgroundAudioSource;
        [SerializeField] private AudioSource clipAudioSource;
        [SerializeField] private AudioSource timerSoundAudioSource;
        [SerializeField] private String defaultBackgroundClipName = "Official-Anthem"; // Default for scene
        [SerializeField] private float backgroundVolume = 0.5f; // Default volume for background music
        [SerializeField] private float clipVolume = 1.0f; // Default volume for sound effects
        [SerializeField] private float timerAudiofadeDuration = 0.2f; // Duration for volume fade (in seconds)
        Coroutine timerSoundCoroutine;
        #endregion Variables

        #region Unity Methods
        private void Awake()
        {
            if (backgroundAudioSource == null || clipAudioSource == null)
            {
                Debug.LogError("AudioManager: backgroundAudioSource or clipAudioSource is not assigned.", this);
            }

            if (audioConfig == null)
            {
                Debug.LogError("AudioManager: audioConfig is not assigned.", this);
            }

            PlayBackgroundMusic(defaultBackgroundClipName, backgroundVolume);
        }

        private void OnEnable()
        {
            ActionManager.OnPlayCustomSound += PlaySound;
            ActionManager.OnPlayCustomTimerSound += PlayTimerSound; // For timer sounds
            ActionManager.OnStopCustomTimerSound += StopTimerSound; // For timer sounds
            ActionManager.OnPlayBackgroundMusic += PlayBackgroundMusic;
            ActionManager.OnStopBackgroundMusic += StopBackgroundMusic;
            ActionManager.GetClipLength += GetClipLength;
        }

        private void OnDisable()
        {
            ActionManager.OnPlayCustomSound -= PlaySound;
            ActionManager.OnPlayCustomTimerSound -= PlayTimerSound; // For timer sounds
            ActionManager.OnStopCustomTimerSound -= StopTimerSound; // For timer sounds
            ActionManager.OnPlayBackgroundMusic -= PlayBackgroundMusic;
            ActionManager.OnStopBackgroundMusic -= StopBackgroundMusic;
            ActionManager.GetClipLength -= GetClipLength;
        }
        #endregion Unity Methods

        #region Custom Methods
        /// <summary>
        /// you can call this method like this: DivoPOC.ActionManager.OnPlayCustomSound?.Invoke(DivoPOC.AudioClipName.Recycling, 0.5f); 
        /// </summary>
        private void PlaySound(string clipName, float _volume = 1.0f)
        {
            if (audioConfig == null)
            {
                Debug.LogError("AudioManager: audioConfig is null. Cannot play sound.", this);
                return;
            }

            var curentEnvirement = audioConfig.audioDatas[GetCurrentScene()];
            var entry = curentEnvirement.audioEntries.Find(e => e.clipName == clipName);// && !e.isBackgroundMusic);

            if (entry.clip != null)
            {
                Debug.Log($"Playing sound: {clipName}", this);
                clipAudioSource.volume = _volume;
                clipAudioSource.PlayOneShot(entry.clip);
            }
            else
            {
                Debug.LogWarning($"No AudioClip found for {clipName} in audioConfig.", this);
            }
        }

        /// <summary>
        /// you can call this method like this: DivoPOC.ActionManager.OnPlayCustomSound?.Invoke(DivoPOC.AudioClipName.Recycling, 0.5f); 
        /// </summary>
        private void PlayTimerSound(string clipName, float _volume = 1.0f)
        {
            if (timerSoundCoroutine != null)
            {
                timerSoundAudioSource.Stop();
                StopCoroutine(timerSoundCoroutine);
            }
            timerSoundCoroutine = StartCoroutine(PlayTimerSoundCoroutine(clipName, _volume));
        }
        private void StopTimerSound()
        {
            if (timerSoundCoroutine != null)
            {
                StopCoroutine(timerSoundCoroutine);
            }
            timerSoundAudioSource.Stop();
            backgroundAudioSource.volume = backgroundVolume;
        }

        private IEnumerator PlayTimerSoundCoroutine(string clipName, float _volume = 1.0f)
        {
            if (audioConfig == null)
            {
                Debug.LogError("AudioManager: audioConfig is null. Cannot play sound.", this);
                yield break;
            }

            var curentEnvirement = audioConfig.audioDatas[GetCurrentScene()];
            var entry = curentEnvirement.audioEntries.Find(e => e.clipName == clipName);

            if (entry.clip != null)
            {
                float currentBGVolume = 0f;
                Debug.Log($"Playing sound: {clipName}", this);
                float clipLength = entry.clip.length;

                // Fade down background audio
                if (backgroundAudioSource != null)
                {
                    currentBGVolume = backgroundAudioSource.volume;
                    LeanTween.value(backgroundAudioSource.volume, 0.1f, timerAudiofadeDuration)
                        .setOnUpdate((float val) => backgroundAudioSource.volume = val)
                        .setEase(LeanTweenType.easeInQuad);
                    yield return new WaitForSeconds(timerAudiofadeDuration);
                }

                // Play timer sound
                timerSoundAudioSource.volume = _volume;
                timerSoundAudioSource.PlayOneShot(entry.clip);
                yield return new WaitForSeconds(clipLength);

                // Fade up background audio
                if (backgroundAudioSource != null)
                {
                    LeanTween.value(backgroundAudioSource.volume, currentBGVolume, timerAudiofadeDuration)
                        .setOnUpdate((float val) => backgroundAudioSource.volume = val)
                        .setEase(LeanTweenType.easeOutQuad);
                    yield return new WaitForSeconds(timerAudiofadeDuration);
                }
            }
            else
            {
                Debug.LogWarning($"No AudioClip found for {clipName} in audioConfig.", this);
            }
            yield return null;
        }

        /// <summary>
        /// you can call this method like this: DivoPOC.ActionManager.PlayBackgroundMusic?.Invoke(DivoPOC.AudioClipName.BGMName, 1.0f);
        /// </summary>
        private void PlayBackgroundMusic(string clipName, float _volume = 1.0f)
        {
            if (backgroundAudioSource == null)
            {
                Debug.LogError("AudioManager: backgroundAudioSource is null.", this);
                return;
            }

            if (audioConfig == null)
            {
                Debug.LogError("AudioManager: audioConfig is null.", this);
                return;
            }

            var curentEnvirement = audioConfig.audioDatas[GetCurrentScene()];

            Debug.Log($"Current Environment: {curentEnvirement.name}", this);
            var entry = curentEnvirement.audioEntries.Find(e => e.clipName == clipName);// && e.isBackgroundMusic);

            Debug.Log($"Found entry: {entry.clipName} ");

            if (entry.clip != null)
            {
                Debug.Log($"Playing background music: {clipName}", this);
                backgroundAudioSource.clip = entry.clip;
                backgroundAudioSource.volume = _volume;
                backgroundAudioSource.loop = true;
                backgroundAudioSource.playOnAwake = true;
                backgroundAudioSource.Play();
            }
            else
            {
                Debug.LogWarning($"No background AudioClip found for {clipName} in audioConfig.", this);
            }
        }

        private void StopBackgroundMusic()
        {
            if (backgroundAudioSource == null)
            {
                Debug.LogError("AudioManager: backgroundAudioSource is null.", this);
                return;
            }

            if (backgroundAudioSource.isPlaying)
            {
                Debug.Log("Pausing background music.", this);
                backgroundAudioSource.Stop();
            }
            else
            {
                Debug.Log("Background music is already paused.", this);
            }
        }
        /// <summary>
        /// Return a length of a Clip which named Passed
        /// </summary>
        /// <param name="clipName">Name of Clip</param>
        /// <returns></returns>
        private float GetClipLength(string clipName)
        {
            float clipLength = 0;
            if (audioConfig == null)
            {
                Debug.LogError("AudioManager: audioConfig is null. Cannot play sound.", this);
                return 0f;
            }

            var curentEnvirement = audioConfig.audioDatas[GetCurrentScene()];
            var entry = curentEnvirement.audioEntries.Find(e => e.clipName == clipName);// && !e.isBackgroundMusic);

            if (entry.clip != null)
            {
                Debug.Log($"Clip Length: {entry.clip.length}", this);
                clipLength = entry.clip.length;
            }
            else
            {
                Debug.LogWarning($"No AudioClip found for {clipName} in audioConfig.", this);
            }

            return clipLength;
        }
        #endregion Custom Methods

        int GetCurrentScene()
        {
            return SceneManager.GetActiveScene().buildIndex;
        }

    }


}