using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public PlayerInput playerControls;
    private PlayerInput.PlayerActions actions;
    [SerializeField] GameObject projectile;
    [SerializeField] float projSpeed;
    [SerializeField] float cooldown;
    private float lastShot;
    // Start is called before the first frame update
    void Start()
    {
        playerControls = new PlayerInput();
        playerControls.Enable();
        actions = playerControls.Player;
    }

	void Awake()
	{

    }
	// Update is called once per frame
	void Update()
    {
        Shoot();
    }

    void Shoot()
    {
        if (actions.Shoot.IsPressed())
        {
            if (Time.time - lastShot > cooldown)
            {
                GameObject currentprojectile = Instantiate(projectile, transform.position + transform.forward, Quaternion.identity);
                currentprojectile.GetComponent<Rigidbody>().AddForce(transform.forward * projSpeed, ForceMode.Impulse);
                lastShot = Time.time;
            }
        }
    }
}
