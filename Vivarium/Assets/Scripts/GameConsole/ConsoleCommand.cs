using UnityEngine;
using System.Collections;

/// <summary>
/// Represents a generic console command.
/// </summary>
public class ConsoleCommand<T> : BaseConsoleCommand
{
    private System.Action<T> _command;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="id">The command ID.</param>
    /// <param name="description">The description of the command.</param>
    /// <param name="format">Format of the console command.</param>
    /// <param name="command">The action to execute when command is invoked.</param>
    public ConsoleCommand(string id, string description, string format, System.Action<T> command) : base(id, description, format)
    {
        _command = command;
    }

    /// <summary>
    /// Executes the command.
    /// </summary>
    /// <param name="value">Value passed into the command.</param>
    public void Invoke(T value)
    {
        _command.Invoke(value);
    }
}

/// <summary>
/// Represents a generic console command.
/// </summary>
public class ConsoleCommand : BaseConsoleCommand
{
    private System.Action _command;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="id">The command ID.</param>
    /// <param name="description">The description of the command.</param>
    /// <param name="format">Format of the console command.</param>
    /// <param name="command">The action to execute when command is invoked.</param>
    public ConsoleCommand(string id, string description, string format, System.Action command) : base(id, description, format)
    {
        _command = command;
    }

    /// <summary>
    /// Executes the command.
    /// </summary>
    public void Invoke()
    {
        _command.Invoke();
    }
}
