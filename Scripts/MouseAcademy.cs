using UnityEngine;
using UnityEngine.UI;
using MLAgents;

public class MouseAcademy : Academy
{
    public GameObject agent; //the agent being trained i.e the mouse
    public MouseArea area; //the stage

    public int totalScore;

    //the academy provides us the collective resource which each iteration of the agent accesses
    public override void AcademyReset() //resets the playing ground for the new generation of mouse
    {
        ClearObjects(GameObject.FindGameObjectsWithTag("food"));
        ClearObjects(GameObject.FindGameObjectsWithTag("trap"));
        area.ResetArea(agent);
        totalScore = 0;
    }

    void ClearObjects(GameObject[] objects) //helper function to destroy food items
    {
        foreach (GameObject item in objects)
        {
            Destroy(item);
        }
    }
}
