<!-- default file list -->
*Files to look at*:

* [CustomSummaryEventExample.aspx](./CS/CustomSummaryEventExample.aspx) (VB: [Default.aspx](./VB/CustomSummaryEventExample.aspx))
* [CustomSummaryEventExample.aspx.cs](./CS/CustomSummaryEventExample.aspx.cs) (VB: [Default.aspx.vb](./VB/CustomSummaryEventExample.aspx.vb))
* [AggrExpressionExample.aspx](./CS/AggrExpressionExample.aspx) (VB: [AggrExpressionExample.aspx](./VB/AggrExpressionExample.aspx))
<!-- default file list end -->
# How to Calculate Totals for the Minimum Values

This example calculates summaries for the minimum ProductAmount values. Minimum values are calculated in a group defined by the year, a company and a product.

The example demonstrates two different approaches - the legacy approach that uses the **CustomSummary** event, and the approach avialble in the [Optimized mode](https://docs.devexpress.com/CoreLibraries/401367) - an expression with the [Aggr](https://docs.devexpress.com/CoreLibraries/401367) function. 


![](/images/screenshot.png)

## Optimized Mode - Use the [Aggr](https://docs.devexpress.com/CoreLibraries/401367) Function

This approach adds a PivotGrid field bound to the following expression:

```
Aggr(Min(Aggr(Min([ProductAmount]), GetYear([OrderDate]), [ProductName], [CompanyName])), GetYear([OrderDate]), [CompanyName])
```



## Legacy Approach - Handle the CustomSummary Event

You can handle the  [CustomSummary](https://docs.devexpress.com/AspNet/DevExpress.Web.ASPxPivotGrid.ASPxPivotGrid.CustomSummary) event to calculate custom summaries.

The  CustomSummary event handler performs the following:

1. In the root IF clause, it determines the summary type. All fields at the level lower than the "Company" should show Min values. All fields are located in the [Row Area](https://docs.devexpress.com/AspNet/3587), so it is necessary to check the [AreaIndex](https://docs.devexpress.com/CoreLibraries/DevExpress.XtraPivotGrid.PivotGridFieldBase.AreaIndex) property of the [e.RowField](https://docs.devexpress.com/CoreLibraries/DevExpress.XtraPivotGrid.Data.PivotGridCustomSummaryEventArgsBase-1.RowField):

	# [C#](#tab/tabid-csharp)
	
	```csharp
	if (e.RowField != null && e.RowField.AreaIndex >= fieldCompanyName.AreaIndex) {
		//Min
	}
	else {
		//Sum of Min
	}
	```
	
	# [VB.NET](#tab/tabid-vb)
	
	```vb
	If e.RowField IsNot Nothing AndAlso e.RowField.AreaIndex >= fieldCompanyName.AreaIndex Then
		'Min
	Else
		'Sum of Min
	End If
	```
	
	***

2. The Pivot Grid automatically calculates [predefined summary values](https://docs.devexpress.com/CoreLibraries/DevExpress.Data.PivotGrid.PivotSummaryType) for all cells. To obtain summary values, use the [e.SummaryValue](https://docs.devexpress.com/CoreLibraries/DevExpress.XtraPivotGrid.Data.PivotGridCustomSummaryEventArgsBase-1.SummaryValue) to get the **SummaryValue** object, get the [Min](https://docs.devexpress.com/CoreLibraries/DevExpress.Data.PivotGrid.PivotSummaryValue.Min) value and assign it to the [CustomValue](https://docs.devexpress.com/CoreLibraries/DevExpress.XtraPivotGrid.Data.PivotGridCustomSummaryEventArgsBase-1.CustomValue) property:

	# [C#](#tab/tabid-csharp)
	
	```csharp
		e.CustomValue = e.SummaryValue.Min;
	```
	
	# [VB.NET](#tab/tabid-vb)
	
	```vb
		e.CustomValue = e.SummaryValue.Min
	```
	
	***


3. The Pivot Grid calculates summary values for all cells based on the underlying data source rows regardless of the cell type. However, we calculate total values based on the low level cell summary values. For the Year field rows it is necessary to group data by Company, then find Min value in each group and finally summarize all Min values. For the Grand Total cell it is necessary to group data by the Year, Quarter and Company values.

	**3.1.** To get grouping fields, iterate through the [Fields](https://docs.devexpress.com/AspNet/DevExpress.Web.ASPxPivotGrid.ASPxPivotGrid.Fields) collection and find fields located in the Row Area below the current [e.RowField](https://docs.devexpress.com/CoreLibraries/DevExpress.XtraPivotGrid.Data.PivotGridCustomSummaryEventArgsBase-1.RowField) but above the **Company** field:

	# [C#](#tab/tabid-csharp)
	
	```csharp
	var groupingFields = pivot.Fields.Cast<PivotGridField>().Where(f => f.Visible && f.Area == fieldCompanyName.Area && f.AreaIndex <= fieldCompanyName.AreaIndex && (e.RowField == null || f.AreaIndex > e.RowField.AreaIndex));
	```
	
	# [VB.NET](#tab/tabid-vb)
	
	```vb
	Dim groupingFields = pivot.Fields.Cast(Of PivotGridField)().Where(Function(f) f.Visible AndAlso f.Area = fieldCompanyName.Area AndAlso f.AreaIndex <= fieldCompanyName.AreaIndex AndAlso (e.RowField Is Nothing OrElse f.AreaIndex > e.RowField.AreaIndex))
	```
	
	***

	
	**3.3.** This is a collection of data source rows. To create a collection of row groups, run the following code:
	# [C#](#tab/tabid-csharp)
	
	```csharp
	List<IEnumerable<PivotDrillDownDataRow>> temporaryList = new List<IEnumerable<PivotDrillDownDataRow>>();
	temporaryList.Add( drillDownDataSource  );
	IEnumerable<IEnumerable<PivotDrillDownDataRow>> dataGroups = temporaryList;
	```
	
	# [VB.NET](#tab/tabid-vb)
	
	```vb
	Dim temporaryList As New List(Of IEnumerable(Of PivotDrillDownDataRow))()
	temporaryList.Add(drillDownDataSource)
	Dim dataGroups As IEnumerable(Of IEnumerable(Of PivotDrillDownDataRow)) = temporaryList
	```
	
	***

	**3.4.** Group the entire data source by the recently obtained _groupingFields_:

	# [C#](#tab/tabid-csharp)
	
	```csharp
		foreach (var field in groupingFields) {
		    var localField = field;
		    dataGroups = dataGroups.SelectMany(groupRows => groupRows.GroupBy(r => r[localField]).Select(g => g.AsEnumerable()));
		}
	```
	
	# [VB.NET](#tab/tabid-vb)
	
	```vb
			For Each field In groupingFields
				Dim localField = field
				dataGroups = dataGroups.SelectMany(Function(groupRows) groupRows.GroupBy(Function(r) r(localField)).Select(Function(g) g.AsEnumerable()))
			Next field
	```
	
	***


	**3.5.** The final step is to iterate through all groups, find a Min value in each group and calculate the sum of all Min values:
	# [C#](#tab/tabid-csharp)
	
	```csharp
	var value = dataGroups.Sum(groupRows => groupRows.Min( r => Convert.ToDecimal(r[e.FieldName])));
	e.CustomValue = value;
	```
	
	# [VB.NET](#tab/tabid-vb)
	
	```vb
	Dim value = dataGroups.Sum(Function(groupRows) groupRows.Min(Function(r) Convert.ToDecimal(r(e.FieldName))))
	e.CustomValue = value
	```
	
	***