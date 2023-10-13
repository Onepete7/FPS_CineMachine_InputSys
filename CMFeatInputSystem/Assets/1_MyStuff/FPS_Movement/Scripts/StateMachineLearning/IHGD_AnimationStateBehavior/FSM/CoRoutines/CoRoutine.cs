// In this example we show how to invoke a coroutine and wait until it
// is completed

using UnityEngine;
using System.Collections;

public class ExampleClass : MonoBehaviour
{
    IEnumerator Start()
    {
        // - After 0 seconds, prints "Starting 0.0"
        // - After 2 seconds, prints "WaitAndPrint 2.0"
        // - After 2 seconds, prints "Done 2.0"
        print("Starting " + Time.time);

        // Start function WaitAndPrint as a coroutine. And wait until it is completed.
        // the same as yield return WaitAndPrint(2.0f);
        yield return StartCoroutine(WaitAndPrint(2.0f));
        print("Done " + Time.time);
    }

    // suspend execution for waitTime seconds
    IEnumerator WaitAndPrint(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        print("WaitAndPrint " + Time.time);
    }
}