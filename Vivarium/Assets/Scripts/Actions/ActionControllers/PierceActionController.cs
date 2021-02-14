using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

public class PierceActionController : ActionController
{
    public override void CalculateAffectedTiles(int x, int y)
    {
        var minRange = StatCalculator.CalculateStat(ActionReference, StatType.AttackMinRange);
        var maxRange = StatCalculator.CalculateStat(ActionReference, StatType.AttackMaxRange);
    }




}