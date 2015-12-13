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
		float growth = branch.Growth - 0.5f;
		float topWidth = branch.Growth * 0.5f / 2f;

		Vector3 baseLeft = new Vector3(-0.5f, -0.5f ,0f);
		Vector3 baseRight = new Vector3(0.5f, -0.5f ,0f);
		Vector3 topLeft = new Vector3(-topWidth, growth ,0f);
		Vector3 topRight = new Vector3(topWidth, growth ,0f);

		GL.PushMatrix();
		mat.SetPass(0);
		GL.MultMatrix(transform.localToWorldMatrix);

		for (int i = 0; i < numLines; i++)
		{
			float t = (float)i/((float)numLines-1f);
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
}
