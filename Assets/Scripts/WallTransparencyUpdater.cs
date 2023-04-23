using System.Collections.Generic;
using UnityEngine;

public class WallTransparencyUpdater : MonoBehaviour
{
    // -- FIELDS

    [SerializeField] private Transform _player;

    private List<Material> _wallMaterials = new List<Material>();

    // -- UNITY

    private void Awake()
    {
        foreach(Transform child in transform)
        {
            _wallMaterials.Add(child.GetComponent<Renderer>().material);
        }
    }

    private void Update()
    {
        foreach(Material material in _wallMaterials)
        {
            material.SetVector("_PlayerPosition", _player.position);
        }
    }
}
