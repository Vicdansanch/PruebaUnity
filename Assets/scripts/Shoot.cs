using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Shoot : MonoBehaviour
{
    public GameObject ball;
    private Vector3 velocidadLanzamiento = new Vector3(0, 10, 30);
    public Vector3 ballPos;
    private bool lanzado = false;
    private GameObject ballClone;

    public Text availableShotsGO;
    private int availableShots = 5;

    public GameObject meter;
    public GameObject arrow;
    private float arrowSpeed = 0.1f; //Difficulty
    private bool right = true;

    public GameObject gameOver;
    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity = new Vector3(0, -20, 0);
    }

     void FixedUpdate()
    {
        if (arrow.transform.position.x < .2f && right)
        {
            arrow.transform.position += new Vector3(arrowSpeed, 0, 0);
        }
        if (arrow.transform.position.x >= .2f)
        {
            right = false;
        }
        if (right == false)
        {
            arrow.transform.position -= new Vector3(arrowSpeed, 0, 0);
        }
        if (arrow.transform.position.x <= -2.57f)
        {
            right = true;
        }


        //Disparar Pelota
        if (Input.GetKey(KeyCode.Space) && !lanzado && availableShots > 0)
        {
            lanzado = true;
            availableShots--;
            availableShotsGO.text = availableShots.ToString();

            ballClone = Instantiate(ball, ballPos, transform.rotation) as GameObject;
            velocidadLanzamiento.y = velocidadLanzamiento.y + arrow.transform.position.x;
            velocidadLanzamiento.z = velocidadLanzamiento.z + arrow.transform.position.x;

            ballClone.GetComponent<Rigidbody>().AddForce(velocidadLanzamiento, ForceMode.Impulse);
            GetComponent<AudioSource>().Play();
        }

        /* Remove Ball when it hits the floor */

        if (ballClone != null && (ballClone.transform.position.y < -16 || ballClone.transform.position.z >15))
        {
            Destroy(ballClone);
            lanzado = false;
            velocidadLanzamiento = new Vector3(0, 26, 40);//Reset perfect shot variable

            /* Check if out of shots */

            if (availableShots == 0)
            {
                arrow.GetComponent<Renderer>().enabled = false;
                Instantiate(gameOver, new Vector3(0.31f, 0.2f, 0), transform.rotation);
                Invoke("restart", 2);
            }
        }



    }
    void restart()
    {
        Debug.Log(SceneManager.GetActiveScene());

        //SceneManager.LoadScene();
    }
    
}
