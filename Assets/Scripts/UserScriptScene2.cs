using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UserScriptScene2: MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MapLocation());
    }

    public IEnumerator MapLocation()
    {
        for (; ; )
        {
            yield return new WaitForSeconds(5);
            double x = Math.Floor(MapManager2.TileX);
            double y = Math.Floor(MapManager2.TileY);
            int zoom = MapManager.zoom;

            double a = DrawCubeX(Input.location.lastData.longitude, TileToWorldPos(x, y, zoom).X, TileToWorldPos(x + 1, y, zoom).X);
            double b = DrawCubeY(Input.location.lastData.latitude, TileToWorldPos(x, y + 1, zoom).Y, TileToWorldPos(x, y, zoom).Y);

            float aa = (float)a;
            if (aa >= 1.0f) aa = -(aa - 1.0f);
            if ((aa > 0.0f) && (aa < 1.0f)) aa = 1.0f - aa;
            if (aa <= 0.0f) a = Math.Abs(aa);
            if (aa == 0) aa = 1.0f;
            if (aa == 1) aa = 0.0f;

            //Debug.Log(" Pos" + a + "/" + b);
            GameObject o = GameObject.Find("Description");
            o.GetComponent<TMPro.TextMeshProUGUI>().text = "Usuari " + a + "--" +b;
            this.transform.position = new Vector3((float)aa - 0.5f, 0.0f, (float)b - 0.5f);
            
        }
    }
    public struct Point
    {
        public double X;
        public double Y;
    }


    // p.X -> longitud
    // p.Y -> latitud
    // left upper corner
    public Point TileToWorldPos(double tile_x, double tile_y, int zoom)
    {
        Point p = new Point();
        double n = System.Math.PI - ((2.0 * System.Math.PI * tile_y) / System.Math.Pow(2.0, zoom));

        p.X = ((tile_x / System.Math.Pow(2.0, zoom) * 360.0) - 180.0);
        p.Y = (180.0 / System.Math.PI * System.Math.Atan(System.Math.Sinh(n)));

        return p;
    }

    public double DrawCubeY(double targetLat, double minLat, double maxLat)
    {
        double pixelY = ((targetLat - minLat) / (maxLat - minLat));
        return pixelY;
    }

    public double DrawCubeX(double targetLong, double minLong, double maxLong)
    {
        double pixelX = ((targetLong - minLong) / (maxLong - minLong));
        return pixelX;
    }


   
}
