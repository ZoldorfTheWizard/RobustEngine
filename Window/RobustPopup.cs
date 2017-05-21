using System.Drawing;
using OpenTK;
using OpenTK.Graphics;


namespace RobustEngine.Window
{
    class RobustPopup : NativeWindow
    {
        public RobustPopup() : base()
        {
            Width = 300;
            Height = 300;
            Title = "Robust Popup";
            Location = new Point(1920/2-150, 1080/2-150);
            ProcessEvents();
            base.Visible = true;
        }

        public RobustPopup(RobustSettings RS) : base()
        {

        }

        private RobustPopup(int W, int H, string Title, GameWindowFlags GWF, DisplayDevice DD) : base (W ,H , Title, GWF, GraphicsMode.Default ,DD)
        {

        }


        public void Show()
        {

        }


    }
}
