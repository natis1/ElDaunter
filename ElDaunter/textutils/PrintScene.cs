using UnityEngine;
using UnityEngine.UI;

namespace ElDaunter.textutils
{
    public static class PrintScene
    {
        public static bool FindAndDestroyGameObjectInChildren( this GameObject gameObject, string name )
        {
            bool found = false;
            GameObject toDestroy = gameObject.FindGameObjectInChildren(name);
            if( toDestroy != null )
            {
                GameObject.Destroy( toDestroy );
                found = true;
            }
            return found;
        }

        public static GameObject FindGameObjectInChildren( this GameObject gameObject, string name )
        {
            if( gameObject == null )
                return null;

            foreach( var t in gameObject.GetComponentsInChildren<Transform>( true ) )
            {
                if( t.name == name )
                    return t.gameObject;
            }
            return null;
        }

        public static GameObject FindGameObjectNameContainsInChildren( this GameObject gameObject, string name )
        {
            if( gameObject == null )
                return null;

            foreach( var t in gameObject.GetComponentsInChildren<Transform>( true ) )
            {
                if( t.name.Contains( name ) )
                    return t.gameObject;
            }
            return null;
        }
        public static string PrintSceneHierarchyPath( this GameObject gameObject )
        {
            if( gameObject == null )
                return "WARNING: NULL GAMEOBJECT";

            string objStr = gameObject.name;

            if( gameObject.transform.parent != null )
                objStr = gameObject.transform.parent.gameObject.PrintSceneHierarchyPath() + "\\" + gameObject.name;

            return objStr;
        }
        
        public static void PrintSceneHierarchyTree( this GameObject gameObject, string fileName )
        {
            
            
            System.IO.StreamWriter file = null;
            file = new System.IO.StreamWriter( fileName );
            gameObject.PrintSceneHierarchyTree( true, file );
            file.Close();
        }
        
        public static void PrintSceneHierarchyTree( this GameObject gameObject, bool printComponents = false, System.IO.StreamWriter file = null )
        {
            if( gameObject == null )
                return;

            if( file != null )
            {
                file.WriteLine( "START =====================================================" );
                file.WriteLine( "Printing scene hierarchy for game object: " + gameObject.name );
            }
            foreach( Transform t in gameObject.GetComponentsInChildren<Transform>( true ) )
            {
                string objectNameAndPath = t.gameObject.PrintSceneHierarchyPath();

                if( file != null )
                {
                    file.WriteLine( objectNameAndPath );
                }


                if( printComponents )
                {
                    string componentHeader = "";
                    for( int i = 0; i < ( objectNameAndPath.Length - t.gameObject.name.Length ); ++i )
                        componentHeader += " ";

                    foreach( Component c in t.GetComponents<Component>() )
                    {
                        c.PrintComponentType( componentHeader, file );
                        c.PrintTransform( componentHeader, file );
                        c.PrintBoxCollider2D( componentHeader, file );
                        c.PrintCircleCollider2D( componentHeader, file );
                        c.PrintPolygonCollider2D( componentHeader, file );
                        c.PrintRigidbody2D( componentHeader, file );
                        c.PrintTextComponent(componentHeader, file);
                    }
                }
            }

            if( file != null )
            {
                file.WriteLine( "END +++++++++++++++++++++++++++++++++++++++++++++++++++++++" );
            }
        }

        public static void PrintTextComponent(this Component c, string componentHeader = "", System.IO.StreamWriter file = null)
        {
            if( c as Text != null )
            {
                var t = c as Text;
                if( file != null )
                {
                    file.WriteLine( componentHeader + @" \--Fonts Used: " + string.Join(" ", t.font.fontNames) );
                    file.WriteLine( componentHeader + @" \--Font Size: " + t.fontSize );
                    file.WriteLine( componentHeader + @" \--Text Contents: " + t.text );
                }
            }
        }
        
        public static void PrintComponentType( this Component c, string componentHeader = "", System.IO.StreamWriter file = null )
        {
            if( c == null )
                return;

            if( file != null )
            {
                file.WriteLine( componentHeader + @" \--Component: " + c.GetType().Name );
            }
        }

        public static void PrintTransform( this Component c, string componentHeader = "", System.IO.StreamWriter file = null )
        {
            if( c as Transform != null )
            {
                if( file != null )
                {
                    file.WriteLine( componentHeader + @" \--GameObject activeSelf: " + ( c as Transform ).gameObject.activeSelf );
                    file.WriteLine( componentHeader + @" \--GameObject layer: " + ( c as Transform ).gameObject.layer );
                    file.WriteLine( componentHeader + @" \--GameObject tag: " + ( c as Transform ).gameObject.tag );
                    file.WriteLine( componentHeader + @" \--Transform Position: " + ( c as Transform ).position );
                    file.WriteLine( componentHeader + @" \--Transform Rotation: " + ( c as Transform ).rotation.eulerAngles );
                    file.WriteLine( componentHeader + @" \--Transform LocalScale: " + ( c as Transform ).localScale );
                }
            }
        }

        public static void PrintBoxCollider2D( this Component c, string componentHeader = "", System.IO.StreamWriter file = null )
        {
            if( c as BoxCollider2D != null )
            {
                if( file != null )
                {
                    file.WriteLine( componentHeader + @" \--BoxCollider2D Size: " + ( c as BoxCollider2D ).size );
                    file.WriteLine( componentHeader + @" \--BoxCollider2D Offset: " + ( c as BoxCollider2D ).offset );
                    file.WriteLine( componentHeader + @" \--BoxCollider2D Bounds-Min: " + ( c as BoxCollider2D ).bounds.min );
                    file.WriteLine( componentHeader + @" \--BoxCollider2D Bounds-Max: " + ( c as BoxCollider2D ).bounds.max );
                    file.WriteLine( componentHeader + @" \--BoxCollider2D isTrigger: " + ( c as BoxCollider2D ).isTrigger );
                }
            }
        }

        public static void PrintCircleCollider2D( this Component c, string componentHeader = "", System.IO.StreamWriter file = null )
        {
            if( c as CircleCollider2D != null )
            {
                if( file != null )
                {
                    file.WriteLine( componentHeader + @" \--CircleCollider2D radius: " + ( c as CircleCollider2D ).radius );
                    file.WriteLine( componentHeader + @" \--CircleCollider2D Offset: " + ( c as CircleCollider2D ).offset );
                    file.WriteLine( componentHeader + @" \--CircleCollider2D Bounds-Min: " + ( c as CircleCollider2D ).bounds.min );
                    file.WriteLine( componentHeader + @" \--CircleCollider2D Bounds-Max: " + ( c as CircleCollider2D ).bounds.max );
                    file.WriteLine( componentHeader + @" \--CircleCollider2D isTrigger: " + ( c as CircleCollider2D ).isTrigger );
                }
            }
        }

        public static void PrintPolygonCollider2D( this Component c, string componentHeader = "", System.IO.StreamWriter file = null )
        {
            if( c as PolygonCollider2D != null )
            {
                if( file != null )
                {
                    foreach(var p in ( c as PolygonCollider2D ).points )
                        file.WriteLine( componentHeader + @" \--PolygonCollider2D points: " + p );
                    file.WriteLine( componentHeader + @" \--PolygonCollider2D Offset: " + ( c as PolygonCollider2D ).offset );
                    file.WriteLine( componentHeader + @" \--PolygonCollider2D Bounds-Min: " + ( c as PolygonCollider2D ).bounds.min );
                    file.WriteLine( componentHeader + @" \--PolygonCollider2D Bounds-Max: " + ( c as PolygonCollider2D ).bounds.max );
                    file.WriteLine( componentHeader + @" \--PolygonCollider2D isTrigger: " + ( c as PolygonCollider2D ).isTrigger );
                }
            }
        }

        public static void PrintRigidbody2D( this Component c, string componentHeader = "", System.IO.StreamWriter file = null )
        {
            if( c as Rigidbody2D != null )
            {
                if( file != null )
                {
                    file.WriteLine( componentHeader + @" \--PrintRigidbody2D mass: " + ( c as Rigidbody2D ).mass );
                    file.WriteLine( componentHeader + @" \--PrintRigidbody2D velocity: " + ( c as Rigidbody2D ).velocity );
                    file.WriteLine( componentHeader + @" \--PrintRigidbody2D drag: " + ( c as Rigidbody2D ).drag );
                    file.WriteLine( componentHeader + @" \--PrintRigidbody2D angularVelocity: " + ( c as Rigidbody2D ).angularVelocity );
                    file.WriteLine( componentHeader + @" \--PrintRigidbody2D angularDrag: " + ( c as Rigidbody2D ).angularDrag );
                    file.WriteLine( componentHeader + @" \--PrintRigidbody2D gravityScale: " + ( c as Rigidbody2D ).gravityScale );
                    file.WriteLine( componentHeader + @" \--PrintRigidbody2D isKinematic: " + ( c as Rigidbody2D ).isKinematic );
                    file.WriteLine( componentHeader + @" \--PrintRigidbody2D interpolation: " + ( c as Rigidbody2D ).interpolation );
                    file.WriteLine( componentHeader + @" \--PrintRigidbody2D freezeRotation: " + ( c as Rigidbody2D ).freezeRotation );
                    file.WriteLine( componentHeader + @" \--PrintRigidbody2D collisionDetectionMode: " + ( c as Rigidbody2D ).collisionDetectionMode );
                }
            }
        }
        
    }
}