// Elise Li, Sophia Cho
// ============================================================================
// SpottedVignette.cs
// Shows a flickering red vignette (red, darkened screen edges) while any
// enemy can see the player. Fades in when spotted and fades out when the
// enemy loses sight of the player.
//
// Uses URP post-processing: attach to a Global Volume whose profile has a
// Vignette override. The Main Camera must have Post Processing ticked.
// ============================================================================

using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class SpottedVignette : MonoBehaviour
{
    [Header("Volume")]
    [Tooltip("The Global Volume with a Vignette override. Leave empty to use the one on this object.")]
    [SerializeField] private Volume volume;

    [Header("Look")]
    [SerializeField] private Color spottedColor = new Color(0.6f, 0f, 0f);
    [Tooltip("How strong the vignette gets when fully spotted (0 = none, 1 = very strong).")]
    [Range(0f, 1f)] [SerializeField] private float maxIntensity = 0.45f;

    [Header("Flicker")]
    [Tooltip("How much the intensity jumps around while flickering.")]
    [Range(0f, 0.5f)] [SerializeField] private float flickerAmount = 0.15f;
    [Tooltip("How fast it flickers.")]
    [SerializeField] private float flickerSpeed = 8f;

    [Header("Fade")]
    [Tooltip("How fast the vignette fades in when spotted and out when hidden.")]
    [SerializeField] private float fadeSpeed = 2f;

    private Vignette vignette;
    private Sight[] sights;
    private float strength; 

    private void Start()
    {
        if (volume == null) volume = GetComponent<Volume>();

        // .profile makes a copy for this play session, so the saved profile asset isn't changed.
        if (volume == null || !volume.profile.TryGet(out vignette))
        {
            Debug.LogWarning("SpottedVignette: no Volume with a Vignette override found.", this);
            enabled = false;
            return;
        }

        vignette.active = true;
        vignette.color.overrideState = true;
        vignette.intensity.overrideState = true;
        vignette.color.value = spottedColor;
        vignette.intensity.value = 0f;

        // Every enemy's Sight in the scene.
        sights = FindObjectsByType<Sight>(FindObjectsSortMode.None);
    }

    private void Update()
    {
        // Spotted if ANY enemy can currently see the player.
        bool spotted = false;
        foreach (Sight sight in sights)
        {
            if (sight != null && sight.detectedObject != null)
            {
                spotted = true;
                break;
            }
        }

        // Smoothly fade toward 1 when spotted, 0 when not.
        strength = Mathf.MoveTowards(strength, spotted ? 1f : 0f, fadeSpeed * Time.deltaTime);
        float flicker = (Mathf.PerlinNoise(Time.time * flickerSpeed, 0f) - 0.5f) * 2f * flickerAmount;

        vignette.intensity.value = Mathf.Clamp01(strength * (maxIntensity + flicker));
    }
}