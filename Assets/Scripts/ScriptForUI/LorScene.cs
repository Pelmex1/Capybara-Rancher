using System;
using System.Collections;
using DevionGames;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LorScene : MonoBehaviour
{
    [HeaderLine("Panels Objects")]
    [SerializeField] private GameObject LoadPanel;
    [SerializeField] private Image FirstImage;
    [SerializeField] private Image SecondImage;
    [SerializeField] private TMP_Text text;
    [SerializeField] private  AudioSource audioSourse;
    [SerializeField] private Sprite[] LoadImages = new Sprite[3];

    [HeaderLine("Alfa")]
    [SerializeField] private float fadeDuration;

    public int CountImage = 0;
    private float StartAlfa;
    private float StartAlfa2;
    private string AllText;

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
            
            StartCoroutine(WriteText(CountImage));
            yield return new WaitForSeconds(10);
            audioSourse.Stop();
            text.text = "";
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


    private IEnumerator WriteText(int NumberImage)
    {     
        audioSourse.Play();
        text.text = "";
        switch (NumberImage)
        {
            case 0:
                AllText = @"In the distant past, colonists from another galaxy arrived on the planet Capybara tens of thousands of years ago. 
                They brought capybaras with them, believing that these cute creatures would make good companions in their new environment. 
                However, as a result of unknown events, the colonists disappeared and the capybaras remained on the planet and flourished in their development";
                break;
            case 1:
                AllText = @"Many millennia later, our civilisation has detected signals from the system in which Capybara is located from afar. 
                Fascinated by uncharted worlds and life forms, we sent a robotic explorer to the planet to study the Capybara and their fascinating world.";
                break;
            case 2:
                AllText = @"However, after some time, communication with the robot broke down. What happened to it became a mystery to our civilisation.
                 They decided to send a special agent you to the planet to solve the mystery and find out what happened to their robot";
                break;
        }
        for (int i = 0; i <= AllText.Length; i++)
        {          
            text.text += AllText[i];
            yield return new WaitForSecondsRealtime(0.02f);
        }
    }
}
