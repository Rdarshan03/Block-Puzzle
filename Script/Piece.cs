using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class Piece : MonoBehaviour
{

    public Vector2Int[] structure;
    public Vector2 size;
    [HideInInspector]
    public Vector3 basePosition;
    BlockMovingAnimation animation;
    internal Vector3 scaledScale = new Vector3(1, 1, 1);
    internal Vector3 baseScale  = new  Vector3(1,1,1);
    [HideInInspector]
    public int posIndex;


    private void Start()
    {
        basePosition = transform.position;
        animation =GetComponent<BlockMovingAnimation>();
    }

    public void Move(float v, Vector3 basePosition)
    {
        animation.enabled = true;
        animation.SetAnimation(v, basePosition);
    }

    public void ScaleTiles(Vector3 s)
    {
        foreach (Transform t in transform)
            t.localScale = s;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Scale(bool s, float t)
    {
        GetComponent<BlockScaleAnimation>().enabled = true;
        GetComponent<BlockScaleAnimation>().SetAnimation(s, t);
    }

    public void SetBasePosition(int i, bool cp = true)
    {

        Vector2 scale = transform.localScale;
        Vector2 colliderSize = GetComponent<BoxCollider>().size * scale;

        Vector3 position = new Vector3(colliderSize.x / 2 - 0.5f + colliderSize.x * i, GameScaler.GetBlockY(), 0);

        basePosition = position;

        if (cp)
            transform.position = basePosition;

        posIndex = i;
    }
}
