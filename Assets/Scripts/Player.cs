using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    AudioSource successfulHit;
    public Hammer hammer;
    public int score = 0;
    private 
    // Start is called before the first frame update
    void Start()
    {
        successfulHit = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.instance.gameStarted)
        {
            RaycastHit hit;
            if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Space))
            {
                if (Physics.Raycast(transform.position, transform.forward, out hit))
                {
                    if (hit.transform.GetComponent<Mole>() != null)
                    {
                        Debug.Log("Hit a mole!");
                        Mole mole = hit.transform.GetComponent<Mole>();
                        if (mole.givesPoints)
                        {
                            successfulHit.Play();
                            score++;
                        }
                        mole.OnHit();
                        hammer.Hit(mole.transform.position);
                    }
                    if (hit.transform.tag == "Table")
                    {
                        hammer.Hit(hit.point);
                    }

                    Debug.Log(hit.transform.name);
                }
            }
        }
    }
}
