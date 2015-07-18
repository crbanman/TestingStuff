using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathFinder : MonoBehaviour {

	public static PathFinder instance;

	public GameObject pathTile;
	public GameObject openTile;

	List<node> openList;
	List<node> closedList;

	private bool cutCorners = true;

	public void Start () {
		instance = this;
	}

	public void Update () {
	}

	public List<Vector2> AStar (int startingX, int startingY, int targetX, int targetY, bool cutCorners) {
		this.cutCorners = cutCorners;
		return AStar (startingX, startingY, targetX, targetY);
	}

	public List<Vector2> AStar (int startingX, int startingY, int targetX, int targetY) {

		openList = new List<node>();
		closedList = new List<node>();

		Vector2 startingTile = new Vector2(startingX, startingY);
		Vector2 targetTile = new Vector2(targetX, targetY);

		node startingNode = new node(startingTile, null, 0, 0);
		node targetNode = new node(targetTile, null, 0, 0);

		bool pathFound = false;
		bool noPath = false;

		// Add starting node to the open list.
		openList.Add(startingNode);

		do{
			// Look for the lowest F cost square on the open list.
			node currentNode = LowestCostingNode(openList);

			// Remove current node from open list.
			openList.Remove(currentNode);

			// Add current node to closed list.
			closedList.Add (currentNode);

			// Evaluate adjacent nodes.
			AddAjacentWalkableNodes(currentNode, targetNode);

			if(GetNodeFromClosedList(targetTile) != null) {
				pathFound = true;
			}

			if(!pathFound && openList.Count == 0) {
				noPath = true;
			}
		
		} while (!pathFound && !noPath);

		if(pathFound){
			return GetPath(GetNodeFromClosedList(targetTile));
		}
		else {
			Debug.Log ("No Path Exists to (" + targetX + ", " + targetY + ")");
			return null;
		}
	}

	node LowestCostingNode(List<node> list) {
		node lowestCost = null;
		foreach (node node in list) {
			if(lowestCost == null || node.f < lowestCost.f) {
				lowestCost = node;
			}
		}
		return lowestCost;
	}

	void AddAjacentWalkableNodes(node currentNode, node targetNode) {
		Vector2 tile;

		// above		
		tile = new Vector2(currentNode.tile.x, currentNode.tile.y + 1);
		EvaluateNode(tile, currentNode, targetNode, 10);

		// above right
		if (cutCorners) {
			tile = new Vector2 (currentNode.tile.x + 1, currentNode.tile.y + 1);
			EvaluateNode (tile, currentNode, targetNode, 14);
		}

		// right
		tile = new Vector2(currentNode.tile.x + 1, currentNode.tile.y);
		EvaluateNode(tile, currentNode, targetNode, 10);

		// below right
		if (cutCorners) {
			tile = new Vector2 (currentNode.tile.x + 1, currentNode.tile.y - 1);
			EvaluateNode (tile, currentNode, targetNode, 14);
		}

		// below
		tile = new Vector2(currentNode.tile.x, currentNode.tile.y - 1);
		EvaluateNode(tile, currentNode, targetNode, 10);

		// below left
		if (cutCorners) {
			tile = new Vector2 (currentNode.tile.x - 1, currentNode.tile.y - 1);
			EvaluateNode (tile, currentNode, targetNode, 14);
		}

		// left
		tile = new Vector2(currentNode.tile.x - 1, currentNode.tile.y);
		EvaluateNode(tile, currentNode, targetNode, 10);

		// above left
		if (cutCorners) {
			tile = new Vector2 (currentNode.tile.x - 1, currentNode.tile.y + 1);
			EvaluateNode (tile, currentNode, targetNode, 14);
		}
	}

	node GetNodeFromOpenList (Vector2 tile) {
		foreach(node node in openList) {
			if(node.tile.x == tile.x && node.tile.y == tile.y) {
				return node;
			}
		}
		return null;
	}

	node GetNodeFromClosedList (Vector2 tile) {
		foreach(node node in closedList) {
			if(node.tile.x == tile.x && node.tile.y == tile.y) {
				return node;
			}
		}
		return null;
	}

	void EvaluateNode (Vector2 tile, node currentNode, node targetNode, int movementCost) {
		if(Map.IsTileWalkable(tile.x, tile.y) && GetNodeFromClosedList(tile) == null) {
			// Check if node is in the open list
			node node = GetNodeFromOpenList(tile);
			
			if(node == null){
				if (tile.x == targetNode.tile.x && tile.y == targetNode.tile.y) {
					movementCost = 0;
				}
				int g = currentNode.g + movementCost;
				int h = CalculateH (currentNode, targetNode);
				openList.Add (new node(tile, currentNode, g, h));
			}
			else {
				if (node.g > currentNode.g + movementCost) {
					openList.Remove(node);
					node.parentNode = currentNode;
					node.g = currentNode.g + movementCost;
					node.f = node.g + node.h;
					openList.Add (node);
				}
			}
		}
	}

	int CalculateH (node currentNode, node targetNode) {
		float deltaX = Mathf.Abs(targetNode.tile.x - currentNode.tile.x);
		float deltaY = Mathf.Abs(targetNode.tile.y - currentNode.tile.y);

		return (int)(deltaX + deltaY);
	}

	List<Vector2> GetPath(node node) {
		List<Vector2> path = new List<Vector2>();
		node currentNode = node;
		while (currentNode.parentNode != null) {
			path.Add (currentNode.tile);
			currentNode = currentNode.parentNode;
		}
		return path;
	}

	public class node {
		public Vector2 tile;
		public node parentNode;
		public int f, g, h;

		public node (Vector2 tile, node parentNode, int g, int h) {
			this.tile = tile;
			this.parentNode = parentNode;
			this.g = g;
			this.h = h;
			f = g + h;
		}

		public override bool Equals(System.Object obj)
		{
			// If parameter is null return false.
			if (obj == null)
			{
				return false;
			}
			
			// If parameter cannot be cast to Point return false.
			node node = obj as node;
			if ((System.Object)node == null)
			{
				return false;
			}
			
			// Return true if the fields match:
			return tile == node.tile;
		}
		
		public bool Equals(node node)
		{
			// If parameter is null return false:
			if ((object)node == null)
			{
				return false;
			}

			// Return true if the fields match:
			return tile == node.tile;
		}
		
		public override int GetHashCode()
		{
			return (int)tile.x ^ (int)tile.y;
		}
	}
}
