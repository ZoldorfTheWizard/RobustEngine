﻿using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobustEngine.Graphics.Shape
{
    public class Rectangle
    {
        public Vector4 AABB { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }

        public BufferObject[] Vertices; 

        public Rectangle(int posX, int posY, int sizeX, int sizeY)
        {
            //glLoadIdentity();                 // Reset the model-view matrix
            //glTranslatef(1.5f, 0.0f, -7.0f);  // Move right and into the screen

            //glBegin(GL_QUADS);                // Begin drawing the color cube with 6 quads
            //                                  // Top face (y = 1.0f)
            //                                  // Define vertices in counter-clockwise (CCW) order with normal pointing out
            //glColor3f(0.0f, 1.0f, 0.0f);     // Green
            //glVertex3f(1.0f, 1.0f, -1.0f);
            //glVertex3f(-1.0f, 1.0f, -1.0f);
            //glVertex3f(-1.0f, 1.0f, 1.0f);
            //glVertex3f(1.0f, 1.0f, 1.0f);

            //// Bottom face (y = -1.0f)
            //glColor3f(1.0f, 0.5f, 0.0f);     // Orange
            //glVertex3f(1.0f, -1.0f, 1.0f);
            //glVertex3f(-1.0f, -1.0f, 1.0f);
            //glVertex3f(-1.0f, -1.0f, -1.0f);
            //glVertex3f(1.0f, -1.0f, -1.0f);

            //// Front face  (z = 1.0f)
            //glColor3f(1.0f, 0.0f, 0.0f);     // Red
            //glVertex3f(1.0f, 1.0f, 1.0f);
            //glVertex3f(-1.0f, 1.0f, 1.0f);
            //glVertex3f(-1.0f, -1.0f, 1.0f);
            //glVertex3f(1.0f, -1.0f, 1.0f);

            //// Back face (z = -1.0f)
            //glColor3f(1.0f, 1.0f, 0.0f);     // Yellow
            //glVertex3f(1.0f, -1.0f, -1.0f);
            //glVertex3f(-1.0f, -1.0f, -1.0f);
            //glVertex3f(-1.0f, 1.0f, -1.0f);
            //glVertex3f(1.0f, 1.0f, -1.0f);

            //// Left face (x = -1.0f)
            //glColor3f(0.0f, 0.0f, 1.0f);     // Blue
            //glVertex3f(-1.0f, 1.0f, 1.0f);
            //glVertex3f(-1.0f, 1.0f, -1.0f);
            //glVertex3f(-1.0f, -1.0f, -1.0f);
            //glVertex3f(-1.0f, -1.0f, 1.0f);

            //// Right face (x = 1.0f)
            //glColor3f(1.0f, 0.0f, 1.0f);     // Magenta
            //glVertex3f(1.0f, 1.0f, -1.0f);
            //glVertex3f(1.0f, 1.0f, 1.0f);
            //glVertex3f(1.0f, -1.0f, 1.0f);
            //glVertex3f(1.0f, -1.0f, -1.0f);
            //glEnd();
        }

    }
}
