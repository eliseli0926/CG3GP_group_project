// Elise Li, Sophia Cho
// ============================================================================
// KeypadFocus.cs
// The interaction script for the keypad, to focus on click and defocus on
// pressing E, with clickable buttons.
// Attach this to the root keypad prefab and give it a collider.
// In the keypad's OnAccessGranted event, call the relevant function to unlock
// the attached object (For example, + -> chest -> Unlock())
// ============================================================================


using UnityEngine;
using UnityEngine.InputSystem;

namespace NavKeypad
{
    public class KeypadFocus : MonoBehaviour, IInteractable
    {
        [Header("References")]
        [SerializeField] private Transform focusPoint;   // empty child in front of keypad, blue Z axis facing the keypad
        [SerializeField] private ThirdPersonCamera thirdPersonCamera;
        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private Playerinteractor interactor;
        [Tooltip("Movement / interaction scripts to disable while using the keypad.")]
        [SerializeField] private MonoBehaviour[] disableWhileFocused;

        [Header("Settings")]
        [Tooltip("Layers the click ray can hit. Exclude the Player layer so the ray passes through your character.")]
        [SerializeField] private LayerMask interactMask = ~0;

        private Camera cam;
        private bool isFocused;
        private int enterFrame;

        private void Awake()
        {
            cam = Camera.main;
        }

        public void Interact()
        {
            if (isFocused) return;
            EnterFocus();
        }

        private void Update()
        {
            if (!isFocused)
            {
                return;
            }

            // Exit upon pressing E
            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                ExitFocus();
                return;
            }

            if (Time.frameCount == enterFrame) return;

            // Hit keypad button
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
                var hits = Physics.RaycastAll(ray, 100f, interactMask, QueryTriggerInteraction.Collide);
                System.Array.Sort(hits, (a, b) => a.distance.CompareTo(b.distance));

                foreach (var h in hits)
                {
                    var button = h.collider.GetComponentInParent<KeypadButton>();
                    if (button != null)
                    {
                        button.PressButton();
                        break;
                    }
                }
            }
        }

        // Make the camera focus on the keypad
        private void EnterFocus()
        {
            isFocused = true;
            enterFrame = Time.frameCount;
            SetPlayerControl(false);
            interactor.SetBlocked(true);
            thirdPersonCamera.SetFocus(focusPoint);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        // Make the camera defocus from the keypad and return to default
        private void ExitFocus()
        {
            isFocused = false;
            thirdPersonCamera.ClearFocus();
            interactor.SetBlocked(false);
            SetPlayerControl(true);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        // While focused on the keypad, player cannot move
        private void SetPlayerControl(bool enable)
        {
            if (playerMovement != null) playerMovement.SetControlEnabled(enable);
            foreach (var s in disableWhileFocused)
                if (s != null) s.enabled = enable;
        }
    }
}
