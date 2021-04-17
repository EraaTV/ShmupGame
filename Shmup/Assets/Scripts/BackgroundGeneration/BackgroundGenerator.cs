using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class BackgroundGenerator : MonoBehaviour
{
    public GameObject backgroundTilePrefab;
    public GameObject[] backgroundTilePrefabArray;
    public GameObject[] backgroundTilePrefabArrayRed;
    public GameObject[] backgroundTilePrefabArrayBlue;
    List<GameObject> generatedBackgroundTiles;
    public int tilePoolIndex;
    public float scrollSpeed;
    public float scrollAmount;
    public GameObject playerCube;
    public Texture[] backgroundTexturesArray;
    public bool changeTilePool;
    public int tileCountForChange;
    public bool useTimer;
    // Start is called before the first frame update
    void Start()
    {
        generatedBackgroundTiles = new List<GameObject>();
        GameObject initialTile = Instantiate(backgroundTilePrefab, Vector3.zero, Quaternion.identity);
        float initialTileHeight = Camera.main.orthographicSize * 2;
        float initialTileWidth = initialTileHeight * Screen.width / Screen.height;
        initialTile.GetComponent<Renderer>().material.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1);
        initialTile.transform.localScale =new Vector3(initialTileWidth, initialTileHeight, 1);

        GameObject initialTile2 = Instantiate(backgroundTilePrefab, Vector3.zero, Quaternion.identity);
        float initialTileHeight2 = Camera.main.orthographicSize * 2;
        float initialTileWidth2 = initialTileHeight * Screen.width / Screen.height;
        initialTile2.transform.localScale =new Vector3(initialTileWidth2, initialTileHeight2, 1);
        initialTile2.GetComponent<Renderer>().material.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1);

        initialTile2.transform.localPosition = new Vector3(0, 20, 0);

        generatedBackgroundTiles.Add(initialTile);
        generatedBackgroundTiles.Add(initialTile2);
        print(generatedBackgroundTiles[0]);
        StartCoroutine(ScrollBackground(scrollSpeed, scrollAmount));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ScrollBackground(float _scrollspeed, float _scrollamount)
    {
        GameObject newTile = null;
        Color colorRand = Vector4.zero;
        bool newTileExists = false;
        float changePoolTimer = 0;
        int tileCount = 0;
        while (true)
        {
            changePoolTimer += Time.deltaTime;
            if (useTimer)
            {
                if (changePoolTimer > 5)
                {
                    changeTilePool = true;
                    changePoolTimer = 0;
                    
                }
            } else
            {
                if (tileCount > tileCountForChange)
                {
                    changeTilePool = true;                   
                    tileCount = 0;
                    
                }
            }
            
            if(playerCube.transform.position.x > 0 && changeTilePool)
            {
                tilePoolIndex = 1;
                changeTilePool = false;
                AnalyticsResult temp = Analytics.CustomEvent("right_path_taken", new Dictionary<string, object>{
                    {"scene_name", SceneManager.GetActiveScene().name },
                    {"poolIndex", tilePoolIndex } }
                    
                    );
                
            } else if (playerCube.transform.position.x < 0 && changeTilePool)
            {
                tilePoolIndex = 2;
                changeTilePool = false;
                AnalyticsResult temp = Analytics.CustomEvent("left_path_taken", new Dictionary<string, object>{
                    {"scene_name", SceneManager.GetActiveScene().name },
                    {"currentHP", playerCube.GetComponent<PlayerShoot>().currentHp }
                });
                

            }
            for (int i = 0; i < generatedBackgroundTiles.Count; i++)
            {
                if (generatedBackgroundTiles.Count > 2)
                {
                    newTileExists = true;
                }
                generatedBackgroundTiles[i].transform.position += Vector3.down * _scrollamount;
                if (generatedBackgroundTiles[0].transform.localPosition.y < -20)
                {
                    Destroy(generatedBackgroundTiles[0]);
                    generatedBackgroundTiles.RemoveAt(0);
                    int tileIndexRand = 0;
                    switch (tilePoolIndex)
                    {
                        case 0:
                            //Eample of random tiles from an array
                            tileIndexRand = Random.Range(0, backgroundTilePrefabArray.Length);
                            newTile = Instantiate(backgroundTilePrefabArray[tileIndexRand], Vector3.zero, Quaternion.identity);
                            //Generates a random color. Replace with "has point amount" or some other flag as a bool, and have it generate from another Tile pool
                            colorRand = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1);
                            break;
                        case 1:
                            newTile = Instantiate(backgroundTilePrefabArrayBlue[Random.Range(0, backgroundTilePrefabArray.Length)], Vector3.zero, Quaternion.identity);
                            //Generates a random color. Replace with "has point amount" or some other flag as a bool, and have it generate from another Tile pool
                            colorRand = new Color(Random.Range(0f, 0f), Random.Range(0f, 0f), Random.Range(0f, 1f), 1);
                            break;
                        case 2:
                            newTile = Instantiate(backgroundTilePrefabArrayRed[Random.Range(0, backgroundTilePrefabArray.Length)], Vector3.zero, Quaternion.identity);
                            //Generates a random color. Replace with "has point amount" or some other flag as a bool, and have it generate from another Tile pool
                            colorRand = new Color(Random.Range(0f, 1f), Random.Range(0f, 0f), Random.Range(0f, 0f), 1);
                            break;
                    }


                    //GameObject newTile = Instantiate(backgroundTilePrefab, Vector3.zero, Quaternion.identity);
                    newTile.GetComponent<Renderer>().material.color = colorRand;
                    //Apply texture to new tile clone, use the same ID as the generated background tile, this is an alternative to needing a prefab for every tile.
                    //newTile.GetComponent<Renderer>().material.mainTexture = backgroundTexturesArray[tileIndexRand];
                    float newTileHeight = Camera.main.orthographicSize * 2;
                    float newTileWidth = newTileHeight * Screen.width / Screen.height;
                    newTile.transform.localScale = new Vector3(newTileWidth, newTileHeight, 1);
                    newTile.transform.localPosition = new Vector3(0, 20, 0);
                    generatedBackgroundTiles.Add(newTile);
                    tileCount++;

                }
            }
            yield return new WaitForSeconds(_scrollspeed);
        }
    }
}
