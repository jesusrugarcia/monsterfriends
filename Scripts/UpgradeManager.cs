using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public GameController controller;

    public UpgradeData[] data;
    public GameObject upgradeObject;
    public GameObject contentObject;
    public UpgradeClick clickUpgrade;
    public float timer = 0;
    public bool[] initialized;

    public void  startGame()
    {
        initialized = new bool[data.Length];
        setUpgradeData();
        addUpgradesShop();
        setUpgradeClick();
    }

    public void setUpgradeClick(){
        clickUpgrade.price.text = controller.calculator(controller.save.clickPrice);
        clickUpgrade.level.text = "LV " + controller.save.clickLevel.ToString();
        clickUpgrade.friendship.text = controller.calculator(controller.save.click) + "/tap";
    }

    public void setUpgradeData(){
        for (int i=0; i < data.Length; i++){
            data[i].initialWorld = 1 + (5*i);
            data[i].initialPrice = 25 + Mathf.Pow(1.10f + (i*0.02f),i);
            data[i].initialFriendship = 0.5f + Mathf.Pow(1.10f + (i*0.02f),i);
        }
    }

    public void addUpgradesShop(){
        for(int i= 0 ; i< data.Length; i++){
            addUpgradeShop(i);
        }
    }

    public void addUpgradeShop(int upNum){
        if (controller.save.world >= data[upNum].initialWorld && !initialized[upNum]){
            initialized[upNum] = true;
            var upgrade = Instantiate(upgradeObject, contentObject.transform);
            var up = upgrade.GetComponent<Upgrade>();
            up.controller = controller;
            up.pos = upNum;
            up.upgradeName.text = data[upNum].name;
            up.image.sprite = data[upNum].sprite;
            up.baseFriendship = data[upNum].initialFriendship;
            if(controller.save.upgrades[upNum] > 0){
                up.price.text = controller.calculator(controller.save.prices[upNum]);
            } else {
                up.price.text = controller.calculator(data[upNum].initialPrice);
                controller.save.prices[upNum] = data[upNum].initialPrice;
            }
            up.basePrice = data[upNum].initialPrice;
            up.level.text = "LV " +controller.save.upgrades[upNum].ToString();
            up.friendship.text = controller.calculator(controller.save.friendships[upNum]) + "/s";
        }
        
    }
    void Update()
    {
        timer += Time.deltaTime;
            if (timer >= .25f){
                timer = 0;
                for (int i=0 ;i < data.Length; i++){
                    befriend(i);
                }
                controller.currentFriendship.text = controller.calculator(controller.monster.GetComponent<Monster>().friendship) + " / " + controller.calculator(controller.monster.GetComponent<Monster>().maxFriendship);
            }
        
    }
    public void befriend(int pos){
        if (controller.save.upgrades[pos] >0){
            controller.monster.GetComponent<Monster>().friendship += controller.save.friendships[pos] * 0.25f;
            controller.slider.value += controller.save.friendships[pos] * 0.25f;
        }
    }


}
