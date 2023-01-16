using UnityEngine;

[ExecuteInEditMode]
public class WorldCurver : MonoBehaviour
{
	[Range(-0.1f, 0.1f)]
	public float curveStrengthx = 0.01f;
	[Range(-0.1f, 0.1f)]
	public float curveStrengthy = 0.01f;

    int m_CurveStrengthIDx;
    int m_CurveStrengthIDy;

    private void OnEnable()
    {
        m_CurveStrengthIDy = Shader.PropertyToID("_CurveStrengthy");
        m_CurveStrengthIDx = Shader.PropertyToID("_CurveStrengthx");
    }

	void Update()
	{
		Shader.SetGlobalFloat(m_CurveStrengthIDx, curveStrengthx);
		Shader.SetGlobalFloat(m_CurveStrengthIDy, curveStrengthy);
	}
}
