Option Infer On

Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports DevExpress.XtraPivotGrid
Imports DevExpress.Web.ASPxPivotGrid

Partial Public Class _Default
	Inherits System.Web.UI.Page

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
	End Sub


	Protected Sub ASPxPivotGrid1_CustomSummary(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxPivotGrid.PivotGridCustomSummaryEventArgs)
		Dim pivot As ASPxPivotGrid = DirectCast(sender, ASPxPivotGrid)
		If e.RowField IsNot Nothing AndAlso e.RowField.AreaIndex >= fieldCompanyName.AreaIndex Then
			e.CustomValue = e.SummaryValue.Min
		Else
			Dim groupingFields = pivot.Fields.Cast(Of PivotGridField)().Where(Function(f) f.Visible AndAlso f.Area = fieldCompanyName.Area AndAlso f.AreaIndex <= fieldCompanyName.AreaIndex AndAlso (e.RowField Is Nothing OrElse f.AreaIndex > e.RowField.AreaIndex))
			Dim drillDownDataSource = e.CreateDrillDownDataSource().Cast(Of PivotDrillDownDataRow)()
			Dim temporaryList As New List(Of IEnumerable(Of PivotDrillDownDataRow))()
			temporaryList.Add(drillDownDataSource)
			Dim dataGroups As IEnumerable(Of IEnumerable(Of PivotDrillDownDataRow)) = temporaryList
			For Each field In groupingFields
				Dim localField = field
				dataGroups = dataGroups.SelectMany(Function(groupRows) groupRows.GroupBy(Function(r) r(localField)).Select(Function(g) g.AsEnumerable()))
			Next field
			Dim value = dataGroups.Sum(Function(groupRows) groupRows.Min(Function(r) Convert.ToDecimal(r(e.FieldName))))
			e.CustomValue = value
		End If
	End Sub
End Class
