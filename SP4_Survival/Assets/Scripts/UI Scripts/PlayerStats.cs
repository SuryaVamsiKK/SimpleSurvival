using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{

    [Header("Health Values")]
    public float health;
    public float maxHealth;
    public float rateOfHealthLoseByRadiation;
    public float rateOfHealthGain;


    [Header("Radiation")]
    public float radiation;
    public float maxRadiation;


    [Header("Stamina Values")]
    public float stamina;
    public float maxStamina;
    public float rateOfStaminaLoseOnRun;
    public float rateOfStaminaLoseOnWalk;
    public float rateOfStaminaGain;
    public float amountOfReductionOnJump;


    [Header("UI")]

    public Color staminaColor = Color.white;
    public Slider staminaBar;
    public Color healthColor = Color.white;
    public Slider healthBar;
    public Color radiationColor = Color.white;
    public Slider radiationBar;

    [Header("Inventory")]
    public GameObject Inventory;
    public bool InventoryStatus = false;

    bool isSprinting, isIdel;

    void Start()
    {
        staminaBar.fillRect.GetComponent<Image>().color = staminaColor;
        healthBar.fillRect.GetComponent<Image>().color = healthColor;
        radiationBar.fillRect.GetComponent<Image>().color = radiationColor;
        
        staminaBar.maxValue = maxStamina;
        healthBar.maxValue = maxHealth;
        radiationBar.maxValue = maxRadiation;
    }
    
    void Update()
    {
        inventory();
        StaminaCalculation();
        HealthCalculation();
    }

    float ClampStats(float stat, float max)
    {
        if (stat > max)
        {
            stat = max;
        }
        if (stat <= 0)
        {
            stat = 0;
        }
        return stat;
    }
    void StaminaCalculation()
    {
        staminaBar.maxValue = maxStamina;

        if (Input.GetKey(GetComponent<Controls>().Run))
        {
            isSprinting = true;
        }
        else
        {
            isSprinting = false;
        }
        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
        {
            isIdel = true;
        }
        else
        {
            isIdel = false;
        }

        if (isSprinting)
        {
            stamina -= rateOfStaminaLoseOnRun / maxStamina;
        }
        if (!isIdel && !isSprinting)
        {
            stamina -= rateOfStaminaLoseOnWalk / maxStamina;
        }

        if (isIdel && !isSprinting)
        {
            stamina += rateOfStaminaGain / maxStamina;
        }

        if(Input.GetKeyDown(GetComponent<Controls>().Jump) && stamina > amountOfReductionOnJump)
        {
            stamina -= amountOfReductionOnJump;
        }
        
        stamina = ClampStats(stamina, maxStamina);
        staminaBar.value = stamina;
    }
    void HealthCalculation()
    {
        healthBar.maxValue = maxHealth;
        radiationBar.maxValue = maxRadiation;

        if (radiation > 0)
        {
            health -= (rateOfHealthLoseByRadiation / maxHealth) * (radiation / 50);
        }
        else
        {
            health += rateOfHealthGain / maxHealth;
        }
        
        health = ClampStats(health, maxHealth);
        radiation = ClampStats(radiation, maxRadiation);

        healthBar.value = health;
        radiationBar.value = radiation;
    }
    void inventory()
    {
        if (Input.GetKeyDown(GetComponent<Controls>().Inventory))
        {
            InventoryStatus = !InventoryStatus;
        }
        if (InventoryStatus == true)
        {
            Cursor.lockState = CursorLockMode.None;
            Inventory.SetActive(true);
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Inventory.transform.GetChild(3).gameObject.SetActive(true);
            Inventory.transform.GetChild(4).gameObject.SetActive(false);
            Inventory.SetActive(false);
        }
    }
}
