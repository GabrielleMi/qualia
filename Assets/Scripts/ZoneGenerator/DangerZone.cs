using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerZone : Zone
{
    private void Start()
    {
    }

    public override Type GetZoneType()
    {
        return Type.Danger;
    }
}
