using System;
using OpenTK;
using System.IO;

namespace RobustEngine.System.Settings
{

    [Serializable]
    public class VideoSettings
    {
        public VSyncMode    VSync;
        public WindowBorder Border;
        public WindowIcon   Icon;
        public Size         Size;
        public bool         Fullscreen;
        public int          Antialiasing;
        public int          Anistrophic;


        /// <summary>
        /// Default Constructor. Builds a 600x800 window.
        /// </summary>
        public VideoSettings()
        {
            VSync = VSyncMode.Off;
            Size = new Size(800,600);
            Fullscreen = false;
        }

        public VideoSettings Load(string Pathtosettings)
        {
            return null; // Todo
        }      


    }


}
