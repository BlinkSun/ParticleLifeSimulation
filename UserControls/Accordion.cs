using ParticleLifeSimulation.Core;
using ParticleLifeSimulation.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleLifeSimulation.UserControls
{
    public class Accordion : FlowLayoutPanel
    {
        public Action<Graphics, int, Color, float, float, float, float>? ParticleDraw = null;
        public CheckBox ChkTitleBar;
        public bool IsDraggable { get; set; } = false;
        public Atom? Atom { get; set; } = null;
        public string Title
        {
            get => ChkTitleBar.Text;
            set => ChkTitleBar.Text = value;
        }

        public Accordion()
        {
            WrapContents = false;
            BorderStyle = BorderStyle.FixedSingle;
            FlowDirection = FlowDirection.TopDown;
            BackColor = SystemColors.Control;
            ControlAdded += new((s, cev) => ResizeAccordionControls());
            ControlRemoved += new((s, cev) => ResizeAccordionControls());
            ClientSizeChanged += new((s, ev) => ResizeAccordionControls());

            ChkTitleBar = new()
            {
                Appearance = Appearance.Button,
                FlatStyle = FlatStyle.Flat,
                TextAlign = ContentAlignment.MiddleLeft,
                TextImageRelation = TextImageRelation.ImageBeforeText,
                ImageAlign = ContentAlignment.MiddleLeft,
                UseVisualStyleBackColor = true,
                Tag = "TITLEBAR",
                Margin = new(0),
            };
            ChkTitleBar.FlatAppearance.BorderSize = 0;
            ChkTitleBar.MouseDown += new((s, mev) =>
            {
                if (IsDraggable)
                {
                    RectangleF rectangleF = new(ChkTitleBar.ClientSize.Width - ChkTitleBar.ClientSize.Height, 0, ChkTitleBar.ClientSize.Height, ChkTitleBar.ClientSize.Height);
                    if (rectangleF.Contains(mev.Location)) DoDragDrop(this, DragDropEffects.Move);
                }
            });
            ChkTitleBar.Paint += new((s, pev) =>
            {
                if (Atom is not null && ParticleDraw is not null)
                {
                    Bitmap bitmap = new(ChkTitleBar.ClientSize.Height, ChkTitleBar.ClientSize.Height);
                    using (Graphics graphics = Graphics.FromImage(bitmap))
                    {
                        graphics.Clear(Color.Transparent);
                        float x = bitmap.Width * 0.25f;
                        float y = bitmap.Height * 0.25f;
                        float width = bitmap.Width / 2.0f;
                        float height = bitmap.Height / 2.0f;
                        ParticleDraw(graphics, 255, Atom.Color, x, y, width, height);
                    }
                    pev.Graphics.DrawImage(bitmap, ChkTitleBar.ClientSize.Width - ChkTitleBar.ClientSize.Height, 0, ChkTitleBar.ClientSize.Height, ChkTitleBar.ClientSize.Height);
                }
            });
            ChkTitleBar.CheckedChanged += new((s, ev) => ResizeAccordionControls());

            Controls.Add(ChkTitleBar);
        }

        public void ResizeAccordionControls()
        {
            Size iconSize = new(ChkTitleBar.ClientSize.Height / 4 * 3, ChkTitleBar.ClientSize.Height / 4 * 3);
            IEnumerable<Control> resetChildIndexes = Enumerable.Empty<Control>();

            //width
            foreach (Control control in Controls)
                control.Width = ClientSize.Width - control.Margin.Right - control.Margin.Left;

            //height
            if (ChkTitleBar.Checked)
            {
                Bitmap bitmap = new(Resources.arrow_bottom, iconSize);
                ChkTitleBar.Image = bitmap;
                int height = Margin.Top;
                foreach (Control control in Controls)
                {
                    height += control.Height + control.Margin.Top + control.Margin.Bottom;
                    if (control.Tag is string) resetChildIndexes = resetChildIndexes.Append(control);
                }
                Height = height;
            }
            else
            {
                Bitmap bitmap = new(Resources.arrow_right, iconSize);
                ChkTitleBar.Image = bitmap;
                Height = ChkTitleBar.ClientSize.Height + 2;
            }

            //position
            foreach (Control control in resetChildIndexes)
            {
                switch ((string)control.Tag)
                {
                    case "TITLEBAR":
                        Controls.SetChildIndex(control, 0);
                        break;
                    case "LAST":
                        Controls.SetChildIndex(control, Controls.Count - 1);
                        break;
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ChkTitleBar.Dispose();
                ParticleDraw = null;
                Atom = null;
            }

            base.Dispose(disposing);
        }
    }
}
