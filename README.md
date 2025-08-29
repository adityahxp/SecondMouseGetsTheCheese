# SecondMouseGetsTheCheese
Reinforcement Learning Experiment in Unity.
2019: A 3D reenactment and visualization of reinforcement learning.
Here's a [link to the visualization](https://www.youtube.com/watch?v=s3OBuZosL5E).

## Overview
In this project, I designed a program and environment in Unity to teach a mouse with no prior knowledge of cheese vs. trap to **find cheese and avoid traps**.  

This was implemented using **Reinforcement Learning** in Unity with C# and the [Unity ML-Agents](https://github.com/Unity-Technologies/ml-agents) plugin.  

The goal was to visualize reinforcement learning algorithms and architectures we studied in class by simulating a mouse that learns through experience.

---

## A Brief Introduction to Reinforcement Learning
Reinforcement learning works similarly to how pets learn tricks:

- **Positive Rewards:** Given when the agent does what we want.  
- **Negative Rewards (Punishments):** Given when it performs unwanted actions.  

The agent learns by iterating over many rounds, basing its future decisions on past rewards. Unlike supervised learning, reinforcement learning rewards sequences of states rather than labeling individual states.

---

## Policy Functions
Unity ML-Agents uses **Policy Learning**, which maps an agent’s **state** (observations and past vectors) to an **action**.  

An **optimal action** is defined as one that maximizes predicted future rewards. The agent can prioritize short-term or long-term rewards depending on configuration.

---

## Machine Learning in Unity
Unity ML-Agents provides two main components:

- **Agent** – The character (mouse) that interacts with the environment and learns.  
- **Academy** – Defines the structure, rules, rewards, punishments, and resets.  

ML-Agents is built on TensorFlow, so the reinforcement learning algorithm didn’t need to be implemented from scratch—only adapted by configuring environment, inputs, outputs, and hyperparameters.

---

## Environment
The environment consists of:

- **Mouse (Agent):** Learns movement.  
- **Cheese:** Reward of `+3` when eaten.  
- **Traps:** Punishment of `-1` when eaten.  
- **Walls:** Punishment of `-0.5` when collided with.  

Cheese and traps are **randomly generated** by the `MouseArea` script.

---

## Program Design
- **Inputs:**  
  - `RayPerception3D` heuristic used for vision.  
  - The mouse looks along **7 vectors** (`20°, 90°, 160°, 45°, 135°, 70°, 110°`) within 50f distance.  
  - Detects “food” and “trap” objects.  

- **Outputs:**  
  - Rotate left/right.  
  - Move forward/back.  

- **Rewards:**  
  - Cheese = `+3`  
  - Trap = `-3`  
  - Wall = `-0.5`  

---

## Training
Training uses **Proximal Policy Optimization (PPO)**.  

The mouse runs multiple episodes, with PPO updating its policy after each round to maximize rewards. Training required **hyperparameter tuning** to achieve smooth behavior.

---

## Parameter Tuning
Key tuned parameters:

1. **Trainer Type:** PPO  
2. **Lambda:** 0.99  
3. **Buffer Size:** 12000  
4. **Beta:** 0.001  
5. **Learning Rate Schedule:** Linear  
6. **Num Layers:** 2  
7. **Sequence Length:** 64  

Progress was monitored using **TensorBoard**.

---

## Training Visualizations
- **Cumulative Rewards per Generation vs Time**  
- **Entropy vs Time**  
- **Learning Rate vs Time**  
- **Mean Value Estimate vs Time**  
- **Mean Value Loss vs Time**  

These showed stabilization of rewards and decreasing entropy as the agent learned.

---

## Performance
- Final mouse consistently ate cheese and avoided traps.  
- Still struggled with **deceleration** (stopping before traps).  
- Suggested improvement: Include **current velocity as an input vector**.  

---

## Final Thoughts
- Most effort was in **environment setup** and **parameter tuning**.  
- Unity ML-Agents was exciting to use, but required experimentation.  
- Reinforcement learning in Unity is valuable for video game AI, NPCs, and more.  
- Future work could involve **modifying ML-Agents’ neural network generator directly**.  

---

## References
- [DeepSense: What is Reinforcement Learning?](https://deepsense.ai/what-is-reinforcement-learning-the-complete-guide/)  
- [Reinforcement Learning for Humans](https://medium.com/machine-learning-for-humans/reinforcement-learning-6eacf258b265)  
- [Unity ML-Agents Docs](https://github.com/Unity-Technologies/ml-agents/tree/master/docs)  
- [Training PPO](https://github.com/Unity-Technologies/ml-agents/blob/master/docs/Training-PPO.md)  
- [Sutton & Barto – Reinforcement Learning Book](http://incompleteideas.net/sutton/book/RLbook2018trimmed.pdf)  
- Juliani, A., Berges, V., Vckay, E., Gao, Y., Henry, H., Mattar, M., Lange, D. (2018). *Unity: A General Platform for Intelligent Agents*. arXiv preprint [arXiv:1809.02627](https://arxiv.org/abs/1809.02627).  
