using UnityEngine;
using System.Collections;

public class ActionFactory
{
    public static void Create(
        GameObject gameObject,
        Action action,
        out ActionController actionController,
        out ActionViewer actionViewer)
    {
        switch (action.ControllerType)
        {
            case ActionControllerType.GiantLazer:
                actionController = gameObject.AddComponent<GiantLazerActionController>();
                actionViewer = gameObject.AddComponent<ActionViewer>();
                break;
            case ActionControllerType.Projectile:
                actionController = gameObject.AddComponent<ProjectileActionController>();
                actionViewer = gameObject.AddComponent<ActionViewer>();
                break;
            case ActionControllerType.KnockBack:
                actionController = gameObject.AddComponent<KnockBackActionController>();
                actionViewer = gameObject.AddComponent<ActionViewer>();
                break;
            case ActionControllerType.SwitchPosition:
                actionController = gameObject.AddComponent<SwitchPositionActionController>();
                actionViewer = gameObject.AddComponent<ActionViewer>();
                break;
            case ActionControllerType.Screw:
                actionController = gameObject.AddComponent<ScrewActionController>();
                actionViewer = gameObject.AddComponent<ActionViewer>();
                break;
            case ActionControllerType.Skewer:
                actionController = gameObject.AddComponent<SkewerActionController>();
                actionViewer = gameObject.AddComponent<ActionViewer>();
                break;
            case ActionControllerType.Staple:
                actionController = gameObject.AddComponent<StapleActionController>();
                actionViewer = gameObject.AddComponent<ActionViewer>();
                break;
            case ActionControllerType.Pierce:
                actionController = gameObject.AddComponent<PierceActionController>();
                actionViewer = gameObject.AddComponent<PierceActionViewer>();
                break;
            case ActionControllerType.ArcProjectile:
                actionController = gameObject.AddComponent<ArcProjectileActionController>();
                actionViewer = gameObject.AddComponent<ActionViewer>();
                break;
            case ActionControllerType.MinionSummon:
                var minionSummonActionController = gameObject.AddComponent<MinionSummonActionController>();
                actionViewer = gameObject.AddComponent<ActionViewer>();
                actionController = minionSummonActionController;
                break;
            case ActionControllerType.Heal:
                actionController = gameObject.AddComponent<HealActionController>();
                actionViewer = gameObject.AddComponent<ActionViewer>();
                break;
            case ActionControllerType.Default:
            default:
                actionController = gameObject.AddComponent<ActionController>();
                actionViewer = gameObject.AddComponent<ActionViewer>();
                break;
        }

        actionController.ActionReference = action;
        actionViewer.ActionReference = action;
    }
}
