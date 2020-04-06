using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    private AudioSource hammerSound;
    private Vector3 initialPosition;
    // Start is called before the first frame update
    void Start()
    {
        hammerSound = GetComponent<AudioSource>();
        initialPosition = transform.position;
    }

    public void Hit(Vector3 targetPosition)
    {
        hammerSound.Play();
        transform.position = targetPosition;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, initialPosition, Time.deltaTime);
    }
}
