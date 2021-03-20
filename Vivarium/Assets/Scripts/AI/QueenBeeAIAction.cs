/// <summary>
/// Represents an action that the queen bee AI can perform on each turn.
/// </summary>
public class QueenBeeAIAction
{
    /// <summary>
    /// Reference to the action.
    /// </summary>
    public Action ActionReference { get; set; }

    /// <summary>
    /// The target tile of the action.
    /// </summary>
    public Tile TargetTile { get; set; }

    /// <summary>
    /// Function for executing the action.
    /// </summary>
    public System.Action<Action, Tile, System.Action> ExecuteAction { get; set; }
}
