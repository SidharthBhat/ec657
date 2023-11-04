using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newCaster : MonoBehaviour
{
    public PlayerInput playerControls;
    private PlayerInput.PlayerActions actions;
    [SerializeField] float cooldown;
    private float lastShot;

    SpellStack spellStack;
    [SerializeField] GameObject sStack;
    HotBarController hotBarController;
    [SerializeField] GameObject hotBar;

    // Start is called before the first frame update
    void Start()
    {
        playerControls = new PlayerInput();
        playerControls.Enable();
        actions = playerControls.Player;
        spellStack = sStack.GetComponent<SpellStack>();
        hotBarController = hotBar.GetComponent<HotBarController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (actions.Shoot.IsPressed()) {
            if (Time.time - lastShot > cooldown)
            {
                spellStack.castStack();
                lastShot = Time.time;
            }
        }
        if (actions.Hotbar.WasPressedThisFrame())
        {
            int slot = (int) actions.Hotbar.ReadValue<float>();
            hotBarController.AddSpell(slot-1);
        }
    }
}
