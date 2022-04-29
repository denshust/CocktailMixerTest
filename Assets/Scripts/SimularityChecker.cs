using UnityEngine;
using UnityEngine.UI;

public class SimularityChecker : MonoBehaviour
{
    [SerializeField] private GameObject _liquid;
    [SerializeField] private Text _persentage;
    [SerializeField] private MeshRenderer _colorWanted;
    [SerializeField] private Mixer _mixer;
    [SerializeField] private GameObject _restart, _nextLevel;
    [SerializeField] private MeshRenderer _liquidMeshRenderer;
    
    private int EstimateSimularity(Color mixedColor, Color wantedColor)
    {
        float r = 100 - Mathf.Abs(mixedColor.r - wantedColor.r) * 100;
        float g = 100 - Mathf.Abs(mixedColor.g - wantedColor.g) * 100;
        float b = 100 - Mathf.Abs(mixedColor.b - wantedColor.b) * 100;
        var estimateResult = (int) ((r + g + b) / 3f);
        return estimateResult;
    }
    public void CheckSimilarity()
    {
        Color mixedColor = _mixer.GetLocalMix();
        _liquidMeshRenderer.material.color = mixedColor;
        _liquid.SetActive(true);
        _persentage.enabled = true;
        int resultingSimilarity = EstimateSimularity(mixedColor, _colorWanted.material.color);
        _persentage.text = resultingSimilarity + "%";

        if (resultingSimilarity >= 90) 
        {
            Win();
        }
        else
        {
            Lose();
        }
    }

    private void Win()
    {
        _nextLevel.SetActive(true);
    }
    
    private void Lose()
    {
        _restart.SetActive(true);
    }
}
