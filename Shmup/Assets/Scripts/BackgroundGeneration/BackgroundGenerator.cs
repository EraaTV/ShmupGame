using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundGenerator : MonoBehaviour
{
    public GameObject backgroundTilePrefab;
    List<GameObject> generatedBackgroundTiles;
    public float scrollSpeed;
    // Start is called before the first frame update
    void Start()
    {
        generatedBackgroundTiles = new List<GameObject>();
        GameObject initialTile = Instantiate(backgroundTilePrefab, Vector3.zero, Quaternion.identity);
        float initialTileHeight = Camera.main.orthographicSize * 2;
        float initialTileWidth = initialTileHeight * Screen.width / Screen.height;
        initialTile.transform.localScale =new Vector3(initialTileWidth, initialTileHeight, 1);
        generatedBackgroundTiles.Add(initialTile);
        print(generatedBackgroundTiles[0]);
        StartCoroutine(ScrollBackground(scrollSpeed));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ScrollBackground(float scrollspeed)
    {
        while (true)
        {
            for (int i = 0; i < generatedBackgroundTiles.Count; i++)
            {
                generatedBackgroundTiles[i].transform.position += Vector3.down * scrollspeed;

            }
            yield return new WaitForSeconds(.1f);
        }
    }
}
