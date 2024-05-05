using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class UIManagerController : MonoBehaviour
{
    public static UIManagerController Instance { get; private set; }
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject dialogue;
    [SerializeField] private TMP_Text nameOfNPC;
    [SerializeField] private TMP_Text message;
    [SerializeField] private float dialogueDuration;
    [Header("FadeControl")]
    [SerializeField] private Image image;
    [SerializeField] private Color startColorIn;
    [SerializeField] private Color targetColorIn;
    [SerializeField] private Color startColorOut;
    [SerializeField] private Color targetColorOut;
    [SerializeField] private float smoothing;
    public Image ImageFade
    {
        get
        {
            return image;
        }
        set
        {
            image = value;
        }
    }
    public float DialogueDuration
    {
        get
        {
            return dialogueDuration;
        }
    }
    public TMP_Text NameOfNPC
    {
        get
        {
            return nameOfNPC;
        }
        set
        {
            nameOfNPC = value;
        }
    }
    public TMP_Text Message
    {
        get
        {
            return message;
        }
        set
        {
            message = value;
        }
    }
    public GameObject Menu
    {
        get
        {
            return menu;
        }
        set
        {
            menu = value;
        }
    }
    public GameObject Dialogue
    {
        get
        {
            return dialogue;
        }
        set
        {
            dialogue = value;
        }
    }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    public void ActivateMenu()
    {
        menu.SetActive(true);
    }
    public void DisableMenu()
    {
        menu.SetActive(false);
    }
    public void ShowDialogueInCanvas(string npcName, string message)
    {
        dialogue.SetActive(true);
        nameOfNPC.text = npcName;
        this.message.text = message;
    }
    public void StartFade()
    {
        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        float alpha = 0f;
        while (alpha <= 1)
        {
            Color currentColor = Color.Lerp(startColorOut, targetColorOut, alpha);
            image.color = currentColor;
            yield return new WaitForSeconds(smoothing);
            alpha += 0.1f;
        }
        image.color = targetColorOut;
        alpha = 0f;
        yield return new WaitForSeconds(0.3f);
        while (alpha <= 1)
        {
            Color currentColor = Color.Lerp(startColorIn, targetColorIn, alpha);
            image.color = currentColor;
            yield return new WaitForSeconds(smoothing);
            alpha += 0.1f;
        }
    }
}

