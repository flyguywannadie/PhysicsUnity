using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterManager : MonoBehaviour
{
    [SerializeField] Transform[] shootpoints;
    [SerializeField] int currentpoint = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Camera.main.transform.localPosition = Vector3.Lerp(Camera.main.transform.localPosition, Vector3.zero, 2 * Time.deltaTime);
        Camera.main.transform.localRotation = Quaternion.Lerp(Camera.main.transform.localRotation, Quaternion.identity, 2 * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Q))
        {
            currentpoint--;
            if (currentpoint < 0)
            {
                currentpoint = shootpoints.Length - 1;
            }
            SwitchPoint();
		}

        if (Input.GetKeyDown(KeyCode.E))
        {
            currentpoint++;
            if (currentpoint >= shootpoints.Length)
            {
                currentpoint = 0;
            }
            SwitchPoint();
		}
    }

    private void SwitchPoint()
    {
        Camera.main.transform.SetParent(shootpoints[currentpoint], true);
    }
}