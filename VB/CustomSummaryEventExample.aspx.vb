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
        ' In the root IF clause, determine the summary type.
        ' All fields at the level lower than the "Company" should show Min values.
        ' All fields are located in the Row Area, so it Is necessary to check the AreaIndex property of the e.RowField.
        If e.RowField IsNot Nothing AndAlso e.RowField.AreaIndex >= fieldCompanyName.AreaIndex Then
            ' The Pivot Grid automatically calculates predefined summary values for all cells.
            ' To obtain summary values, use the e.SummaryValue to get the SummaryValue object,
            ' get the Min value And assign it to the CustomValue property.
            e.CustomValue = e.SummaryValue.Min
        Else
            ' The Pivot Grid calculates summary values for all cells based on the underlying data source rows regardless of the cell type.
            ' However, the current scenario calculates total values based on the low level cell summary values.
            ' For the Year field rows it Is necessary to group data by Company, 
            ' find Min value in each group And finally summarize all Min values.
            ' For the Grand Total cell it Is necessary to group data by the Year, Quarter And Company values. 
            ' 
            ' To get grouping fields, iterate through the Fields collection 
            ' And find the fields located in the Row Area below the current e.RowField but above the Company field.
            Dim groupingFields = pivot.Fields.Cast(Of PivotGridField)().Where(Function(f) f.Visible AndAlso f.Area = fieldCompanyName.Area AndAlso f.AreaIndex <= fieldCompanyName.AreaIndex AndAlso (e.RowField Is Nothing OrElse f.AreaIndex > e.RowField.AreaIndex))
            ' Get the underlying data.
            Dim drillDownDataSource = e.CreateDrillDownDataSource().Cast(Of PivotDrillDownDataRow)()
			Dim temporaryList As New List(Of IEnumerable(Of PivotDrillDownDataRow))()
			temporaryList.Add(drillDownDataSource)
            Dim dataGroups As IEnumerable(Of IEnumerable(Of PivotDrillDownDataRow)) = temporaryList
            ' Group the entire data source by the recently obtained groupingFields:
            For Each field In groupingFields
                Dim localField = field
                dataGroups = dataGroups.SelectMany(Function(groupRows) groupRows.GroupBy(Function(r) r(localField)).Select(Function(g) g.AsEnumerable()))
            Next field
            ' The final step is to iterate through all groups, find a Min value in each group and calculate the sum of all Min values.
            Dim value = dataGroups.Sum(Function(groupRows) groupRows.Min(Function(r) Convert.ToDecimal(r(e.FieldName))))
			e.CustomValue = value
		End If
	End Sub
End Class
