using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class IngredientMover : MonoBehaviour
{
    [SerializeField] private  GameObject _mixButton;
    [SerializeField] private  Transform[] _bezierPoints;
    [SerializeField] private  Blender _blender;
    private  GameObject _bezieObject;
    private bool _isMoving;
    private bool _canMove = true;
    private  float _bezierCoefT;

    private void Start()
    {
        _mixButton.SetActive(false);
    }
    void Move()
    {
        _bezierCoefT += Time.deltaTime * 2;
        _bezieObject.transform.position = BezierTool.GetPoint(_bezierPoints[0].position,
        _bezierPoints[1].position, _bezierPoints[2].position, _bezierPoints[3].position, _bezierCoefT);
        _bezieObject.transform.rotation = Quaternion.LookRotation(BezierTool.GetFirstDerivative(_bezierPoints[0].position,
        _bezierPoints[1].position, _bezierPoints[2].position, _bezierPoints[3].position, _bezierCoefT));
        var angles = _bezieObject.transform.localEulerAngles;
        _bezieObject.transform.localEulerAngles = new Vector3(angles.x - 60f, angles.y - 60f, angles.z);
    }
    void Drop()
    {
        _canMove = false;
        _isMoving = false;
        _bezieObject.GetComponentInChildren<MeshCollider>().enabled = true;
        _bezieObject.GetComponent<Rigidbody>().isKinematic = false;
        _bezieObject.GetComponent<Rigidbody>().velocity = new Vector3(0, -3, 0);
        _bezieObject = null;
        _bezierCoefT = 0;
        StartCoroutine(CloseAndShake());
    }
    void Update()
    {
        if(_isMoving)
        {
            if (_bezierCoefT < 1)
            {
                Move();
            }
            else
            {
                Drop();
            }
        }
    }

    public void MoveIngredien(GameObject ingredientPrefab)
    {
        if (_bezieObject==null && _canMove)
        {
            _blender.Lid(true);
            _mixButton.SetActive(false);
            GameObject ingredient = Instantiate(ingredientPrefab, _bezierPoints[0].position, Quaternion.identity);
            ingredient.GetComponentInChildren<MeshCollider>().enabled = false;
            _bezieObject = ingredient;
            _isMoving = true;
            
        }
    }

    IEnumerator CloseAndShake()
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
        Vector3 preveousePoint = _bezierPoints[0].position;

        for (int i = 0; i < sigmentsNumber + 1; i++) {
            float paremeter = (float)i / sigmentsNumber;
            Vector3 point = BezierTool.GetPoint(_bezierPoints[0].position, _bezierPoints[1].position, _bezierPoints[2].position, _bezierPoints[3].position, paremeter);
            Gizmos.DrawLine(preveousePoint, point);
            preveousePoint = point;
        }
    }
}
