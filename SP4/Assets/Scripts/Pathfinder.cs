using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System;

public class Pathfinder : MonoBehaviour {
	//Variables for use and to attach to any GameObject in Unity
	//public Transform seeker, target;


	PathRequest requestManager;
	GridNode grid;

	void Awake() {
		requestManager = GetComponent<PathRequest>();
		grid = GetComponent<GridNode>();
	}

	/*void Update() {
		if (Input.GetButtonDown("Jump"))
		{
		FindPath(seeker.position, target.position);
		}
	}*/

	public void StartFindPath(Vector3 startPos, Vector3 targetPos) {
		StartCoroutine(FindPath(startPos,targetPos));
	}

	IEnumerator FindPath(Vector3 startPos, Vector3 targetPos) {

		Stopwatch sw = new Stopwatch();
		sw.Start();
		Vector3[] waypoints = new Vector3[0];
		bool pathSuccess = false;

		Node startNode = grid.NodeFromWorldPoint (startPos);
		Node targetNode = grid.NodeFromWorldPoint (targetPos);

		if (startNode.walkable && targetNode.walkable)
		{

			Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
			HashSet<Node> closedSet = new HashSet<Node> ();
			openSet.Add(startNode);

			while (openSet.Count > 0) {
				//Expensive!
				//optimising for Heap Later on
				//Node currentNode = openSet[0];
				Node currentNode = openSet.RemoveFirst();
				/*for (int i = 1; i < openSet.Count; i++){
					if (openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost) {
						currentNode = openSet[i];
					}
				}
				openSet.Remove (currentNode);*/
				closedSet.Add(currentNode);

				if (currentNode == targetNode) {
					sw.Stop();
					print ("Path found: " + sw.ElapsedMilliseconds + " ms");
					pathSuccess = true;
					break;
				}

				foreach (Node neighbour in grid.GetNeighbours(currentNode)) {
					if (!neighbour.walkable || closedSet.Contains(neighbour)) {
						continue;
					}

					//cost of current node getting to neighbour node
					int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
					if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour)) {
						neighbour.gCost = newMovementCostToNeighbour;
						neighbour.hCost = GetDistance (neighbour, targetNode);
						neighbour.parent = currentNode;

						if (!openSet.Contains(neighbour))
							openSet.Add(neighbour);
						openSet.UpdateItem(neighbour);
					}
				}
			}
		}
		yield return null;
		if (pathSuccess) {
			waypoints = RetracePath(startNode, targetNode);
		}
		requestManager.FinishedProcessingPath(waypoints, pathSuccess);
	}

	Vector3[] RetracePath(Node startNode, Node endNode) {
		List<Node> path = new List<Node>();
		Node currentNode = endNode;

		while (currentNode != startNode) {
			path.Add(currentNode);
			currentNode = currentNode.parent;
		}
		Vector3[] waypoints = SimplifyPath (path);
		Array.Reverse(waypoints);
		return waypoints;

	}

	Vector3[] SimplifyPath(List<Node> path)
	{
		List<Vector3> waypoints = new List<Vector3>();
		Vector2 directionOld = Vector2.zero;
		for (int i = 1; i < path.Count; i++) {
			Vector2 directionNew = new Vector2(path[i-1].gridX - path[i].gridX,path[i-1].gridY - path[i].gridY);
			if (directionNew != directionOld) {
				waypoints.Add (path[i].worldPosition);
			}
			directionOld = directionNew;
		}
		return waypoints.ToArray();
	}

	//tool for getting the distance between Node A and B
	int GetDistance(Node nodeA, Node nodeB) {
		int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
		int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

		if (dstX > dstY)
			return 14 * dstY + 10 * (dstX - dstY);
		return 14 * dstX + 10 * (dstY - dstX);
	}
}
