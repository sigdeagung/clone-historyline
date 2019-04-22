List<UserScore> leaderBoard = new List<UserScore>();
db.Child("users").Child(uid).Child("friendList").GetValueAsync().ContinueWith(task => {
    if (task.IsCompleted)
    {
        //foreach friend
        foreach(DataSnapshot h in task.Result.Children)
        {
            // kick off the task to retrieve friend's name
            Task nameTask = db.Child("users").Child(h.Key).Child("name").GetValueAsync();
            // kick off the task to retrieve friend's score
            Task scoreTask = db.Child("scores").Child(h.Key).GetValueAsync();
            // join tasks into one final task
            Task finalTask = Task.Factory.ContinueWhenAll((new[] {nameTask, scoreTask}), tasks => {
              if (nameTask.IsCompleted && scoreTask.IsCompleted) {
                // both tasks are complete; add new record to leaderboard
                string name = nameTask.Result.Value.ToString();
                int score = int.Parse(scoreTask.Result.Value.ToString());
                leaderBoard.Add(new UserScore(name, score));
                Debug.Log(name);
                Debug.Log(score.ToString());
              }
            })
        }
    }
});
var friendList = await db.Child("users").Child(uid).Child("friendList").GetValueAsync();

List<Task<UserScore>> tasks = new List<Task<UserScore>>();
//foreach friend
foreach(DataSnapshot friend in friendList.Children) {
    var task = Task.Run( async () => {
        var friendKey = friend.Key;
        //get his name in the user table
        var getName = db.Child("users").Child(friendKey).Child("name").GetValueAsync();
        //get his score from the scores table
        var getScore = db.Child("scores").Child(friendKey).GetValueAsync();

        await Task.WhenAll(getName, getScore);

        var name = getName.Result.Value.ToString();
        var score = int.Parse(getScore.Result.Value.ToString());

        //make new userscore to add to leader board
        var userScore = new UserScore(name, score);
        Debug.Log($"{name} : {score}");
        return userScore;
    });
    tasks.Add(task);
}

var scores = await Task.WhenAll(tasks);

List<UserScore> leaderBoard = new List<UserScore>(scores);