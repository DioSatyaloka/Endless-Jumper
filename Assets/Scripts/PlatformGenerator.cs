using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour {

    public GameObject platform;
    public Transform generationPoint;
    public float distanceBetween;

    private float platformWidth;

    public float distanceBetweenMin;
    public float distanceBetweenMax;

    //public GameObject[] platforms;
    private int platformSelector;
    private float[] platformWidths;

    public ObjectPooler[] objectPools;

    private float minHeight;
    public Transform maxHeightPoint;
    private float maxHeight;
    public float maxHeightChange;
    private float heightChange;


	// Use this for initialization
	void Start () {

        //platformWidth = platform.GetComponent<BoxCollider2D>().size.x;

        platformWidths = new float[objectPools.Length];

        for(int i = 0; i < objectPools.Length; i++)
        {
            platformWidths[i] = objectPools[i].pooledObject.GetComponent<BoxCollider2D>().size.x;
        }

        minHeight = transform.position.y;
        maxHeight = maxHeightPoint.position.y;


	}
	
	// Update is called once per frame
	void Update () {

        if (transform.position.x < generationPoint.position.x)
        {
            distanceBetween = Random.Range(distanceBetweenMin, distanceBetweenMax);

            platformSelector = Random.Range(0, objectPools.Length);

            heightChange = transform.position.y + Random.Range(maxHeightChange, -maxHeightChange);

            if(heightChange > maxHeight)
            {
                heightChange = maxHeight;
            } 
            else if(heightChange < minHeight)
            {
                heightChange = minHeight;
            }

            transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 2) + distanceBetween, heightChange, transform.position.z);

            //Instantiate(/*platform*/ platforms[platformSelector], transform.position, transform.rotation);
                
            GameObject newPlatform = objectPools[platformSelector].GetPooledObejct();

            newPlatform.transform.position = transform.position;
            newPlatform.transform.rotation = transform.rotation;
            newPlatform.SetActive(true);


            transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 2), transform.position.y, transform.position.z);

        }

    }
}
