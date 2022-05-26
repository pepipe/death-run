using UnityEngine;

namespace pepipe.Utils.Logging
{
	[AddComponentMenu("_Pepipe/Services/Logger")]
	public class CustomLogger : MonoBehaviour
	{
		[Header("Settings")]
		[SerializeField] bool m_ShowLogs = true;
		[SerializeField] string m_Prefix;
		[SerializeField] Color m_PrefixColor;
		
		string _hexColor;

		void OnValidate()
		{
			_hexColor = "#" + ColorUtility.ToHtmlStringRGB(m_PrefixColor);
			m_Prefix = m_Prefix.ToUpper();
		}

		void Awake()
		{
			m_Prefix += ":";
		}

		public void Log(object message, Object sender)
		{
			if (!m_ShowLogs) return;
			
			Debug.Log($"<color={_hexColor}>{m_Prefix}</color> {message}", sender);
		}
	}
}
