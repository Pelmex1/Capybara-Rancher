using System.Collections;
using CapybaraRancher.Interfaces;
using UnityEngine;

public class Hunger : MonoBehaviour
{
    private const string MOVEBLE_OBJECT_TAG = "movebleObject";

    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject _infoText;
    [SerializeField] private float _hungerDelay;
    [SerializeField] private float _raycastDistance = 10f;

    private IMovingPlayer _stats;
    private float _takingAwayHunger;

    private void Start()
    {
        _stats = GetComponent<IMovingPlayer>();
        _takingAwayHunger = (1f / _hungerDelay) * _stats.HungerMaxValue;
        StartCoroutine(HungerUpdate());
    }

    private void Update()
    {
        HungerFix();
        CheckAndEatCrystal();
    }
    private void HungerFix() => _stats.Hunger = _stats.Hunger > _stats.HungerMaxValue ? _stats.HungerMaxValue : _stats.Hunger < 0f ? 0f : _stats.Hunger;
    private int CheckAndEatCrystal()
    {
        Vector3 cameraForward = _camera.transform.forward;
        Ray cameraRay = new Ray(_camera.transform.position, cameraForward);
        ICrystalItem crystal;
        RaycastHit hit;
        if (Physics.Raycast(cameraRay, out hit, _raycastDistance))
            if (hit.collider.CompareTag(MOVEBLE_OBJECT_TAG))
                if (hit.collider.TryGetComponent<ICrystalItem>(out crystal))
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        HungerRegen(crystal.PercentOfRegen);
                        Destroy(hit.collider.gameObject);
                    }
                    _infoText.SetActive(true);
                    return 1;
                }
        _infoText.SetActive(false);
        return 0;
    }

    private void HungerRegen(float percentOfRegen)
    {
        _stats.Hunger += percentOfRegen * _stats.HungerMaxValue;
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
}