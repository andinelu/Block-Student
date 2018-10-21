using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour {
    //[SerializeField] private GameObject[] blockTypes;
    public GameObject[] prefabs;// = new GameObject[4];
    private Timer time = new Timer(3);
    // Use this for initialization
    void Start() {
        //int blockType = Random.Range(0, blockTypes.Length);
        //Instantiate(blockTypes[blockType], Vector3.one,Quaternion.identity);
        prefabs[0] = Resources.Load("Prefabs/Block") as GameObject;
        prefabs[1] = Resources.Load("Prefabs/I-block") as GameObject;
        prefabs[2] = Resources.Load("Prefabs/L-block") as GameObject;
        prefabs[3] = Resources.Load("Prefabs/O-block") as GameObject;
        prefabs[4] = Resources.Load ("Prefabs/Curriculum_block") as GameObject;

        // prefabs[3] = Resources.Load()

    }
	
	// Update is called once per frame
	void Update () {
		if (time.hasEnded())
        {
            Instantiate(prefabs[Random.Range(0, prefabs.Length)],
                new Vector3(Random.Range(-5,5), 10,0),Quaternion.identity,GameObject.Find("BlockContainer").transform);
            time.restart();
        }
	}
}
