using UnityEngine;

public class UIManagerController : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    public void ActivateMenu()
    {
        menu.SetActive(true);
    }

    public void DisableMenu()
    {
        menu.SetActive(false);
    }
}
