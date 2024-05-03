using UnityEngine;
using TMPro;
using System.Collections;

public class UIManagerController : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject dialogue;
    [SerializeField] private TMP_Text nameOfNPC;
    [SerializeField] private TMP_Text message;
    [SerializeField] private float dialogueDuration;
    private void OnEnable()
    {
        
    }
    public void ActivateMenu()
    {
        menu.SetActive(true);
    }
    public void DisableMenu()
    {
        menu.SetActive(false);
    }
    public void ActivateDialog(string npcName, string message)
    {
        nameOfNPC.text = npcName;
        this.message.text = message;
        dialogue.SetActive(true);
        StartCoroutine(DisableDialogAfterDelay());
    }
    IEnumerator DisableDialogAfterDelay()
    {
        yield return new WaitForSeconds(dialogueDuration);
        dialogue.SetActive(false);
    }
}
