using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractCellObject : MonoBehaviour
{

    public virtual bool CanPlace(Vector2Int coords, ObjectGrid objectGrid)
    {
        return (!objectGrid.CheckCell(coords)
            && !objectGrid.CheckCell(coords + new Vector2Int(1, 0))
            && !objectGrid.CheckCell(coords + new Vector2Int(-1, 0))
            && !objectGrid.CheckCell(coords + new Vector2Int(0, 1))
            && !objectGrid.CheckCell(coords + new Vector2Int(0, -1))
            && !objectGrid.CheckCell(coords + new Vector2Int(1, 1))
            && !objectGrid.CheckCell(coords + new Vector2Int(-1, -1))
            && !objectGrid.CheckCell(coords + new Vector2Int(1, -1))
            && !objectGrid.CheckCell(coords + new Vector2Int(-1, 1)));
    }

    public virtual void DeleteSelf(ObjectGrid objectGrid)
    {
        Object.Destroy(this.gameObject);
    }

    public virtual void Place(Vector3 screenPos, ObjectGrid objectGrid)
    {
        objectGrid.CreateCellObject(screenPos, this);
    }

}
