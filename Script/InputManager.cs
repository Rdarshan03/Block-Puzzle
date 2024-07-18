using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    Piece dragablePiece;
    private Vector3 startPos;

    public void ResetBlock()
    {
        if (dragablePiece)
        {
            MovedragablePiece();
            ResetdragablePiece();
        }
    }
    void Start()
    {

    }

    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        if (Input.GetMouseButtonDown(0))
        {
            Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);

            if (targetObject)
            {
                dragablePiece = targetObject.transform.gameObject.GetComponent<Piece>();

                dragablePiece.Scale(true, 0.2f);

                Vector3 p = dragablePiece.transform.position;
                startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                startPos = new Vector3(startPos.x - p.x, startPos.y - p.y, 0);
            }
        }
        else if (Input.GetMouseButton(0))
        {
            if (dragablePiece)
            {
                dragablePiece.transform.position = mousePosition;
            }
        }
        else if (Input.GetMouseButtonUp(0) && dragablePiece)
        {
            Vector3 size = dragablePiece.size;
            size = new Vector3(size.x - 1, size.y - 1, 0);

            Vector2 origin = dragablePiece.transform.GetChild(0).position;
            Vector2 end = dragablePiece.transform.GetChild(0).position + size;
            if (IsInRange(origin, end) && IsEmpty(dragablePiece, RoundVector2(origin)))
            {

                //print("It's Empty");
                var lastPosition = BlockPosition(origin, size);
                dragablePiece.transform.localPosition = lastPosition;
                dragablePiece.GetComponent<BoxCollider2D>().enabled = false;
                dragablePiece.enabled = false;

                Vector2Int start = RoundVector2(origin);

                for (int i = 0; i < dragablePiece.structure.Length; i++)
                {
                    Vector2Int coords = dragablePiece.structure[i];

                    if (dragablePiece.transform.GetChild(i).tag == "BlockTile")
                    {
                        PicecTile b = dragablePiece.transform.GetChild(i).GetComponent<PicecTile>();
                        Grid.boardBlocks[start.x + coords.x, start.y + coords.y] = b;
                    }
                }
                BoardManager.ins.removeBlock(dragablePiece);
                BoardManager.ins.sblock();
                BoardManager.ins.disblockv();
                BoardManager.ins.disblockh();
            }
            else
            {
                MovedragablePiece();
            }
            ResetdragablePiece();
        }
    }

    private Vector2Int RoundVector2(Vector2 v)
    {
        return new Vector2Int((int)(v.x + 0.5f), (int)(v.y + 0.5f));
    }

    public bool IsEmpty(Piece b, Vector2 o)
    {
        for (int i = 0; i < b.structure.Length; i++)
        {
           // print(b.transform.GetChild(i).name);
            if (b.transform.GetChild(i).tag == "BlockTile")
            {
                Vector2Int coords = b.structure[i];

               // print("======  " + ((int)o.x + coords.x) + "   |    " + ((int)o.y + coords.y));

                if (Grid.boardBlocks[(int)o.x + coords.x, (int)o.y + coords.y])
                    return false;
            }
        }

        return true;
    }

    private Vector3 BlockPosition(Vector2 o, Vector2 s)
    {
        Vector3 off = Vector3.zero;

        if (s.x % 2 == 1) off.x = 0.5f;

        if (s.y % 2 == 1) off.y = 0.5f;

        return new Vector3((int)(o.x + 0.5f) + (int)(s.x / 2), (int)(o.y + 0.5f) + (int)(s.y / 2), -1) + off;
    }

    public bool IsInRange(Vector2 o, Vector2 e)
    {
        return o.x >= -0.5f && e.x <= Grid.BOARD_SIZE - 0.5f &&
               o.y >= -0.5f && e.y <= Grid.BOARD_SIZE - 0.5f;
    }
    private void MovedragablePiece()
    {
        dragablePiece.Scale(false, 0.2f);
        dragablePiece.Move(.25f, dragablePiece.basePosition);
    }

    private void ResetdragablePiece()
    {
        startPos = Vector3.zero;
        dragablePiece = null;

    }
  
}
