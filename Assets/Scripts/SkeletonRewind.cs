using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonRewind : MonoBehaviour, IRewindable
{
    public Material staticMaterial;
    private Material material;
    private List<(Vector3,Quaternion)> positions = new List<(Vector3, Quaternion)>();
    private bool isRewinding = false;
    public void StartRewind()
    {
        if (isRewinding) return;
        StopAllCoroutines();
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<FollowObject>().enabled = false;
        ApplyRewindEffect();
        StartCoroutine(Rewind());
        isRewinding = true;

    }

    public void EndRewind()
    {
        positions.Clear();
        GetComponent<NavMeshAgent>().enabled = true;
        GetComponent<FollowObject>().enabled = true;
        ClearRewindEffect();
        StartCoroutine(GatherPositions());
        isRewinding = false;

    }

    public IEnumerator Rewind()
    {
        for(int i  = positions.Count-1; i >= 0; i--) 
        {
            transform.position = positions[i].Item1;
            transform.rotation = positions[i].Item2;
    
            yield return new WaitForSeconds(0.5f);
        }
        EndRewind();
    }

    public IEnumerator GatherPositions()
    {
        while (true)
        {
            positions.Add((transform.position,transform.rotation));
            if (positions.Count > 10)
            {
                positions.RemoveAt(0);
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(GatherPositions());
    }

    void ApplyRewindEffect()
    {
        SkinnedMeshRenderer[] renderers = GetComponentsInChildren<SkinnedMeshRenderer>();

        foreach (var renderer in renderers)
        {
            // Apply the material
            material = renderer.material != staticMaterial ? renderer.material : material;
            renderer.material = staticMaterial;
        }
    }

    void ClearRewindEffect()
    {
        SkinnedMeshRenderer[] renderers = GetComponentsInChildren<SkinnedMeshRenderer>();

        foreach (var renderer in renderers)
        {
            // Apply the material
            renderer.material = material;
        }
    }
}
