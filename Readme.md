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



## Legacy Approach - Handle the [CustomSummary](https://docs.devexpress.com/AspNet/DevExpress.Web.ASPxPivotGrid.ASPxPivotGrid.CustomSummary) Event

You can handle the **CustomSummary** event to calculate custom summaries. The code demonstrates how to get the underlying data, group them and calculate the summaries.

