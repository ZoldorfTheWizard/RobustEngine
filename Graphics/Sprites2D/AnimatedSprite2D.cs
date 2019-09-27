using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RobustEngine.Graphics.Shapes2D;

namespace RobustEngine.Graphics.Sprites2D
{
    public class AnimatedSprite2D
    {
      /*     public string Key;
        public bool  IsLooping;
        public int   FPS;
        public int   CurrentFrame;
        public int   MaxFrames;
        public float MaxTime;   
        public float CurrentTime;
        public Sprite[] Sprites;
        
        public Direction CurrentDirection;        
        public Dictionary<Direction, Sprite[]> Animation;
        public Dictionary<Direction, Rect2D> AABB;


        public AnimatedSprite()
        {

        }
   
        public void LoadAnimation(Sprite[] Frames, Direction direction)
        {
            Animation.Add(direction, Frames);
            foreach (var Frame in Frames)
            {
                AABB.Add(direction, Frame.AABB);
                MaxTime = (1 / FPS) * (1 / MaxFrames);          }
        }
 

        public void AddTime(float time)
        {
            CurrentTime += time;
            while (CurrentTime > MaxTime)
            {
                if (IsLooping)
                    CurrentTime -= MaxTime;
                else
                    CurrentTime = MaxTime;
            }
            CurrentFrame = (int)Math.Floor(CurrentTime * FPS);
        }


        public void Reset()
        {
            CurrentTime = 0;          
            IsLooping = false;
            CurrentFrame = 0;
        }

        public void Update()
        {

        }

        public void Draw()
        {
                
        }

        public void SetTime(float time)
        {
            CurrentTime = time;
            CurrentFrame = (int)Math.Floor(CurrentTime * FPS);
        }
        public void SetDirection(Direction dir)
        {
            CurrentDirection = dir;
        }
 */


    }

    public enum Direction
    {
        N,
        NE,
        E,
        SE,
        S,
        SW,
        W,
        NW
    }
}
