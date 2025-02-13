using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public GameObject thePlatform;
    public Transform generationPoint;
    public float distanceBetween;

    public float distanceBetweenMin;
    public float distanceBetweenMax;

    private float platformWidth;
    private int platformSelector;

    public ObjectPooler[] theObjectPools;

    public ObjectPooler theObjectPool;

    private float minHeight;
    public Transform maxHeightPoint;
    private float maxHeight;
    public float maxHeightChange;
    private float heightChange;

    public float randomLogTreshold;
    public ObjectPooler LogPool;

    void Start()
    {
        platformWidth = thePlatform.GetComponent<BoxCollider2D>().size.x;
        
        minHeight = transform.position.y;
        maxHeight = maxHeightPoint.position.y;
}
    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < generationPoint.position.x)
        {
            distanceBetween = Random.Range(distanceBetweenMin, distanceBetweenMax);

            platformSelector = Random.Range(0, theObjectPools.Length);

            heightChange = transform.position.y + Random.Range(maxHeightChange, -maxHeightChange);

            if (heightChange > maxHeight)
            {
                heightChange = maxHeight;
            } else if (heightChange < minHeight)
            {
                heightChange = minHeight;
            }
            transform.position = new Vector3(transform.position.x + platformWidth + distanceBetween, heightChange, transform.position.z);

            //Instantiate(thePlatform, transform.position, transform.rotation);
            GameObject newPlatform = theObjectPool.GetPooledObject();

            newPlatform.transform.position = transform.position;
            newPlatform.transform.rotation = transform.rotation;
            newPlatform.SetActive(true);

        }

        if(Random.Range(0f,100f) < randomLogTreshold)
        {
            GameObject newLog = LogPool.GetPooledObject();
            
            Vector3 logPosition = new Vector3(0f, 0.5f, 0f);

            newLog.transform.position = transform.position + logPosition;
            newLog.transform.rotation = transform.rotation;
            newLog.SetActive(true);
        }

    }
}
