var accumulatedDelta= new Vector2[MAX_PARTICLES];

for(int i = 0; i < _numActiveParticles; ++i)
{
    accumulatedDelta = calculateForce(_activeParticles[i], accumulatedDelta);
}

for (int i = _numActiveParticles - 1; i >= 0; i--)
{
    int index = _activeParticles[i];
    _delta[index] += accumulatedDelta[index] / MULTIPLIER;
}
public static void ParallerFor<TLocal>(int startIndex, int endIndex, Func<TLocal> initData, Func<int, TLocal, TLocal> body, Action<TLocal> finalizer)
    {
        int numThreads = Environment.ProcessorCount;
        int chunkOffset = ((endIndex - startIndex) / numThreads) + 1;

        Task[] tasks = new Task[numThreads];

        Enumerable.Range(0, numThreads).ToList().ForEach(x =>
            {
                int start = x * chunkOffset;
                int end = ((x + 1) * chunkOffset);
                end = end > endIndex ? endIndex : end;

                tasks[x] = Task.Factory.StartNew(() =>
                {
                    TLocal init = initData();

                    for(int i = start; i < end; ++i)
                    {
                        init = body(i, init);
                    }

                    finalizer(init);
                });
            });

        Task.WhenAll(tasks).Wait();
    }