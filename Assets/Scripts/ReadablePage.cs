// Elise Li, Sophia Cho
// ============================================================================
// ReadablePage.cs
// A note page the user can click on to view. Cannot be picked up, but can be
// viewed multiple times. Click to open the page; click again or press Esc to
// close it.
//
// Needs ONE normal collider (Is Trigger unticked) so the crosshair can hit it.
// How close you have to be is set by Max Distance on the Main Camera's
// Playerinteractor. Needs a PageContents in the scene.
// ============================================================================

using UnityEngine;

public class ReadablePage : MonoBehaviour, IInteractable
{
    [Header("Item")]
    [Tooltip("Contents of the page")]
    [TextArea(5, 15)]
    [SerializeField] private string pageContents;

    // Click: open the page, or close it if it's already open.
    public void Interact()
    {
        if (PageContents.Instance.IsOpen) PageContents.Instance.Hide();
        else PageContents.Instance.Show(pageContents);
    }
}