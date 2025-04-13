using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Source.Speech
{
    public class Tts : MonoBehaviour
    {
        [SerializeField] private List<string> _textToSpeak;
        [SerializeField] private int _textIndex;
        [SerializeField] private Transform _visualizer;
        [SerializeField] private float _visualizerScale = 1.0f;
        [SerializeField] private float _visualizerSmoothing = 0.5f;

        [SerializeField] private bool _nextLine;

        [SerializeField] private Strobotnik.Klattersynth.Speech _speech;
        private Coroutine _routine;
        private Quaternion _visualizerInitialRotation;
        private float _visualizerX;

        [SerializeField] private AudioSource _audioSource;
        private float[] _samples = new float[1024];
        private float _currentVolume;

        private void Awake()
        {
            _visualizerSmoothing = Mathf.Clamp(_visualizerSmoothing, 0, 0.99f);
            _visualizerInitialRotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
        }

        private IEnumerator SpeakRoutine()
        {
            _textIndex++;

            if (_textIndex >= _textToSpeak.Count)
                _textIndex = 0;

            _speech.speak(_textToSpeak[_textIndex]);

            while (_speech.isTalking())
                yield return new WaitForEndOfFrame();
        }

        private void OnDisable()
        {
            if (_routine != null)
                StopCoroutine(_routine);

            _routine = null;
        }

        private void FixedUpdate()
        {
            float loudness = GetVolume(_audioSource);
            _visualizerX = _visualizerSmoothing * _visualizerX +
                           (1 - _visualizerSmoothing) * loudness * _visualizerScale;
        }

        private float GetVolume(AudioSource audioSource)
        {
            audioSource.GetOutputData(_samples, 0);

            float sum = 0;

            foreach (var sample in _samples)
                sum += Mathf.Abs(sample);

            _currentVolume = sum / _samples.Length;

            return _currentVolume;
        }

        private void Update()
        {
            if (_nextLine)
            {
                _nextLine = false;

                if (_routine != null)
                {
                    StopCoroutine(_routine);
                    _routine = null;
                }

                _routine = StartCoroutine(SpeakRoutine());
            }

            _visualizer.localRotation = new Quaternion(_visualizerInitialRotation.x + _visualizerX,
                _visualizerInitialRotation.y, _visualizerInitialRotation.z, 1);
        }
    }
}