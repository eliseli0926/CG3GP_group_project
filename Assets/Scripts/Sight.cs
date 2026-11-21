// Elise Li, Sophia Cho
// ============================================================================
// Sight.cs
// The enemy's sight, used for its movement AI.
// Set objectsLayers to the Player's layer, and set obstacleLayers to the
// Default layer.
// ============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sight : MonoBehaviour
{
    [Header("Vision Settings")]
    [Tooltip("The distance the enemy can see.")]
    [SerializeField] private float distance;
    [Tooltip("The angle the enemy can see to each side from the centerline.")]
    [SerializeField] private float angle;

    [Header("Object Detection")]
    [Tooltip("Layers that can block the enemy from seeing the target.")]
    [SerializeField] private LayerMask obstacleLayers;
    [Tooltip("Layers for the enemy to view as a target. The target must have a collider.")]
    [SerializeField] private LayerMask objectsLayers;
    [Tooltip("The currently detected object. Do not change this in the inspector.")]
    public Collider detectedObject;

    [Header("Sound")]
    [Tooltip("Loops while the enemy can see the player.")]
    [SerializeField] private AudioClip laughClip;
    [Tooltip("Within this distance the laugh plays at full volume; it fades out beyond it.")]
    [SerializeField] private float laughRange = 10f;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = laughClip;
        audioSource.loop = true;
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 1f;   // 3D sound: louder when the enemy is closer
        audioSource.minDistance = laughRange;
    }

    // Determines the currently detected object based on whether the target is within the viewing angle and distance and not blocked by obstacles
    void Update()
    {
        FindTarget();

        // Loop the laugh while the player is visible; stop when out of sight
        bool seesPlayer = detectedObject != null;
        if (seesPlayer && !audioSource.isPlaying) audioSource.Play();
        else if (!seesPlayer && audioSource.isPlaying) audioSource.Stop();
    }

    private void FindTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, distance, objectsLayers);
        detectedObject = null;

        for (int i = 0; i < colliders.Length; i++)
        {
            Collider collider = colliders[i];
            Vector3 directionToCollider = Vector3.Normalize(collider.bounds.center - transform.position);
            float angleToCollider = Vector3.Angle(transform.forward, directionToCollider);

            if (angleToCollider < angle)
            {
                if (!Physics.Linecast(transform.position, collider.bounds.center, out RaycastHit hit, obstacleLayers, QueryTriggerInteraction.Ignore) || hit.collider.transform.root == collider.transform.root)
                {
                    Debug.DrawLine(transform.position, collider.bounds.center, Color.green);
                    detectedObject = collider;
                    break;
                } else
                {
                    Debug.Log($"Blocked by {hit.collider.name} (layer: {LayerMask.LayerToName(hit.collider.gameObject.layer)})", hit.collider);
                    Debug.DrawLine(transform.position, hit.point, Color.red);
                }
            }
        }
    }

    // Displays gizmos for enemy viewing distance and angle
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distance);

        Vector3 rightDirection = Quaternion.Euler(0, angle, 0) * transform.forward;
        Gizmos.DrawRay(transform.position, rightDirection * distance);

        Vector3 leftDirection = Quaternion.Euler(0, -angle, 0) * transform.forward;
        Gizmos.DrawRay(transform.position, leftDirection * distance);
    }
}
