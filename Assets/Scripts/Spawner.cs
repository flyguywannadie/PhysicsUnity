using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject toSpawn;
    [SerializeField] KeyCode keyToPress;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            Instantiate(toSpawn, transform.position, Quaternion.identity);
        }
    }
}
