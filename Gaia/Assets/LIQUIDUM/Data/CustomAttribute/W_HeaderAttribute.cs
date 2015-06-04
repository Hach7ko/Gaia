using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif 

public class W_HeaderAttribute : PropertyAttribute
{
		public string headerText;
		public string text;
	    public int FontSize;
    	public Color HeaderColor=Color.gray;
	    

		public W_HeaderAttribute (string header)
		{
				headerText = header;
		}
		public W_HeaderAttribute (string header, string text)
		{
				headerText = header;
				this.text = text;
		      
		}
	
				public W_HeaderAttribute (string header, string text, int FontSize)
		{
				headerText = header;
				this.text = text;
		        this.FontSize=FontSize;
		        //this.HeaderColor=HeaderColor;
		}

	
	
			public W_HeaderAttribute (string header, string text, int FontSize, string HeaderColor)
		{
				headerText = header;
				this.text = text;
		        this.FontSize=FontSize;
		        if(HeaderColor=="RedColor")
		        this.HeaderColor=Color.red;
			    else if(HeaderColor=="WhiteColor")
		        this.HeaderColor=Color.white;
				else if(HeaderColor=="YellowColor")
		        this.HeaderColor=Color.yellow;
				else if(HeaderColor=="BlackColor")
		        this.HeaderColor=Color.black;
				else if(HeaderColor=="GreenColor")
		        this.HeaderColor=Color.green;
				else if(HeaderColor=="BlueColor")
		        this.HeaderColor=Color.blue;
		}
}


#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(W_HeaderAttribute))]
public class HeaderDrawer : PropertyDrawer
{
    const int HeaderHeight = 25, TextHeight = 40, LineHeight =2;
    const int HeaderY = 10, LineX = 15;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        position.y += HeaderY+3;
        position.height = HeaderHeight;

        //DrawHeader
        EditorGUI.LabelField(position, headerAttribute.headerText, headerStyle);
		
		//Draw Simple small text
        if (!string.IsNullOrEmpty(headerAttribute.text))
        {
            position.y += HeaderHeight - 5;
            EditorGUI.LabelField(position, headerAttribute.text, EditorStyles.whiteMiniLabel);
        }

        position.y += string.IsNullOrEmpty(headerAttribute.text) ? HeaderHeight : TextHeight;
        position.x += LineX;
        position.height = LineHeight;
		
        GUI.Box(position, "");

    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return base.GetPropertyHeight(property, label) + HeaderHeight + (string.IsNullOrEmpty(headerAttribute.text) ? 0 : TextHeight);
    }

    W_HeaderAttribute headerAttribute
    {
        get
        {
            return (W_HeaderAttribute)attribute;
        }
    }

     GUIStyle headerStyle
    {
        get
        {
            GUIStyle style = new GUIStyle(EditorStyles.largeLabel);
            style.fontStyle = FontStyle.Bold;
            style.fontSize = headerAttribute.FontSize;
            style.normal.textColor = EditorGUIUtility.isProSkin ? headerAttribute.HeaderColor : new Color(0.4f, 0.4f, .4f, 1f);
            return style;
        }
    }
}

#endif