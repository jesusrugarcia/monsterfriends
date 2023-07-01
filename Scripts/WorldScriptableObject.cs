using UnityEngine.UI;
using UnityEngine;

[CreateAssetMenu (menuName = "World")]
public class WorldScriptableObject : ScriptableObject
{
    public Sprite[] normal;
    public Sprite boss;
    public Sprite background;
}
