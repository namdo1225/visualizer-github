﻿using Microsoft.ApplicationInsights;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GitVisualizer.UI.UI_Forms
{
    public partial class BranchesControl : UserControl
    {
        private const int pixelsPerBranchNode = 32;
        private const int pixelsPerBranchRow = 42;
        private const int branchNodeRadius = 12;
        private List<Color> branchNodeColors =
            [Color.PowderBlue,
            Color.LightGreen,
            Color.DarkSalmon,
            Color.Gold,
            Color.Plum];

        private class TestTree
        {
            public class TestNode
            {
                public TestNode Parent;
                public List<TestNode> Children = new();
                public string Branch;
                public string ID;
                public string User;
                public string Date;
                public string Comment;
            }
            public TestNode Root;

        }

        public BranchesControl()
        {
            InitializeComponent();
            ApplyColorTheme(MainForm.AppTheme);
        }

        /// <summary>
        /// Draws little circle nodes representing branches in the branch cell view on any cell draw event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BranchesGridViewDrawCell(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // Ignore header row and any columns besides Graph column
            if (e.ColumnIndex > 0 || e.RowIndex == -1) { return; }

            // Get Branch index for offset
            int branchIndex = 1;
            // get node area in graph
            int xOffset = branchIndex * pixelsPerBranchNode;

            // Get branch color from index
            Pen pen = new Pen(branchNodeColors[branchIndex], 3);
            // Set position based on cell bounds using branch offset and radius
            int x = e.CellBounds.X + (xOffset / 2);
            int y = e.CellBounds.Y + ((e.CellBounds.Height / 2) - (branchNodeRadius / 2));
            // Paint background first, how the cell was going to do it anyways
            e.PaintBackground(e.CellBounds, true);
            // Save smoothing mode before changing, then change to antialias for smooth node drawing
            SmoothingMode prevSmoothing = e.Graphics.SmoothingMode;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.DrawEllipse(pen, x, y, branchNodeRadius, branchNodeRadius);
            e.Graphics.SmoothingMode = prevSmoothing;
            // Handle event and return to continue drawing
            e.Handled = true;
        }
    }
}
