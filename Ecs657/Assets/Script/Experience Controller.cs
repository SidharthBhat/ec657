using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceController : MonoBehaviour
{
    [SerializeField] private int velocityMultiplier;
    [SerializeField] private bool chasingPlayer;
    private GameObject player;
    [SerializeField] private GameObject self;
    [SerializeField] private float xp;
    [SerializeField] private float bobAmplitude = 0.5f; // Adjust this value to control the bobbing amplitude
    [SerializeField] private float bobSpeed = 2.0f; // Adjust this value to control the bobbing speed

    private Vector3 initialPosition;

    void Start()
    {
        // Searches for player
        player = GameObject.FindGameObjectWithTag("Player");
        initialPosition = transform.position;
    }

    // Allows for a variable quantity of exp to be dropped
    public void SetXp(float value)
	{
        xp = value;
	}

    void LateUpdate()
    {
        // Follows player
        if (chasingPlayer)
        {
            Transform character = player.transform;
            Vector3 direction = (character.position - transform.position).normalized;
            this.transform.Translate(direction * Time.deltaTime * velocityMultiplier);
        }
    }

    // Detects collision with player and allows them to gain exp
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerStats>().AddXp(xp);
            Destroy(self);
        }
        else if (other.CompareTag("xpPickUp"))
        {
            player = other.gameObject;
            chasingPlayer = true;
        }
    }
}