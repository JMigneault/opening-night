using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DoubleBarricade : AbstractTrap
{

    [SerializeField] DoubleBarricade pairPrefab;

    private bool alreadyDeleting = false;
    private DoubleBarricade pair;
    public DoubleBarricade Pair { set { this.pair = value; } }
    private Vector2Int coords;
    public Vector2Int Coords
    {
        get { return this.coords; }
        set { this.coords = value; }
    }

    protected abstract Vector2Int GetPairOffset();

    public override bool CanPlace(Vector2Int coords, ObjectGrid objectGrid)
    {
        return base.CanPlace(coords, objectGrid) && base.CanPlace(coords + GetPairOffset(), objectGrid);
    }

    public override void Place(Vector3 screenPos, ObjectGrid objectGrid)
    {
        Vector2Int mainCoords = objectGrid.GetCoords(screenPos);
        Vector2Int pairCoords = mainCoords + GetPairOffset();
        DoubleBarricade main = (DoubleBarricade) objectGrid.CreateCellObject(mainCoords, this);
        DoubleBarricade pair = (DoubleBarricade) objectGrid.CreateCellObject(pairCoords, pairPrefab);
        main.Pair = pair;
        main.Coords = mainCoords;
        pair.Pair = main;
        pair.Coords = pairCoords;
    }

    public override void DeleteSelf(ObjectGrid objectGrid)
    {
        if (pair.alreadyDeleting)
        {
            base.DeleteSelf(objectGrid);
        }
        else
        {
            alreadyDeleting = true;
            objectGrid.DeleteCellObject(pair.Coords);
            base.DeleteSelf(objectGrid);
        }
    }
}
