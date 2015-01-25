using Assets.GGJ2015.Scripts.Extensions;
using Assets.GGJ2015.Scripts.Utils;
using UnityEngine;


namespace Assets.GGJ2015.Scripts.Gui.PivotAnimations {
    public class GetStungAnimation : PivotAnimation {

        [SerializeField] private AnimationClip _getStungClip;

        [SerializeField] private float _fadeTime = 0.5f;
        [SerializeField] private AnimationCurve _fadeEasing = AnimationCurveUtils.GetLinearCurve();

        [SerializeField] private SpriteRenderer _getStungSpriteRenderer;
        [SerializeField] private SpriteRenderer _hospitalSpriteRenderer;



        private void OnEnable() {
            Length = _getStungClip.length;
            this.InvokeAfterTime(_getStungClip.length - _fadeTime, FadeToHospital);

            _hospitalSpriteRenderer.color = new Color(1f, 1f, 1f, 0f);
        }


        private void FadeToHospital() {
            _hospitalSpriteRenderer.color = new Color(1f, 1f, 1f, 1f);
            TweenUtils.TweenAlpha(_getStungSpriteRenderer, 0f, _fadeTime, _fadeEasing, RaiseFinishedEvent);
        }

    }
}