<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128577227/19.2.3%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T158425)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
[![](https://img.shields.io/badge/💬_Leave_Feedback-feecdd?style=flat-square)](#does-this-example-address-your-development-requirementsobjectives)
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
<!-- feedback -->
## Does this example address your development requirements/objectives?

[<img src="https://www.devexpress.com/support/examples/i/yes-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=web-forms-pivot-grid-calculate-total-cell-values-based-on-the-low-level-cell-summary-values&~~~was_helpful=yes) [<img src="https://www.devexpress.com/support/examples/i/no-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=web-forms-pivot-grid-calculate-total-cell-values-based-on-the-low-level-cell-summary-values&~~~was_helpful=no)

(you will be redirected to DevExpress.com to submit your response)
<!-- feedback end -->
