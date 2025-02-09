using System;
using UnityEngine;

namespace Playground2D.Tool.Time
{
    public class Timer : MonoBehaviour
    {
        public float duration = 1000f;
        public bool startOnAwake = false;
        public bool loop = false;

        private float _startTime;
        private float _currentTime = 0f;
        private bool _isRunning = false;

        public event Action OnTimerStart;
        public event Action OnTimerStop;
        public event Action OnTimerComplete;
        public event Action<float> OnTimerUpdate;

        private void Awake()
        {
            if (startOnAwake)
                StartTimer();
        }

        private void Update()
        {
            if (_isRunning)
            {
                _currentTime = GetElapsedTime();
                OnTimerUpdate?.Invoke(_currentTime);

                if (_currentTime >= duration)
                {
                    StopTimer();
                    OnTimerComplete?.Invoke();

                    if (loop)
                        StartTimer();
                }
            }
        }

        public void StartTimer()
        {
            if (!_isRunning)
            {
                _startTime = DateTime.UtcNow.Ticks / TimeSpan.TicksPerSecond;
                _isRunning = true;
                OnTimerStart?.Invoke();
            }
        }

        public void StopTimer()
        {
            if (_isRunning)
            {
                _isRunning = false;
                OnTimerStop?.Invoke();
            }
        }

        public void ResetTimer()
        {
            _startTime = DateTime.UtcNow.Ticks / TimeSpan.TicksPerSecond;
            _currentTime = 0f;
        }

        public void SetDuration(float newDuration)
        {
            duration = newDuration;
        }

        public float GetCurrentTime()
        {
            return _currentTime;
        }

        public float GetRemainingTime()
        {
            return duration - _currentTime;
        }

        public bool IsRunning()
        {
            return _isRunning;
        }

        private float GetElapsedTime()
        {
            return (DateTime.UtcNow.Ticks / TimeSpan.TicksPerSecond) - _startTime;
        }
    }
}