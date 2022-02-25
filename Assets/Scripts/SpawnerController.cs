using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    public float offsetY;
    public float offsetX;
    public float timeBetweenSpawn;
    private float countdown = 1;

    public static bool spawn = false;

    public List<GameObject> gameObjects = new List<GameObject>();
    private PlayerController playerController;

    
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(countdown <= 0 || spawn)
        {
            SpawnTarget();
            countdown = timeBetweenSpawn;
        }
        else countdown -= Time.deltaTime;

        if (playerController.ReturnPoints() >= 20) timeBetweenSpawn = 2;
        if (playerController.ReturnPoints() >= 50) timeBetweenSpawn = 1;
    }

    void SpawnTarget(){
        float posX = Random.Range(-8, 8);
        float posY = Random.Range(-1, 5);

        int prefabSelector = Random.Range(0, gameObjects.Count);

        //if((gameObjects[prefabSelector].tag == "TimeButterfly" && GameManager.CheckTimer() < 25) ||
        //   (gameObjects[prefabSelector].tag == "Butterfly" && GameManager.CheckTimer() < 30)
        //    )
        //{

        //}

        GameObject target = Instantiate(gameObjects[prefabSelector], 
                                        new Vector2(posX, posY), 
                                        transform.rotation);
        spawn = false;
    }
}
