using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceController : MonoBehaviour
{
    [SerializeField] private int velocityMultiplier;
    [SerializeField] private bool chasingPlayer;
    private GameObject player;
    [SerializeField] private GameObject self;
    [SerializeField] private float bobAmplitude = 0.5f; // Adjust this value to control the bobbing amplitude
    [SerializeField] private float bobSpeed = 2.0f; // Adjust this value to control the bobbing speed
    [SerializeField] private float xp;

    private Vector3 initialPosition;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        initialPosition = transform.position;
    }

    public void SetXp(int value)
	{
        xp = value;
	}

    void LateUpdate()
    {
        if (chasingPlayer)
        {
            Transform character = player.transform;
            Vector3 direction = (character.position - transform.position).normalized;
            this.transform.Translate(direction * Time.deltaTime * velocityMultiplier);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        print(other.tag);
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