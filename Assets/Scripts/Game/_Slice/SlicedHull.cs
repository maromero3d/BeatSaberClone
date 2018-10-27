using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class _Sliced
{
    private Mesh _leftSide;
    private Mesh _rightSide;

    public _Sliced(Mesh _leftSide, Mesh _rightSide)
    {
        this._leftSide = _leftSide;
        this._rightSide = _rightSide;
    }

    public GameObject CreateUpperHull(GameObject original)
    {
        return CreateUpperHull(original, null);
    }

    public GameObject CreateUpperHull(GameObject original, Material crossSectionMat)
    {
        GameObject newObject = CreateUpperHull();

        if (newObject != null)
        {
            newObject.transform.position = original.transform.position;
            newObject.transform.localRotation = original.transform.localRotation;
            newObject.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);

            //if (Game.makeHard)
            //newObject.transform.localScale = newObject.transform.localScale / 2;

            newObject.AddComponent<Rigidbody>();
            newObject.AddComponent<BoxCollider>();

            var opposite = -newObject.GetComponent<Rigidbody>().velocity;
            newObject.GetComponent<Rigidbody>().AddForce(opposite * 800f);
            newObject.GetComponent<Rigidbody>().mass = 9;
            Material[] shared = original.GetComponent<MeshRenderer>().sharedMaterials;
            Mesh mesh = original.GetComponent<MeshFilter>().sharedMesh;

            // nothing changed in the hierarchy, the cross section must have been batched
            // with the submeshes, return as is, no need for any changes
            if (mesh.subMeshCount == _leftSide.subMeshCount)
            {
                // the the material information
                newObject.GetComponent<Renderer>().sharedMaterials = shared;

                return newObject;
            }

            // otherwise the cross section was added to the back of the submesh array because
            // it uses a different material. We need to take this into account
            Material[] newShared = new Material[shared.Length + 1];

            // copy our material arrays across using native copy (should be faster than loop)
            System.Array.Copy(shared, newShared, shared.Length);
            newShared[shared.Length] = crossSectionMat;

            // the the material information
            newObject.GetComponent<Renderer>().sharedMaterials = newShared;
            newObject.AddComponent<Trash>();
        }

        return newObject;
    }

    public GameObject CreateLowerHull(GameObject original)
    {
        return CreateLowerHull(original, null);
    }

    public GameObject CreateLowerHull(GameObject original, Material crossSectionMat)
    {
        GameObject newObject = CreateLowerHull();

        if (newObject != null)
        {
            newObject.transform.position = original.transform.position;
            newObject.transform.localRotation = original.transform.localRotation;
            newObject.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);

            //if (Game.makeHard)
            //newObject.transform.localScale = newObject.transform.localScale / 2;

            newObject.AddComponent<Rigidbody>();
            newObject.AddComponent<BoxCollider>();
            var opposite = -newObject.GetComponent<Rigidbody>().velocity;
            newObject.GetComponent<Rigidbody>().AddForce(opposite * 800f);
            newObject.GetComponent<Rigidbody>().mass = 9;
            Material[] shared = original.GetComponent<MeshRenderer>().sharedMaterials;
            Mesh mesh = original.GetComponent<MeshFilter>().sharedMesh;

            // nothing changed in the hierarchy, the cross section must have been batched
            // with the submeshes, return as is, no need for any changes
            if (mesh.subMeshCount == _rightSide.subMeshCount)
            {
                // the the material information
                newObject.GetComponent<Renderer>().sharedMaterials = shared;

                return newObject;
            }

            // otherwise the cross section was added to the back of the submesh array because
            // it uses a different material. We need to take this into account
            Material[] newShared = new Material[shared.Length + 1];

            // copy our material arrays across using native copy (should be faster than loop)
            System.Array.Copy(shared, newShared, shared.Length);
            newShared[shared.Length] = crossSectionMat;

            // the the material information
            newObject.GetComponent<Renderer>().sharedMaterials = newShared;
            newObject.AddComponent<Trash>();
        }

        return newObject;
    }

    /**
     * Generate a new GameObject from the upper hull of the mesh
     * This function will return null if upper hull does not exist
     */
    public GameObject CreateUpperHull()
    {
        return CreateEmptyObject("Upper_Hull", _leftSide);
    }

    /**
     * Generate a new GameObject from the Lower hull of the mesh
     * This function will return null if lower hull does not exist
     */
    public GameObject CreateLowerHull()
    {
        return CreateEmptyObject("Lower_Hull", _rightSide);
    }

    public Mesh upperHull
    {
        get { return this._leftSide; }
    }

    public Mesh lowerHull
    {
        get { return this._rightSide; }
    }

    /**
     * Helper function which will create a new GameObject to be able to add
     * a new mesh for rendering and return.
     */
    private static GameObject CreateEmptyObject(string name, Mesh hull)
    {
        if (hull == null)
        {
            return null;
        }

        GameObject newObject = new GameObject(name);

        newObject.AddComponent<MeshRenderer>();
        MeshFilter filter = newObject.AddComponent<MeshFilter>();

        filter.mesh = hull;

        return newObject;
    }
}