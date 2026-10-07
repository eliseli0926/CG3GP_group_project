// ============================================================================
// MessagePopup.cs
// Displays a popup text when the user interacts with something and needs to
// learn some information. For example, an object which needs another item to be
// usable.
//
// Attach to the Canvas.
// ============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MessagePopup : MonoBehaviour
{
    public static MessagePopup Instance {get; private set;}

    [Tooltip("The panel that holds the message.")]
    [SerializeField] private GameObject popupPanel;

    [Tooltip("Text inside the popup panel.")]
    [SerializeField] private TMP_Text messageText;

    [SerializeField] private float displaySeconds = 3f;

    private float timeLeft;

    void Start()
    {
        Instance = this;
        popupPanel.SetActive(false);
    }

    void Update()
    {
        if (timeLeft > 0f)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0f)
            {
                popupPanel.SetActive(false);
            }
        }
    }

    public void Show(string message)
    {
        messageText.text = message;
        popupPanel.SetActive(true);
        timeLeft = displaySeconds;
    }
}
