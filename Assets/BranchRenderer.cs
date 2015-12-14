using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Branch))]
public class BranchRenderer : MonoBehaviour
{
	public Branch branch;
	public int numLines = 5;
	public Material mat;

	// Use this for initialization
	void Start ()
	{
		Renderer renderer = GetComponent<Renderer>();
		if (renderer != null)
			renderer.enabled = false;
	}

	void OnRenderObject()
	{
		BranchRenderer parent = (branch.parent == null) ? null : branch.parent.GetComponent<BranchRenderer>();

		float growth = branch.Growth - 0.5f;
		float topWidth = branch.Growth * 0.5f / 2f * transform.localScale.x;

		Vector3 baseLeft, baseRight;
		if (parent == null)
		{
			baseLeft = new Vector3(-0.5f, -0.5f, 0f);
			baseRight = new Vector3(0.5f, -0.5f, 0f);
		}
		else
		{
			Vector3[] parentTop = parent.GetTopCorners();
			baseLeft = transform.InverseTransformPoint(parentTop[0]);
			baseRight = transform.InverseTransformPoint(parentTop[1]);
		}
		Vector3 topLeft = new Vector3(-topWidth, growth, 0f);
		Vector3 topRight = new Vector3(topWidth, growth, 0f);

		GL.PushMatrix();
		mat.SetPass(0);
		GL.MultMatrix(transform.localToWorldMatrix);

		int lineCount = Mathf.Max(1, numLines - (branch.Depth/2)*2);

		for (int i = 0; i < lineCount; i++)
		{
			float t = (float)i/((float)lineCount-1f);
			Vector3 basePos = Vector3.Lerp(baseLeft, baseRight, t);
			Vector3 topPos = Vector3.Lerp(topLeft, topRight, t);

			GL.Begin(GL.LINES);
			GL.Color(mat.color);
			GL.Vertex(basePos);
			GL.Vertex(topPos);
			GL.End();
		}

		GL.PopMatrix();
	}

	public Vector3[] GetBaseCorners()
	{
		Vector3 baseLeft = transform.TransformPoint(new Vector3(-0.5f, -0.5f, 0f));
		Vector3 baseRight = transform.TransformPoint(new Vector3(0.5f, -0.5f, 0f));
		Vector3[] baseCorners = { baseLeft, baseRight };
		return baseCorners;
	}

	public Vector3[] GetTopCorners()
	{
		float growth = branch.Growth - 0.5f;
		float topWidth = branch.Growth * 0.5f / 2f * transform.localScale.x;
		Vector3 topLeft = transform.TransformPoint(new Vector3(-topWidth, growth, 0f));
		Vector3 topRight = transform.TransformPoint(new Vector3(topWidth, growth, 0f));
		Vector3[] topCorners = { topLeft, topRight };
		return topCorners;
	}
}
