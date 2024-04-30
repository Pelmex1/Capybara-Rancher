using System.Collections;
using CapybaraRancher.EventBus;
using UnityEngine;
using UnityEngine.UI;

public class RespawnFogging : MonoBehaviour
{
    [SerializeField] private float _foggingTime;
    [SerializeField] private Image _fogImage;

    private void OnEnable()
    {
        EventBus.PlayerRespawned += Fogging;
    }

    private void Fogging() => StartCoroutine(AlphaChanging());

    private IEnumerator AlphaChanging()
    {
        float timer = 0f;
        Color originalColor = _fogImage.color;

        while (timer < _foggingTime)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, timer / _foggingTime);
            _fogImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        yield return new WaitForSeconds(_foggingTime);

        while (timer > 0f)
        {
            timer -= Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, timer / _foggingTime);
            _fogImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }
    }
}
