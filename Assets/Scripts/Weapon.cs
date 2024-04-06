using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
	[Serializable]
	struct WeaponAmmo
	{
        public GameObject prefab;
        public float fireRate;
		public Sprite icon;
		public float shootCooldown;
	}

	[SerializeField] WeaponAmmo[] ammoPrefab;
    [SerializeField] int ammoSelection;
    [SerializeField] Transform whereItSpawns;
    [SerializeField] AudioSource audioSource;
	[SerializeField] Image ammoImg;
	[SerializeField] TextMeshProUGUI weaponText;


    void Update()
    {
		for (int i = 0; i < ammoPrefab.Length; i++)
		{
			if (ammoPrefab[i].shootCooldown > 0)
			{
				ammoPrefab[i].shootCooldown -= Time.deltaTime;
			}
		}

		weaponText.text = ammoPrefab[ammoSelection].prefab.name + "\n" + ((ammoPrefab[ammoSelection].shootCooldown > 0) ? ammoPrefab[ammoSelection].shootCooldown.ToString("00") : "READY"); 

		if (Input.GetMouseButtonDown(0) && ammoPrefab[ammoSelection].shootCooldown <= 0)
        {
            audioSource.Play();
            Instantiate(ammoPrefab[ammoSelection].prefab, whereItSpawns.position, whereItSpawns.rotation);
			ammoPrefab[ammoSelection].shootCooldown = ammoPrefab[ammoSelection].fireRate;
		}

		if (Input.GetKeyDown(KeyCode.S))
		{
			ammoSelection--;
			if (ammoSelection < 0)
			{
				ammoSelection = ammoPrefab.Length - 1;
			}
			SwapAmmo();
		}

		if (Input.GetKeyDown(KeyCode.W))
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
		ammoImg.sprite = ammoPrefab[ammoSelection].icon;
	}
}
