using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Respawns player if they make contact with certain objects ( used to prevent falling off of map
public class Respawn : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform respawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Entered");
        // Checks if player has made contact with the respawn barrier
        if (other.CompareTag("Player"))
        {
            CharacterController charactercontroller = player.GetComponent<CharacterController>();
            charactercontroller.enabled = false;
            // Teleports player to the respawn point
            player.transform.position = respawnPoint.transform.position;
            charactercontroller.enabled = true;
        }
    }
}
