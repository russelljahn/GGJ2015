using System;
using System.Collections;
using UnityEngine;


namespace Assets.GGJ2015.Scripts.Extensions {
    public static class MonoBehaviourExtensions {

        #region InvokeAfterCoroutine
        public static void InvokeAfterCoroutine(this MonoBehaviour behaviour, IEnumerator coroutine, params Action [] onComplete) {
            behaviour.StartCoroutine(
                Co_InvokeAfterCoroutineHelper(behaviour, coroutine, onComplete)
            );
        }
        private static IEnumerator Co_InvokeAfterCoroutineHelper(this MonoBehaviour behaviour, IEnumerator coroutine, params Action [] onComplete) {
            yield return behaviour.StartCoroutine(coroutine);
            // ReSharper disable once ForCanBeConvertedToForeach
            for (var i = 0; i < onComplete.Length; ++i) {
                var callback = onComplete[i];
                if (callback != null) {
                    callback();
                }
            }
        }


        public static void InvokeAfterCoroutine<T>(this MonoBehaviour behaviour, IEnumerator coroutine, Action<T> onComplete, T onCompleteArgument) {
            behaviour.StartCoroutine(
                Co_InvokeAfterCoroutineHelper(behaviour, coroutine, onComplete, onCompleteArgument)
            );
        }
        private static IEnumerator Co_InvokeAfterCoroutineHelper<T>(this MonoBehaviour behaviour, IEnumerator coroutine, Action<T> onComplete, T onCompleteArgument) {
            yield return behaviour.StartCoroutine(coroutine);
            if (onComplete != null) {
                onComplete.Invoke(onCompleteArgument);
            }
        }


        public static void InvokeAfterCoroutine<T1, T2>(this MonoBehaviour behaviour, IEnumerator coroutine, Action<T1, T2> onComplete, T1 onCompleteArg1, T2 onCompleteArg2) {
            behaviour.StartCoroutine(
                Co_InvokeAfterCoroutineHelper(behaviour, coroutine, onComplete, onCompleteArg1, onCompleteArg2)
            );
        }
        private static IEnumerator Co_InvokeAfterCoroutineHelper<T1, T2>(this MonoBehaviour behaviour, IEnumerator coroutine, Action<T1, T2> onComplete, T1 onCompleteArg1, T2 onCompleteArg2) {
            yield return behaviour.StartCoroutine(coroutine);
            if (onComplete != null) {
                onComplete.Invoke(onCompleteArg1, onCompleteArg2);
            }
        }
        #endregion


        #region InvokeAfterTime
        public static void InvokeAfterTime(this MonoBehaviour behaviour, float time, params Action [] onComplete) {
            behaviour.StartCoroutine(
                Co_InvokeAfterTimeHelper(behaviour, time, onComplete)
            );
        }
        private static IEnumerator Co_InvokeAfterTimeHelper(this MonoBehaviour behaviour, float time, params Action [] onComplete) {
            yield return new WaitForSeconds(time);
            // ReSharper disable once ForCanBeConvertedToForeach
            for (var i = 0; i < onComplete.Length; ++i) {
                var callback = onComplete [i];
                if (callback != null) {
                    callback();
                }
            }
        }


        public static void InvokeAfterTime<T>(this MonoBehaviour behaviour, float time, Action<T> onComplete, T onCompleteArgument) {
            behaviour.StartCoroutine(
                Co_InvokeAfterTimeHelper(behaviour, time, onComplete, onCompleteArgument)
            );
        }
        private static IEnumerator Co_InvokeAfterTimeHelper<T>(this MonoBehaviour behaviour, float time, Action<T> onComplete, T onCompleteArgument) {
            yield return new WaitForSeconds(time);
            if (onComplete != null) {
                onComplete.Invoke(onCompleteArgument);
            }
        }


        public static void InvokeAfterTime<T1, T2>(this MonoBehaviour behaviour, float time, Action<T1, T2> onComplete, T1 onCompleteArg1, T2 onCompleteArg2) {
            behaviour.StartCoroutine(
                Co_InvokeAfterTimeHelper(behaviour, time, onComplete, onCompleteArg1, onCompleteArg2)
            );
        }
        private static IEnumerator Co_InvokeAfterTimeHelper<T1, T2>(this MonoBehaviour behaviour, float time, Action<T1, T2> onComplete, T1 onCompleteArg1, T2 onCompleteArg2) {
            yield return new WaitForSeconds(time);
            if (onComplete != null) {
                onComplete.Invoke(onCompleteArg1, onCompleteArg2);
            }
        }
        #endregion
    }
}
