using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Canasta : MonoBehaviour
{
    public Text valorSc;  
    public AudioClip canasta;
    public int scoreVal=0;
    void OnCollisionEnter()
    {
        Debug.Log("play sound");        
        GetComponent<AudioSource>().Play();
    }

    void OnTriggerEnter()
    {
        scoreVal = int.Parse(valorSc.text)+1;
        valorSc.text =  scoreVal.ToString();        
        AudioSource.PlayClipAtPoint(canasta, transform.position);        
    }
}
