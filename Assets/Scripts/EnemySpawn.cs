using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : Activatable
{
    public GameObject[] Conditionals;
    public Transform spawnPoint;
    public GameObject enemyType;
    public int numberOfEnemies;
    
    public override void CheckCondition()
    {
        //foreach (var item in Conditionals)
        //{
        //    if (!item.GetComponent<Torch>().isLit)
        //    {
        //        return;
        //    }
            
        //}
        Activate();
    }

    public override void Activate()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            Instantiate(enemyType, spawnPoint);
        }
    }
}
