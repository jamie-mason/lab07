using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class SingledonDesign
{
    // Start is called before the first frame update
    private SingledonDesign() { }
    private static SingledonDesign GetPoints = null;
    public static SingledonDesign Instance
    {
        
        get
        {
            if (GetPoints == null)
            {
                GetPoints = new SingledonDesign();
            }
            return GetPoints;
        }
    }
}
