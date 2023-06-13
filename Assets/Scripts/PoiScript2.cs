using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PoiScript2 : MonoBehaviour
{
	public double latitude;
	public double longitude;
	public string description;

    void Start()
    {
        this.gameObject.GetComponent<MeshRenderer>().material.color = Color.yellow;
    }
    public void OnMouseDown()
    {
        GameObject o = GameObject.Find("Description");
        o.GetComponent<TMPro.TextMeshProUGUI>().text = description;
        GameObject[] pois = GameObject.FindGameObjectsWithTag("poi");
        for (int i = 0;i < pois.Length; i++)
        {
            pois[i].gameObject.GetComponent<MeshRenderer>().material.color = Color.blue;

        }
        this.gameObject.GetComponent<MeshRenderer>().material.color = Color.yellow;
        UnityEngine.Debug.Log("Clicked");
    }
    public void MapLocation(){
		UnityEngine.Debug.Log("Point of interest located in the map");
        double x = Math.Floor(MapManager2.TileX);
        double y = Math.Floor(MapManager2.TileY);
        int zoom = MapManager2.zoom;

        double a = DrawCubeXx(longitude, TileToWorldPosx(x, y, zoom).X, TileToWorldPosx(x + 1, y, zoom).X);
        double b = DrawCubeYy(latitude, TileToWorldPosx(x, y + 1, zoom).Y, TileToWorldPosx(x, y, zoom).Y);

        UnityEngine.Debug.Log(" Pos" + a + "/" + b + "--"+ x +"---"+y);
        float aa = (float)a;
        if (aa >= 1.0f) aa = -(aa-1.0f);
        if ((aa > 0.0f) && (aa < 1.0f)) aa = 1.0f - aa;
        if (aa <= 0.0f) a = Math.Abs(aa);
        if (aa == 0) aa = 1.0f;
        if (aa == 1) aa = 0.0f;

        

        UnityEngine.Debug.Log(" Pos2 "+ aa + "/" + b + "--" + x + "---" + y);
        
       this.transform.position = new Vector3((float)aa - 0.5f, 0.0f, (float)b - 0.5f);

       GameObject map = GameObject.Find("Map");
       this.transform.SetParent(map.transform);

    }
    public struct Point
    {
        public double X;
        public double Y;
    }


    // p.X -> longitud
    // p.Y -> latitud
    // left upper corner
    public Point TileToWorldPosx(double tile_x, double tile_y, int zoom)
    {
        Point p = new Point();
        double n = System.Math.PI - ((2.0 * System.Math.PI * tile_y) / System.Math.Pow(2.0, zoom));

        p.X = ((tile_x / System.Math.Pow(2.0, zoom) * 360.0) - 180.0);
        p.Y = (180.0 / System.Math.PI * System.Math.Atan(System.Math.Sinh(n)));

        return p;
    }

    public double DrawCubeYy(double targetLat, double minLat, double maxLat)
    {
        double pixelY = ((targetLat - minLat) / (maxLat - minLat));
        return pixelY;
    }

    public double DrawCubeXx(double targetLong, double minLong, double maxLong)
    {
        double pixelX = ((targetLong - minLong) / (maxLong - minLong));
        return pixelX;
    }
}
