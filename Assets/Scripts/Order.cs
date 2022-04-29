using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Order : MonoBehaviour
{
    [Range(3, 11)]
    [SerializeField] private int _maxDifficulty;
    [SerializeField] private Mixer _mixer;
    private  Color _orderColor;
    private  MeshRenderer _meshRenderer;

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    public void SetOrder(GameObject[] ingredientsToOrder)
    {
        _orderColor = GenerateOrder(ingredientsToOrder);
        _meshRenderer.material.color = _orderColor;
    }

    private Color GenerateOrder(GameObject[] ingredientsToGenerateFrom)
    {
        int quantity = Random.Range(2, _maxDifficulty);
        List<GameObject> orderIngredients = new List<GameObject>();
        for (int i = 0; i < quantity; i++)
        {
            orderIngredients.Add(ingredientsToGenerateFrom[Random.Range(0, ingredientsToGenerateFrom.Length)]);
        }
        var orderColor = _mixer.Mix(orderIngredients);
        return orderColor;
    }
}
