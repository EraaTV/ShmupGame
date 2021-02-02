using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundGenerator : MonoBehaviour
{
    public GameObject backgroundTilePrefab;
    List<GameObject> generatedBackgroundTiles;
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

                    GameObject newTile = Instantiate(backgroundTilePrefab, Vector3.zero, Quaternion.identity);
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
