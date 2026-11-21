// Elise Li, Sophia Cho
// ============================================================================
// PlayerAnimationSounds.cs
// Plays footstep and jump sounds when called by Animation Events.
// Must be on the same GameObject as the player's Animator.
// ============================================================================

using UnityEngine;

public class PlayerAnimationSounds : MonoBehaviour
{
    [Tooltip("Short single-step sounds. One is picked at random each step.")]
    [SerializeField] private AudioClip[] footstepClips;
    [SerializeField] private AudioClip jumpClip;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    // Called by Animation Events on the walk clip (one per foot landing)
    public void Footstep()
    {
        if (footstepClips.Length == 0) return;
        audioSource.PlayOneShot(footstepClips[Random.Range(0, footstepClips.Length)]);
    }

    // Called by an Animation Event on the jump clip
    public void JumpSound()
    {
        if (jumpClip != null) audioSource.PlayOneShot(jumpClip);
    }
}
