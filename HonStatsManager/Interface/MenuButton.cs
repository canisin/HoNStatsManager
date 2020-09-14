using System.Drawing;
using System.Windows.Forms;

namespace HonStatsManager.Interface
{
    // mostly from the answers at https://stackoverflow.com/questions/10803184/windows-forms-button-with-drop-down-menu

    public class MenuButton : Button
    {
        public ContextMenuStrip Menu { get; set; }
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

            var arrowX = ClientRectangle.Width - 14;
            var arrowY = ClientRectangle.Height / 2 - 1;

            var brush = Enabled ? SystemBrushes.ControlText : SystemBrushes.ControlDark;
            var arrows = new[]
            {
                new Point(arrowX, arrowY),
                new Point(arrowX + 7, arrowY),
                new Point(arrowX + 3, arrowY + 4)
            };
            paintEvent.Graphics.FillPolygon(brush, arrows);
        }
    }
}