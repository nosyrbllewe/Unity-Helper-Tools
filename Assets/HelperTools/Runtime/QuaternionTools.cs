using UnityEngine;

public static class QuaternionTools
{
	public static void DecomposeSwingTwist(this Quaternion q, Vector3 twistAxis, out Quaternion swing, out Quaternion twist)
	{
		Vector3 r = new Vector3(q.x, q.y, q.z);
		
		// singularity: rotation by 180 degree
		if(r.sqrMagnitude < Mathf.Epsilon)
		{
			Vector3 rotatedTwistAxis = q * twistAxis;
			Vector3 swingAxis = Vector3.Cross(twistAxis, rotatedTwistAxis);
			
			if(swingAxis.sqrMagnitude > Mathf.Epsilon)
			{
				float swingAngle = Vector3.Angle(twistAxis, rotatedTwistAxis);
				swing = Quaternion.AngleAxis(swingAngle, swingAxis);
			}
			else
			{
				// more singularity:
				// rotation axis parallel to twist axis
				swing = Quaternion.identity; // no swing
			}
			
			twist = Quaternion.AngleAxis(0.0f, twistAxis);
			return;
		}
		
		// meat of swing-twist decomposition
		Vector3 p = Vector3.Project(r, twistAxis);
		twist = new Quaternion(p.x, p.y, p.z, q.w);
		twist.Normalize();
		swing = q * Quaternion.Inverse(twist);
	}
}