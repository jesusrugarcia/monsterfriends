using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class UpgradeClick : MonoBehaviour
{
    public TMP_Text upgradeName;
    public Image image;
    public  TMP_Text price;
    public TMP_Text level;
    public TMP_Text friendship;

    public GameController controller;

    public float baseFriendship = 1;
    public float basePrice =15;

    

    public void buy(){
        if(controller.save.money > controller.save.clickPrice){
            controller.save.money += - controller.save.clickPrice;
            controller.save.clickLevel ++;
            controller.save.click = baseFriendship *  Mathf.Pow(1.2f , controller.save.clickLevel);
            controller.save.clickPrice = basePrice *  Mathf.Pow(1.2f , controller.save.clickLevel);
            price.text = controller.calculator(controller.save.clickPrice);
            level.text = "LV " + controller.save.clickLevel.ToString();
            friendship.text = controller.calculator(controller.save.click) + "/tap";
        }
        controller.setMoney();
        controller.saveSaveFile();
    }

}
