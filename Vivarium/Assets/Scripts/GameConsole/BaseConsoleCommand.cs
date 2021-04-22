using UnityEngine;
using System.Collections;

/// <summary>
/// Base class for a console command.
/// </summary>
public class BaseConsoleCommand
{
    /// <summary>
    /// The command ID.
    /// </summary>
    public string Id { get; private set; }

    /// <summary>
    /// The description of the command.
    /// </summary>
    public string Description { get; private set; }

    /// <summary>
    /// Format of the console command.
    /// </summary>
    public string Format { get; private set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="id">The command ID.</param>
    /// <param name="description">The description of the command.</param>
    /// <param name="format">Format of the console command.</param>
    public BaseConsoleCommand(string id, string description, string format)
    {
        Id = id;
        Description = description;
        Format = format;
    }
}
