using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CommandController : MonoBehaviour
{
    public static CommandController Instance { get; private set; }
    private Queue<IEnumerator> _coroutineQueue = new Queue<IEnumerator>();
    //private Queue<ICommand> _coroutineQueue = new Queue<ICommand>();

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

    // possibly change frames
    // add animation action to queue 
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

    //public IEnumerator Wait()
    //{
    //    yield return new WaitForSeconds(1);
    //}

    public void ExecuteCommand(ICommand command)
    {
        _coroutineQueue.Enqueue(command.Execute());
    }

    public void ExecuteCoroutine(IEnumerator coroutine)
    {
        _coroutineQueue.Enqueue(coroutine);
    }

    public bool CommandsAreExecuting()
    {
        return _coroutineQueue.Count > 0;
    }
}
