using UnityEngine;
using System.Collections;

public class PuertasController : MonoBehaviour
{
    [SerializeField] private RoomsData data;
    [SerializeField] private AudioManagerController audioManager;
    private void Start()
    {
        audioManager = AudioManagerController.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (data.HasPlayerPassed == false)
            {
                audioManager.PlaySfx(data.Sfx_player_passed_enter);
            }
            else
            {
                audioManager.PlaySfx(data.Sfx_player_passed_enter);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if(data.HasPlayerPassed == false)
            {
                Invoke("PlayExitSFX", 1f);

                audioManager.PlayMusic(data.Music_background);
                data.HasPlayerPassed = true;
            }
            else
            {
                Invoke("PlayExitSFX", 1f);
                audioManager.MusicAudioSource.Stop();
                data.HasPlayerPassed = false;
            }
            
        }
    }
    private void PlayExitSFX()
    {
        audioManager.PlaySfx(data.Sfx_player_passed_exit);
    }

}