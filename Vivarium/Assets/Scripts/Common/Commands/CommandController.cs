using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Object the adds commands to the queue
/// </summary>
public class CommandController : MonoBehaviour
{
    public static CommandController Instance { get; private set; }
    private Queue<IEnumerator> _coroutineQueue = new Queue<IEnumerator>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    // Use this for initialization
    void Start()
    {
        StartCoroutine(CoroutineCoordinator());
    }

    /// <summary>
    /// Executes each command in the queue until the count is 0
    /// </summary>
    /// <returns></returns>
    IEnumerator CoroutineCoordinator()
    {
        while (true)
        {
            while (_coroutineQueue.Count > 0)
            {
                yield return StartCoroutine(_coroutineQueue.Peek());
                _coroutineQueue.Dequeue();
            }
            yield return null;
        }
    }

    /// <summary>
    /// Add a command to the execution queue
    /// </summary>
    /// <param name="command">The command object to be executed</param>
    public void ExecuteCommand(ICommand command)
    {
        _coroutineQueue.Enqueue(command.Execute());
    }

    /// <summary>
    /// Add a coroutine to the execution queue
    /// </summary>
    /// <param name="coroutine">The coroutine to be executed</param>
    public void ExecuteCoroutine(IEnumerator coroutine)
    {
        _coroutineQueue.Enqueue(coroutine);
    }


    /// <summary>
    /// Checks if the coroutine queue is still executing
    /// </summary>
    /// <returns></returns>
    public bool CommandsAreExecuting()
    {
        return _coroutineQueue.Count > 0;
    }
}
