using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public GameObject gridprefab;
    public static int BOARD_SIZE = 10;

    [HideInInspector]
    public SpriteRenderer[,] boardTiles = new SpriteRenderer[BOARD_SIZE, BOARD_SIZE];

    [HideInInspector]
    public static PicecTile[,] boardBlocks = new PicecTile[BOARD_SIZE, BOARD_SIZE];

    void Start()
    {
        for (int i = 0; i < BOARD_SIZE; i++)
        {
            for (int y = 0; y < BOARD_SIZE; y++)
            {
                GameObject grid = Instantiate(gridprefab, transform);
                grid.transform.position = new Vector3(y, i, 0f);
                boardTiles[i, y] = grid.GetComponent<SpriteRenderer>();
                grid.name = y + "-" + i;
            }
        }
        var newPosition = transform.position;
        newPosition.z = 10f;
        transform.position = newPosition;
    }


}
