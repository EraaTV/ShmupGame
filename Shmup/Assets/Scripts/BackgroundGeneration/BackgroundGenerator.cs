using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundGenerator : MonoBehaviour
{
    public GameObject backgroundTilePrefab;
    List<GameObject> generatedBackgroundTiles;
    // Start is called before the first frame update
    void Start()
    {
        GameObject initialTile = Instantiate(backgroundTilePrefab, Vector3.zero, Quaternion.identity);
        float initialTileHeight = Camera.main.orthographicSize * 2;
        float initialTileWidth = initialTileHeight * Screen.width / Screen.height;
        initialTile.transform.localScale =new Vector3(initialTileWidth, initialTileHeight, 1);
        initialTile.transform.Rotate(90, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
