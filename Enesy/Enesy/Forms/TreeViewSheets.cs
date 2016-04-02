using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Enesy.Forms
{
    public partial class TreeViewSheets : System.Windows.Forms.TreeView
    {
        #region Properties and Fields
        /// <summary>
        /// Node being dragged
        /// </summary>
        private TreeNode dragNode = null;

        /// <summary>
        /// Temporary drop node for selection
        /// </summary>
        private TreeNode tempDropNode = null;

        /// <summary>
        /// Timer for scrolling
        /// Loop time (interval = 200 milisecond)
        /// </summary>
        private Timer timer = new Timer();

        /// <summary>
        /// List of icon of node (SubSet, RoofSheetsSet, Sheet), where:
        /// 0 - Icon of Sheet node
        /// 1 - Icon of Subset
        /// 2 - Icon of Sheets Set
        /// </summary>
        private ImageList imageListTreeView = new ImageList();
        public ImageList NodeIcon
        {
            set
            {
                this.imageListTreeView = value;
            }
        }

        /// <summary>
        /// List of image contains temporary bitmap followed cursor when dragging
        /// </summary>
        private ImageList imageListDrag = new ImageList();

        /// <summary>
        /// Name of roof node (sheets set name)
        /// </summary>
        public string RoofNodeName
        {
            set
            {
                this.Nodes.Add(value);
            }
        }
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public TreeViewSheets()
        {
            InitializeComponent();
            this.Init();
        }

        public TreeViewSheets(string roofNodeName)
        {
            InitializeComponent();
            this.Nodes.Add(roofNodeName);
            this.Init();
        }

        /// <summary>
        /// Initialize properties, event,.... for constructor
        /// </summary>
        private void Init()
        {
            timer.Interval = 200;
            timer.Tick += new EventHandler(timer_Tick);
        }
        
        /// <summary>
        /// When item dragged:
        /// - Create bitmap of node icon & label
        /// - Move above bitmap following cursor
        /// - Drawing a line above or below temporary selected target node
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnItemDrag(ItemDragEventArgs e)
        {
            base.OnItemDrag(e);

            // Get drag node and select it
            this.dragNode = e.Item as TreeNode;
            this.SelectedNode = this.dragNode;

            // Reset image list used for drag image
            imageListDrag.Images.Clear();
            this.imageListDrag.ImageSize = new Size(
                this.dragNode.Bounds.Size.Width + this.Indent,
                this.dragNode.Bounds.Height
            );

            // Create new bitmap
            // This bitmap will contain the tree node image to be dragged
            Bitmap bmp = new Bitmap(
                this.dragNode.Bounds.Width + this.Indent, 
                this.dragNode.Bounds.Height
            );

            // Get graphics from bitmap
            Graphics gfx = Graphics.FromImage(bmp);

            // Draw node icon into the bitmap
            gfx.DrawImage(this.imageListTreeView.Images[0], 0, 0);

            // Draw node label into bitmap
            gfx.DrawString(
                this.dragNode.Text,
                this.Font,
                new SolidBrush(this.ForeColor),
                (float)this.Indent, 1.0f
            );

            // Add bitmap to imagelist
            this.imageListDrag.Images.Add(bmp);

            // Get mouse position in client coordinates
            Point p = this.PointToClient(Control.MousePosition);

            // Compute delta between mouse position and node bounds
            int dx = p.X + this.Indent - this.dragNode.Bounds.Left;
            int dy = p.Y - this.dragNode.Bounds.Top;

            // Begin dragging image
            if (DragHelper.ImageList_BeginDrag(this.imageListDrag.Handle, 0, dx, dy))
            {
                // Begin dragging
                this.Parent.DoDragDrop(bmp, DragDropEffects.Move);
                // End dragging image
                DragHelper.ImageList_EndDrag();
            }
        }

        protected override void OnDragOver(DragEventArgs drgevent)
        {
            base.OnDragOver(drgevent);

            // Compute drag position and move image
            Point formP = this.PointToClient(new Point(drgevent.X, drgevent.Y));
            DragHelper.ImageList_DragMove(formP.X - this.Left, formP.Y - this.Top);

            // Get actual drop node
            TreeNode dropNode = this.GetNodeAt(this.PointToClient(new Point(drgevent.X, drgevent.Y)));
            if (dropNode == null)
            {
                drgevent.Effect = DragDropEffects.None;
                return;
            }

            drgevent.Effect = DragDropEffects.Move;

            // if mouse is on a new node select it
            if (this.tempDropNode != dropNode)
            {
                DragHelper.ImageList_DragShowNolock(false);
                this.SelectedNode = dropNode;
                DragHelper.ImageList_DragShowNolock(true);
                tempDropNode = dropNode;
            }

            // Avoid that drop node is child of drag node 
            TreeNode tmpNode = dropNode;
            while (tmpNode.Parent != null)
            {
                if (tmpNode.Parent == this.dragNode) drgevent.Effect = DragDropEffects.None;
                tmpNode = tmpNode.Parent;
            }
        }

        protected override void OnDragDrop(DragEventArgs drgevent)
        {
            base.OnDragDrop(drgevent);

            // Unlock updates
            DragHelper.ImageList_DragLeave(this.Handle);

            // Get drop node
            TreeNode dropNode = this.GetNodeAt(
                this.PointToClient(new Point(drgevent.X, drgevent.Y)));

            // If drop node isn't equal to drag node, add drag node as child of drop node
            if (this.dragNode != dropNode)
            {
                // Remove drag node from parent
                if (this.dragNode.Parent == null)
                {
                    this.Nodes.Remove(this.dragNode);
                }
                else
                {
                    this.dragNode.Parent.Nodes.Remove(this.dragNode);
                }

                // Add drag node to drop node
                if (dropNode.Parent != null)
                {
                    TreeNode pr = dropNode.Parent;
                    pr.Nodes.Add(this.dragNode);
                    pr.ExpandAll();
                }
                //dropNode.Nodes.Add(this.dragNode);
                //dropNode.ExpandAll();

                // Set drag node to null
                this.dragNode = null;

                // Disable scroll timer
                this.timer.Enabled = false;
            }
        }

        protected override void OnDragEnter(DragEventArgs drgevent)
        {
            base.OnDragEnter(drgevent);

            DragHelper.ImageList_DragEnter(
                this.Handle, drgevent.X - this.Left,
                drgevent.Y - this.Top
            );

            // Enable timer for scrolling dragged item
            this.timer.Enabled = true;
        }

        protected override void OnDragLeave(EventArgs e)
        {
            base.OnDragLeave(e);

            DragHelper.ImageList_DragLeave(this.Handle);

            // Disable timer for scrolling dragged item
            this.timer.Enabled = false;
        }

        protected override void OnGiveFeedback(GiveFeedbackEventArgs gfbevent)
        {
            base.OnGiveFeedback(gfbevent);

            if (gfbevent.Effect == DragDropEffects.Move)
            {
                // Show pointer cursor while dragging
                gfbevent.UseDefaultCursors = false;
                this.Cursor = Cursors.Default;
            }
            else gfbevent.UseDefaultCursors = true;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            // Get node at mouse position
            Point pt = PointToClient(Control.MousePosition);
            TreeNode node = this.GetNodeAt(pt);

            if (node == null) return;

            // If mouse is near to the top, scroll up
            if (pt.Y < 30)
            {
                // Set actual node to the upper one
                if (node.PrevVisibleNode != null)
                {
                    node = node.PrevVisibleNode;

                    // Hide drag image
                    DragHelper.ImageList_DragShowNolock(false);

                    // Scroll and refresh
                    node.EnsureVisible();
                    this.Refresh();

                    // Show drag image
                    DragHelper.ImageList_DragShowNolock(true);

                }
            }

            // If mouse is near to the bottom, scroll down
            else if (pt.Y > this.Size.Height - 30)
            {
                if (node.NextVisibleNode != null)
                {
                    node = node.NextVisibleNode;

                    DragHelper.ImageList_DragShowNolock(false);
                    node.EnsureVisible();
                    this.Refresh();
                    DragHelper.ImageList_DragShowNolock(true);
                }
            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            // TODO: Add custom paint code here

            // Calling the base class OnPaint
            base.OnPaint(pe);
        }
    }
}
