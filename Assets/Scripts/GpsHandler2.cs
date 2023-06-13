using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class GpsHandler2 : MonoBehaviour
{
    void Awake()
    {
#if PLATFORM_ANDROID
       if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation)){
            Permission.RequestUserPermission(Permission.FineLocation);
            Permission.RequestUserPermission(Permission.Camera); 
       }
      
#endif

    }
    IEnumerator Start() {


        GameObject o = GameObject.Find("GpsInfo");
        o.GetComponent<TMPro.TextMeshProUGUI>().text = "Start ";
        // Check if the user has location service enabled.
        if (!Input.location.isEnabledByUser)
            yield break;
        o.GetComponent<TMPro.TextMeshProUGUI>().text = "Enabled by user ";
        // Starts the location service.
        Input.location.Start();

        // Waits until the location service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
    maxWait--;
        }

// If the service didn't initialize in 20 seconds this cancels location service use.
if (maxWait < 1)
{
    print("Timed out");
    yield break;
}

// If the connection failed this cancels location service use.
if (Input.location.status == LocationServiceStatus.Failed)
{
    print("Unable to determine device location");
    yield break;
}
else
{
    // If the connection succeeded, this retrieves the device's current location and displays it in the Console window.
    print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
}
       
        o.GetComponent<TMPro.TextMeshProUGUI>().text = "Start Finish: " + Input.location.lastData.longitude + "-"+ Input.location.lastData.altitude;
// Stops the location service if there is no need to query location updates continuously.
//Input.location.Stop();
    }
 
    void OnGUI()
    {
        if (GUILayout.Button("Restart  Gps", GUILayout.Width(600), GUILayout.Height(200)))
        {
            GameObject o = GameObject.Find("GpsInfo");
            o.GetComponent<TMPro.TextMeshProUGUI>().text = "--" + Input.location.lastData.latitude;
            StartCoroutine("restartGps");
        }
    }
  
    IEnumerator restartGps()
    {
        // Check if the user has location service enabled.
        if (!Input.location.isEnabledByUser)
            yield break;

        // Starts the location service.
        Input.location.Start();

        // Waits until the location service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // If the service didn't initialize in 20 seconds this cancels location service use.
        if (maxWait < 1)
        {
            print("Timed out");
            yield break;
        }

        // If the connection failed this cancels location service use.
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            print("Unable to determine device location");
            yield break;
        }
        else
        {
            // If the connection succeeded, this retrieves the device's current location and displays it in the Console window.
            print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
        }
    }
}
