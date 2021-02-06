using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    // Start is called before the first frame update
    void Start()
    {
        generatedBackgroundTiles = new List<GameObject>();
        GameObject initialTile = Instantiate(backgroundTilePrefab, Vector3.zero, Quaternion.identity);
        float initialTileHeight = Camera.main.orthographicSize * 2;
        float initialTileWidth = initialTileHeight * Screen.width / Screen.height;
        initialTile.transform.localScale =new Vector3(initialTileWidth, initialTileHeight, 1);

        GameObject initialTile2 = Instantiate(backgroundTilePrefab, Vector3.zero, Quaternion.identity);
        float initialTileHeight2 = Camera.main.orthographicSize * 2;
        float initialTileWidth2 = initialTileHeight * Screen.width / Screen.height;
        initialTile2.transform.localScale =new Vector3(initialTileWidth2, initialTileHeight2, 1);
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

        while (true)
        {
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
                    switch (tilePoolIndex)
                    {
                        case 0:
                            //Eample of random tiles from an array
                            newTile = Instantiate(backgroundTilePrefabArray[Random.Range(0, backgroundTilePrefabArray.Length)], Vector3.zero, Quaternion.identity);
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
                    float newTileHeight = Camera.main.orthographicSize * 2;
                    float newTileWidth = newTileHeight * Screen.width / Screen.height;
                    newTile.transform.localScale = new Vector3(newTileWidth, newTileHeight, 1);
                    newTile.transform.localPosition = new Vector3(0, 20, 0);
                    generatedBackgroundTiles.Add(newTile);
                }
            }
            yield return new WaitForSeconds(_scrollspeed);
        }
    }
}
