using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabinManager : MonoBehaviour
{
    AudioSource ads;
    Animator chairAnimator;
    public GameObject rockingChair;
    public GameObject tvLight;
    public GameObject windowScareJump;
    AudioSource chairSwingSFX;
    AudioSource tvSFX;
    AudioSource doorSlamSFX;
    AudioSource scaryPianoSFX;
    bool jumpScareFinshed;

    // Start is called before the first frame update
    void Start()
    {
        ads = GetComponent<AudioSource>();
        chairAnimator = rockingChair.gameObject.GetComponent<Animator>();

        chairSwingSFX = GameObject.Find("Rocking Chair").GetComponent<AudioSource>();
        tvSFX = GameObject.Find("TV").GetComponent<AudioSource>();
        doorSlamSFX = GameObject.Find("Slamed Door").GetComponent<AudioSource>();
        scaryPianoSFX = GameObject.Find("PianoNote").GetComponent<AudioSource>();
        jumpScareFinshed = false;
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    public void CabinJumpScare()
    {
        // Show a scary img infront of the window for 0.5 sec
        scaryPianoSFX.Play();
        windowScareJump.SetActive(true);

        // Sets chair anim to idle and stops swinging sound
        chairAnimator.SetBool("Idle", true);
        chairSwingSFX.Stop();

        // stop tv static sound
        tvSFX.Stop();
        tvLight.SetActive(false);

        // play door slam
        doorSlamSFX.Play();
        Destroy(windowScareJump, 0.5f);

        // switch bool to true
        jumpScareFinshed = true;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !jumpScareFinshed)
        {
            CabinJumpScare();
        }
    }
}
