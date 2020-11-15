using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitCommand : ICommand
{
    public IEnumerator Execute()
    {
        yield return new WaitForSeconds(0.3f);
    }
}
