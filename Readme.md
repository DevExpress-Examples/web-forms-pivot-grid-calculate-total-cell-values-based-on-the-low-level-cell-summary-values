<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128577227/14.1.7%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T158425)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
[![](https://img.shields.io/badge/💬_Leave_Feedback-feecdd?style=flat-square)](#does-this-example-address-your-development-requirementsobjectives)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [Default.aspx](./CS/Default.aspx) (VB: [Default.aspx](./VB/Default.aspx))
* [Default.aspx.cs](./CS/Default.aspx.cs) (VB: [Default.aspx.vb](./VB/Default.aspx.vb))
<!-- default file list end -->
# How to calculate Total cell values based on the low level Cell summary values


<p>This example demonstrates how to use the <a href="https://documentation.devexpress.com/#AspNet/DevExpressWebASPxPivotGridASPxPivotGrid_CustomSummarytopic">CustomSummary Event</a> to resolve the problem described in the <a href="https://www.devexpress.com/Support/Center/p/Q268380">Total values calculation seems to be incorrect, how to calculate Min, Max, Average values based on the cell</a> thread. The sample is built based on the ASPxPivotGrid control, but it is also possible to use this approach with other pivot control for WinForms, WPF, Silverlight and MVC. <br />Below is the <a href="https://documentation.devexpress.com/#AspNet/DevExpressWebASPxPivotGridASPxPivotGrid_CustomSummarytopic">CustomSummary </a>event handler code description:<br /><strong>1.</strong> In the root IF clause, we determine the required summary type. For all fields below the "Company" one, it is necessary to show min values. In the attached sample project, all fields are located in the <a href="https://documentation.devexpress.com/#AspNet/CustomDocument3587">Row Area</a><u>,</u> so it is enough to check the <a href="https://documentation.devexpress.com/#CoreLibraries/DevExpressXtraPivotGridPivotGridFieldBase_AreaIndextopic">AreaIndex Property</a> of the corresponding <a href="https://documentation.devexpress.com/#CoreLibraries/DevExpressXtraPivotGridDataPivotGridCustomSummaryEventArgsBase~T~_RowFieldtopic">e.RowField</a>:</p>


```cs
if (e.RowField != null && e.RowField.AreaIndex >= fieldCompanyName.AreaIndex) {
	//Min
}
else {
	//Sum of Min
}

```


<p><strong>2.</strong> Pivot Grid automatically calculates <a href="https://documentation.devexpress.com/#CoreLibraries/DevExpressDataPivotGridPivotSummaryTypeEnumtopic">predefined summary values</a> for all cells, so it is not necessary to recalculate them manually. You can get them from the <a href="https://documentation.devexpress.com/#CoreLibraries/DevExpressXtraPivotGridDataPivotGridCustomSummaryEventArgsBase~T~_SummaryValuetopic">e.SummaryValue </a> object and assign them to the <a href="https://documentation.devexpress.com/#CoreLibraries/DevExpressXtraPivotGridDataPivotGridCustomSummaryEventArgsBase~T~_CustomValuetopic">CustomValue Property</a>:</p>


```cs
e.CustomValue = e.SummaryValue.Min;

```


<p><strong>3.</strong> Now it is necessary to calculate custom summary values (Sum of Min). Pivot Grid calculates summary values for all cells based on the underlying data source rows regardless the cell type. However, for our task it is necessary to calculate total values based on the low level cells summary values. E.g for Quarter rows it is necessary to group data by Company, then find Min value in each group and finally summarize all min values. For Year field logic will be the same but it is necessary to group data by Quarter and Company. For Grand Total cell it is necessary to group data by Year, Quarter and Company. This is a rather complex task so it is better to discuss the code part by part.</p>
<p><strong>3.1.</strong> To get grouping fields simply iterates through the <a href="https://documentation.devexpress.com/#AspNet/DevExpressWebASPxPivotGridASPxPivotGrid_Fieldstopic">Fields</a> collection and find field that are located into the Row Area below the current <a href="https://documentation.devexpress.com/#CoreLibraries/DevExpressXtraPivotGridDataPivotGridCustomSummaryEventArgsBase~T~_RowFieldtopic">e.RowField</a> but above the Company field:</p>


```cs
var groupingFields = pivot.Fields.Cast<PivotGridField>().Where(f => f.Visible && f.Area == fieldCompanyName.Area && f.AreaIndex <= fieldCompanyName.AreaIndex && (e.RowField == null || f.AreaIndex > e.RowField.AreaIndex));

```


<p><strong> 3.2.</strong> Get the pivot drill-down data source and cast it to the IEnumerable<PivotDrillDownDataRow> type:</p>


```cs
var drillDownDataSource = e.CreateDrillDownDataSource().Cast<PivotDrillDownDataRow>();

```


<p><strong>3.3.</strong> At present, we have a single collection of data source rows. To create a collection of row groups, execute the following code:</p>


```cs
List<IEnumerable<PivotDrillDownDataRow>> temporaryList = new List<IEnumerable<PivotDrillDownDataRow>>();
temporaryList.Add( drillDownDataSource  );
IEnumerable<IEnumerable<PivotDrillDownDataRow>> dataGroups = temporaryList;  

```


<p><strong>3.4.</strong> Now it is necessary to group the entire data source by <em>groupingFields </em>requested earlier:</p>


```cs
foreach (var field in groupingFields) {
    var localField = field;
    dataGroups = dataGroups.SelectMany(groupRows => groupRows.GroupBy(r => r[localField]).Select(g => g.AsEnumerable()));
}

```


<p><strong>3.5.</strong> The final step is to iterate through all groups, find a min value in each of them and then calculate the sum of all found values:</p>


```cs
var value = dataGroups.Sum(groupRows => groupRows.Min( r => Convert.ToDecimal(r[e.FieldName])));
e.CustomValue = value;
```



<br/>


<!-- feedback -->
## Does this example address your development requirements/objectives?

[<img src="https://www.devexpress.com/support/examples/i/yes-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=web-forms-pivot-grid-calculate-total-cell-values-based-on-the-low-level-cell-summary-values&~~~was_helpful=yes) [<img src="https://www.devexpress.com/support/examples/i/no-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=web-forms-pivot-grid-calculate-total-cell-values-based-on-the-low-level-cell-summary-values&~~~was_helpful=no)

(you will be redirected to DevExpress.com to submit your response)
<!-- feedback end -->
