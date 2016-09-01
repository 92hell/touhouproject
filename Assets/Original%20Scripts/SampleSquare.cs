using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

class SampleSquare : Square
{
    public Sprite TileImage;
    public String TerrainType;
    public bool IsHighlighted;
    public bool WantsToMove;

    public override Vector3 GetCellDimensions()
    {
        return GetComponent<Renderer>().bounds.size;
    }

    public override void MarkAsHighlighted()
    {
        IsHighlighted=true;
        //Debug.Log(IsHighlighted);
        GetComponent<Renderer>().material.color = new Color(0.75f, 0.75f, 0.75f); ;
    }

    public override void MarkAsPath()
    {
        GetComponent<Renderer>().material.color = Color.green;
    }

    public override void MarkAsReachable()
    {
        GetComponent<Renderer>().material.color = new Color32(0,51,204,1);
    }

    //public override void MarkAsEnemy()
    //{
      //  GetComponent<Renderer>().material.color = new Color32(255,15,15,1);
    //}

    public override void UnMark()
    {
        GetComponent<Renderer>().material.color = Color.white;
    }
}

