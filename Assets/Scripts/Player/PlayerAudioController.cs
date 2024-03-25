using UnityEngine;

public class PlayerAudioController : MonoBehaviour
{
    [SerializeField] private AudioSource walkAudio;
    [SerializeField] private AudioSource runAudio;
    [SerializeField] private AudioSource gunAttractionAudio;
    [SerializeField] private AudioSource gunRemoveAudio;
    
    public void FootStepPlay(bool isRunning)
    {
        bool moving = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);
        if (moving)
        {
            if (!isRunning && !walkAudio.isPlaying)
            {
                walkAudio.Play();
                runAudio.Stop();
            }
            else if (isRunning && !runAudio.isPlaying)
            {
                runAudio.Play();
                walkAudio.Stop();
            }
        }
        else if(!moving)
        {
            walkAudio.Stop();
            runAudio.Stop();
        }
    }
    public void GunAttractionPlay()
    {
        if (!(Input.GetMouseButton(0) && Cursor.lockState == CursorLockMode.Locked))
            gunAttractionAudio.Stop();
        else if (!gunAttractionAudio.isPlaying)
            gunAttractionAudio.Play();
    }
    public void GunRemovePlay()
    {
        gunRemoveAudio.Play();
    }
}
