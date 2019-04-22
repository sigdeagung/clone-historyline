using UnityEngine;
using System.Collections;

public class borderListener : MonoBehaviour {
        public Respawn rS;

        void OnTriggerEnter2D(Collider2D target){
                rS.spawnIt ();

        }

}

IEnumerator RandomObstacleSpawn(){

            yield return new WaitForSeconds(Random.Range(3.5f,4.5f));
            int index = Random.Range (0, listname.Count);


            if(!listname[index].activeInHierarchy){
                listname[index].SetActive(true);
                listname[index].transform.position = new Vector3(transform.position.x,transform.position.y,-2f);

            }else{

                index = Random.Range(0,listname.Count);

            }

             StartCoroutine(RandomObstacleSpawn());
        }