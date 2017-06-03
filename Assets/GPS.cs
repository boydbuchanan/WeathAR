﻿using UnityEngine;
using System.Collections;

public class GPS : MonoBehaviour
{
    public static GPS Instance { get; set; }
    public float latitude;
    public float longitude;

    private void Start()
    {
        StartCoroutine(StartLocationService());
    }

    private IEnumerator StartLocationService()
    {
        if (!Input.location.isEnabledByUser)
        {
            Debug.Log("GPS Not Enabled");
            yield break;
        }

        Input.location.Start();
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait -= 1;
        }

        if (maxWait <= 0)
        {
            Debug.Log("GPS Timed out");
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.Log("Unable to determine location.");
            yield break;
        }

        // service working
        latitude = Input.location.lastData.latitude;
        longitude = Input.location.lastData.longitude;

        yield break;
    }
}
