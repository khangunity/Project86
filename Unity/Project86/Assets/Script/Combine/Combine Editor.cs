using UnityEngine;
using UnityEditor;

public class CombineEditor 
{
    [MenuItem("Tools/Combine Selected Meshes")]
    static void CombineSelectMeshes()
    {
        GameObject [] selection = Selection.gameObjects;
        if(selection.Length == 0)
        {
            Debug.Log("a");
            return;
        }

        MeshFilter[] meshFilters = Selection.activeGameObject.GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];

        for(int i = 0; i < meshFilters.Length; i++)
        {
            combine[i].mesh =  meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
        }

        Mesh combinedMesh = new Mesh();
        combinedMesh.CombineMeshes(combine);

        GameObject combinedObject = new GameObject("Combined Mesh");
        MeshFilter meshFilter = combinedObject.AddComponent<MeshFilter>();
        meshFilter.mesh = combinedMesh;

        MeshRenderer meshRenderer = combinedObject.AddComponent<MeshRenderer>();
        meshRenderer.sharedMaterial = meshFilters[0].GetComponent<MeshRenderer>().sharedMaterial;

        AssetDatabase.CreateAsset(combinedMesh, "Assets/CombineMesh.asset");
        
        foreach(var item in selection)
        {
            item.gameObject.SetActive(false);
        }
        
    }
}
