using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Character", order = 1)]
public class Character : ScriptableObject
{
    public string characterName;
    public ElementTypes elementType;

    [Header("Panel")]
    public Sprite panelPortraitSprite;
    public Color panelBackgroundTint;
}
