
using UnityEngine;

namespace StardropTools.Tween
{
    public static class Tween
    {
        /* to do */
        public enum TweenType // many more to do!
        {
            // values:
            Float, Integer, Vector2, Vector3, Vector4, Quaternion, Color,
            ShakeFloat, ShakeInteger, ShakeVector2, ShakeVector3, ShakeVector4, ShakeQuaternion,

            // Transform:
            Position, LocalPosition,
            Rotation, LocalRotation,
            RotationEuler, LocalRotationEuler,
            LocalScale,

            ShakePosition, ShakeLocalPosition,
            ShakeRotation, ShakeLocalRotation,
            ShakeRotationEuler, ShakeLocalRotationEuler,
            ShakeLocalScale,

            // RectTransform
            SizeDelta, AnchoredPosition,
            ShakeSize, ShakeAnchoredPosition, // to do

            // Image
            ImageColor, ImagePixelPerUnitMultiplier, ImageFillAmount,
            SpriteRendererColor, MaterialColor,
        }
        public enum LoopType { none, loop, pingPong }
        public enum TweenState { waiting, running, complete, paused, canceled }
        public enum EaseCurve
        {
            liner,
            easeIn, easeOut, easeInOut,
            easeInBack, easeOutBack, easeInOutBack,
            easeInBounce, easeOutBounce, easeInOutBounce,
            easeInElastic, easeOutElastic, easeInOutElastic,
            easeWavePositive, easeWaveNegative,
            easeWaveStrongPositive, easeWaveStrongNegative,
            easePeakPositive, easePeakNegative,
            easeWobble,
        }

        #region Curves

        private static AnimationCurve linear;
        private static AnimationCurve easeIn;
        private static AnimationCurve easeOut;
        private static AnimationCurve easeInOut;

        private static AnimationCurve easeInBack;
        private static AnimationCurve easeOutBack;
        private static AnimationCurve easeInOutBack;

        private static AnimationCurve easeInBounce;
        private static AnimationCurve easeOutBounce;
        private static AnimationCurve easeInOutBounce;

        private static AnimationCurve easeInElastic;
        private static AnimationCurve easeOutElastic;
        private static AnimationCurve easeInOutElastic;

        private static AnimationCurve easeWavePositive;
        private static AnimationCurve easeWaveNegative;

        private static AnimationCurve easeWavePositiveStrong;
        private static AnimationCurve easeWaveNegativeStrong;

        private static AnimationCurve easePeakPositive;
        private static AnimationCurve easePeakNegative;

        private static AnimationCurve easeWobble;


        public static AnimationCurve Linear { get { if (linear == null) linear = new AnimationCurve(new Keyframe(0f, 0f, 1f, 1f), new Keyframe(1f, 1f, 1f, 1f)); return linear; } }
        public static AnimationCurve EaseIn { get { if (easeIn == null) easeIn = new AnimationCurve(new Keyframe(0f, 0f, 0f, 0f), new Keyframe(1f, 1f, 2.388632f, 2.388632f)); return easeIn; } }
        public static AnimationCurve EaseOut { get { if (easeOut == null) easeOut = new AnimationCurve(new Keyframe(0f, 0f, 2f, 2f), new Keyframe(1f, 1f, 0f, 0f)); return easeOut; } }
        public static AnimationCurve EaseInOut { get { if (easeInOut == null) easeInOut = new AnimationCurve(new Keyframe(0f, 0f, 0f, 0f), new Keyframe(1f, 1f, 0f, 0f)); return easeInOut; } }
        
        public static AnimationCurve EaseInBack { get { if (easeInBack == null) easeInBack = new AnimationCurve(new Keyframe(0f, 0f, 0f, 0f), new Keyframe(0.4f, -0.1f, 0.1783385f, 0.1783385f), new Keyframe(1f, 1f, 3.343508f, 3.343508f)); return easeInBack; } }
        public static AnimationCurve EaseOutBack { get { if (easeOutBack == null) easeOutBack = new AnimationCurve(new Keyframe(0f, 0f, 3.494377f, 3.494377f), new Keyframe(0.6f, 1.1f, 0.3653818f, 0.3653818f), new Keyframe(1f, 1f, 0f, 0f)); return easeOutBack; } }
        public static AnimationCurve EaseInOutBack { get { if (easeInOutBack == null) easeInOutBack = new AnimationCurve(new Keyframe(0f, 0f, 0f, 0f), new Keyframe(0.25f, -0.1f, 1.259989f, 1.259989f), new Keyframe(0.75f, 1.1f, 1.271497f, 1.271497f), new Keyframe(1f, 1f, 0f, 0f)); return easeInOutBack; } }
        
        public static AnimationCurve EaseInBounce { get { if (easeInBounce == null) easeInBounce = new AnimationCurve(new Keyframe(0f, 0f, 4.720257f, 4.720257f), new Keyframe(0.05f, 0.1f, 0f, 0f), new Keyframe(0.1f, 0f, -4.10869f, 5.888695f), new Keyframe(0.2f, 0.25f, -9.513498E-08f, -9.513498E-08f), new Keyframe(0.3f, 0f, -6.05509f, 6.342979f), new Keyframe(0.45f, 0.5f, 0f, 0f), new Keyframe(0.6f, 0f, -7.369156f, 5.472521f), new Keyframe(1f, 1f, 0f, 0f)); return easeInBounce; } }
        public static AnimationCurve EaseOutBounce { get { if (easeOutBounce == null) easeOutBounce = new AnimationCurve(new Keyframe(0f, 0f, 0f, 0f), new Keyframe(0.4f, 1f, 5.472521f, -7.369156f), new Keyframe(0.55f, 0.499887f, 0f, 0f), new Keyframe(0.7f, 1f, 6.342979f, -6.05509f), new Keyframe(0.8f, 0.7498865f, -9.513498E-08f, -9.513498E-08f), new Keyframe(0.9f, 1f, 5.888695f, -4.10869f), new Keyframe(0.95f, 0.899886f, 0f, 0f), new Keyframe(1f, 1f, 4.720257f, 4.720257f)); return easeOutBounce; } }
        public static AnimationCurve EaseInOutBounce { get { if (easeInOutBounce == null) easeInOutBounce = new AnimationCurve(new Keyframe(0f, 0f, 4.720257f, 4.720257f), new Keyframe(0.02500096f, 0.05001157f, 0f, 0f), new Keyframe(0.05000193f, 0f, -4.10869f, 5.888695f), new Keyframe(0.1000039f, 0.1250289f, -9.513498E-08f, -9.513498E-08f), new Keyframe(0.1500058f, 0f, -6.05509f, 6.342979f), new Keyframe(0.2250087f, 0.2500578f, 0f, 0f), new Keyframe(0.3000116f, 0f, -7.369156f, 5.472521f), new Keyframe(0.5000193f, 0.5001157f, 0f, 0f), new Keyframe(0.6f, 1f, 11.79358f, -5.820361f), new Keyframe(0.7f, 0.75f, 0f, 0f), new Keyframe(0.805f, 1f, 4.386343f, -5.962316f), new Keyframe(0.8608643f, 0.8708649f, 0f, 0f), new Keyframe(0.919391f, 0.9996701f, 5.448766f, -2.422245f), new Keyframe(0.9562561f, 0.9457275f, 0f, 0f), new Keyframe(1.000286f, 0.9954951f, 2.497174f, 2.497174f)); return easeInOutBounce; } }
        
        public static AnimationCurve EaseInElastic { get { if (easeInElastic == null) easeInElastic = new AnimationCurve(new Keyframe(0f, 0f, 0.4605525f, 0.4605525f), new Keyframe(0.15f, -0.05f, 0f, 0f), new Keyframe(0.3f, 0.1f, 0f, 0f), new Keyframe(0.45f, -0.2f, 0f, 0f), new Keyframe(0.6f, 0.25f, 0f, 0f), new Keyframe(0.75f, -0.35f, 0f, 0f), new Keyframe(1f, 1f, 5.4f, 5.4f)); return easeInElastic; } }
        public static AnimationCurve EaseOutElastic { get { if (easeOutElastic == null) easeOutElastic = new AnimationCurve(new Keyframe(0f, 0f, 6.899229f, 6.899229f), new Keyframe(0.25f, 1.25f, 0f, 0f), new Keyframe(0.4f, 0.8f, 0f, 0f), new Keyframe(0.55f, 1.15f, 0f, 0f), new Keyframe(0.7f, 0.9f, 0f, 0f), new Keyframe(0.85f, 1.05f, 0f, 0f), new Keyframe(1f, 1f, 0.4605525f, 0.4605525f)); return easeOutElastic; } }
        public static AnimationCurve EaseInOutElastic { get { if (easeInOutElastic == null) easeInOutElastic = new AnimationCurve(new Keyframe(0f, -0.1296169f, 0.4605525f, 0.4605525f), new Keyframe(0.07500369f, -0.1611002f, 0f, 0f), new Keyframe(0.1500074f, -0.06665026f, 0f, 0f), new Keyframe(0.2250111f, -0.2555501f, 0f, 0f), new Keyframe(0.3000148f, 0.02779965f, 0f, 0f), new Keyframe(0.3750185f, -0.35f, 0f, 0f), new Keyframe(0.5000246f, 0.5000492f, 11.70122f, 11.70122f), new Keyframe(0.625f, 1.25f, -0.121671f, -0.121671f), new Keyframe(0.7080119f, 0.7316632f, -1.725249f, -1.725249f), new Keyframe(0.7978281f, 1.118169f, 0.3863115f, 0.3863115f), new Keyframe(0.8704107f, 0.8603979f, 0.1551553f, 0.1551553f), new Keyframe(0.9472989f, 1.033288f, 0.04791922f, 0.04791922f), new Keyframe(1f, 1f, -0.6316382f, -0.6316382f)); return easeInOutElastic; } }
        
        public static AnimationCurve EaseWavePositive { get { if (easeWavePositive == null) easeWavePositive = new AnimationCurve(new Keyframe(0f, 0f, 0f, 0f), new Keyframe(0.5f, 1f, 0f, 0f), new Keyframe(1f, 0f, 0f, 0f)); return easeWavePositive; } }
        public static AnimationCurve EaseWaveNegative { get { if (easeWaveNegative == null) easeWaveNegative = new AnimationCurve(new Keyframe(0f, 0f, 0f, 0f), new Keyframe(0.5f, -1f, 0f, 0f), new Keyframe(1f, 0f, 0f, 0f)); return easeWaveNegative; } }

        public static AnimationCurve EaseWavePositiveStrong { get { if (easeWavePositiveStrong == null) easeWavePositiveStrong = new AnimationCurve(new Keyframe(0f, 0f, 4.722973f, 4.722973f), new Keyframe(0.5f, 1f, 0f, 0f), new Keyframe(1f, 0f, -5.247751f, -5.247751f)); return easeWavePositiveStrong; } }
        public static AnimationCurve EaseWaveNegativeStrong { get { if (easeWaveNegativeStrong == null) easeWaveNegativeStrong = new AnimationCurve(new Keyframe(0f, 0f, -5.086275f, -5.086275f), new Keyframe(0.5f, -1f, 0f, 0f), new Keyframe(1f, 0f, 4.722971f, 4.722971f)); return easeWaveNegativeStrong; } }

        public static AnimationCurve EasePeakPositive { get { if (easePeakPositive == null) easePeakPositive = new AnimationCurve(new Keyframe(0f, 0f, 0.02668267f, 0.02668267f), new Keyframe(0.5f, 1f, 4.254927f, -4.304457f), new Keyframe(1f, 0f, -0.009783249f, -0.009783249f)); return easePeakPositive; } }
        public static AnimationCurve EasePeakNegative { get { if (easePeakNegative == null) easePeakNegative = new AnimationCurve(new Keyframe(0f, 1f, -0.02668267f, -0.02668267f), new Keyframe(0.5f, 0f, -4.254927f, 4.304457f), new Keyframe(1f, 1f, 0.009783249f, 0.009783249f)); return easePeakNegative; } }

        public static AnimationCurve EaseWobble { get { if (easeWobble == null) easeWobble = new AnimationCurve(new Keyframe(0f, 0f, 11.01978f, 30.76278f), new Keyframe(0.08054394f, 1f, 0f, 0f), new Keyframe(0.3153235f, -0.75f, 0f, 0f), new Keyframe(0.5614113f, 0.5f, 0f, 0f), new Keyframe(0.75f, -0.25f, 0f, 0f), new Keyframe(0.9086903f, 0.1361611f, 0f, 0f), new Keyframe(1f, 0f, -4.159244f, -1.351373f)); return easeWobble; } }


        public static AnimationCurve GetEaseCurve(EaseCurve ease)
        {
            switch (ease)
            {
                case EaseCurve.liner:
                    return Linear;

                case EaseCurve.easeIn:
                    return EaseIn;

                case EaseCurve.easeOut:
                    return EaseOut;

                case EaseCurve.easeInOut:
                    return EaseInOut;

                case EaseCurve.easeInBack:
                    return EaseInBack;

                case EaseCurve.easeOutBack:
                    return EaseOutBack;

                case EaseCurve.easeInOutBack:
                    return EaseInOutBack;

                case EaseCurve.easeInBounce:
                    return EaseInBounce;

                case EaseCurve.easeOutBounce:
                    return EaseOutBounce;

                case EaseCurve.easeInOutBounce:
                    return EaseInOutBounce;

                case EaseCurve.easeInElastic:
                    return EaseInElastic;

                case EaseCurve.easeOutElastic:
                    return EaseOutElastic;

                case EaseCurve.easeInOutElastic:
                    return EaseInOutElastic;

                case EaseCurve.easeWavePositive:
                    return EaseWavePositive;

                case EaseCurve.easeWaveNegative:
                    return EaseWaveNegative;

                case EaseCurve.easeWaveStrongPositive:
                    return EaseWavePositiveStrong;

                case EaseCurve.easeWaveStrongNegative:
                    return EaseWaveNegativeStrong;

                case EaseCurve.easePeakPositive:
                    return EasePeakPositive;

                case EaseCurve.easePeakNegative:
                    return EasePeakNegative;

                case EaseCurve.easeWobble:
                    return EaseWobble;
            }

            return null;
        }

        #endregion // curves

        static TweenBase ProcessTween(TweenBase tween)
        {
            if (Application.isPlaying == false)
            {
                Debug.Log("Can't Tween in edit mode");
                return null;
            }

            TweenManager.Instance.ProcessTween(tween);
            return tween;
        }


        // --------------- \\     ACTIONS     // --------------- //
        // ----------------->>               <<----------------- //
        // --------------- //     ACTIONS     \\ --------------- // 
        #region Actions

        // VALUES
        #region Values
        public static TweenBase Float(float startFloat, float targetFloat, float duration, float delay, bool ignoreTimeScale, AnimationCurve curve, Tween.LoopType loop, int tweenID = -1, CoreEvent<float> eventToUdapte = null)
        {
            TweenBase tween = new TweenFloat(tweenID, startFloat, targetFloat, duration, delay, ignoreTimeScale, curve, loop, eventToUdapte);
            return ProcessTween(tween);
        }

        public static TweenBase Int(int startInt, int targetInt, float duration, float delay, bool ignoreTimeScale, AnimationCurve curve, Tween.LoopType loop, int tweenID = -1, CoreEvent<int> eventToUdapte = null)
        {
            TweenBase tween = new TweenInt(tweenID, startInt, targetInt, duration, delay, ignoreTimeScale, curve, loop, eventToUdapte);
            return ProcessTween(tween);
        }

        public static TweenBase Vector2(Vector2 startVector, Vector2 targetVector, float duration, float delay, bool ignoreTimeScale, AnimationCurve curve, Tween.LoopType loop, int tweenID = -1, CoreEvent<Vector2> eventToUdapte = null)
        {
            TweenBase tween = new TweenVector2(tweenID, startVector, targetVector, duration, delay, ignoreTimeScale, curve, loop, eventToUdapte);
            return ProcessTween(tween);
        }

        public static TweenBase Vector3(Vector3 startVector, Vector3 targetVector, float duration, float delay, bool ignoreTimeScale, AnimationCurve curve, Tween.LoopType loop, int tweenID = -1, CoreEvent<Vector3> eventToUdapte = null)
        {
            TweenBase tween = new TweenVector3(tweenID, startVector, targetVector, duration, delay, ignoreTimeScale, curve, loop, eventToUdapte);
            return ProcessTween(tween);
        }

        public static TweenBase Vector4(Vector4 startVector, Vector4 targetVector, float duration, float delay, bool ignoreTimeScale, AnimationCurve curve, Tween.LoopType loop, int tweenID = -1, CoreEvent<Vector4> eventToUdapte = null)
        {
            TweenBase tween = new TweenVector4(tweenID, startVector, targetVector, duration, delay, ignoreTimeScale, curve, loop, eventToUdapte);
            return ProcessTween(tween);
        }

        public static TweenBase Quaternion(Quaternion startVector, Quaternion targetVector, float duration, float delay, bool ignoreTimeScale, AnimationCurve curve, Tween.LoopType loop, int tweenID = -1, CoreEvent<Quaternion> eventToUdapte = null)
        {
            TweenBase tween = new TweenQuaternion(tweenID, startVector, targetVector, duration, delay, ignoreTimeScale, curve, loop, eventToUdapte);
            return ProcessTween(tween);
        }

        public static TweenBase Color(Color startColor, Color targetColor, float duration, float delay, bool ignoreTimeScale, AnimationCurve curve, Tween.LoopType loop, int tweenID = -1, CoreEvent<Color> eventToUdapte = null)
        {
            TweenBase tween = new TweenColor(tweenID, startColor, targetColor, duration, delay, ignoreTimeScale, curve, loop, eventToUdapte);
            return ProcessTween(tween);
        }


        // Shake Values
        public static TweenBase ShakeFloat(float targetFloat, float intensity, float duration, float delay, bool ignoreTimeScale, AnimationCurve curve, Tween.LoopType loop, int tweenID = -1, CoreEvent<float> eventToUdapte = null)
        {
            TweenBase tween = new TweenShakeFloat(tweenID, targetFloat, intensity, duration, delay, ignoreTimeScale, curve, loop, eventToUdapte);
            return ProcessTween(tween);
        }

        public static TweenBase ShakeInt(int targetFloat, int intensity, float duration, float delay, bool ignoreTimeScale, AnimationCurve curve, Tween.LoopType loop, int tweenID = -1, CoreEvent<int> eventToUdapte = null)
        {
            TweenBase tween = new TweenShakeInt(tweenID, targetFloat, intensity, duration, delay, ignoreTimeScale, curve, loop, eventToUdapte);
            return ProcessTween(tween);
        }

        public static TweenBase ShakeVector2(Vector2 targetVector, Vector2 intensity, float duration, float delay, bool ignoreTimeScale, AnimationCurve curve, Tween.LoopType loop, int tweenID = -1, CoreEvent<Vector2> eventToUdapte = null)
        {
            TweenBase tween = new TweenShakeVector2(tweenID, targetVector, intensity, duration, delay, ignoreTimeScale, curve, loop, eventToUdapte);
            return ProcessTween(tween);
        }

        public static TweenBase ShakeVector3(Vector3 targetVector, Vector3 intensity, float duration, float delay, bool ignoreTimeScale, AnimationCurve curve, Tween.LoopType loop, int tweenID = -1, CoreEvent<Vector3> eventToUdapte = null)
        {
            TweenBase tween = new TweenShakeVector3(tweenID, targetVector, intensity, duration, delay, ignoreTimeScale, curve, loop, eventToUdapte);
            return ProcessTween(tween);
        }

        public static TweenBase ShakeVector4(Vector4 targetVector, Vector4 intensity, float duration, float delay, bool ignoreTimeScale, AnimationCurve curve, Tween.LoopType loop, int tweenID = -1, CoreEvent<Vector4> eventToUdapte = null)
        {
            TweenBase tween = new TweenShakeVector4(tweenID, targetVector, intensity, duration, delay, ignoreTimeScale, curve, loop, eventToUdapte);
            return ProcessTween(tween);
        }

        public static TweenBase ShakeQuaternion(Quaternion targetVector, Vector4 intensity, float duration, float delay, bool ignoreTimeScale, AnimationCurve curve, Tween.LoopType loop, int tweenID = -1, CoreEvent<Quaternion> eventToUdapte = null)
        {
            TweenBase tween = new TweenShakeQuaternion(tweenID, targetVector, intensity, duration, delay, ignoreTimeScale, curve, loop, eventToUdapte);
            return ProcessTween(tween);
        }
        #endregion // values


        // TRANSFORM
        #region Transform

        // Position
        public static TweenBase Position(Transform target, Vector3 targetVector, float duration, float delay, bool ignoreTimeScale, AnimationCurve curve, Tween.LoopType loop, int tweenID = -1, CoreEvent<Vector3> eventToUdapte = null)
        {
            TweenBase tween = new TweenPosition(target, tweenID, targetVector, duration, delay, ignoreTimeScale, curve, loop, eventToUdapte);
            return ProcessTween(tween);
        }

        public static TweenBase Position(Transform target, Vector3 startVector, Vector3 targetVector, float duration, float delay, bool ignoreTimeScale, AnimationCurve curve, Tween.LoopType loop, int tweenID = -1, CoreEvent<Vector3> eventToUdapte = null)
        {
            target.position = startVector;
            TweenBase tween = new TweenPosition(target, tweenID, targetVector, duration, delay, ignoreTimeScale, curve, loop, eventToUdapte);
            return ProcessTween(tween);
        }

        // Local Position
        public static TweenBase LocalPosition(Transform target, Vector3 targetVector, float duration, float delay, bool ignoreTimeScale, AnimationCurve curve, Tween.LoopType loop, int tweenID = -1, CoreEvent<Vector3> eventToUdapte = null)
        {
            TweenBase tween = new TweenLocalPosition(target, tweenID, targetVector, duration, delay, ignoreTimeScale, curve, loop, eventToUdapte);
            return ProcessTween(tween);
        }

        public static TweenBase LocalPosition(Transform target, Vector3 startVector, Vector3 targetVector, float duration, float delay, bool ignoreTimeScale, AnimationCurve curve, Tween.LoopType loop, int tweenID = -1, CoreEvent<Vector3> eventToUdapte = null)
        {
            target.localPosition = startVector;
            TweenBase tween = new TweenLocalPosition(target, tweenID, targetVector, duration, delay, ignoreTimeScale, curve, loop, eventToUdapte);
            return ProcessTween(tween);
        }



        // Rotation
        public static TweenBase Rotation(Transform target, Quaternion targetRotation, float duration, float delay, bool ignoreTimeScale, AnimationCurve curve, Tween.LoopType loop, int tweenID = -1, CoreEvent<Quaternion> eventToUdapte = null)
        {
            TweenBase tween = new TweenRotation(target, tweenID, targetRotation, duration, delay, ignoreTimeScale, curve, loop, eventToUdapte);
            return ProcessTween(tween);
        }

        public static TweenBase Rotation(Transform target, Quaternion startQuaternion, Quaternion targetRotation, float duration, float delay, bool ignoreTimeScale, AnimationCurve curve, Tween.LoopType loop, int tweenID = -1, CoreEvent<Quaternion> eventToUdapte = null)
        {
            target.rotation = startQuaternion;
            TweenBase tween = new TweenRotation(target, tweenID, targetRotation, duration, delay, ignoreTimeScale, curve, loop, eventToUdapte);
            return ProcessTween(tween);
        }

        // Local Rotation
        public static TweenBase LocalRotation(Transform target, Quaternion targetRotation, float duration, float delay, bool ignoreTimeScale, AnimationCurve curve, Tween.LoopType loop, int tweenID = -1, CoreEvent<Quaternion> eventToUdapte = null)
        {
            TweenBase tween = new TweenLocalRotation(target, tweenID, targetRotation, duration, delay, ignoreTimeScale, curve, loop, eventToUdapte);
            return ProcessTween(tween);
        }

        public static TweenBase LocalRotation(Transform target, Quaternion startRotation, Quaternion targetRotation, float duration, float delay, bool ignoreTimeScale, AnimationCurve curve, Tween.LoopType loop, int tweenID = -1, CoreEvent<Quaternion> eventToUdapte = null)
        {
            target.localRotation = startRotation;
            TweenBase tween = new TweenLocalRotation(target, tweenID, targetRotation, duration, delay, ignoreTimeScale, curve, loop, eventToUdapte);
            return ProcessTween(tween);
        }



        // Rotation Euler
        public static TweenBase RotationEuler(Transform target, Vector3 targetRotation, float duration, float delay, bool ignoreTimeScale, AnimationCurve curve, Tween.LoopType loop, int tweenID = -1, CoreEvent<Vector3> eventToUdapte = null)
        {
            TweenBase tween = new TweenRotationEuler(target, tweenID, targetRotation, duration, delay, ignoreTimeScale, curve, loop, eventToUdapte);
            return ProcessTween(tween);
        }

        public static TweenBase RotationEuler(Transform target, Vector3 startRotation, Vector3 targetRotation, float duration, float delay, bool ignoreTimeScale, AnimationCurve curve, Tween.LoopType loop, int tweenID = -1, CoreEvent<Vector3> eventToUdapte = null)
        {
            target.eulerAngles = startRotation;
            TweenBase tween = new TweenRotationEuler(target, tweenID, targetRotation, duration, delay, ignoreTimeScale, curve, loop, eventToUdapte);
            return ProcessTween(tween);
        }

        // Local Rotation Euler
        public static TweenBase LocalRotationEuler(Transform target, Vector3 targetRotation, float duration, float delay, bool ignoreTimeScale, AnimationCurve curve, Tween.LoopType loop, int tweenID = -1, CoreEvent<Vector3> eventToUdapte = null)
        {
            TweenBase tween = new TweenLocalRotationEuler(target, tweenID, targetRotation, duration, delay, ignoreTimeScale, curve, loop, eventToUdapte);
            return ProcessTween(tween);
        }

        public static TweenBase LocalRotationEuler(Transform target, Vector3 startRotation, Vector3 targetRotation, float duration, float delay, bool ignoreTimeScale, AnimationCurve curve, Tween.LoopType loop, int tweenID = -1, CoreEvent<Vector3> eventToUdapte = null)
        {
            target.localEulerAngles = startRotation;
            TweenBase tween = new TweenLocalRotationEuler(target, tweenID, targetRotation, duration, delay, ignoreTimeScale, curve, loop, eventToUdapte);
            return ProcessTween(tween);
        }



        // Local Scale
        public static TweenBase LocalScale(Transform target, Vector3 targetScale, float duration, float delay, bool ignoreTimeScale, AnimationCurve curve, Tween.LoopType loop, int tweenID = -1, CoreEvent<Vector3> eventToUdapte = null)
        {
            TweenBase tween = new TweenLocalScale(target, tweenID, targetScale, duration, delay, ignoreTimeScale, curve, loop, eventToUdapte);
            return ProcessTween(tween);
        }

        public static TweenBase LocalScale(Transform target, Vector3 startScale, Vector3 targetScale, float duration, float delay, bool ignoreTimeScale, AnimationCurve curve, Tween.LoopType loop, int tweenID = -1, CoreEvent<Vector3> eventToUdapte = null)
        {
            target.localScale = startScale;
            TweenBase tween = new TweenLocalScale(target, tweenID, targetScale, duration, delay, ignoreTimeScale, curve, loop, eventToUdapte);
            return ProcessTween(tween);
        }



        // --------------- \\     Shake Transform     // --------------- //

        // Position Shake
        public static TweenBase ShakePosition(Transform target, Vector3 intensity, Vector3 endPosition, float duration, float delay, bool ignoreTimeScale, AnimationCurve curve, Tween.LoopType loop, int tweenID = -1, CoreEvent<Vector3> eventToUdapte = null)
        {
            TweenBase tween = new TweenShakePosition(target, tweenID, intensity, endPosition, duration, delay, ignoreTimeScale, curve, loop, eventToUdapte);
            return ProcessTween(tween);
        }

        public static TweenBase ShakeLocalPosition(Transform target, Vector3 intensity, Vector3 endPosition, float duration, float delay, bool ignoreTimeScale, AnimationCurve curve, Tween.LoopType loop, int tweenID = -1, CoreEvent<Vector3> eventToUdapte = null)
        {
            TweenBase tween = new TweenShakeLocalPosition(target, tweenID, intensity, endPosition, duration, delay, ignoreTimeScale, curve, loop, eventToUdapte);
            return ProcessTween(tween);
        }


        // Rotation
        public static TweenBase ShakeRotation(Transform target, Vector3 intensity, float duration, float delay, bool ignoreTimeScale, AnimationCurve curve, Tween.LoopType loop, int tweenID = -1, CoreEvent<Quaternion> eventToUdapte = null)
        {
            TweenBase tween = new TweenShakeRotation(target, tweenID, intensity, duration, delay, ignoreTimeScale, curve, loop, eventToUdapte);
            return ProcessTween(tween);
        }

        public static TweenBase ShakeLocalRotation(Transform target, Vector3 intensity, float duration, float delay, bool ignoreTimeScale, AnimationCurve curve, Tween.LoopType loop, int tweenID = -1, CoreEvent<Quaternion> eventToUdapte = null)
        {
            TweenBase tween = new TweenShakeLocalRotation(target, tweenID, intensity, duration, delay, ignoreTimeScale, curve, loop, eventToUdapte);
            return ProcessTween(tween);
        }

        public static TweenBase ShakeRotationEuler(Transform target, Vector3 intensity, float duration, float delay, bool ignoreTimeScale, AnimationCurve curve, Tween.LoopType loop, int tweenID = -1, CoreEvent<Vector3> eventToUdapte = null)
        {
            TweenBase tween = new TweenShakeRotationEuler(target, tweenID, intensity, duration, delay, ignoreTimeScale, curve, loop, eventToUdapte);
            return ProcessTween(tween);
        }

        public static TweenBase ShakeLocalRotationEuler(Transform target, Vector3 intensity, float duration, float delay, bool ignoreTimeScale, AnimationCurve curve, Tween.LoopType loop, int tweenID = -1, CoreEvent<Vector3> eventToUdapte = null)
        {
            TweenBase tween = new TweenShakeLocalRotationEuler(target, tweenID, intensity, duration, delay, ignoreTimeScale, curve, loop, eventToUdapte);
            return ProcessTween(tween);
        }


        // Local Scale
        public static TweenBase ShakeLocalScale(Transform target, Vector3 intensity, float duration, float delay, bool ignoreTimeScale, AnimationCurve curve, Tween.LoopType loop, int tweenID = -1, CoreEvent<Vector3> eventToUdapte = null)
        {
            TweenBase tween = new TweenShakeLocalScale(target, tweenID, intensity, duration, delay, ignoreTimeScale, curve, loop, eventToUdapte);
            return ProcessTween(tween);
        }
        #endregion // transform


        // RECT TRANSFORM
        #region Rect Transform

        // Size Delta
        public static TweenBase SizeDelta(RectTransform target, Vector2 targetSize, float duration, float delay, bool ignoreTimeScale, AnimationCurve curve, Tween.LoopType loop, int tweenID = -1, CoreEvent<Vector2> eventToUdapte = null)
        {
            TweenBase tween = new TweenSizeDelta(target, tweenID, targetSize, duration, delay, ignoreTimeScale, curve, loop, eventToUdapte);
            return ProcessTween(tween);
        }

        public static TweenBase SizeDelta(RectTransform target, Vector2 startSize, Vector2 targetSize, float duration, float delay, bool ignoreTimeScale, AnimationCurve curve, Tween.LoopType loop, int tweenID = -1, CoreEvent<Vector2> eventToUdapte = null)
        {
            target.sizeDelta = startSize;
            TweenBase tween = new TweenSizeDelta(target, tweenID, targetSize, duration, delay, ignoreTimeScale, curve, loop, eventToUdapte);
            return ProcessTween(tween);
        }



        // Anchored Position
        public static TweenBase AnchoredPosition(RectTransform target, Vector2 targetPosition, float duration, float delay, bool ignoreTimeScale, AnimationCurve curve, Tween.LoopType loop, int tweenID = -1, CoreEvent<Vector2> eventToUdapte = null)
        {
            TweenBase tween = new TweenAnchoredPosition(target, tweenID, targetPosition, duration, delay, ignoreTimeScale, curve, loop, eventToUdapte);
            return ProcessTween(tween);
        }

        public static TweenBase AnchoredPosition(RectTransform target, Vector2 startPosition, Vector2 targetPosition, float duration, float delay, bool ignoreTimeScale, AnimationCurve curve, Tween.LoopType loop, int tweenID = -1, CoreEvent<Vector2> eventToUdapte = null)
        {
            target.anchoredPosition = startPosition;
            TweenBase tween = new TweenAnchoredPosition(target, tweenID, targetPosition, duration, delay, ignoreTimeScale, curve, loop, eventToUdapte);
            return ProcessTween(tween);
        }
        #endregion // rectTransform


        // IMAGE
        #region Image

        // image color
        public static TweenBase ImageColor(UnityEngine.UI.Image image, Color targetColor, float duration, float delay, bool ignoreTimeScale, AnimationCurve curve, Tween.LoopType loop, int tweenID = -1, CoreEvent<Color> eventToUdapte = null)
        {
            TweenBase tween = new TweenImageColor(image, tweenID, targetColor, duration, delay, ignoreTimeScale, curve, loop, eventToUdapte);
            return ProcessTween(tween);
        }

        public static TweenBase ImageColor(UnityEngine.UI.Image image, Color startColor, Color targetColor, float duration, float delay, bool ignoreTimeScale, AnimationCurve curve, Tween.LoopType loop, int tweenID = -1, CoreEvent<Color> eventToUdapte = null)
        {
            image.color = startColor;
            TweenBase tween = new TweenImageColor(image, tweenID, targetColor, duration, delay, ignoreTimeScale, curve, loop, eventToUdapte);
            return ProcessTween(tween);
        }



        // image Pixels per unit multiplier
        public static TweenBase ImagePixelsPerUnitMultiplier(UnityEngine.UI.Image image, float targetFloat, float duration, float delay, bool ignoreTimeScale, AnimationCurve curve, Tween.LoopType loop, int tweenID = -1, CoreEvent<float> eventToUdapte = null)
        {
            TweenBase tween = new TweenImagePixelsPerUnitMultiplier(image, tweenID, targetFloat, duration, delay, ignoreTimeScale, curve, loop, eventToUdapte);
            return ProcessTween(tween);
        }

        public static TweenBase ImagePixelsPerUnitMultiplier(UnityEngine.UI.Image image, float startFloat, float targetFloat, float duration, float delay, bool ignoreTimeScale, AnimationCurve curve, Tween.LoopType loop, int tweenID = -1, CoreEvent<float> eventToUdapte = null)
        {
            image.pixelsPerUnitMultiplier = startFloat;
            TweenBase tween = new TweenImagePixelsPerUnitMultiplier(image, tweenID, targetFloat, duration, delay, ignoreTimeScale, curve, loop, eventToUdapte);
            return ProcessTween(tween);
        }


        // Image Fill Amount
        public static TweenBase ImageFillAmount(UnityEngine.UI.Image image, float targetFloat, float duration, float delay, bool ignoreTimeScale, AnimationCurve curve, Tween.LoopType loop, int tweenID = -1, CoreEvent<float> eventToUdapte = null)
        {
            TweenBase tween = new TweenImageFillAmount(image, tweenID, targetFloat, duration, delay, ignoreTimeScale, curve, loop, eventToUdapte);
            return ProcessTween(tween);
        }

        public static TweenBase ImageFillAmount(UnityEngine.UI.Image image, float startFloat, float targetFloat, float duration, float delay, bool ignoreTimeScale, AnimationCurve curve, Tween.LoopType loop, int tweenID = -1, CoreEvent<float> eventToUdapte = null)
        {
            image.fillAmount = startFloat;
            TweenBase tween = new TweenImageFillAmount(image, tweenID, targetFloat, duration, delay, ignoreTimeScale, curve, loop, eventToUdapte);
            return ProcessTween(tween);
        }

        // Text Mesh color
        public static TweenBase TextMeshColor(TMPro.TextMeshProUGUI textMesh, Color targetColor, float duration, float delay, bool ignoreTimeScale, AnimationCurve curve, Tween.LoopType loop, int tweenID = -1, CoreEvent<Color> eventToUdapte = null)
        {
            TweenBase tween = new TweenTextMeshColor(textMesh, tweenID, targetColor, duration, delay, ignoreTimeScale, curve, loop, eventToUdapte);
            return ProcessTween(tween);
        }

        public static TweenBase TextMeshColor(TMPro.TextMeshProUGUI textMesh, Color startColor, Color targetColor, float duration, float delay, bool ignoreTimeScale, AnimationCurve curve, Tween.LoopType loop, int tweenID = -1, CoreEvent<Color> eventToUdapte = null)
        {
            textMesh.color = startColor;
            TweenBase tween = new TweenTextMeshColor(textMesh, tweenID, targetColor, duration, delay, ignoreTimeScale, curve, loop, eventToUdapte);
            return ProcessTween(tween);
        }

        #endregion // image

        public static TweenBase MaterialColor(Material material, Color targetColor, float duration, float delay, bool ignoreTimeScale, AnimationCurve curve, Tween.LoopType loop, int tweenID = -1, CoreEvent<Color> eventToUdapte = null)
        {
            TweenBase tween = new TweenMaterialColor(material, tweenID, targetColor, duration, delay, ignoreTimeScale, curve, loop, eventToUdapte);
            return ProcessTween(tween);
        }

        public static TweenBase MaterialColor(Material material, Color startColor, Color targetColor, float duration, float delay, bool ignoreTimeScale, AnimationCurve curve, Tween.LoopType loop, int tweenID = -1, CoreEvent<Color> eventToUdapte = null)
        {
            material.color = startColor;
            TweenBase tween = new TweenMaterialColor(material, tweenID, targetColor, duration, delay, ignoreTimeScale, curve, loop, eventToUdapte);
            return ProcessTween(tween);
        }



        public static TweenBase SpriteRendererColor(SpriteRenderer renderer, Color targetColor, float duration, float delay, bool ignoreTimeScale, AnimationCurve curve, Tween.LoopType loop, int tweenID = -1, CoreEvent<Color> eventToUdapte = null)
        {
            TweenBase tween = new TweenSpriteRendererColor(renderer, tweenID, targetColor, duration, delay, ignoreTimeScale, curve, loop, eventToUdapte);
            return ProcessTween(tween);
        }

        public static TweenBase SpriteRendererColor(SpriteRenderer renderer, Color startColor, Color targetColor, float duration, float delay, bool ignoreTimeScale, AnimationCurve curve, Tween.LoopType loop, int tweenID = -1, CoreEvent<Color> eventToUdapte = null)
        {
            renderer.color = startColor;
            TweenBase tween = new TweenSpriteRendererColor(renderer, tweenID, targetColor, duration, delay, ignoreTimeScale, curve, loop, eventToUdapte);
            return ProcessTween(tween);
        }

        #endregion Actions
    }
}