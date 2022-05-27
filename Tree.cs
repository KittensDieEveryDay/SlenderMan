using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public Transform target;
    public float range = 4f;
    AudioSource ads;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player").transform;
        ads = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToTarget = Vector3.Distance(target.position, transform.position);

        if (distanceToTarget <= range)
        {
            Vector3 targetDiraction = transform.position - target.position;
            Quaternion lookRotation = Quaternion.LookRotation(targetDiraction);
            Vector3 rotation = lookRotation.eulerAngles;
            transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
            ads.enabled = true;
        }  
        else
        {
            ads.enabled = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
