using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PathNode : MonoBehaviour
{
	public bool forceStop;

	public bool teleportToNode;

	public PathNode teleportNode;

	public bool loadScene;

	public string loadSceneName = string.Empty;

	public string startAtNodeNamed = string.Empty;

	public bool startFacingRight;

	public bool setTrigger;

	public string triggerName;

	public bool adjustCameraFOV;

	public float targetCameraFOV = 12f;

	public bool tutorialStop;

	public tk2dUIItem uiItem;

	public List<PathNode> connectedNodes;

	public List<float> angles;

	public List<float> distances;

	public Conversation triggersConvo;

	public AudioClip triggersMusic;

	public AudioClip triggersMusicNighttime;

	public string soundPath;

	public string soundGroup;

	public bool overrideTurningForgiveness;

	public List<TurningForgivenessOverrideData> turningForgivenessOverrideData;

	private PathNode backupNode;

	[SerializeField]
	private bool _isActive = true;

	public bool isActive
	{
		get
		{
			return _isActive;
		}
		set
		{
			_isActive = value;
		}
	}

	public bool isTemporary { get; set; }

	public float distToSource { get; set; }

	public bool visited { get; set; }

	public PathNode BackupNode
	{
		get
		{
			return backupNode;
		}
	}

	private void Awake()
	{
		if (connectedNodes == null)
		{
			connectedNodes = new List<PathNode>();
		}
		if (angles == null)
		{
			angles = new List<float>();
		}
		if (distances == null)
		{
			distances = new List<float>();
		}
		angles = new List<float>();
		int count = connectedNodes.Count;
		for (int i = 0; i < count; i++)
		{
			float item = Mathf.Atan2(connectedNodes[i].transform.position.z - base.transform.position.z, connectedNodes[i].transform.position.x - base.transform.position.x) * 57.29578f;
			angles.Add(item);
			distances.Add(Vector3.Distance(base.transform.position, connectedNodes[i].transform.position));
		}
		visited = false;
		distToSource = float.PositiveInfinity;
	}

	public void AddConnectedNode(PathNode node)
	{
		if (!(node == null))
		{
			float newAngle = Mathf.Atan2(node.transform.position.z - base.transform.position.z, node.transform.position.x - base.transform.position.x) * 57.29578f;
			float newDistance = Vector3.Distance(base.transform.position, node.transform.position);
			AddConnectedNode(node, newAngle, newDistance);
		}
	}

	public void AddConnectedNode(PathNode node, float newAngle, float newDistance)
	{
		if (!connectedNodes.Contains(node))
		{
			connectedNodes.Add(node);
			angles.Add(newAngle);
			distances.Add(newDistance);
		}
	}

	public void RemoveConnectedNode(PathNode node)
	{
		if (!connectedNodes.Contains(node))
		{
			return;
		}
		int index = connectedNodes.IndexOf(node);
		connectedNodes.Remove(node);
		angles.RemoveAt(index);
		distances.RemoveAt(index);
		if (isTemporary && node.isTemporary && backupNode != null)
		{
			AddConnectedNode(backupNode);
			if (!backupNode.connectedNodes.Contains(this))
			{
				backupNode.AddConnectedNode(this);
			}
		}
	}

	public void SetBackupNode(PathNode node)
	{
		backupNode = node;
	}

	public float GetDistanceToConnectedNode(PathNode node)
	{
		if (!connectedNodes.Contains(node))
		{
			return -1f;
		}
		int num = connectedNodes.IndexOf(node);
		if (num >= 0 && num < distances.Count)
		{
			return distances[connectedNodes.IndexOf(node)];
		}
		return -1f;
	}

	public void SetForceStop(bool shouldForceStop)
	{
		forceStop = shouldForceStop;
	}

	public int GetIndexOfConnectedNodeAtAngle(float angle, float tolerance = 0.01f)
	{
		for (int i = 0; i < angles.Count; i++)
		{
			float num = angles[i];
			if (Mathf.Abs(num - angle) < tolerance)
			{
				return i;
			}
		}
		return -1;
	}

	public bool IsNodeConnectedOrBackup(PathNode node)
	{
		if (node == null)
		{
			return false;
		}
		return connectedNodes.Contains(node) || backupNode == node;
	}
}
