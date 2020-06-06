using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Kogane.Internal
{
	/// <summary>
	/// ゲームオブジェクトの Hierarchy におけるパスをコピーするエディタ拡張
	/// </summary>
	internal static class GameObjectHierarchyPathCopyer
	{
		//==============================================================================
		// 定数
		//==============================================================================
		private const string ITEM_NAME = "GameObject/Copy Hierarchy Path";

		//==============================================================================
		// 関数(static)
		//==============================================================================
		/// <summary>
		/// ゲームオブジェクトの Hierarchy におけるパスをコピーするメニュー
		/// </summary>
		[MenuItem( ITEM_NAME, false, 12 )]
		private static void Copy()
		{
			var gameObjects = Selection.gameObjects.OrderBy( x => x.name );

			var stringBuilder = new StringBuilder();

			foreach ( var gameObject in gameObjects )
			{
				stringBuilder.AppendLine( gameObject.GetHierarchyPath() );
			}

			EditorGUIUtility.systemCopyBuffer = stringBuilder.ToString().TrimEnd();
		}

		/// <summary>
		/// 指定されたゲームオブジェクトの Hierarchy におけるパスを返します
		/// </summary>
		private static string GetHierarchyPath( this GameObject gameObject )
		{
			var path   = gameObject.name;
			var parent = gameObject.transform.parent;

			while ( parent != null )
			{
				path   = parent.name + "/" + path;
				parent = parent.parent;
			}

			return path;
		}

		/// <summary>
		/// ゲームオブジェクトの Hierarchy におけるパスをコピーできるかどうかを返します
		/// </summary>
		[MenuItem( ITEM_NAME, true )]
		private static bool CanCopy()
		{
			return 0 < Selection.gameObjects.Length;
		}
	}
}