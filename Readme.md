<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128577227/19.2.3%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T158425)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->

# Pivot Grid for Web Forms -  How to Calculate Totals for the Minimum Values

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

## Files to Review

* [CustomSummaryEventExample.aspx](./CS/CustomSummaryEventExample.aspx) (VB: [Default.aspx](./VB/CustomSummaryEventExample.aspx))
* [CustomSummaryEventExample.aspx.cs](./CS/CustomSummaryEventExample.aspx.cs) (VB: [Default.aspx.vb](./VB/CustomSummaryEventExample.aspx.vb))
* [AggrExpressionExample.aspx](./CS/AggrExpressionExample.aspx) (VB: [AggrExpressionExample.aspx](./VB/AggrExpressionExample.aspx))
