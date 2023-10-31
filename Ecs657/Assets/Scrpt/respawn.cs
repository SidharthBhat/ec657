using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform respawnPoint;

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
