using System.Collections;
using UnityEngine;

[ExecuteAlways]
public class IngredientMover : MonoBehaviour
{
    [SerializeField] private GameObject _mixButton;
    [SerializeField] private Transform[] _bezierPoints;
    [SerializeField] private Blender _blender;
    private GameObject _bezierObject;
    private bool _isMoving;
    private bool _canMove = true;
    private float _bezierCoefT;

    private void Start()
    {
        _mixButton.SetActive(false);
    }

    private void Move()
    {
        _bezierCoefT += Time.deltaTime * 2;
        var bezierTransform = _bezierObject.transform;
        bezierTransform.position = BezierTool.GetPoint(_bezierPoints[0].position,
        _bezierPoints[1].position, _bezierPoints[2].position, _bezierPoints[3].position, _bezierCoefT);
        bezierTransform.rotation = Quaternion.LookRotation(BezierTool.GetFirstDerivative(_bezierPoints[0].position,
        _bezierPoints[1].position, _bezierPoints[2].position, _bezierPoints[3].position, _bezierCoefT));
        var angles = bezierTransform.localEulerAngles;
        bezierTransform.localEulerAngles = new Vector3(angles.x - 60f, angles.y - 60f, angles.z);
    }

    private void Drop()
    {
        _canMove = false;
        _isMoving = false;
        _bezierObject.GetComponentInChildren<MeshCollider>().enabled = true;
        var rigidbody = _bezierObject.GetComponent<Rigidbody>();
        rigidbody.isKinematic = false;
        rigidbody.velocity = new Vector3(0, -3, 0);
        _bezierObject = null;
        _bezierCoefT = 0;
        StartCoroutine(CloseAndShake());
    }

    private void Update()
    {
        if (!_isMoving) return;
        
        if (_bezierCoefT < 1)
        {
            Move();
        }
        else
        {
            Drop();
        }
    }

    public void MoveIngredient(GameObject ingredientPrefab)
    {
        if (_bezierObject != null || !_canMove) return;
        
        _blender.Lid(true);
        _mixButton.SetActive(false);
        GameObject ingredient = Instantiate(ingredientPrefab, _bezierPoints[0].position, Quaternion.identity);
        ingredient.GetComponentInChildren<MeshCollider>().enabled = false;
        _bezierObject = ingredient;
        _isMoving = true;
    }

    private IEnumerator CloseAndShake()
    {
        yield return new WaitForSeconds(0.1f);
        _blender.Shake();
        yield return new WaitForSeconds(0.2f);
        _blender.Lid(false);
        yield return new WaitForSeconds(0.4f);
        _mixButton.SetActive(true);
        _canMove = true;
    }

    private void OnDrawGizmos() {

        int sigmentsNumber = 20;
        Vector3 previousPoint = _bezierPoints[0].position;

        for (int i = 0; i < sigmentsNumber + 1; i++) 
        {
            float parameter = (float)i / sigmentsNumber;
            Vector3 point = BezierTool.GetPoint(
                _bezierPoints[0].position, 
                _bezierPoints[1].position, 
                _bezierPoints[2].position, 
                _bezierPoints[3].position, parameter);
            Gizmos.DrawLine(previousPoint, point);
            previousPoint = point;
        }
    }
}
