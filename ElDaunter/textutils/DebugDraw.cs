using UnityEngine;

namespace ElDaunter.textutils
{
    public class DebugDraw : MonoBehaviour
    {
        private GUIStyle style;
        // What text should be drawn
        public string drawString;
        // What color the text should be
        private Color drawColor = Color.white;
        // Are we currently drawing text
        private bool drawingText = true;

        
        
        private void OnGUI()
        {
            // First, make sure our style is loaded. this contains important information like the font used.
            if (style == null)
            {
                style = new GUIStyle(GUI.skin.label);
            }

            // A Matrix4x4 is a unity object that allows us to place images and text overlaying the game.
            Matrix4x4 matrix = GUI.matrix;
            
            // Set our text to the current color we have selected
            GUI.backgroundColor = Color.clear;
            GUI.contentColor = drawColor;
            GUI.color = drawColor;
            
            
            // This essentially creates a 1920x1080 pixel screen on which to draw the text
            // This will overlay the game screen.
            GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity,
                new Vector3(Screen.width / 1920f, Screen.height / 1080f, -1f));
            
            // We want our text to appear in the middle of the screen, some number of pixels down from the top.
            // This alignment centers our text for us
            style.alignment = TextAnchor.UpperCenter;
            style.fontSize = 32;
            
            // The first two numbers represent our x and y positions for the text since it is anchored to the upper middle
            // For the upper-middle of the screen we can set x to 0 and y to around 100 pixels. This is the number of pixels
            // from the upper middle to move right and then down respectively.
            GUI.Label(new Rect((float) 0f, 100f, 1920f, 1080f), drawString, style);
            GUI.matrix = matrix;
        }
    }
}