using UnityEngine;
using MLAgents;

public class Mouse : Agent
{
    private MouseAcademy mouseacademy; //the academy  affiliated to this mouse
    public GameObject cage; //the cage with walls
    MouseArea mousearea; //the floor
   
    Rigidbody body; //the mouse's body for physics engine
    // 
    public float turnSpeed = 300; //how fast does he move and turn
    public float moveSpeed = 2;
    private RayPerception3D m_RayPer; //the input for vector observations using Unity's RayPerception methods


    public override void InitializeAgent() //initialize the mouse and its physicalposition in the stage
    {
        base.InitializeAgent();
        body = GetComponent<Rigidbody>();
        Monitor.verticalOffset = 1f;
        mousearea = cage.GetComponent<MouseArea>();
        m_RayPer = GetComponent<RayPerception3D>();
        mouseacademy = FindObjectOfType<MouseAcademy>();
        SetResetParameters();
    }

    public override void CollectObservations() //keep taking vector inputs at rayAngles with respect to the mouses forward direction
    {     
            const float rayDistance = 50f;
            float[] rayAngles = { 20f, 90f, 160f, 45f, 135f, 70f, 110f }; //this is the input vectors for the emouse
            string[] detectableObjects = { "food", "wall", "trap"}; //the detectable objects are stacked values onto the above vectors
            AddVectorObs(m_RayPer.Perceive(rayDistance, rayAngles, detectableObjects, 0f, 0f)); //how he views
            var localVelocity = transform.InverseTransformDirection(body.velocity);
            AddVectorObs(localVelocity.x);
            AddVectorObs(localVelocity.z);
    }

    public void MoveAgent(float[] act) //movement follows Unity SDK MLAgents Heuristic for movement
    { //the mouse has a vectors space of 2 for action

        Vector3 dirToGo = Vector3.zero;
        Vector3 rotateDir = Vector3.zero;

            var forwardAxis = (int)act[0];//first action vector is movement on the forward axis
            var rotateAxis = (int)act[1]; //second vector is the rotation vector about the y axis. 
            //the neural network model will give us the optimum values for these action vectors on the trained mouse and tell it
            //how to move
            switch (forwardAxis)
            {
                case 1:
                    dirToGo = transform.forward;
                    break;
                case 2:
                    dirToGo = -transform.forward;
                    break;
            }
            switch (rotateAxis)
            {
                case 1:
                    rotateDir = -transform.up;
                    break;
                case 2:
                    rotateDir = transform.up;
                    break;
            }

            body.AddForce(dirToGo * moveSpeed, ForceMode.VelocityChange); //move him
            transform.Rotate(rotateDir, Time.fixedDeltaTime * turnSpeed);

        if (body.velocity.sqrMagnitude > 25f) // slow it down
        {
            body.velocity *= 0.95f;
        }

    }



    public override void AgentAction(float[] vectorAction, string textAction)
    {
        MoveAgent(vectorAction);
    }

    public override float[] Heuristic()
    {
        var action = new float[4];
        if (Input.GetKey(KeyCode.D))
        {
            action[1] = 2f;
        }
        if (Input.GetKey(KeyCode.W))
        {
            action[0] = 1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            action[1] = 1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            action[0] = 2f;
        }
        action[3] = 0.0f;
        return action;
    } 

    public override void AgentReset() //resets the mouse when a generation is over .randomly place him.
    {
        body.velocity = Vector3.zero;
        transform.position = new Vector3(Random.Range(-mousearea.range, mousearea.range),
            2f, Random.Range(-mousearea.range, mousearea.range));
        transform.rotation = Quaternion.Euler(new Vector3(0f, Random.Range(0, 360)));
        SetResetParameters();
    }

    void OnCollisionEnter(Collision collision) //check when the mouse runs into something and give him rewards or punishments
    {
        if (collision.gameObject.CompareTag("food"))
        {
            collision.gameObject.GetComponent<Item>().Eaten();
            AddReward(3f); //the mouse ran into cheese, so give him a reward scalar of +3
                mouseacademy.totalScore += 3;
        }
        if (collision.gameObject.CompareTag("trap"))
        {
            collision.gameObject.GetComponent<Item>().Eaten();
            AddReward(-3f); //he ran into a trap so punish him 3 points i.e. -3 reward
                mouseacademy.totalScore -= 3;
        }
        if (collision.gameObject.CompareTag("wall"))
        {
            AddReward(-0.5f); //I got sick of him running into walls so I added this. Now the mouse becomes really wary of walls..
                mouseacademy.totalScore -= 0;
        }
    }

    public override void AgentOnDone()
    {
    }


    public void SetResetParameters()
    {
    }
}
