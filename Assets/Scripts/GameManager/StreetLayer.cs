using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetLayer : MonoBehaviour {

    public GameObject street, street_middle, pavement;
    public int height_y = 8;
    public int width_x = 70;
    private Transform streetholder;
	// Use this for initialization
	void Start () {
        StreetSetup();
	}
	
	// Magic numbers everywhere

    void StreetSetup()
    {
        //Setting room size
        height_y = 3;
        width_x = 70;
    

        for (int x = -71; x < width_x + 1; x++)
        {
            for (int y = -3; y < height_y + 1; y++)
            {
                GameObject toInstantiate;
                float mid = y / 2;
                if (y == mid)
                {
                    toInstantiate = street_middle;
                }
                else {
                    toInstantiate = street;
                }
                
                if (x == -71 || x == width_x || y == -4 || y == height_y)
                {
                    toInstantiate = pavement;
                }

                GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
                instance.transform.SetParent(streetholder);
                
            }
        }
    }
}
