using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Upgrade : MonoBehaviour
{
    public TMP_Text upgradeName;
    public Image image;
    public  TMP_Text price;
    public TMP_Text level;
    public TMP_Text friendship;

    public GameController controller;

    public int pos = 0;
    public float baseFriendship = 0;
    public float basePrice = 0;

    

    public void buy(){
        if(controller.save.money > controller.save.prices[pos]){
            controller.save.money += - controller.save.prices[pos];
            controller.save.upgrades[pos] ++;
            controller.save.friendships[pos] = baseFriendship * Mathf.Pow(1.1f + pos*0.1f,  controller.save.upgrades[pos]);
            controller.save.prices[pos] = basePrice *  Mathf.Pow(1.1f + pos*0.1f, controller.save.upgrades[pos]);
            price.text =  controller.calculator(controller.save.prices[pos]);
            level.text = "LV " + controller.save.upgrades[pos].ToString();
            friendship.text = controller.calculator(controller.save.friendships[pos]) + "/s";
            controller.effectManager.playMenu();
        } else {
            controller.effectManager.closeMenu();
        }
        controller.setMoney();
        controller.saveSaveFile();
    }
}
