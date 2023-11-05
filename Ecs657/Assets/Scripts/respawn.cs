using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform respawnPoint;

    // Teleports player to respawn point when colliding with object
    // Used to prevent falling off of map
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Entered");
        if (other.CompareTag("Player"))
        {
            CharacterController charactercontroller = player.GetComponent<CharacterController>();
            charactercontroller.enabled = false;
            player.transform.position = respawnPoint.transform.position;
            charactercontroller.enabled = true;
        }
    }
}
