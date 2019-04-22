public GameObject[] myPrefabs; //make sure to add the size in the inspector

myPrefabs[0] = (GameObject)Instantiate(nameofPrefab);
myPrefabs[0].name = "myPrefabs0"; // this is to make sure that name (Clone) is removed;

// Then call the animation.
myPrefabs[0].GetComponent<Animator>().SetTrigger("Attack");

Animator animator;

GameObject prefab;
List<GameObject> prefabs;

prefab = (GameObject)Instantiate(Resources.Load("prefabNameinAssets"));
prefab.AddComponent<Animator>(); // add each prefab its own Animator component

animator = prefab.GetComponent<Animator>();
animator.runtimeAnimatorController = (RuntimeAnimatorController)Instantiate(Resources.Load("animatorControllerNameinAssets"));
//get prefab's Animator Component and set its controller

prefab.name = "prefabName";
prefabs.Add(prefab);