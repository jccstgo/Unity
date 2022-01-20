using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name == "Sol_2")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }
    }

    public void again() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -2);
    }
}
