/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapManager1 : MonoBehaviour
{
  	public int[,] grid;
	public GameObject[,] gridObj;
	public int mapSize = 100;
	public GameObject WallsPrefab; 
	
    // Start is called before the first frame update
    void Start()
    {
		grid =  new int[mapSize, mapSize];
		gridObj = new GameObject[mapSize,mapSize];
		MapCreat();
		  
        
    }

	public void MapCreat(){
		for(int i = 0; i<mapSize; i++){
			for(int j = 0; j<mapSize; j++){
				if(Random.Range(0,100)<20){
					grid[i,j] = 1;
				}
				else
				{
					grid[i,j] = 0;
				}

				if(grid[i,j]==1&&Random.Range(1,8)>=1&&Random.Range(1,8)<2){
					gridObj[i,j] = Instantiate(WallsPrefab, transform.position+ new Vector3(i,0,j), Quaternion.identity);
				}
				if(grid[i,j]==1&&Random.Range(1,8)>=3&&Random.Range(1,8)<4){
					gridObj[i,j] = Instantiate(WallsPrefab, transform.position+ new Vector3(-i,0,-j), Quaternion.identity);
				}
				if(grid[i,j]==1&&Random.Range(1,8)>=5&&Random.Range(1,8)<6){
					gridObj[i,j] = Instantiate(WallsPrefab, transform.position+ new Vector3(i,0,-j), Quaternion.identity);
				}
				if(grid[i,j]==1&&Random.Range(1,8)>=7&&Random.Range(1,8)<8){
					gridObj[i,j] = Instantiate(WallsPrefab, transform.position+ new Vector3(-i,0,j), Quaternion.identity);
				}
				
			}
		}
	}



}*/
