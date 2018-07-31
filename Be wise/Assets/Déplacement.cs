using System.Collections;

using System.Collections.Generic;

using UnityEngine;



public class Déplacement : MonoBehaviour
{



    public float speed = 15;



    // Use this for initialization

    void Start()
    {



    }



    // Update is called once per frame

    void Update()
    {

        if (Input.GetKey("z"))
        {

            transform.position += transform.forward * Time.deltaTime * speed;

        }

        if (Input.GetKey("s"))
        {

            transform.position -= transform.forward * Time.deltaTime * speed;

        }



        if (Input.GetKey("d"))
        {

            transform.position += transform.right * Time.deltaTime * speed;

        }



        if (Input.GetKey("q"))
        {

            transform.position -= transform.right * Time.deltaTime * speed;

        }

    }

}
