using UnityEngine;

public class EnemyTouchPlayer : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            GameManager.Instance.Lose();
    }
}