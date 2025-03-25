using System.Collections;
using CapybaraRancher.Consts;
using UnityEngine;

public class CaveFog : MonoBehaviour
{

    [SerializeField] private float _fogDestiny = 0.15f;
    [SerializeField] private float _transitionTime = 3f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.PLAYER_TAG))
        {
            StartCoroutine(ChangeFogDensity(_fogDestiny));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constants.PLAYER_TAG))
        {
            StartCoroutine(ChangeFogDensity(0));
        }
    }

    private IEnumerator ChangeFogDensity(float target)
    {
        float currentDensity = RenderSettings.fogDensity;
        float timer = 0f;
        
        while (timer < _transitionTime)
        {
            timer += Time.deltaTime;
            float newDensity = Mathf.Lerp(currentDensity, target, timer / _transitionTime);
            RenderSettings.fogDensity = newDensity;
            yield return null;
        }

        RenderSettings.fogDensity = target;
    }
}
