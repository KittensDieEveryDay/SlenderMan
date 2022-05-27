using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flashlight : MonoBehaviour
{
    [Header("Flashlight Parameters")]
    public GameObject flashlight;
    public bool isOn = false;

    [SerializeField] float batteryLife;
    public float maxBattery = 5f;
    float batteryDrain = 0.025f;
    public Slider batteryLifeSlider;

    [Header("Charging System")]
    bool isCharging = false;
    public GameObject chargingAnm;
    public GameObject radioTower;
    float towerChargingRange = 2f;

    [Header("Sound")]
    public AudioClip clickSound;

    GameManager gameManager;
    AudioSource ads;
   
    // Start is called before the first frame update
    void Start()
    {
        ads = GameObject.Find("Flashlight").GetComponent<AudioSource>();

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        isOn = false;

        batteryLife = maxBattery;
    }

    // Update is called once per frame
    void Update()
    {
        flashlight.GetComponent<Light>().intensity = batteryLife;

        if (!gameManager.isIntroPlaying)
        {
            batteryLifeSlider.value = batteryLife;

            if (batteryLife <= 0f)
            {
                batteryLife = 0f;
                isOn = false;
            }
            
            // flashlight toggle on/off
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                ToggleFlashLight();
                ToggleFlashLightSFX();

                if (isOn)
                {
                    isOn = false;
                }
                else if (!isOn && batteryLife > 0f)
                {
                    isOn = true;
                }
            }

            BatteryDrain();
            CheckRadioTowerDistance();
        }
    }

    public void ToggleFlashLight()
    {
        if (isOn)
        {
            flashlight.GetComponent<Light>().enabled = false;
        }
        else
        {
            flashlight.GetComponent<Light>().enabled = true;
        }
    }

    public void ToggleFlashLightSFX()
    {
        ads.clip = clickSound;
        ads.PlayOneShot(clickSound);
    }

    public void CheckRadioTowerDistance()
    {
        // battery charge near Radio Tower engine
        float distance = Vector3.Distance(gameObject.transform.position, radioTower.transform.position);

        if (distance <= towerChargingRange && batteryLife <= maxBattery)
        {
            isCharging = true;
            chargingAnm.SetActive(true);
            batteryLife += batteryDrain * 10 * Time.deltaTime;
            if (batteryLife >= maxBattery)
            {
                batteryLife = maxBattery;
            }
        }
        else
        {
            isCharging = false;
            chargingAnm.SetActive(false);
        }
    }

    public void BatteryDrain()
    {
        // battery drain
        if (isOn && batteryLife >= 0f && !isCharging)
        {
            batteryLife -= batteryDrain * Time.deltaTime;
        }
    }

    public void RestoreFlashlight()
    {
        maxBattery = 5f;
        batteryLife = maxBattery;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(radioTower.transform.position, towerChargingRange);
    }
}
