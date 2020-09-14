﻿using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace HonStatsManager.Interface
{
    // mostly from the answers at https://stackoverflow.com/questions/10803184/windows-forms-button-with-drop-down-menu

    public class MenuButton : Button
    {
        [DefaultValue(null)]
        public ContextMenuStrip Menu { get; set; }

        [DefaultValue(false)]
        public bool ShowMenuUnderCursor { get; set; }

        protected override void OnMouseDown(MouseEventArgs mouseEvent)
        {
            base.OnMouseDown(mouseEvent);

            if (Menu == null || mouseEvent.Button != MouseButtons.Left)
                return;

            var menuLocation = ShowMenuUnderCursor ? mouseEvent.Location : new Point(0, Height);
            Menu.Show(this, menuLocation);
        }

        protected override void OnPaint(PaintEventArgs paintEvent)
        {
            base.OnPaint(paintEvent);

            if (Menu == null)
                return;

            var arrowX = ClientRectangle.Width - 8;
            var arrowY = ClientRectangle.Height - 8;

            var brush = Enabled ? SystemBrushes.ControlText : SystemBrushes.ControlDark;
            var arrows = new[]
            {
                new Point(arrowX - 3, arrowY - 1),
                new Point(arrowX + 4, arrowY - 1),
                new Point(arrowX, arrowY + 3)
            };
            paintEvent.Graphics.FillPolygon(brush, arrows);
        }
    }
}