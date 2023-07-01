using UnityEngine.UI;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameController controller;
    public GameObject monster;
    public GameObject boss;
    public Canvas canvas;

    public WorldScriptableObject[] worlds;

    public int spawnNum = 0;
    
    public GameObject spawnMonster(){
        spawnNum = controller.save.world;
        while(spawnNum > worlds.Length -1){ // make the game continue even if the world is superior to the scripted ones, repeat them.
            spawnNum += -worlds.Length;
        }
        controller.background.GetComponent<Image>().sprite = worlds[spawnNum].background;
        if(controller.save.stage == 20){
            return spawnBoss();
        } else {
            return spawnNormal();
        }
        
    }

    public GameObject spawnNormal(){
        var mons = Instantiate(monster, canvas.transform);
        mons.GetComponent<Image>().sprite = worlds[spawnNum].normal[UnityEngine.Random.Range(0,worlds[spawnNum].normal.Length)];
        mons.GetComponent<Monster>().controller = controller;
        mons.GetComponent<Monster>().initialize();
        return mons;
    }

    public GameObject spawnBoss(){
        var mons = Instantiate(boss, canvas.transform);
        mons.GetComponent<Image>().sprite = worlds[spawnNum].boss;
        mons.GetComponent<Monster>().controller = controller;
        mons.GetComponent<Monster>().isBoss = true;
        mons.GetComponent<Monster>().initialize();
        return mons;
    }
}
