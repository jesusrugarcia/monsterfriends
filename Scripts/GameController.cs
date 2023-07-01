using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    public GameObject monster;

    [SerializeField]
    public SaveFile save;
    public TMP_Text money;
    public MonsterSpawner spawner;

    public bool menuOpened = false;
    public GameObject menuBackground;

    public GameObject background;
    public Slider slider;
    public TMP_Text currentFriendship;
    public GameObject normalSlider;
    public GameObject bossSlider;
    public GameObject upgrades;
    public GameObject optionsObject;
    public GameObject creditsObject;
    public GameObject worldObject;

    public UpgradeManager upgradeManager;

    public MusicManager musicManager;
    public SoundEffectManager effectManager;

    public GameObject adButton;
    // Start is called before the first frame update
    void Start()
    {
        startGame();
    }

    public void startGame(){
        loadSaveFile();
        monster = spawner.spawnMonster();
        setMoney();
        upgradeManager.startGame();
        musicManager.setVolume();
        setWorldText();
    }

    // Update is called once per frame
    void Update()
    {
        try {
            if (monster == null){
                monster = spawner.spawnMonster();
            }
        } catch (Exception e){
            Debug.Log(e);
            monster = spawner.spawnMonster();
        }
    }

    public void setWorldText(){
        worldObject.GetComponent<TMP_Text>().text = save.world.ToString();
    }

    public void setMoney(){
        money.text = calculator(save.money);
    }

    public void openShop(){
       openMenu();
       upgrades.SetActive(true);
       optionsObject.SetActive(false);
    }

    public void openOptions(){
        openMenu();
        upgrades.SetActive(false);
        optionsObject.SetActive(true);
    }

    public void openMenu(){
        //monster.gameObject.SetActive(false);
        menuOpened = true;
        menuBackground.SetActive(true);
        slider.gameObject.SetActive(false);
        effectManager.playMenu();
        worldObject.SetActive(false);
        currentFriendship.gameObject.SetActive(false);
        adButton.SetActive(false);
        
    }

    public void closeMenu(){
        menuOpened = false;
        menuBackground.SetActive(false);
        slider.gameObject.SetActive(true);
        //monster.gameObject.SetActive(true);
        upgrades.SetActive(false);
        optionsObject.SetActive(false);
        effectManager.closeMenu();
        creditsObject.SetActive(false);
        worldObject.SetActive(true);
        currentFriendship.gameObject.SetActive(true);
        adButton.SetActive(true);
    }

    public void openCredits(){
        creditsObject.SetActive(true);
        optionsObject.SetActive(false);
        effectManager.playMenu();
    }

    public void closeCredits(){
        creditsObject.SetActive(false);
        optionsObject.SetActive(true);
        effectManager.closeMenu();
    }

    public void loadSaveFile(){
        try{
            var jsonData = System.IO.File.ReadAllText(Application.persistentDataPath + "/" + "save" + ".json");
            save = JsonUtility.FromJson<SaveFile>(jsonData);
        } catch (Exception e){
            Debug.Log(e);
            save = new SaveFile(upgradeManager.data.Length);
        }
    }

    public void saveSaveFile(){
        var json = JsonUtility.ToJson(save);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/save.json", json);
    }

    public string calculator(float ammount){
        if (ammount > Mathf.Pow(10,18)){
            return (ammount/Mathf.Pow(10,18)).ToString("n2") + "Q";
        } else if (ammount > Mathf.Pow(10,15)){
            return (ammount/Mathf.Pow(10,15)).ToString("n2") + "C";
        }else if (ammount > Mathf.Pow(10,12)){
            return (ammount/Mathf.Pow(10,12)).ToString("n2") + "T";
        }else if (ammount > Mathf.Pow(10,9)){
            return (ammount/Mathf.Pow(10,9)).ToString("n2") + "B";
        }else if (ammount > Mathf.Pow(10,6)){
            return (ammount/Mathf.Pow(10,6)).ToString("n2") + "M";
        } else if (ammount > Mathf.Pow(10,3)){
            return (ammount/Mathf.Pow(10,3)).ToString("n2") + "K";
        } else return ammount.ToString("n2");
    }
}
