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
                UIManagerController.Instance.StartFade();
                audioManager.PlaySfx(data.Sfx_player_passed_enter);
                data.HasPlayerPassed = true;
                audioManager.PlayMusic(data.Music_background);
            }
            else
            {
                UIManagerController.Instance.StartFade();
                audioManager.PlaySfx(data.Sfx_player_passed_exit);
                data.HasPlayerPassed = false;
                audioManager.MusicAudioSource.Stop();
            } 
        }
    }
}