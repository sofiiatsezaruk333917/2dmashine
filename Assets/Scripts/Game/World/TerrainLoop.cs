using UnityEngine;
using UnityEngine.U2D;

public class TerrainLoop : MonoBehaviour
{
    public GameObject camera;

    public float offsetRight = 0;
    public int countOfPrefabs = 2;

    public float generateNextTrigger = 10f;
    public float removePrevTrigger = 100f;

    private float x1;
    private float x2;
    private float width;
    private float height;
    private int countOfNodes = 1;
    private int lastVariationOfGeneratedNode = 1;

    private void Start()
    {
        height = camera.GetComponent<Camera>().orthographicSize * 2;
        width = camera.GetComponent<Camera>().aspect * height;

        x1 = transform.position.x;

        updateEdgesCoordinates();
    }

    void Update()
    {
        if (camera.transform.position.x + (width / 2) > x2 - generateNextTrigger)
        {
            generateNextNodes();
            updateEdgesCoordinates();
        }

        if (camera.transform.position.x > (x1 + removePrevTrigger))
        {
            removeLeftNode();
        }
    }

    int getNextNodeVariation() {
        int nextNodeNumber;

        do
        {
            nextNodeNumber = Random.Range(2, countOfPrefabs + 1);
        } while (nextNodeNumber == lastVariationOfGeneratedNode);

        lastVariationOfGeneratedNode = nextNodeNumber;

        return nextNodeNumber;
    }

    void spawnBridge(GameObject nextNode) {

        float positionX = nextNode.transform.position.x - 2.69f;
        float positionY = nextNode.transform.position.y + 3.32f;

        GameObject bridge = Instantiate(
            Resources.Load("prefabs/Terrain/Bridge"),
            new Vector3(positionX, positionY, nextNode.transform.position.z),
            Quaternion.identity
        ) as GameObject;

        bridge.transform.SetParent(nextNode.transform);
    }

    void generateNextNodes()
    {
        int nextNodeNumber = getNextNodeVariation();

        GameObject nextNode = Instantiate(
            Resources.Load("prefabs/Terrain/Node" + nextNodeNumber),
            new Vector3(x2, transform.position.y, transform.position.z),
            Quaternion.identity
        ) as GameObject;

        countOfNodes = countOfNodes + 1;

        nextNode.transform.SetParent(transform);

        nextNode.name = "Node" + countOfNodes;

        int setupNumber = Random.Range(1, 4);

        spawnBridge(nextNode);

        if (setupNumber < 4)
        {
            nextNode.transform.Find("Setup" + setupNumber).gameObject.active = true;
        }
    }

    void removeLeftNode()
    {
        GameObject leftTerrainNode = transform.GetChild(0).gameObject;

        float nodeWidth = calculateTerrainNodeWidth(leftTerrainNode) + offsetRight;

        Destroy(leftTerrainNode);

        x1 = x1 + (nodeWidth);
    }

    float calculateTerrainNodeWidth(GameObject terrain)
    {
        SpriteShapeController spriteShapeController = terrain.GetComponent<SpriteShapeController>();

        float minX = 0;
        float maxX = 0;

        for (int i = 0; i < spriteShapeController.spline.GetPointCount(); i++)
        {
            Vector3 pos = spriteShapeController.spline.GetPosition(i);
            if (pos.x > maxX)
            {
                maxX = pos.x;
            }
            if (pos.x < minX)
            {
                minX = pos.x;
            }
        }

        return maxX - minX;
    }

    void updateEdgesCoordinates()
    {
        GameObject terrainNode = transform.GetChild(transform.childCount - 1).gameObject;

        float terrainWidth = calculateTerrainNodeWidth(terrainNode);

        if (countOfNodes > 1)
        {
            x2 = x2 + terrainWidth + offsetRight;
        }
        else
        {
            x2 = x1 + terrainWidth + offsetRight;
        }
    }

}
