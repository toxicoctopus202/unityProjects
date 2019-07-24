﻿using UnityEngine; 

public class Shape : PersistableObject
{

    public int ShapeId {

        get {
            return shapeId;
        }
        set{
            if (shapeId == int.MinValue && value != int.MinValue)
            {
                shapeId = value;
            }
            else
            {
                Debug.LogError("Not allowed to change shapeId."); 
            }
        }

    } 

    int shapeId = int.MinValue;

    public int MaterialId { get; private set; }

    public void SetMaterial(Material material, int materialId)
    {
        MaterialId = materialId;
        meshRenderer.material = material; 

    }



    static int colorPropertyId = Shader.PropertyToID("_Color");
    static MaterialPropertyBlock sharedPropertyBlock; 

    Color color; 

    public void SetColor (Color color)
    {
        this.color = color;
       if (sharedPropertyBlock == null)
        {
            sharedPropertyBlock = new MaterialPropertyBlock(); 
        }
        sharedPropertyBlock.SetColor(colorPropertyId, color);
        meshRenderer.SetPropertyBlock(sharedPropertyBlock); 
    }



    public override void Save(GameDataWriter writer)
    {
        base.Save(writer);
        writer.Write(color);
    }

    public override void Load(GameDataReader reader)
    {
        base.Load(reader);
        SetColor(reader.Version > 0 ? reader.ReadColor() : Color.white);
        
    }

    MeshRenderer meshRenderer; 

    boid Awake ()
    {
        meshRenderer = GetComponent<meshRenderer>(); 
    }

}