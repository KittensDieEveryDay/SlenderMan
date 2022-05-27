using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesTriggers : MonoBehaviour
{
    public AudioClip creepyBreath;
    
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().playOnAwake = false;
        GetComponent<AudioSource>().clip = creepyBreath;
        PlayerEngine_v2 player = GameObject.FindWithTag("Player").GetComponent<PlayerEngine_v2>();
        player.notesAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision col)
    {
        PlayerEngine_v2 player = GameObject.FindWithTag("Player").GetComponent<PlayerEngine_v2>();

        if (col.gameObject.name == "Player")
        {
            GameObject.Find("Player").GetComponent<Flashlight>().RestoreFlashlight();
            player.isCollected = true;
            GetComponent<AudioSource>().Play();
            Destroy(gameObject, 0.5f);
        }
        player.notesAmount += 1;
        player.isCollected = false;

        //player.notesAmount+=1;
    }
}
