using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallAndChainFK : MonoBehaviour
{

    private static Transform _linksParent;

    [SerializeField] private List<Transform> _links;
    [SerializeField] private List<float> _linksLengths;

    [SerializeField] private bool _runInGizmos;

    protected virtual void Awake()
    {
        _linksParent ??= new GameObject("LINKS_PARENT").transform;
    }

    protected virtual void Start()
    {
        for (int i = 1; i < _links.Count; i++)
        {
            _links[i].transform.parent = _linksParent;
        }
    }

    public void RemoveRandomLinks()
    {
        for (int i = 0; i < Random.Range(0, 8); i++)
        {
            if (_links.Count > 1)
            {
                Destroy(_links[1].gameObject);
                _links.RemoveAt(1);
                _linksLengths.RemoveAt(1);
            }
        }
    }

    public void SetInitialPosition(Vector3 initialPosition, bool includeFirstLink = true)
    {
        for (int i = includeFirstLink ? 0 : 1; i < _links.Count; i++)
        {
            _links[i].position = initialPosition;
        }
    }

    public void SetInitialDirection(Vector3 direction)
    {
        _links[0].right = -direction;

        for (int i = 0; i < _links.Count; i++)
        {
            _links[i].right = direction;
        }
    }

    private void OnDestroy()
    {
        for (int i = 0; i < _links.Count; i++)
        {
            Destroy(_links[i].gameObject);
        }
    }

    protected virtual void Update()
    {
        if (_links.Count < 2 || _linksLengths.Count != _links.Count - 1)
            return;

        for (int i = 0; i < _links.Count; i++)
        {
            Vector3 dirToThisLink = _links[i].position - _links[i - 1].position;
            _links[i].transform.position = _links[i - 1].position + (dirToThisLink.normalized * _linksLengths[i - 1]);

            _links[i].right = dirToThisLink.normalized * -1f;
        }

        _links[0].right = (_links[0].position - _links[1].position).normalized;
    }

    private void OnDrawGizmos()
    {
        if (_runInGizmos == false)
            return;

        Update();
    }

}