using UnityEngine;
using System;

public interface ITweenManager
{
    // Move Abstract Methods
    void MoveObject(GameObject target, Vector3 startPos, Vector3 endPos, float duration);
    
    void MoveObject(GameObject target, Vector3 startPos, Vector3 endPos, float duration, LeanTweenType easeType);

    void MoveObject(GameObject target, Vector3 startPos, Vector3 endPos, float duration, Action onStart = null, Action onComplete = null);

    void MoveObject(GameObject target, Vector3 startPos, Vector3 endPos, float duration,
                      LeanTweenType easeType , Action onStart = null, Action onComplete = null);

    void MoveObject(GameObject target, Vector3 startPos, Vector3 endPos, float duration, bool ignoreTimeScale);

    void MoveObject(GameObject target, Vector3 startPos, Vector3 endPos, float duration, bool ignoreTimeScale,
                           Action onStart = null, Action onComplete = null);

    void MoveObject(GameObject target, Vector3 startPos, Vector3 endPos, float duration, bool ignoreTimeScale,
                           LeanTweenType easeType);

    void MoveObject(GameObject target, Vector3 startPos, Vector3 endPos, float duration, bool ignoreTimeScale,
                           LeanTweenType easeType, Action onStart = null, Action onComplete = null);

    // Scale Abstract Methods

    void ScaleObject(GameObject target, Vector3 startScale, Vector3 endScale, float duration);

    void ScaleObject(GameObject target, Vector3 startScale, Vector3 endScale, float duration,
                    LeanTweenType easeType);

    void ScaleObject(GameObject target, Vector3 startScale, Vector3 endScale, float duration,
                     Action onStart = null, Action onComplete = null);

    void ScaleObject(GameObject target, Vector3 startScale, Vector3 endScale, float duration,
                    LeanTweenType easeType,
                    Action onStart = null, Action onComplete = null);

    void ScaleObject(GameObject target, Vector3 startScale, Vector3 endScale, float duration, bool ignoreTimeScale);

    void ScaleObject(GameObject target, Vector3 startScale, Vector3 endScale, float duration, bool ignoreTimeScale, LeanTweenType easeType);

    void ScaleObject(GameObject target, Vector3 startScale, Vector3 endScale, float duration, bool ignoreTimeScale,
                            Action onStart = null, Action onComplete = null);

    void ScaleObject(GameObject target, Vector3 startScale, Vector3 endScale, float duration, bool ignoreTimeScale, LeanTweenType easeType,
                            Action onStart = null, Action onComplete = null);

    // Move and Scale Abstract Methods

    void MoveAndScaleObject(GameObject target,
                           Vector3 startPos, Vector3 endPos,
                           Vector3 startScale, Vector3 endScale,
                           float duration,
                           LeanTweenType easeType,
                           Action onStart = null, Action onComplete = null);

    void MoveAndScaleObject(GameObject target,
                           Vector3 startPos, Vector3 endPos,
                           Vector3 startScale, Vector3 endScale,
                           float duration,
                           LeanTweenType easeType);

    void MoveAndScaleObject(GameObject target,
                            Vector3 startPos, Vector3 endPos,
                            Vector3 startScale, Vector3 endScale,
                            float duration,
                            LeanTweenType moveEaseType, LeanTweenType scaleEaseType);

    void MoveAndScaleObject(GameObject target,
                           Vector3 startPos, Vector3 endPos,
                           Vector3 startScale, Vector3 endScale,
                           float duration,
                           LeanTweenType moveEaseType,
                           LeanTweenType scaleEaseType,
                           Action onStart = null, Action onComplete = null);

    void MoveAndScaleObject(GameObject target,
                           Vector3 startPos, Vector3 endPos,
                           Vector3 startScale, Vector3 endScale,
                           float moveDuration, float scaleDuration,
                           LeanTweenType moveEaseType,
                           LeanTweenType scaleEaseType,
                           Action onStart = null, Action onComplete = null);

    void MoveAndScaleObject(GameObject target,
                       Vector3 startPos, Vector3 endPos,
                       Vector3 startScale, Vector3 endScale,
                       float duration, bool ignoreTimeScale,
                       LeanTweenType easeType,
                       Action onStart = null, Action onComplete = null);

    void MoveAndScaleObject(GameObject target,
                            Vector3 startPos, Vector3 endPos,
                            Vector3 startScale, Vector3 endScale,
                            float duration, bool ignoreTimeScale,
                            LeanTweenType easeType);

    void MoveAndScaleObject(GameObject target,
                            Vector3 startPos, Vector3 endPos,
                            Vector3 startScale, Vector3 endScale,
                            float duration, bool ignoreTimeScale,
                            LeanTweenType moveEaseType, LeanTweenType scaleEaseType );

    void MoveAndScaleObject(GameObject target,
                            Vector3 startPos, Vector3 endPos,
                            Vector3 startScale, Vector3 endScale,
                            float duration, bool ignoreTimeScale,
                            LeanTweenType moveEaseType , LeanTweenType scaleEaseType,
                            Action onStart = null, Action onComplete = null);

    void MoveAndScaleObject(GameObject target,
                           Vector3 startPos, Vector3 endPos,
                           Vector3 startScale, Vector3 endScale,
                           float moveDuration, float scaleDuration,
                           bool ignoreTimeScale,
                           LeanTweenType moveEaseType,
                           LeanTweenType scaleEaseType,
                           Action onStart = null, Action onComplete = null);

    // Rotation Abstract Methods

    void RotateObject(GameObject target, Vector3 startRotation, Vector3 endRotation, float duration,
                         LeanTweenType easeType,
                         Action onStart = null, Action onComplete = null);

    void RotateObject(GameObject target, Vector3 startRotation, Vector3 endRotation, float duration,
                       Action onStart = null, Action onComplete = null);

    void RotateObject(GameObject target, Vector3 startRotation, Vector3 endRotation, float duration);

    void RotateObject(GameObject target, Vector3 startRotation, Vector3 endRotation, float duration,
                         LeanTweenType easeType);

    void RotateObject(GameObject target, Vector3 startRotation, Vector3 endRotation, float duration, bool ignoreTimeScale, LeanTweenType easeType);

    void RotateObject(GameObject target, Vector3 startRotation, Vector3 endRotation, float duration, bool ignoreTimeScale);

    void RotateObject(GameObject target, Vector3 startRotation, Vector3 endRotation, float duration, bool ignoreTimeScale,
                             LeanTweenType easeType, Action onStart = null, Action onComplete = null);

    void RotateObject(GameObject target, Vector3 startRotation, Vector3 endRotation, float duration, bool ignoreTimeScale,
                             Action onStart = null, Action onComplete = null);

    // Fade Abstract Methods 
    void FadeCanvasGroup(CanvasGroup target, float startAlpha, float endAlpha, float duration, bool ignoreTimeScale);

    void FadeCanvasGroup(CanvasGroup target, float startAlpha, float endAlpha, float duration, bool ignoreTimeScale,
                         LeanTweenType easeType = LeanTweenType.notUsed);

    void FadeCanvasGroup(CanvasGroup target, float startAlpha, float endAlpha, float duration, bool ignoreTimeScale,
                         Action onStart = null, Action onComplete = null);

    void FadeCanvasGroup(CanvasGroup target, float startAlpha, float endAlpha, float duration, bool ignoreTimeScale,
                         LeanTweenType easeType = LeanTweenType.notUsed,
                         Action onStart = null, Action onComplete = null);

    void FadeCanvasGroup(CanvasGroup target, float startAlpha, float endAlpha, float duration);

    void FadeCanvasGroup(CanvasGroup target, float startAlpha, float endAlpha, float duration,
                         LeanTweenType easeType = LeanTweenType.notUsed);

    void FadeCanvasGroup(CanvasGroup target, float startAlpha, float endAlpha, float duration,
                         Action onStart = null, Action onComplete = null);

    void FadeCanvasGroup(CanvasGroup target, float startAlpha, float endAlpha, float duration,
                         LeanTweenType easeType = LeanTweenType.notUsed,
                         Action onStart = null, Action onComplete = null);
}