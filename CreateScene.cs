using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateScene : MonoBehaviour
{
    public int pyramidBaseSize = 5;
    public int numberOfTrees = 20;
    public float treeSpacing = 2f;
    public float treeRange = 10f;

    Transform pyramidParent;
    Transform forestParent;
    Transform groundParent;
    Transform celestialParent;
    Light directionalLight;
    


    void Start()
    {
        Variables();
        CreateGround();
        CreateForest();
        CreatePyramid();
        CreateCelestial();

    }


    void Variables()
    {
        pyramidParent = new GameObject("pyramid").transform;
        forestParent = new GameObject("Forest").transform;
        groundParent = new GameObject("Ground").transform;
        celestialParent = new GameObject("Celestial").transform;
        directionalLight = FindAnyObjectByType<Light>();
    }
    void CreateGround()
    {
        GameObject ground = GameObject.CreatePrimitive(PrimitiveType.Plane);
        ground.transform.parent = groundParent;
        ground.transform.localScale = new Vector3(2, 1, 2);
        ground.transform.position = new Vector3(0, 0, 0);
        ground.GetComponent<Renderer>().material.color = Color.red;
    }
    void CreateForest()
    {
        for (int i = 0; i < numberOfTrees; i++)
        {
            GameObject tree = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            tree.transform.parent = forestParent;

            float x = Random.Range(-treeRange, treeRange);
            float z = Random.Range(-treeRange, treeRange);
            float height = Random.Range(1f, 4f);
            float xScale = Random.Range(0.5f, 1.5f);
            float zScale = Random.Range(0.5f, 1.5f);

            tree.transform.position = new Vector3(x, height , z);
            tree.transform.localScale = new Vector3(xScale, height, zScale);
            tree.GetComponent<Renderer>().material.color = Color.green;
        }
    }
    void CreatePyramid()
    {
        int size = Mathf.Clamp(pyramidBaseSize, 3, 10);
        

        for(int y = 0; y < size; y++)
        {
            Color levelColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
            int levelSize = size - y;
            

            for (int x = 0; x < levelSize; x++)
            {

                for ( int z = 0; z < levelSize; z++)
                {
                    
                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.transform.parent = pyramidParent;
                    cube.transform.position = new Vector3((x - levelSize / 2f) *1.1f, y+0.5f, (z - levelSize / 2f) * 1.1f);
                    cube.GetComponent<Renderer>().material.color = levelColor;

                }
            }
                
        }
    }
    void CreateCelestial()
    {
        GameObject sun = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sun.transform.parent = celestialParent;
        sun.transform.position = new Vector3(0, 10, -20);
        sun.transform.localScale = Vector3.one * 2;
        sun.GetComponent<Renderer>().material.color = Color.yellow;

        sun.AddComponent<RotatingLight>().directionalLight = directionalLight;

    }

   
   
}
public class RotatingLight : MonoBehaviour
{
    public Light directionalLight;
    public float speed = 100f;
    
    public void Update()
    {
        transform.RotateAround(Vector3.zero, Vector3.right, speed * Time.deltaTime);

        if (directionalLight)
        {
            directionalLight.intensity = Mathf.Clamp01(Mathf.Sin(transform.eulerAngles.x * Mathf.Deg2Rad));
            directionalLight.transform.rotation = transform.rotation;
            float intensity = Mathf.Clamp01(Mathf.Sin(transform.eulerAngles.x * Mathf.Deg2Rad));

            directionalLight.intensity = intensity;
        }
    }
}
