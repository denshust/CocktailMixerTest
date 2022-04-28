using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mixer : MonoBehaviour
{
    private List<GameObject> ingredientsInBlender;
    [SerializeField] private Rescaler rescaler;
    public void ClearBlender()
    {
        foreach (var item in ingredientsInBlender)
        {
            Destroy(item.gameObject);
        }
    }
    public Color GetLocalMix()
    {
        ingredientsInBlender = rescaler.GetIngredients();
        return Mix(ingredientsInBlender);
    }
    public Color Mix(List<GameObject> ingredsToMix)
    {
        Vector3 colorSum = Vector3.zero;
        foreach (var ingredient in ingredsToMix)
        {
            Color ingColor = ingredient.GetComponent<Ingredient>().ingredientColor;
            colorSum += new Vector3(ingColor.r, ingColor.g, ingColor.b);
        }
        return new Color(colorSum.x / ingredsToMix.Count, colorSum.y / ingredsToMix.Count, colorSum.z / ingredsToMix.Count);
    }
}
