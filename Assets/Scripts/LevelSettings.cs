using System.Collections.Generic;
using UnityEngine;

public class LevelSettings : MonoBehaviour
{
    [SerializeField] private Order _order;
    [SerializeField] private GameObject[] _ingredients;
    [SerializeField] private GameObject[] _ingredientButtons;
    private List<GameObject> _activeIngredients;
    private enum Ingredient { Apple, Banana, Cherries, Cucumber, Eggplant, Orange, Tomato };

    private void Start()
    {
        foreach (var button in _ingredientButtons)
        {
            button.SetActive(false);
        }
        _activeIngredients = new List<GameObject>();
        switch (Level.LevelIndex)
        {
            case 0:
                Activate((int)Ingredient.Apple);
                Activate((int)Ingredient.Banana);
                break;
            case 1:
                Activate((int)Ingredient.Apple);
                Activate((int)Ingredient.Orange);
                Activate((int)Ingredient.Cherries);
                break;
            case 2:
                Activate((int)Ingredient.Tomato);
                Activate((int)Ingredient.Cucumber);
                Activate((int)Ingredient.Eggplant);
                break;
        }
        foreach (var item in _activeIngredients)
        {
            Debug.Log(item.name);
        }
        _order.SetOrder(_activeIngredients.ToArray());
    }

    private void Activate(int index)
    {
        _ingredientButtons[index].SetActive(true);
        _activeIngredients.Add(_ingredients[index]);
    }
}
