using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinModal
{
    public class WinModal
    {
        private static Form Parent;
        private static Form Modal;
        private static Form Overlay;

        private static void ApplyOptions(Form modal, Form overlay, WinModalOptions options)
        {
            if (options == null)
                return;

            if (!options.Overlay)
            {
                overlay.Opacity = 0;
                overlay.BackColor = Color.Transparent;
            }

            if (options.Transition)
            {
                modal.Opacity = 0;
                var t = new Timer { Interval = 10 };
                t.Tick += (s, e) =>
                {
                    modal.Opacity += 0.05;
                    if (modal.Opacity >= 1)
                        t.Stop();
                };
                t.Start();
            }

            if (options.Draggable)
            {
                bool dragging = false;
                Point dragCursor = Point.Empty;
                Point dragForm = Point.Empty;

                modal.MouseDown += (s, e) =>
                {
                    dragging = true;
                    dragCursor = Cursor.Position;
                    dragForm = modal.Location;
                };

                modal.MouseMove += (s, e) =>
                {
                    if (dragging)
                    {
                        var diff = Point.Subtract(Cursor.Position, new Size(dragCursor));
                        modal.Location = Point.Add(dragForm, new Size(diff));
                    }
                };

                modal.MouseUp += (s, e) => dragging = false;
            }
        }


        public static DialogResult Show(Form parent, Form modal, WinModalOptions options = null)
        {
            if (parent == null || modal == null)
                throw new ArgumentNullException("Parent and modal cannot be null.");

            Parent = parent;
            Modal = modal;


            Overlay = new Form
            {
                FormBorderStyle = FormBorderStyle.None,
                ShowInTaskbar = false,
                StartPosition = FormStartPosition.Manual,
                BackColor = Color.Black,
                Opacity = 0.55,
                Location = parent.PointToScreen(Point.Empty),
                Size = parent.ClientSize,
                Owner = parent,
            };

            EventHandler moveHandler = (s, e) =>
            {
                if (!Overlay.IsDisposed)
                    Overlay.Location = parent.PointToScreen(Point.Empty);
            };
            EventHandler sizeHandler = (s, e) =>
            {
                if (!Overlay.IsDisposed)
                    Overlay.Size = parent.ClientSize;
            };

            parent.LocationChanged += moveHandler;
            parent.SizeChanged += sizeHandler;

            modal.ShowInTaskbar = false;
            modal.FormBorderStyle = FormBorderStyle.None;
            modal.StartPosition = FormStartPosition.CenterParent;
            modal.Owner = Overlay;

            ApplyOptions(modal, Overlay, options);

            DialogResult result = DialogResult.None;

            try
            {
                Overlay.Show();
                result = modal.ShowDialog(Overlay);
            }
            finally
            {
                parent.LocationChanged -= moveHandler;
                parent.SizeChanged -= sizeHandler;

                Overlay.Dispose();
                Overlay = null;
                Modal = null;
                Parent = null;
            }

            return result;
        }
    }
}
