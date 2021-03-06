﻿using System;
using System.Collections.Generic;
using Assets.Stem.Scripts.Extensions;
using Assets.Stem.Scripts.Audio;
using Assets.Stem.Scripts.Gui;
using Assets.Stem.Scripts.Gui.PivotAnimations;
using Assets.Stem.Scripts.PropertyAttributes;
using Assets.Stem.Scripts.Utils;
using UnityEngine;


namespace Assets.Stem.Scripts.Pivots {
    [RequireComponent(typeof(CanvasGroup))]
    public class Page : MonoBehaviour {
        [SerializeField] private List<ChoiceGui> _choiceGuis = new List<ChoiceGui>();
        [SerializeField] private AudioManager _audioManager;

        [SerializeField, Readonly] private Pivot _currentPivot;
        [SerializeField, Readonly] private Story _currentStory;
        [SerializeField, Readonly] private PivotAnimation _currentPivotAnimation;

        [SerializeField] private float _guiFadeTime = 1f;
        [SerializeField] private float _musicFadeInTime = 1f;
        [SerializeField] private AnimationCurve _musicFadeInEasing = AnimationCurveUtils.GetLinearCurve();


        public void Setup(Story story, PivotAnimation initialAnimation) {
            _currentStory = story;
            _currentPivotAnimation = Animations.LoadAnimation(initialAnimation.name);
        }


        private void Start() {
            _audioManager.LoadClip(AudioClips.BgNormal, 0f, loop: true);
            _audioManager.PlayTrack(AudioClips.BgNormal);
            _audioManager.Fade(AudioClips.BgNormal, _musicFadeInTime, _musicFadeInEasing);
        }


        public void LoadPivot(Pivot pivot) {
            if (_choiceGuis.Count != 2) {
                Debug.LogError("Choices text count isn't 2!");
            }
            if (pivot.Choices.Count != 2) {
                Debug.LogError(string.Format("'{0}' doesn't have 2 choices!", pivot.Id));
            }

            _currentPivot = pivot;

            for (int i = 0; i < pivot.Choices.Count; ++i) {
                var choiceGui = _choiceGuis [i];
                var choice = pivot.Choices[i];
                choiceGui.LoadChoice(choice);
            }
        }


        private void UnloadCurrentPivot(Action onComplete = null) {
            if (_currentPivot.IsNotNull()) {
                for (int i = 0; i < _currentPivot.Choices.Count; ++i) {
                    var choiceGui = _choiceGuis [i];
                    choiceGui.ChoiceClicked -= OnClickChoice;
                    choiceGui.AnimateOut(_guiFadeTime);
                }
                _currentPivot = null;
            }
            if (onComplete.IsNotNull()) {
                this.InvokeAfterTime(_guiFadeTime, onComplete);
            }
        }


        public void AnimatePivotTransition(Pivot pivot) {
            _currentPivotAnimation.Play();
            var guiAnimationWaitTime = _currentPivotAnimation.Length - _guiFadeTime;
            this.InvokeAfterTime(guiAnimationWaitTime, () => {
                for (int i = 0; i < pivot.Choices.Count; ++i) {
                    var choiceGui = _choiceGuis[i];
                    choiceGui.AnimateIn(_guiFadeTime, () => { choiceGui.ChoiceClicked += OnClickChoice; });
                }
            });
        }


        private void OnClickChoice(Choice choice) {
            var pivot = _currentStory.GetPivot(choice.NextPivot);
            var previousPivotAnimation = _currentPivotAnimation;
            _currentPivotAnimation = Animations.LoadAnimation(choice.OnTriggerAnimationName);

            HandleOnClickChoiceAudio(choice);

            UnloadCurrentPivot(() => {
                LoadPivot(pivot);
                previousPivotAnimation.Stop();
                Animations.ReleaseAnimation(previousPivotAnimation);
                AnimatePivotTransition(pivot);
            });
        }


        private void HandleOnClickChoiceAudio(Choice choice) {
            _audioManager.PlayTrackOneShot(AudioClips.SfxButton);
        }


        


    }
}
