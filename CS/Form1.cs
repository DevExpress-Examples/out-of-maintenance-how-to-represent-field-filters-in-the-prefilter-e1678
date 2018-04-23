using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.Data.Filtering;
using DevExpress.XtraPivotGrid;

namespace S132472 {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            // TODO: This line of code loads data into the 'nwindDataSet.ProductReports' table. You can move, or remove it, as needed.
            this.productReportsTableAdapter.Fill(this.nwindDataSet.ProductReports);

        }

        private void pivotGridControl1_FieldFilterChanged(object sender, DevExpress.XtraPivotGrid.PivotFieldEventArgs e) {
            PivotGridControl pivot = sender as PivotGridControl;
            CriteriaOperator prefilter = pivot.Prefilter.Criteria as CriteriaOperator;
            GroupOperator rootGroup = prefilter as GroupOperator;
            if(object.ReferenceEquals(rootGroup, null)) {
                rootGroup = new GroupOperator(GroupOperatorType.And);
                if(!object.ReferenceEquals(prefilter, null))
                    rootGroup.Operands.Add(prefilter);
            }
            if(rootGroup.OperatorType == GroupOperatorType.Or)
                rootGroup = new GroupOperator(GroupOperatorType.And, rootGroup);
            InOperator op = rootGroup.Operands.Find(delegate(CriteriaOperator item) {
                InOperator binOp = item as InOperator;
                if(object.ReferenceEquals(binOp, null)) return false;
                OperandProperty prop = binOp.LeftOperand as OperandProperty;
                if(object.ReferenceEquals(prop, null)) return false;
                return prop.PropertyName == e.Field.Name;
            }) as InOperator;
            if(e.Field.FilterValues.IsEmpty) {
                if(!object.ReferenceEquals(op, null))
                    rootGroup.Operands.Remove(op);
            } else {
                if(object.ReferenceEquals(op, null)) {
                    op = new InOperator(new OperandProperty(e.Field.Name));
                    rootGroup.Operands.Add(op);
                }
                op.Operands.Clear();
                foreach(object val in e.Field.FilterValues.ValuesIncluded)
                    op.Operands.Add(new OperandValue(val));
            }
            if(rootGroup.Operands.Count > 0)
                pivot.Prefilter.Criteria = rootGroup;
            else
                pivot.Prefilter.Criteria = null;
        }
    }
}