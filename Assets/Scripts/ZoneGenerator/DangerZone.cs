using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerZone : Zone
{
    public override Type GetZoneType()
    {
        return Type.Danger;
    }
}
