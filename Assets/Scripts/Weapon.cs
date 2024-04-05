using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	[Serializable]
	struct WeaponAmmo
	{
        public GameObject prefab;
        public float fireRate;
		public Sprite icon;
	}

	[SerializeField] WeaponAmmo[] ammoPrefab;
    [SerializeField] int ammoSelection;
    [SerializeField] Transform whereItSpawns;
    [SerializeField] AudioSource audioSource;

	[SerializeField]

    float shootCooldown = 0;


    void Update()
    {
        if (shootCooldown > 0)
        {
            shootCooldown -= Time.deltaTime;
        } 
        else if (Input.GetMouseButtonDown(0))
        {
            audioSource.Play();
            Instantiate(ammoPrefab[ammoSelection].prefab, whereItSpawns.position, whereItSpawns.rotation);
            shootCooldown = ammoPrefab[ammoSelection].fireRate;

		}

		if (Input.GetKeyDown(KeyCode.W))
		{
			ammoSelection--;
			if (ammoSelection < 0)
			{
				ammoSelection = ammoPrefab.Length - 1;
			}
			SwapAmmo();
		}

		if (Input.GetKeyDown(KeyCode.S))
		{
			ammoSelection++;
			if (ammoSelection >= ammoPrefab.Length)
			{
				ammoSelection = 0;
			}
			SwapAmmo();
		}
	}

	private void SwapAmmo()
	{

	}
}
