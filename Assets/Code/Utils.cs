using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class Utils
{ 
	public static t FindComp<t>(Transform trans, string childPath)
	{
		if ((object)trans == null)
		{
			Debug.LogError("Utils.findComp : trans is null");
			return default;
		}

		var find = trans.Find(childPath);
		if ((object)find == null)
		{
			Debug.LogError($"Utils.FindComp : not found transform child : ${trans.name} - ${childPath}");
			return default;
		}

		var comp = find.GetComponent<t>();
		if ((object)comp == null)
		{
			Debug.LogError($"Utils.FindComp : not found component : ${trans.name} - ${typeof(t)}");
			return default;
		}

		return comp;
	}

	public static float GetAngleBetweenPoints(Vector2 a, Vector2 b, Vector2 c)
	{
		// 벡터 AB와 AC를 계산
		double abX = b.x - a.x;
		double abY = b.y - a.y;
		double acX = c.x - a.x;
		double acY = c.y - a.y;

		// 벡터 AB와 AC의 크기를 계산 (벡터 길이)
		double abLength = Math.Sqrt(abX * abX + abY * abY);
		double acLength = Math.Sqrt(acX * acX + acY * acY);

		// 벡터 AB와 AC의 내적
		double dotProduct = abX * acX + abY * acY;

		// 두 벡터 사이의 코사인 값
		double cosTheta = dotProduct / (abLength * acLength);

		// 아크 코사인으로 각도를 구함 (라디안 값)
		double angle = Math.Acos(cosTheta);

		return (float)angle;
	}
}
