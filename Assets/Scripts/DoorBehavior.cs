using UnityEngine;

public class DoorTeleport : MonoBehaviour
{
    [SerializeField] private Vector2 teleportLocation; // Set destination in Inspector
    [SerializeField] private Transform player; // Drag player here in Inspector

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform == player) // Check if it's the player
        {
            player.position = teleportLocation; // Move player to destination
        }
    }
}
