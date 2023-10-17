using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceController : MonoBehaviour
{
    [SerializeField] private int radius;
    [SerializeField] private Transform transform;
    [SerializeField] private int velocityMultiplier;
    [SerializeField] private bool chasingPlayer;
    [SerializeField] private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void LateUpdate()
	{
		if(chasingPlayer)
		{
            Transform character = player.transform;
            Vector3 direction = (character.position - transform.position).normalized;
            transform.Translate(direction * Time.deltaTime * velocityMultiplier);
        }
	}

	void OnTriggerEnter(Collider other)
	{
        if(other.CompareTag("Player"))
		{
            //player = other.gameObject;
            chasingPlayer = true;
        }
	}

	private void OnTriggerExit(Collider other)
	{
        if (other.CompareTag("Player"))
        {
            chasingPlayer = false;
        }
    }
}
