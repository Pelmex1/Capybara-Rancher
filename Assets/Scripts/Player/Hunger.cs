using System.Collections;
using CapybaraRancher.Interfaces;
using CapybaraRancher.EventBus;
using UnityEngine;
using CapybaraRancher.Enums;
using CapybaraRancher.Consts;

public class Hunger : MonoBehaviour
{
    

    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject _infoText;
    [SerializeField] private float _hungerDelay;
    [SerializeField] private float _raycastDistance = 10f;

    private IPlayer _stats;
    private float _takingAwayHunger;

    private void Start()
    {
        _stats = GetComponent<IPlayer>();
        _takingAwayHunger = 1f / _hungerDelay * _stats.HungerMaxValue;
        StartCoroutine(HungerUpdate());
    }

    private void Update()
    {
        HungerFix();
    }
    private void HungerFix() => _stats.Hunger = _stats.Hunger > _stats.HungerMaxValue ? _stats.HungerMaxValue : _stats.Hunger < 0f ? 0f : _stats.Hunger;
    private void Eat()
    {
        Vector3 cameraForward = _camera.transform.forward;
        Ray cameraRay = new(_camera.transform.position, cameraForward);
        if (Physics.Raycast(cameraRay, out RaycastHit hit, _raycastDistance))
            if (hit.collider.CompareTag(Constants.MOVEBLE_OBJECT_TAG))
                if (hit.collider.TryGetComponent(out ICrystalItem crystal))
                {
                    HungerRegen(crystal.PercentOfRegen);
                    GameObject crystalGameObject = hit.collider.gameObject;
                    crystalGameObject.SetActive(false);
                    EventBus.AddInPool(crystalGameObject, crystalGameObject.GetComponent<IMovebleObject>().Data.TypeGameObject);
                    _infoText.SetActive(true);
                }
        _infoText.SetActive(false);
    }

    private void HungerRegen(float percentOfRegen)
    {
        _stats.Hunger += percentOfRegen * 0.01f * _stats.HungerMaxValue;
        EventBus.EatTutorial.Invoke();
    }

    private IEnumerator HungerUpdate()
    {
        while (true)
        {
            if (_stats.Hunger > 0f)
                _stats.Hunger -= _takingAwayHunger;
            else
                _stats.Health -= _takingAwayHunger;

            yield return new WaitForSeconds(1f);
        }
    }
    private void OnEnable() {
        EventBus.EatInput += Eat;
    }
    private void OnDisable() {
        EventBus.EatInput -= Eat;
    }
}