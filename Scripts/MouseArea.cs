using UnityEngine;
using MLAgents;

public class MouseArea : Area
{
    public GameObject cheese;
    public GameObject trap;
    public int numCheeses;
    public int numTraps;
    public float range;
    public GameObject floor;

    void CreateItems(int num, GameObject type)
    {
        for (int i = 0; i < num; i++)
        {
            GameObject item = 
            Instantiate(type, new Vector3(Random.Range(-range, range), 2f, Random.Range(-range, range)), Quaternion.identity); //random spawn the food
            item.GetComponent<Item>().area = floor;
        }
    }

     public override void ResetArea()
    {
    }

    public void ResetArea(GameObject agent)
    {
        // reposition the mouse
        agent.transform.position = new Vector3(Random.Range(-range, range), 2f,
        Random.Range(-range, range)) + transform.position;
        agent.transform.rotation = Quaternion.Euler(new Vector3(0f, Random.Range(0, 360)));
        //create items again
        CreateItems(numCheeses, cheese);
        CreateItems(numTraps, trap); 
    }


}
