using System;
using System.Collections;
using DevionGames;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LorScene : MonoBehaviour
{
    [SerializeField] private GameObject LoadPanel;
    [SerializeField] private Image FirstImage;
    [SerializeField] private Image SecondImage;
    [SerializeField] private Sprite[] LoadImages = new Sprite[3];

    [HeaderLine("Alfa")]
    [SerializeField] private float fadeDuration;

    public int CountImage = 0;
    private float StartAlfa;
    private float StartAlfa2;

    public void OnLorScene()
    {
        StartAlfa = FirstImage.color.a;
        StartAlfa2 = SecondImage.color.a;
        LoadPanel.SetActive(true);
        StartCoroutine(Scene());
    }

    private IEnumerator Scene()
    {
        if (CountImage < 3)
        {
            bool IsChangeSecondImage = false;
            FirstImage.sprite = LoadImages[CountImage];
            if (CountImage != 2)
            {
                int TemporaryInt = CountImage + 1;
                SecondImage.sprite = LoadImages[TemporaryInt];
                IsChangeSecondImage = true;
            }
            // WriteText
            yield return new WaitForSeconds(5);
            for (float t = 0f; t < fadeDuration; t += Time.deltaTime)
            {
                Color currentColor = FirstImage.color;
                currentColor.a = Mathf.Lerp(StartAlfa, 0f, t / fadeDuration);
                FirstImage.color = currentColor;
                if (IsChangeSecondImage)
                {
                    Color currentColor2 = SecondImage.color;
                    currentColor2.a = Mathf.Lerp(StartAlfa2, 1f, t / fadeDuration);
                    SecondImage.color = currentColor2;
                }
                yield return null;
            }
            CountImage++;
            FirstImage.color = SecondImage.color;
            Color color = SecondImage.color;
            color.a = 0f;
            SecondImage.color = color;
            StartCoroutine(Scene());
        }
        else
        {
            SceneManager.LoadScene("Map");
        }
    }
}
