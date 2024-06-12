using UnityEngine;

public class FogForTrailer : MonoBehaviour
{
    [SerializeField] private float _fogDestiny = 0.15f;
    private void OnEnable()
    {
        RenderSettings.fogDensity = _fogDestiny;
    }
    private void OnDisable()
    {
        RenderSettings.fogDensity = 0f;
    }
}
