using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] GameObject ammoPrefab;
    [SerializeField] Transform whereItSpawns;
    [SerializeField] AudioSource audioSource;


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            audioSource.Play();
            Instantiate(ammoPrefab, whereItSpawns.position, whereItSpawns.rotation);
        }
    }
}
