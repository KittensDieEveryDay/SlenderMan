using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopDoorMovement : MonoBehaviour
{
    Transform player;
    public Animator anim;
    public float distance;
    bool isOpen;
    public AudioClip doorCreak;

    // Start is called before the first frame update
    void Start()
    {
        isOpen = false;
        GetComponent<AudioSource>().playOnAwake = false;
        GetComponent<AudioSource>().clip = doorCreak;
    }

    // Update is called once per frame
    void Update()
    {
        Welcome();
    }

    private void Welcome()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance <= 1.5f && !isOpen)
        {
            anim.SetBool("isOpening", true);
            GetComponent<AudioSource>().Play();
            anim.SetBool("isClosing", false);
            isOpen = true;
        }
        else if(distance >= 1.5f && isOpen)
        {
            anim.SetBool("isClosing", true);
            GetComponent<AudioSource>().Play();
            anim.SetBool("isOpening", false);
            isOpen = false;
        }
    }
}
