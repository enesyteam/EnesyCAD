using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Internal;
using Autodesk.AutoCAD.MNUParser;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Resources;
using System.Windows.Forms;
using Autodesk.AutoCAD.Customization;

namespace Autodesk.AutoCAD.CustomizationEx
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.collapsiblePanel1 = new Autodesk.AutoCAD.Customization.CollapsiblePanel();
            this.categorySelector1 = new Autodesk.AutoCAD.Customization.CategorySelector();
            this.keyTextBox1 = new Autodesk.AutoCAD.Customization.KeyTextBox();
            this.listViewEx1 = new Autodesk.AutoCAD.Customization.ListViewEx();
            this.listViewResize1 = new Autodesk.AutoCAD.Customization.ListViewResize();
            this.listViewSort1 = new Autodesk.AutoCAD.Customization.ListViewSort();
            this.collapsiblePanel2 = new Autodesk.AutoCAD.Customization.CollapsiblePanel();
            this.SuspendLayout();
            // 
            // collapsiblePanel1
            // 
            this.collapsiblePanel1.AccessibleDescription = "sdfsdfsdfsdfsd";
            this.collapsiblePanel1.AccessibleName = "sdfsdgfer";
            this.collapsiblePanel1.AllowDrop = true;
            this.collapsiblePanel1.Collapsible = true;
            this.collapsiblePanel1.EnableExpanded = true;
            this.collapsiblePanel1.Expanded = true;
            this.collapsiblePanel1.HeaderText = "Testing";
            this.collapsiblePanel1.Location = new System.Drawing.Point(3, 1);
            this.collapsiblePanel1.MinimumHeight = 0;
            this.collapsiblePanel1.Name = "collapsiblePanel1";
            this.collapsiblePanel1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.collapsiblePanel1.Size = new System.Drawing.Size(204, 239);
            this.collapsiblePanel1.TabIndex = 0;
            this.collapsiblePanel1.Collapsed += new Autodesk.AutoCAD.Customization.CollapsiblePanel.ExpandedEventHandler(this.collapsiblePanel1_Collapsed);
            this.collapsiblePanel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.collapsiblePanel1_MouseDown);
            // 
            // categorySelector1
            // 
            this.categorySelector1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.categorySelector1.FormattingEnabled = true;
            this.categorySelector1.Location = new System.Drawing.Point(237, 26);
            this.categorySelector1.Name = "categorySelector1";
            this.categorySelector1.Size = new System.Drawing.Size(121, 23);
            this.categorySelector1.TabIndex = 1;
            // 
            // keyTextBox1
            // 
            this.keyTextBox1.BackColor = System.Drawing.SystemColors.Window;
            this.keyTextBox1.Location = new System.Drawing.Point(237, 70);
            this.keyTextBox1.Name = "keyTextBox1";
            this.keyTextBox1.ReadOnly = true;
            this.keyTextBox1.Size = new System.Drawing.Size(100, 22);
            this.keyTextBox1.TabIndex = 2;
            // 
            // listViewEx1
            // 
            this.listViewEx1.DoubleClickActivation = false;
            this.listViewEx1.FullRowSelect = true;
            this.listViewEx1.Location = new System.Drawing.Point(237, 109);
            this.listViewEx1.Name = "listViewEx1";
            this.listViewEx1.Size = new System.Drawing.Size(121, 97);
            this.listViewEx1.TabIndex = 3;
            this.listViewEx1.UseCompatibleStateImageBehavior = false;
            this.listViewEx1.View = System.Windows.Forms.View.Details;
            // 
            // listViewResize1
            // 
            this.listViewResize1.Location = new System.Drawing.Point(380, 153);
            this.listViewResize1.Name = "listViewResize1";
            this.listViewResize1.Size = new System.Drawing.Size(121, 97);
            this.listViewResize1.TabIndex = 4;
            this.listViewResize1.UseCompatibleStateImageBehavior = false;
            // 
            // listViewSort1
            // 
            this.listViewSort1.Location = new System.Drawing.Point(316, 274);
            this.listViewSort1.Name = "listViewSort1";
            this.listViewSort1.Size = new System.Drawing.Size(121, 97);
            this.listViewSort1.TabIndex = 5;
            this.listViewSort1.UseCompatibleStateImageBehavior = false;
            // 
            // collapsiblePanel2
            // 
            this.collapsiblePanel2.Collapsible = true;
            this.collapsiblePanel2.EnableExpanded = true;
            this.collapsiblePanel2.Expanded = true;
            this.collapsiblePanel2.HeaderText = null;
            this.collapsiblePanel2.Location = new System.Drawing.Point(3, 247);
            this.collapsiblePanel2.MinimumHeight = 0;
            this.collapsiblePanel2.Name = "collapsiblePanel2";
            this.collapsiblePanel2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.collapsiblePanel2.Size = new System.Drawing.Size(204, 193);
            this.collapsiblePanel2.TabIndex = 6;
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(538, 437);
            this.Controls.Add(this.collapsiblePanel2);
            this.Controls.Add(this.listViewSort1);
            this.Controls.Add(this.listViewResize1);
            this.Controls.Add(this.listViewEx1);
            this.Controls.Add(this.keyTextBox1);
            this.Controls.Add(this.categorySelector1);
            this.Controls.Add(this.collapsiblePanel1);
            this.Name = "MainForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CollapsiblePanel collapsiblePanel1;
        private CategorySelector categorySelector1;
        private KeyTextBox keyTextBox1;
        private ListViewEx listViewEx1;
        private ListViewResize listViewResize1;
        private ListViewSort listViewSort1;
        private CollapsiblePanel collapsiblePanel2;
    }
}