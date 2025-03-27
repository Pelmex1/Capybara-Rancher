using System;
using System.Collections.Generic;
using CustomEventBus;
using UnityEngine;

namespace Game.Level
{
    public class InitGame : MonoBehaviour
    {
        private EventBus _eventBus;

        private List<IDisposable> _disposables = new();

        private void Awake()
        {
            _eventBus = new EventBus();

            RegisterServices();
            Init();
            AddDisposables();
        }
        private void RegisterServices()
        {
            ServiceLocator.Initialize();
            ServiceLocator.Current.Register(_eventBus);
        }

        private void Init()
        {
        }

        private void AddDisposables()
        {
            //_disposables.Add(_scoreController);
        }

        private void OnDestroy()
        {
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }
        }
    }
}