using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuizApp_1._0
{
    class Move
    {
        private Form ParentFrom;
        private Form Child;
        private bool isDraged;
        private Point Offset;
        private Control ControlToDragedd;
        private Point currentLocation;



        public Move(Form formName)
        {
            this.ParentFrom = formName;
            ControlToDragedd = formName;

        }

        public Move(Form form, Control ControlToDragedd)
        {
            this.ParentFrom = form;
            this.ControlToDragedd = ControlToDragedd;
        }
        public Move(Form Parent, Form Child, Control ControlToDragedd)
        {
            this.ControlToDragedd = ControlToDragedd;
            this.ParentFrom = Parent;
            this.Child = Child;
        }




        public void MakeFromDraggableViaControl()
        {
            ControlToDragedd.MouseDown += MouseDownEvent;
            ControlToDragedd.MouseMove += MouseMoveEventForOtherControl;

            ControlToDragedd.MouseUp += MouseUpEvent;
        }
        public void MakeFromDraggableViaControlOr()
        {
            ControlToDragedd.MouseDown += MouseDownEvent;
            ControlToDragedd.MouseMove += MouseMoveEventForOtherControlOr;

            ControlToDragedd.MouseUp += MouseUpEvent;
        }

        /* public void MakeFromDraggablwWithParent(Form Parent, Form Child)
         {
             ParentFrom = Parent;
             this.Child = Child;
             int X = Parent.Location.X;
             int Y = Parent.Location.Y;
             int Height = Parent.Height;
             Child.Location = new Point(X, Y + Height);


         }*/
        private void MouseDownEvent(Object sender, MouseEventArgs e)
        {
            Offset = new Point(e.X, e.Y); //recode the mouse position
            isDraged = true;


        }

        private void MouseMoveEventForOtherControlOr(Object sender, MouseEventArgs e)
        {
            if (isDraged)
            {
                currentLocation = ControlToDragedd.PointToScreen(e.Location);
                ParentFrom.Location = new Point(currentLocation.X - Offset.X, currentLocation.Y - Offset.Y);



            }
        }
        public void MouseUpEvent(Object Sender, MouseEventArgs e)
        {
            isDraged = false;
        }
        private void MouseMoveEventForOtherControl(Object sender, MouseEventArgs e)
        {
            if (isDraged)
            {
                currentLocation = ControlToDragedd.PointToScreen(e.Location);
                ParentFrom.Location = new Point(currentLocation.X - Offset.X, currentLocation.Y - Offset.Y);
                Child.Location = new Point(ParentFrom.Location.X, ParentFrom.Location.Y + ParentFrom.Height);


            }
        }
    }
}
