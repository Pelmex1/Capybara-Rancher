using System.Collections;
using TMPro;
using UnityEngine;

public class TextAppearance : MonoBehaviour
{
    [SerializeField] private float _fadeInTime = 2f;

    private TextMeshProUGUI _textToFade;
    private float _currentTime = 0f;

    private void Start()
    {
        _textToFade = GetComponent<TextMeshProUGUI>();
        StartCoroutine(FadeTextIn());
    }

    private IEnumerator FadeTextIn()
    {
        Color originalColor = _textToFade.color;
        _textToFade.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);

        while (_currentTime < _fadeInTime)
        {
            _currentTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, _currentTime / _fadeInTime);
            _textToFade.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }
    }
}
