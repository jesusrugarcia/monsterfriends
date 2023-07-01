using UnityEngine.UI;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float maxFriendship;
    public float friendship = 0;

    public GameController controller;
    public bool initiallized = false;
    public GameObject tapEffect;
    public GameObject befriendEffect;
    public bool isBoss = false;

    public float shakeSpeed = 10.0f; //how fast it shakes
    public float shakeAmount = .25f; //how much it shakes

    public bool shaking;
    public float timer = 0;
    public float maxTime = .25f;
 
    
    
    public void initialize()
    {
        
        if(isBoss){
            maxFriendship = (1 + Mathf.Pow(1.5f,controller.save.world)) * UnityEngine.Random.Range(5f,10f);
            if(!controller.menuOpened){
                controller.bossSlider.SetActive(true);
                controller.normalSlider.SetActive(false);
            }
            controller.slider = controller.bossSlider.GetComponent<Slider>();
        } else {
            maxFriendship = (1 + Mathf.Pow(1.5f,controller.save.world)) * UnityEngine.Random.Range(0.5f,2f);
            if(!controller.menuOpened){
                controller.bossSlider.SetActive(false);
                controller.normalSlider.SetActive(true);
            }
            controller.slider = controller.normalSlider.GetComponent<Slider>();
        }
        controller.slider.maxValue = maxFriendship;
        controller.slider.value = friendship;
        controller.currentFriendship.text = controller.calculator(friendship) + " / " + controller.calculator(maxFriendship);
        initiallized = true;
    }

    
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= maxTime){
            timer = 0;
            shaking = false;
        }
        if (initiallized && friendship >= maxFriendship){
            befriend();
        }

        if (initiallized && (Input.GetMouseButtonDown(0) || (Input.touchCount == 1 && Input.GetTouch(0).phase== TouchPhase.Began))) {  
            friendship += controller.save.click;
            controller.slider.value = friendship;
            controller.currentFriendship.text = controller.calculator(friendship) + " / " + controller.calculator(maxFriendship);
            if(!controller.menuOpened){
                Instantiate(tapEffect);
                controller.effectManager.playTap();
            }
            if (!shaking){
                shaking = true;
                timer = 0;
            }
        }

        if(controller.menuOpened){
            transform.position = new Vector3(9999,9999,9999);
        } else {
            if (!shaking){
                transform.position = new Vector3(0,0,0);
            } else {
                shake();
            }
        }
        
    }

    public void befriend(){ 
        if(isBoss){
            var mult = UnityEngine.Random.Range(5f,10f);
            controller.save.money += (2 + Mathf.Pow(1.35f,controller.save.world)) * mult ;
            controller.save.totalMoney += (2 + Mathf.Pow(1.35f,controller.save.world)) * mult;
        } else {
            var mult = UnityEngine.Random.Range(1f,1.5f);
            controller.save.money += (1 + Mathf.Pow(1.35f,controller.save.world)) * mult;
            controller.save.totalMoney += (1 + Mathf.Pow(1.35f,controller.save.world)) * mult;
        }
        
        controller.save.stage ++;
        if(controller.save.stage > 20){
            controller.save.stage = 0;
            controller.save.world ++;
            controller.setWorldText();
            controller.upgradeManager.addUpgradesShop();
        }
        controller.setMoney();
        controller.saveSaveFile();
        if(!controller.menuOpened){Instantiate(befriendEffect);}
        Destroy(gameObject);
    }

    public void shake(){
        gameObject.transform.position += new Vector3(Mathf.Sin(Time.time * shakeSpeed) * shakeAmount, Mathf.Sin(Time.time * shakeSpeed) * shakeAmount, 0);
    }

    
}
