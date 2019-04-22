var tmp = goals.Select( g=> new {goal = g, weight = rnd.NextDouble()})
        .OrderByDescending(t=>t.goal.Active) // Active first
        .ThenBy(t=>t.goal.Level)
        .ThenBy(t=>t.weight)
        .Select(t=>t.goal)
        .ToList();



    goals.Clear();
    goals.AddRange(tmp);

    Random rnd = new Random();

Comparison<Goal> comparison =  (a, b) => {
// first chooses the Active ones (if any)
var sort = b.Active.CompareTo(a.Active);

if (sort == 0) {
// then sort by level
    return sort = (a.Level).CompareTo(b.Level);
  } else 
{
   return sort;
}
};



int startIndex = 0;
int endIndex = 0;

goals.Sort(comparison);

while (startIndex < goals.Count)
{
    for (endIndex = startIndex + 1; endIndex < goals.Count; ++endIndex)
    {
       if (comparison(goals[startIndex], goals[endIndex]) != 0)
       {
          //End of tie
          break;
       }
    }

    if (endIndex - startIndex > 1)
    {
       // Shuffle goals of the same level
       ShuffleRange(goals, startIndex, endIndex - startIndex, rnd);
    }

    startIndex = endIndex;
}   

static void ShuffleRange<T>(List<T> list, int startIndex, int count, Random rnd)
{
     int n = startIndex + count;  
     while (n > startIndex + 1) 
     {  
        int k = rnd.Next(startIndex, n--);  
        T value = list[k];  
        list[k] = list[n];  
        list[n] = value;  
    }  
}       

var sorted =
(
    from g in goals
    orderby g.Active descending, g.Level, UnityEngine.Random.Range(-1, 2)
    select g
.ToList();

var sorted =
    goals
        .OrderByDescending(g => g.Active)
        .ThenBy(g => g.Level)
        .ThenBy(g => rnd.Next())
        .ToList();