using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallAndChainBendy : MonoBehaviour
{
    private static Transform _linksParent;

    [SerializeField] private List<Transform> _links;
    [SerializeField] private List<float> _lerps;

    private List<Transform> _linkTargets = new List<Transform>();

    protected virtual void Awake()
    {
        _linksParent ??= new GameObject("LINKS_PARENT").transform;
    }

    protected virtual void Start()
    {
        gameObject.transform.parent = null;
        DontDestroyOnLoad(gameObject);
        for (int i = 0; i < _links.Count; i++)
        {
            Transform linkTarget = new GameObject("Link_Target").transform;
            linkTarget.parent = transform;
            linkTarget.position = _links[i].transform.position;
            _linkTargets.Add(linkTarget);

            _links[i].transform.parent = _linksParent;
        }
    }

    private void OnDestroy()
    {
        if(_links != null)
        {
            for (int i = 0; i < _links.Count; i++)
            {
                if (_links[i] != null)
                    Destroy(_links[i].gameObject);
            }
        }
    }

    protected virtual void Update()
    {
        for (int i = 0; i < _links.Count; i++)
        {
            Vector3 dirToThisLink;

            if (i == 0)
                dirToThisLink = _links[i].position - transform.position;
            else
                dirToThisLink = _links[i].position - _links[i - 1].position;

            _links[i].right = dirToThisLink.normalized * -1f;

            _links[i].transform.position = Vector3.Lerp(_links[i].transform.position, _linkTargets[i].transform.position, _lerps[i]);
        }

        _links[0].right = (_links[0].position - _links[1].position).normalized;
    }
}