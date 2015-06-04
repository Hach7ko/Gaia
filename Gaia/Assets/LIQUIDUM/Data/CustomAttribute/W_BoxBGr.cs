using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif 

public class W_BoxBGr : PropertyAttribute
{

	
	    public int  BoxHeight =170;
	    public int  BoxWidth =200;
	    public string text;
	    public int  LineX = 1;
	    public int  LineY = 15;
	    
		public W_BoxBGr ( string text, int BoxHeight, int BoxWidth)
		{
                this.BoxHeight=BoxHeight;
		        this.BoxWidth=BoxWidth;
				this.text = text;
		        
		}
	
	
			public W_BoxBGr ( string text, int BoxHeight,int BoxWidth, int LineX,int LineY)
		{
                this.BoxHeight=BoxHeight;
		        this.BoxWidth=BoxWidth;
				this.text = text;
	        	this.LineX=LineX;
		        this.LineY=LineY;
		        
		}
}


#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(W_BoxBGr))]
public class W_BoxBGrDrawer : PropertyDrawer
{
   


	
		void Awake()
	{
	}
	
    public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
    {

        rect.x += W_BoxBGr_Attribute.LineX;
		 rect.y += W_BoxBGr_Attribute.LineY;
        rect.height = W_BoxBGr_Attribute.BoxHeight;
		 rect.width = W_BoxBGr_Attribute.BoxWidth;
		GUI.color = new Color(0,0,0.2f,1);
        GUI.Box(rect,"");
		GUI.color = Color.white;
    }
	
	
	    W_BoxBGr W_BoxBGr_Attribute
    {
        get
        {
            return (W_BoxBGr)attribute;
        }
    }
	


}

#endif