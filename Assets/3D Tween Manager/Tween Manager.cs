using UnityEngine;
using System;

public sealed class TweenManager : ITweenManager
{
    #region Tween Methods

    #region Tween Move Methods 
    public void MoveObject(GameObject target, Vector3 startPos, Vector3 endPos, float duration, bool ignoreTimeScale)
    {
        target.transform.position = startPos;
        LeanTween.move(target, endPos, duration)
                 .setIgnoreTimeScale(ignoreTimeScale);
    }

    public void MoveObject(GameObject target, Vector3 startPos, Vector3 endPos, float duration, bool ignoreTimeScale,
                           Action onStart = null, Action onComplete = null)
    {
        target.transform.position = startPos;
        LeanTween.move(target, endPos, duration)
                 .setIgnoreTimeScale(ignoreTimeScale)
                 .setOnStart(() => { onStart?.Invoke(); })
                 .setOnComplete(() => { onComplete?.Invoke(); });
    }

    public void MoveObject(GameObject target, Vector3 startPos, Vector3 endPos, float duration, bool ignoreTimeScale,
                           LeanTweenType easeType = LeanTweenType.notUsed)
    {
        target.transform.position = startPos;
        LeanTween.move(target, endPos, duration)
                 .setIgnoreTimeScale(ignoreTimeScale)
                 .setEase(easeType);
    }

    public void MoveObject(GameObject target, Vector3 startPos, Vector3 endPos, float duration, bool ignoreTimeScale,
                           LeanTweenType easeType = LeanTweenType.notUsed,
                           Action onStart = null, Action onComplete = null)
    {
        target.transform.position = startPos;
        LeanTween.move(target, endPos, duration)
                 .setIgnoreTimeScale(ignoreTimeScale)
                 .setOnStart(() => { onStart?.Invoke(); })
                 .setEase(easeType)
                 .setOnComplete(() => { onComplete?.Invoke(); });
    }
    //=====
    public void MoveObject(GameObject target, Vector3 startPos, Vector3 endPos, float duration)
    {
        target.transform.position = startPos;
        LeanTween.move(target, endPos, duration);
    }
    public void MoveObject(GameObject target, Vector3 startPos, Vector3 endPos, float duration, Action onStart = null, Action onComplete = null)
    {
        target.transform.position = startPos;
        LeanTween.move(target, endPos, duration)
                 .setOnStart(() => { onStart?.Invoke(); })
                 .setOnComplete(() => { onComplete?.Invoke(); });
    }
    public void MoveObject(GameObject target, Vector3 startPos, Vector3 endPos, float duration,
                      LeanTweenType easeType = LeanTweenType.notUsed)
    {
        target.transform.position = startPos;
        LeanTween.move(target, endPos, duration)
                 .setEase(easeType);
    }

    public void MoveObject(GameObject target, Vector3 startPos, Vector3 endPos, float duration,
                      LeanTweenType easeType = LeanTweenType.notUsed, Action onStart = null, Action onComplete = null)
    {
        target.transform.position = startPos;
        LeanTween.move(target, endPos, duration)
                 .setOnStart(() => { onStart?.Invoke(); })
                 .setEase(easeType)
                 .setOnComplete(() => { onComplete?.Invoke(); });
    }

    #endregion Tween Move Methods

    #region Tween Scale Methods

    public void ScaleObject(GameObject target, Vector3 startScale, Vector3 endScale, float duration, bool ignoreTimeScale)
    {
        target.transform.localScale = startScale;
        LeanTween.scale(target, endScale, duration)
                 .setIgnoreTimeScale(ignoreTimeScale);
    }

    public void ScaleObject(GameObject target, Vector3 startScale, Vector3 endScale, float duration, bool ignoreTimeScale,
                            LeanTweenType easeType = LeanTweenType.notUsed)
    {
        target.transform.localScale = startScale;
        LeanTween.scale(target, endScale, duration)
                 .setIgnoreTimeScale(ignoreTimeScale)
                 .setEase(easeType);
    }

    public void ScaleObject(GameObject target, Vector3 startScale, Vector3 endScale, float duration, bool ignoreTimeScale,
                            Action onStart = null, Action onComplete = null)
    {
        target.transform.localScale = startScale;
        LeanTween.scale(target, endScale, duration)
                 .setIgnoreTimeScale(ignoreTimeScale)
                 .setOnStart(() => { onStart?.Invoke(); })
                 .setOnComplete(() => { onComplete?.Invoke(); });
    }

    public void ScaleObject(GameObject target, Vector3 startScale, Vector3 endScale, float duration, bool ignoreTimeScale,
                            LeanTweenType easeType = LeanTweenType.notUsed,
                            Action onStart = null, Action onComplete = null)
    {
        target.transform.localScale = startScale;
        LeanTween.scale(target, endScale, duration)
                 .setIgnoreTimeScale(ignoreTimeScale)
                 .setOnStart(() => { onStart?.Invoke(); })
                 .setEase(easeType)
                 .setOnComplete(() => { onComplete?.Invoke(); });
    }

    public void ScaleObject(GameObject target, Vector3 startScale, Vector3 endScale, float duration)
    {
        target.transform.localScale = startScale;
        LeanTween.scale(target, endScale, duration);
    }
    public void ScaleObject(GameObject target, Vector3 startScale, Vector3 endScale, float duration,
                    LeanTweenType easeType = LeanTweenType.notUsed)
    {
        target.transform.localScale = startScale;
        LeanTween.scale(target, endScale, duration)
                 .setEase(easeType);
    }
    public void ScaleObject(GameObject target, Vector3 startScale, Vector3 endScale, float duration,
                     Action onStart = null, Action onComplete = null)
    {
        target.transform.localScale = startScale;
        LeanTween.scale(target, endScale, duration)
                 .setOnStart(() => { onStart?.Invoke(); })
                 .setOnComplete(() => { onComplete?.Invoke(); });
    }


    public void ScaleObject(GameObject target, Vector3 startScale, Vector3 endScale, float duration,
                     LeanTweenType easeType = LeanTweenType.notUsed,
                     Action onStart = null, Action onComplete = null)
    {
        target.transform.localScale = startScale;
        LeanTween.scale(target, endScale, duration)
                 .setOnStart(() => { onStart?.Invoke(); })
                 .setEase(easeType)
                 .setOnComplete(() => { onComplete?.Invoke(); });
    }

    #endregion Tween Scale Methods

    #region Tween Rotation Methods

    public void RotateObject(GameObject target, Vector3 startRotation, Vector3 endRotation, float duration, bool ignoreTimeScale,
                         LeanTweenType easeType = LeanTweenType.notUsed)
    {
        target.transform.eulerAngles = startRotation;
        LeanTween.rotate(target, endRotation, duration)
                 .setIgnoreTimeScale(ignoreTimeScale)
                 .setEase(easeType);
    }

    public void RotateObject(GameObject target, Vector3 startRotation, Vector3 endRotation, float duration, bool ignoreTimeScale)
    {
        target.transform.eulerAngles = startRotation;
        LeanTween.rotate(target, endRotation, duration)
                 .setIgnoreTimeScale(ignoreTimeScale);
    }

    public void RotateObject(GameObject target, Vector3 startRotation, Vector3 endRotation, float duration, bool ignoreTimeScale,
                             LeanTweenType easeType = LeanTweenType.notUsed,
                             Action onStart = null, Action onComplete = null)
    {
        target.transform.eulerAngles = startRotation;
        LeanTween.rotate(target, endRotation, duration)
                 .setIgnoreTimeScale(ignoreTimeScale)
                 .setEase(easeType)
                 .setOnStart(() => onStart?.Invoke())
                 .setOnComplete(() => onComplete?.Invoke());
    }

    public void RotateObject(GameObject target, Vector3 startRotation, Vector3 endRotation, float duration, bool ignoreTimeScale,
                             Action onStart = null, Action onComplete = null)
    {
        target.transform.eulerAngles = startRotation;
        LeanTween.rotate(target, endRotation, duration)
                 .setIgnoreTimeScale(ignoreTimeScale)
                 .setOnStart(() => onStart?.Invoke())
                 .setOnComplete(() => onComplete?.Invoke());
    }

    public void RotateObject(GameObject target, Vector3 startRotation, Vector3 endRotation, float duration,
                         LeanTweenType easeType = LeanTweenType.notUsed)
    {
        // Set starting rotation
        target.transform.eulerAngles = startRotation;

        // Run LeanTween rotation
        LeanTween.rotate(target, endRotation, duration)
                 .setEase(easeType);
    }

    public void RotateObject(GameObject target, Vector3 startRotation, Vector3 endRotation, float duration)
    {
        // Set starting rotation
        target.transform.eulerAngles = startRotation;

        // Run LeanTween rotation
        LeanTween.rotate(target, endRotation, duration);
    }
    public void RotateObject(GameObject target, Vector3 startRotation, Vector3 endRotation, float duration,
                         LeanTweenType easeType = LeanTweenType.notUsed,
                         Action onStart = null, Action onComplete = null)
    {
        // Set starting rotation
        target.transform.eulerAngles = startRotation;

        // Run LeanTween rotation
        LeanTween.rotate(target, endRotation, duration)
                 .setEase(easeType)
                 .setOnStart(() => onStart?.Invoke())
                 .setOnComplete(() => onComplete?.Invoke());
    }

    public void RotateObject(GameObject target, Vector3 startRotation, Vector3 endRotation, float duration,
                        Action onStart = null, Action onComplete = null)
    {
        // Set starting rotation
        target.transform.eulerAngles = startRotation;

        // Run LeanTween rotation
        LeanTween.rotate(target, endRotation, duration)
                 .setOnStart(() => onStart?.Invoke())
                 .setOnComplete(() => onComplete?.Invoke());
    }

    #endregion Tween Rotation Methods

    #region Tween Move and Scale Methods

    public void MoveAndScaleObject(GameObject target,
                       Vector3 startPos, Vector3 endPos,
                       Vector3 startScale, Vector3 endScale,
                       float duration, bool ignoreTimeScale,
                       LeanTweenType easeType = LeanTweenType.notUsed,
                       Action onStart = null, Action onComplete = null)
    {
        target.transform.position = startPos;
        target.transform.localScale = startScale;

        int completedTweens = 0;
        onStart?.Invoke();

        LeanTween.move(target, endPos, duration)
                 .setIgnoreTimeScale(ignoreTimeScale)
                 .setEase(easeType)
                 .setOnComplete(() =>
                 {
                     completedTweens++;
                     if (completedTweens >= 2) onComplete?.Invoke();
                 });

        LeanTween.scale(target, endScale, duration)
                 .setIgnoreTimeScale(ignoreTimeScale)
                 .setEase(easeType)
                 .setOnComplete(() =>
                 {
                     completedTweens++;
                     if (completedTweens >= 2) onComplete?.Invoke();
                 });
    }

    public void MoveAndScaleObject(GameObject target,
                            Vector3 startPos, Vector3 endPos,
                            Vector3 startScale, Vector3 endScale,
                            float duration, bool ignoreTimeScale,
                            LeanTweenType easeType = LeanTweenType.notUsed)
    {
        target.transform.position = startPos;
        target.transform.localScale = startScale;

        LeanTween.move(target, endPos, duration)
                 .setIgnoreTimeScale(ignoreTimeScale)
                 .setEase(easeType);

        LeanTween.scale(target, endScale, duration)
                 .setIgnoreTimeScale(ignoreTimeScale)
                 .setEase(easeType);
    }


    public void MoveAndScaleObject(GameObject target,
                            Vector3 startPos, Vector3 endPos,
                            Vector3 startScale, Vector3 endScale,
                            float duration, bool ignoreTimeScale,
                            LeanTweenType moveEaseType = LeanTweenType.notUsed,
                            LeanTweenType scaleEaseType = LeanTweenType.notUsed)
    {
        target.transform.position = startPos;
        target.transform.localScale = startScale;

        LeanTween.move(target, endPos, duration)
                 .setIgnoreTimeScale(ignoreTimeScale)
                 .setEase(moveEaseType);

        LeanTween.scale(target, endScale, duration)
                 .setIgnoreTimeScale(ignoreTimeScale)
                 .setEase(scaleEaseType);
    }


    public void MoveAndScaleObject(GameObject target,
                            Vector3 startPos, Vector3 endPos,
                            Vector3 startScale, Vector3 endScale,
                            float duration, bool ignoreTimeScale,
                            LeanTweenType moveEaseType = LeanTweenType.notUsed,
                            LeanTweenType scaleEaseType = LeanTweenType.notUsed,
                            Action onStart = null, Action onComplete = null)
    {
        target.transform.position = startPos;
        target.transform.localScale = startScale;

        int completedTweens = 0;
        onStart?.Invoke();

        LeanTween.move(target, endPos, duration)
                 .setIgnoreTimeScale(ignoreTimeScale)
                 .setEase(moveEaseType)
                 .setOnComplete(() =>
                 {
                     completedTweens++;
                     if (completedTweens >= 2) onComplete?.Invoke();
                 });

        LeanTween.scale(target, endScale, duration)
                 .setIgnoreTimeScale(ignoreTimeScale)
                 .setEase(scaleEaseType)
                 .setOnComplete(() =>
                 {
                     completedTweens++;
                     if (completedTweens >= 2) onComplete?.Invoke();
                 });
    }


    public void MoveAndScaleObject(GameObject target,
                           Vector3 startPos, Vector3 endPos,
                           Vector3 startScale, Vector3 endScale,
                           float moveDuration, float scaleDuration,
                           bool ignoreTimeScale,
                           LeanTweenType moveEaseType = LeanTweenType.notUsed,
                           LeanTweenType scaleEaseType = LeanTweenType.notUsed,
                           Action onStart = null, Action onComplete = null)
    {
        target.transform.position = startPos;
        target.transform.localScale = startScale;

        int completedTweens = 0;
        onStart?.Invoke();

        LeanTween.move(target, endPos, moveDuration)
                 .setIgnoreTimeScale(ignoreTimeScale)
                 .setEase(moveEaseType)
                 .setOnComplete(() =>
                 {
                     completedTweens++;
                     if (completedTweens >= 2) onComplete?.Invoke();
                 });

        LeanTween.scale(target, endScale, scaleDuration)
                 .setIgnoreTimeScale(ignoreTimeScale)
                 .setEase(scaleEaseType)
                 .setOnComplete(() =>
                 {
                     completedTweens++;
                     if (completedTweens >= 2) onComplete?.Invoke();
                 });
    }

    //=====

    public void MoveAndScaleObject(GameObject target,
                           Vector3 startPos, Vector3 endPos,
                           Vector3 startScale, Vector3 endScale,
                           float duration,
                           LeanTweenType easeType = LeanTweenType.notUsed,
                           Action onStart = null, Action onComplete = null)
    {
        // Set starting values
        target.transform.position = startPos;
        target.transform.localScale = startScale;

        int completedTweens = 0;

        // Call start callback once when animation begins
        onStart?.Invoke();

        // Move tween
        LeanTween.move(target, endPos, duration)
                 .setEase(easeType)
                 .setOnComplete(() =>
                 {
                     completedTweens++;
                     if (completedTweens >= 2) onComplete?.Invoke();
                 });

        // Scale tween
        LeanTween.scale(target, endScale, duration)
                 .setEase(easeType)
                 .setOnComplete(() =>
                 {
                     completedTweens++;
                     if (completedTweens >= 2) onComplete?.Invoke();
                 });
    }

    public void MoveAndScaleObject(GameObject target,
                            Vector3 startPos, Vector3 endPos,
                            Vector3 startScale, Vector3 endScale,
                            float duration,
                            LeanTweenType easeType = LeanTweenType.notUsed)
    {
        // Set starting values
        target.transform.position = startPos;
        target.transform.localScale = startScale;

        // Move tween
        LeanTween.move(target, endPos, duration)
                 .setEase(easeType);

        // Scale tween
        LeanTween.scale(target, endScale, duration)
                 .setEase(easeType);
    }

    public void MoveAndScaleObject(GameObject target,
                            Vector3 startPos, Vector3 endPos,
                            Vector3 startScale, Vector3 endScale,
                            float duration,
                            LeanTweenType moveEaseType = LeanTweenType.notUsed,
                            LeanTweenType scaleEaseType = LeanTweenType.notUsed)
    {
        // Set starting values
        target.transform.position = startPos;
        target.transform.localScale = startScale;

        // Move tween
        LeanTween.move(target, endPos, duration)
                 .setEase(moveEaseType);

        // Scale tween
        LeanTween.scale(target, endScale, duration)
                 .setEase(scaleEaseType);
    }

    public void MoveAndScaleObject(GameObject target,
                            Vector3 startPos, Vector3 endPos,
                            Vector3 startScale, Vector3 endScale,
                            float duration,
                            LeanTweenType moveEaseType = LeanTweenType.notUsed,
                            LeanTweenType scaleEaseType = LeanTweenType.notUsed,
                            Action onStart = null, Action onComplete = null)
    {
        // Set starting values
        target.transform.position = startPos;
        target.transform.localScale = startScale;

        int completedTweens = 0;

        // Call start callback once when animation begins
        onStart?.Invoke();

        // Move tween
        LeanTween.move(target, endPos, duration)
                 .setEase(moveEaseType)
                 .setOnComplete(() =>
                 {
                     completedTweens++;
                     if (completedTweens >= 2) onComplete?.Invoke();
                 });

        // Scale tween
        LeanTween.scale(target, endScale, duration)
                 .setEase(scaleEaseType)
                 .setOnComplete(() =>
                 {
                     completedTweens++;
                     if (completedTweens >= 2) onComplete?.Invoke();
                 });
    }


    public void MoveAndScaleObject(GameObject target,
                           Vector3 startPos, Vector3 endPos,
                           Vector3 startScale, Vector3 endScale,
                           float moveDuration, float scaleDuration,
                           LeanTweenType moveEaseType = LeanTweenType.notUsed,
                           LeanTweenType scaleEaseType = LeanTweenType.notUsed,
                           Action onStart = null, Action onComplete = null)
    {
        // Set starting values
        target.transform.position = startPos;
        target.transform.localScale = startScale;

        int completedTweens = 0;

        // Call start callback once when animation begins
        onStart?.Invoke();

        // Move tween
        LeanTween.move(target, endPos, moveDuration)
                 .setEase(moveEaseType)
                 .setOnComplete(() =>
                 {
                     completedTweens++;
                     if (completedTweens >= 2) onComplete?.Invoke();
                 });

        // Scale tween
        LeanTween.scale(target, endScale, scaleDuration)
                 .setEase(scaleEaseType)
                 .setOnComplete(() =>
                 {
                     completedTweens++;
                     if (completedTweens >= 2) onComplete?.Invoke();
                 });
    }


    #endregion Tween Move and Scale Methods

    #region Tween Pause and Play
    public void PauseTweens(GameObject target)
    {
        LeanTween.pause(target);
    }

    public void ResumeTweens(GameObject target)
    {
        LeanTween.resume(target);
    }

    #endregion Tween Pause and Play

    #region Tween Fade Methods

    // 1. Simple fade with ignoreTimeScale
    public void FadeCanvasGroup(CanvasGroup target, float startAlpha, float endAlpha, float duration, bool ignoreTimeScale)
    {
        target.alpha = startAlpha;
        LeanTween.alphaCanvas(target, endAlpha, duration)
                 .setIgnoreTimeScale(ignoreTimeScale);
    }

    // 2. Fade with ignoreTimeScale + easeType
    public void FadeCanvasGroup(CanvasGroup target, float startAlpha, float endAlpha, float duration, bool ignoreTimeScale,
                                LeanTweenType easeType = LeanTweenType.notUsed)
    {
        target.alpha = startAlpha;
        LeanTween.alphaCanvas(target, endAlpha, duration)
                 .setIgnoreTimeScale(ignoreTimeScale)
                 .setEase(easeType);
    }

    // 3. Fade with ignoreTimeScale + callbacks
    public void FadeCanvasGroup(CanvasGroup target, float startAlpha, float endAlpha, float duration, bool ignoreTimeScale,
                                Action onStart = null, Action onComplete = null)
    {
        target.alpha = startAlpha;
        LeanTween.alphaCanvas(target, endAlpha, duration)
                 .setIgnoreTimeScale(ignoreTimeScale)
                 .setOnStart(() => { onStart?.Invoke(); })
                 .setOnComplete(() => { onComplete?.Invoke(); });
    }

    // 4. Fade with ignoreTimeScale + easeType + callbacks
    public void FadeCanvasGroup(CanvasGroup target, float startAlpha, float endAlpha, float duration, bool ignoreTimeScale,
                                LeanTweenType easeType = LeanTweenType.notUsed,
                                Action onStart = null, Action onComplete = null)
    {
        target.alpha = startAlpha;
        LeanTween.alphaCanvas(target, endAlpha, duration)
                 .setIgnoreTimeScale(ignoreTimeScale)
                 .setOnStart(() => { onStart?.Invoke(); })
                 .setEase(easeType)
                 .setOnComplete(() => { onComplete?.Invoke(); });
    }

    // 5. Simple fade without ignoreTimeScale
    public void FadeCanvasGroup(CanvasGroup target, float startAlpha, float endAlpha, float duration)
    {
        target.alpha = startAlpha;
        LeanTween.alphaCanvas(target, endAlpha, duration);
    }

    // 6. Fade with easeType
    public void FadeCanvasGroup(CanvasGroup target, float startAlpha, float endAlpha, float duration,
                                LeanTweenType easeType = LeanTweenType.notUsed)
    {
        target.alpha = startAlpha;
        LeanTween.alphaCanvas(target, endAlpha, duration)
                 .setEase(easeType);
    }

    // 7. Fade with callbacks
    public void FadeCanvasGroup(CanvasGroup target, float startAlpha, float endAlpha, float duration,
                                Action onStart = null, Action onComplete = null)
    {
        target.alpha = startAlpha;
        LeanTween.alphaCanvas(target, endAlpha, duration)
                 .setOnStart(() => { onStart?.Invoke(); })
                 .setOnComplete(() => { onComplete?.Invoke(); });
    }

    // 8. Fade with easeType + callbacks
    public void FadeCanvasGroup(CanvasGroup target, float startAlpha, float endAlpha, float duration,
                                LeanTweenType easeType = LeanTweenType.notUsed,
                                Action onStart = null, Action onComplete = null)
    {
        target.alpha = startAlpha;
        LeanTween.alphaCanvas(target, endAlpha, duration)
                 .setOnStart(() => { onStart?.Invoke(); })
                 .setEase(easeType)
                 .setOnComplete(() => { onComplete?.Invoke(); });
    }

    #endregion Tween Fade Methods

    #endregion Tween Methods

}
