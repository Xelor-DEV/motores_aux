using UnityEngine;

public class PuertasController : MonoBehaviour
{
    [SerializeField] private RoomsData data;
    [SerializeField] private AudioManagerController audioManager = AudioManagerController.Instance;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (data.HasPlayerPassed == false)
            {
                audioManager.PlaySfx(data.Sfx_player_passed_enter);
                audioManager.PlayMusic(data.Music_background);
                data.HasPlayerPassed = true;
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
            audioManager.PlaySfx(data.Sfx_player_passed_exit);
            audioManager.MusicAudioSource.Stop();
        }
    }
}