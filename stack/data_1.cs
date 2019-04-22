List<IEnumerator> unblockedCoroutines;
List<IEnumerator> shouldRunNextFrame;
List<IEnumerator> shouldRunAtEndOfFrame;
SortedList<float, IEnumerator> shouldRunAfterTimes;

foreach(IEnumerator coroutine in unblockedCoroutines)
{
    if(!coroutine.MoveNext())
        // This coroutine has finished
        continue;

    if(!coroutine.Current is YieldInstruction)
    {
        // This coroutine yielded null, or some other value we don't understand; run it next frame.
        shouldRunNextFrame.Add(coroutine);
        continue;
    }

    if(coroutine.Current is WaitForSeconds)
    {
        WaitForSeconds wait = (WaitForSeconds)coroutine.Current;
        shouldRunAfterTimes.Add(Time.time + wait.duration, coroutine);
    }
    else if(coroutine.Current is WaitForEndOfFrame)
    {
        shouldRunAtEndOfFrame.Add(coroutine);
    }
    else /* similar stuff for other YieldInstruction subtypes */
}

unblockedCoroutines = shouldRunNextFrame;