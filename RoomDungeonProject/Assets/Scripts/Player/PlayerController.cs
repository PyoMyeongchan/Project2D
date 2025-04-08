using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerMove movement;
    private PlayerAttack attack;
    private PlayerDamage damage;
    private PlayerWeaponMode weaponMode;
    private PlayerUImanager uiManager;


    private void Awake()
    {
        movement = GetComponent<PlayerMove>();
        attack = GetComponent<PlayerAttack>();
        damage = GetComponent<PlayerDamage>();
        weaponMode = GetComponent<PlayerWeaponMode>();
        uiManager = GetComponent<PlayerUImanager>();
        
    }

    void Update()
    {
        movement.HandleMovement();
        attack.InputAttack();
        //uiManager.PauseGame();

    }

}
