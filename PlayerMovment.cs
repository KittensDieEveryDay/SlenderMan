using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    [Header("Game Manager")]
    GameManager gameManager;
    
    [Header("Movment Parameters")]
    float horizontal;
    float vertical;
    float speed;
    float walkingSpeed;
    float runningSpeed;
    [SerializeField] bool isRunning;

    [Header("Camera Settings")]
    Transform cam;
    float mouseX;
    float mouseY;
    float cameraSpeed;
    float cameraX;

    [Header("Stamina")]
    public float stamina;
    float maxStamina;
    float staminaReduce;
    float staminaRestore;
    [SerializeField] Slider staminaSlider;

    [Header("Layers")]
    public GameObject layerChecker;
    public LayerMask sewerLayer;
    public LayerMask indoorLayer;

    [Header("Sounds")]
    public AudioClip currentWalkSFX;
    public AudioClip currentRunSFX;

    public AudioClip walkingStepsSFX;
    public AudioClip runningStepsSFX;

    public AudioClip waterWalkStepsSFX;
    public AudioClip waterRunStepsSFX;

    public AudioClip indoorWalkStepsSFX;
    public AudioClip indoorRunStepsSFX;

    Animator anm;
    AudioSource ads;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        // Mouse cursor lock and disappear
        Cursor.lockState = CursorLockMode.Locked;
        print("Mouse is Invisible click Esc to reveal");

        // Animator & Audiosource setter
        anm = GameObject.Find("Cam Holder").GetComponent<Animator>();
        ads = GetComponent<AudioSource>();

        // Camera Parameters
        cam = GameObject.Find("Main Camera").transform;
        cameraSpeed = 300f;
        cameraX = 0f;

        // Movment Parameters
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        speed = 0f;
        walkingSpeed = 1.7f;
        runningSpeed = 3f;
        isRunning = false;

        // Stamina Parameters
        maxStamina = 35f;
        stamina = maxStamina;
        staminaReduce = 0.4f;
        staminaRestore = 0.8f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.isIntroPlaying)
        {
            // Camera look around
            mouseX = Input.GetAxis("Mouse X") * cameraSpeed * Time.deltaTime;
            transform.Rotate(0f, mouseX, 0f);

            // Camera look up/down
            mouseY = Input.GetAxis("Mouse Y") * cameraSpeed * Time.deltaTime;
            cameraX -= mouseY;
            cameraX = Mathf.Clamp(cameraX, -40f, 75f);
            cam.localRotation = Quaternion.Euler(cameraX, 0f, 0f);

            // Movment
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
            transform.Translate(horizontal * speed * Time.deltaTime, 0f, vertical * speed * Time.deltaTime);

            // Stamina reduce/restore
            staminaSlider.value = stamina;

            Walk();
            CheckLayer();
            Run();
            CheckIfRun();  
        }
    }
    public void Walk()
    {
        // Walking
        if ((horizontal != 0f || vertical != 0f) && !isRunning)
        {
            anm.SetBool("isWalk", true);
            anm.SetBool("isRun", false);

            ads.clip = currentWalkSFX;
            if (!ads.isPlaying)
            {
                ads.Play();
            }
        }
        else if ((horizontal != 0f || vertical != 0f) && isRunning)
        {
            if (!ads.isPlaying)
            {
                ads.Play();
            }
        }
        else
        {
            ads.Stop();

            anm.SetBool("isWalk", false);
            anm.SetBool("isRun", false);
        }
    }

    public void Run()
    {
        // Running
        if ((horizontal != 0f || vertical != 0f) && Input.GetKey(KeyCode.Space))
        {
            isRunning = true;

            if (isRunning)
            {
                ads.clip = currentRunSFX;

                anm.SetBool("isRun", true);
                anm.SetBool("isWalk", false);
            }
        }
        else
        {
            isRunning = false;
            if (!isRunning)
            {
                ads.clip = currentWalkSFX;
                anm.SetBool("isRun", false);
            }
        }
    }

    public void CheckIfRun()
    {
        if (isRunning)
        {
            speed = runningSpeed;
            stamina -= staminaReduce * Time.deltaTime;
            if (stamina <= 0f)
            {
                speed = walkingSpeed;
                stamina = 0f;
            }
        }
        else
        {
            speed = walkingSpeed;
            stamina += staminaRestore * Time.deltaTime;
            if (stamina >= maxStamina)
            {
                stamina = maxStamina;
            }
        }
    }

    public void CheckLayer()
    {
        // checks layer
        if (Physics.CheckSphere(layerChecker.transform.position, 0.5f, sewerLayer))
        {
            currentRunSFX = waterRunStepsSFX;
            currentWalkSFX = waterWalkStepsSFX;
        }
        else if (Physics.CheckSphere(layerChecker.transform.position, 0.5f, indoorLayer))
        {
            currentRunSFX = indoorRunStepsSFX;
            currentWalkSFX = indoorWalkStepsSFX;
        }
        else
        {
            currentRunSFX = runningStepsSFX;
            currentWalkSFX = walkingStepsSFX;
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(layerChecker.transform.position, 0.2f);
    }
}


