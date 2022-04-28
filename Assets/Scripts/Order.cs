using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour
{
    [Range(3, 11)]
    [SerializeField] private  int _maxDifficulty;
    [SerializeField] private  Mixer _mixer;
    private  Color _orderColor;
    enum Ingredient{ Apple, Banana, Cherries, Cucumber, Eggplant, Orange, Tomato};
    public void SetOrder(GameObject[] ingredientsToOrder)
    {
        _orderColor = GenerateOrder(ingredientsToOrder);
        GetComponent<MeshRenderer>().material.color = _orderColor;
    }

    Color GenerateOrder(GameObject[] ingredientsToGenerateFrom)
    {
        int quantity = Random.Range(2, _maxDifficulty);
        List<GameObject> orderIngredients = new List<GameObject>();
        for (int i = 0; i < quantity; i++)
        {
            orderIngredients.Add(ingredientsToGenerateFrom[Random.Range(0, ingredientsToGenerateFrom.Length)]);
        }
        return _mixer.Mix(orderIngredients);
    }
}
