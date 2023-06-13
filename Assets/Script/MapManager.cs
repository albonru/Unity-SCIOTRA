using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Diagnostics;

public class MapManager : MonoBehaviour
{
    [SerializeField]
    GameObject leftup;
    [SerializeField]
    GameObject rightup;
    [SerializeField]
    GameObject rightdown;
    [SerializeField]
    GameObject leftdown;
    [SerializeField]
    GameObject Lleftdown;
    [SerializeField]
    GameObject Lleftup;

    public static double TileX;
    public static double TileY;
    public static int zoom;

    // Start is called before the first frame update
    void Start()
    {
        zoom = 12;
        GetTile(41.40645,2.15223);
        StartCoroutine(GetOpenStreetMap());
    }

    IEnumerator GetOpenStreetMap()
    {
 	    UnityEngine.Debug.Log("https://a.tile.openstreetmap.org/" + zoom + "/" + Math.Floor(TileX) + "/" +Math.Floor(TileY+1) + ".png");

        UnityWebRequest www = UnityWebRequestTexture.GetTexture("https://a.tile.openstreetmap.org/"+zoom+"/"+Math.Floor(TileX)+"/"+Math.Floor(TileY)+".png");
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError)
        {
            UnityEngine.Debug.Log(www.error);
        }
        else
        {
            Texture myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            leftup.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", myTexture);
        }

        www = UnityWebRequestTexture.GetTexture("https://a.tile.openstreetmap.org/" + zoom + "/" + Math.Floor(TileX+1)+ "/" + Math.Floor(TileY) + ".png");
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError)
        {
            UnityEngine.Debug.Log(www.error);
        }
        else
        {
            Texture myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            rightup.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", myTexture);

        }

        www = UnityWebRequestTexture.GetTexture("https://a.tile.openstreetmap.org/" + zoom + "/" + Math.Floor(TileX)+ "/" + Math.Floor(TileY+1) + ".png");
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError)
        {
            UnityEngine.Debug.Log(www.error);
        }
        else
        {
            Texture myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            leftdown.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", myTexture);

        }

        www = UnityWebRequestTexture.GetTexture("https://a.tile.openstreetmap.org/" + zoom + "/" + Math.Floor(TileX+1)+ "/" + Math.Floor(TileY+1) + ".png");
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError)
        {
            UnityEngine.Debug.Log(www.error);
        }
        else
        {
            Texture myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            rightdown.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", myTexture);

        }

        www = UnityWebRequestTexture.GetTexture("https://a.tile.openstreetmap.org/" + zoom + "/" + Math.Floor(TileX-1)+ "/" + Math.Floor(TileY+1) + ".png");
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError)
        {
            UnityEngine.Debug.Log(www.error);
        }
        else
        {
            Texture myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            Lleftdown.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", myTexture);

        }

        www = UnityWebRequestTexture.GetTexture("https://a.tile.openstreetmap.org/" + zoom + "/" + Math.Floor(TileX-1)+ "/" + Math.Floor(TileY) + ".png");
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError)
        {
            UnityEngine.Debug.Log(www.error);
        }
        else
        {
            Texture myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            Lleftup.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", myTexture);

        }

        GameObject openDataloader = GameObject.Find("OpenData");
        openDataloader.GetComponent<OpenDataDownload>().SendMessage("DownLoadData");


    }

    public void GetTile(double lat, double lon)
    {
        TileX = (float)((lon + 180.0) / 360.0 * (1 << zoom));
        TileY = (float)((1.0 - Math.Log(Math.Tan(lat * Math.PI / 180.0) +
        1.0 / Math.Cos(lat * Math.PI / 180.0)) / Math.PI) / 2.0 * (1 <<
        zoom));


    }
}
