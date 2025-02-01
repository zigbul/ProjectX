using UnityEngine;

public class Lava : MonoBehaviour
{
    private float _damage = -20f;

    private const string PLAYER_TAG = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PLAYER_TAG))
        {
            other.GetComponent<Status>().ChangeHealth(_damage);
        }
    }
}
