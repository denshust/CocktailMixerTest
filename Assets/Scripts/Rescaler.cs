using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rescaler : MonoBehaviour
{
    private List<GameObject> _ingredientsInBlender;
    void Start()
    {
        _ingredientsInBlender = new List<GameObject>();
    }
    public List<GameObject> GetIngredients()
    {
        List<GameObject> ingredientObjects = new List<GameObject>();
        foreach (var obj in _ingredientsInBlender)
        {
            ingredientObjects.Add(obj.transform.parent.gameObject);
        }
        return ingredientObjects;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ingredient"))
        {
            if (!_ingredientsInBlender.Contains(other.gameObject))
            {
                if(_ingredientsInBlender.Count>0)
                Rescale();
                _ingredientsInBlender.Add(other.gameObject);
            }
        }
    }
    private void Rescale()
    {
        double scale = Math.Pow(1f / (_ingredientsInBlender.Count), 1.0f / 4f);
        Vector3 newScale = new Vector3((float)scale, (float)scale, (float)scale);
        foreach (var ingredient in _ingredientsInBlender)
        {
            ingredient.transform.localScale = newScale;
        }
    }
}
