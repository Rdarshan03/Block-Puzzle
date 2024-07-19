using System.Collections;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using static Unity.Collections.AllocatorManager;

public class BoardManager : MonoBehaviour
{
    public static BoardManager ins;
    public const int BOARD_SIZE = 10;
    public const int BLOCKS_AMOUNT = 3;
    public Block[] blocks = new Block[BLOCKS_AMOUNT];
    public const int BLOCK_PREFABS_AMOUNT = 18;
    public const int BLOCK_SIZE = 3;
    public const int BLOCK_START_POS = 1;
    public const float BLOCK_OFFSET = 3.5f;
    public GameObject[] blockPrefab;
    public Transform gameTransform;
    ArrayList list = new ArrayList();
    Piece Piece;
    private Vector2Int[] desLinesPos = new Vector2Int[BOARD_SIZE];
    public Text Scoretext;



    private void Awake()
    {
        ins = this;
    }

    public void MoveBlocks(int i)
    {

    }

    private void Start()
    {

        sblock();
    }

    public void removeBlock(Piece dragablePiece)
    {
        list.Remove(dragablePiece);
    }


    
    public void sblock()
    {
        if (blockPrefab.Length <= 0) return;
        //print("asssss" + list.Count);
        //print("asssss" + blockPrefab.Length);
        for (int i = list.Count; i < 3; i++)
        {
        
            Piece piece = Instantiate(blockPrefab[Random.Range(0, blockPrefab.Length)], Vector3.zero, Quaternion.identity).GetComponent<Piece>();
            piece.Scale(false, 0.2f);
            list.Add(piece);
        }

        for (int i = 0; i < list.Count; i++)
        {
            Piece piece = list[i] as Piece;
            var pos = new Vector3(BLOCK_START_POS + (i * BLOCK_OFFSET), -3f, 0);
            piece.basePosition = pos;
            piece.transform.position = pos;
        }
    }

    public void disblockv()
    {
        for (int x = 0; x < BOARD_SIZE; x++)
        {
            int counter = 0;
            for (int y = 0; y < BOARD_SIZE; y++)
            {
                if(Grid.boardBlocks[x, y])
                {
                    //print("x--->" + x + "    y---> " + y + "    ====> " + Grid.boardBlocks[x, y]);
                    counter++;
                }
            }
            if (counter == 10)
            {
                //print("destroy ---->" + counter);
                for (int y = 0; y < BOARD_SIZE; y++)
                {

                    if (Grid.boardBlocks[x, y])
                    {
                        Destroy(Grid.boardBlocks[x, y].gameObject);
                        Grid.boardBlocks[x, y] = null;
                    }
                }
            }
        }
    }
    public void disblockh()
    {
        for (int y = 0; y < BOARD_SIZE; y++)
        {
            int counter = 0;
            for (int x = 0; x < BOARD_SIZE; x++)
            {
                if (Grid.boardBlocks[x, y])
                {
                    //print("x--->" + x + "    y---> " + y + "    ====> " + Grid.boardBlocks[x, y]);
                    counter++;
                }
            }
            if (counter == 10)
            {
                //print("destroy ---->" + counter);
                for (int x = 0; x < BOARD_SIZE; x++)
                {
                    if (Grid.boardBlocks[x, y])
                    {
                        Destroy(Grid.boardBlocks[x, y].gameObject);
                        Grid.boardBlocks[x, y] = null;
                    }
                }
            }
        }
    }
    
}




