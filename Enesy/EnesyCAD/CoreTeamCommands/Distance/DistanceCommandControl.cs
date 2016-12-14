using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Enesy.EnesyCAD.CoreTeamCommands.Distance
{
    public partial class DistanceCommandControl : UserControl
    {
        public DistanceCommand DataContext = null;
        public DistanceCommandControl()
        {
            InitializeComponent();
            PopulateControls();
        }
        public void PopulateControls()
        {
            if (DataContext != null)
            {
                this.txtBasePoint.DataBindings.Add(new Binding("Text", DataContext, "BasePoint"));
                this.txtBasePoint.TextChanged += txtBasePoint_TextChanged;
                this.txtBaseValue.DataBindings.Add(new Binding("Text", DataContext, "BaseValue"));
                this.txtBaseValue.TextChanged += txtBaseValue_TextChanged;
                this.txtInputScale.DataBindings.Add(new Binding("Text", DataContext, "InputScale"));
                this.txtInputScale.TextChanged += txtInputScale_TextChanged;
                this.txtOuputScale.DataBindings.Add(new Binding("Text", DataContext, "OutputScale"));
                this.txtOuputScale.TextChanged += txtOuputScale_TextChanged;
                this.txtPrefix.DataBindings.Add(new Binding("Text", DataContext, "Prefix"));
                this.txtPrefix.TextChanged += txtPrefix_TextChanged;
                this.txtSurfix.DataBindings.Add(new Binding("Text", DataContext, "Surfix"));
                this.txtSurfix.TextChanged += txtSurfix_TextChanged;

                //// Distance Types
                this.cmbDistanceType.DisplayMember = "Description";
                this.cmbDistanceType.ValueMember = "value";
                this.cmbDistanceType.DataSource = Enum.GetValues(typeof(DistanceType));
                this.cmbDistanceType.DataBindings.Add(new Binding("SelectedItem", DataContext, "DistanceType", true, DataSourceUpdateMode.OnPropertyChanged));
                this.cmbDistanceType.SelectedIndexChanged += cmbDistanceType_SelectedIndexChanged;
            }
            
        }

        void txtSurfix_TextChanged(object sender, EventArgs e)
        {
            ((TextBox)sender).DataBindings["Text"].WriteValue();
        }

        void txtPrefix_TextChanged(object sender, EventArgs e)
        {
            ((TextBox)sender).DataBindings["Text"].WriteValue();
        }

        void txtOuputScale_TextChanged(object sender, EventArgs e)
        {
            ((TextBox)sender).DataBindings["Text"].WriteValue();
        }

        void txtInputScale_TextChanged(object sender, EventArgs e)
        {
            ((TextBox)sender).DataBindings["Text"].WriteValue();
        }

        void txtBaseValue_TextChanged(object sender, EventArgs e)
        {
            ((TextBox)sender).DataBindings["Text"].WriteValue();
        }

        void txtBasePoint_TextChanged(object sender, EventArgs e)
        {
            ((TextBox)sender).DataBindings["Text"].WriteValue();
        }

        private void cmbDistanceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ((ComboBox)sender).DataBindings["SelectedItem"].WriteValue();

        }
    }
}
