Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.Data.Filtering
Imports DevExpress.XtraPivotGrid

Namespace S132472
	Partial Public Class Form1
		Inherits Form

		Public Sub New()
			InitializeComponent()
		End Sub

		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
			' TODO: This line of code loads data into the 'nwindDataSet.ProductReports' table. You can move, or remove it, as needed.
			Me.productReportsTableAdapter.Fill(Me.nwindDataSet.ProductReports)

		End Sub

		Private Sub pivotGridControl1_FieldFilterChanged(ByVal sender As Object, ByVal e As DevExpress.XtraPivotGrid.PivotFieldEventArgs) Handles pivotGridControl1.FieldFilterChanged
			Dim pivot As PivotGridControl = TryCast(sender, PivotGridControl)
			Dim prefilter As CriteriaOperator = TryCast(pivot.Prefilter.Criteria, CriteriaOperator)
			Dim rootGroup As GroupOperator = TryCast(prefilter, GroupOperator)
			If Object.ReferenceEquals(rootGroup, Nothing) Then
				rootGroup = New GroupOperator(GroupOperatorType.And)
				If Not Object.ReferenceEquals(prefilter, Nothing) Then
					rootGroup.Operands.Add(prefilter)
				End If
			End If
			If rootGroup.OperatorType = GroupOperatorType.Or Then
				rootGroup = New GroupOperator(GroupOperatorType.And, rootGroup)
			End If
            Dim op As InOperator = CType(rootGroup.Operands.Find(Function(item As CriteriaOperator)
                                                                     Dim binOp As InOperator = TryCast(item, InOperator)
                                                                     If Object.ReferenceEquals(binOp, Nothing) Then
                                                                         Return False
                                                                     End If
                                                                     Dim prop As OperandProperty = TryCast(binOp.LeftOperand, OperandProperty)
                                                                     If Object.ReferenceEquals(prop, Nothing) Then
                                                                         Return False
                                                                     End If
                                                                     Return prop.PropertyName = e.Field.Name
                                                                 End Function), InOperator)
            If e.Field.FilterValues.IsEmpty Then
				If Not Object.ReferenceEquals(op, Nothing) Then
					rootGroup.Operands.Remove(op)
				End If
			Else
				If Object.ReferenceEquals(op, Nothing) Then
					op = New InOperator(New OperandProperty(e.Field.Name))
					rootGroup.Operands.Add(op)
				End If
				op.Operands.Clear()
				For Each val As Object In e.Field.FilterValues.ValuesIncluded
					op.Operands.Add(New OperandValue(val))
				Next val
			End If
			If rootGroup.Operands.Count > 0 Then
				pivot.Prefilter.Criteria = rootGroup
			Else
				pivot.Prefilter.Criteria = Nothing
			End If
		End Sub
	End Class
End Namespace