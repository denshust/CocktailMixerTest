using System.Collections.Generic;
using UnityEngine;

public class Mixer : MonoBehaviour
{
    [SerializeField] private Rescaler _rescaler;
    private List<GameObject> _ingredientsInBlender;
    
    public void ClearBlender()
    {
        foreach (var item in _ingredientsInBlender)
        {
            Destroy(item.gameObject);
        }
    }
    
    public Color GetLocalMix()
    {
        _ingredientsInBlender = _rescaler.GetIngredients();
        var mixColor = Mix(_ingredientsInBlender);
        return mixColor;
    }
    
    public Color Mix(List<GameObject> ingredientsToMix)
    {
        Vector3 colorSum = Vector3.zero;
        foreach (var ingredient in ingredientsToMix)
        {
            Color ingredientColor = ingredient.GetComponent<Ingredient>().ingredientColor;
            colorSum += new Vector3(ingredientColor.r, ingredientColor.g, ingredientColor.b);
        }

        var ingredientsCount = ingredientsToMix.Count;
        var resultingColor = new Color(colorSum.x / ingredientsCount, colorSum.y / ingredientsCount, colorSum.z / ingredientsCount);
        return resultingColor;
    }
}
