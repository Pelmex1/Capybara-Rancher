using System;
using System.Collections.Generic;
using UnityEngine;

public class InitGame : MonoBehaviour
{
    private List<IDisposable> _disposables = new();
    private void Awake() {
        RegisterServices();
        Init();
        AddDisposables();
    }
    private void RegisterServices()
    {
        ServiceLocator.Initialize();
    }
    private void Init()
    {

    }
        private void AddDisposables()
        {
            
        }

        private void OnDestroy()
        {
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }
        }
}
