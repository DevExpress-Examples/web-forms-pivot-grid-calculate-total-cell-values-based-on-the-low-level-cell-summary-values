using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.XtraPivotGrid;
using DevExpress.Web.ASPxPivotGrid;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    
    protected void ASPxPivotGrid1_CustomSummary(object sender, DevExpress.Web.ASPxPivotGrid.PivotGridCustomSummaryEventArgs e)
    {
        ASPxPivotGrid pivot = (ASPxPivotGrid)sender;
        // In the root IF clause, determine the summary type.
        // All fields at the level lower than the "Company" should show Min values.
        // All fields are located in the Row Area, so it is necessary to check the AreaIndex property of the e.RowField.
        if (e.RowField != null && e.RowField.AreaIndex >= fieldCompanyName.AreaIndex)
        {
            // The Pivot Grid automatically calculates predefined summary values for all cells.
            // To obtain summary values, use the e.SummaryValue to get the SummaryValue object,
            // get the Min value and assign it to the CustomValue property.
            e.CustomValue = e.SummaryValue.Min;
        }
        else
        {
            // The Pivot Grid calculates summary values for all cells based on the underlying data source rows regardless of the cell type.
            // However, the current scenario calculates total values based on the low level cell summary values.
            // For the Year field rows it is necessary to group data by Company, 
            // find Min value in each group and finally summarize all Min values.
            // For the Grand Total cell it is necessary to group data by the Year, Quarter and Company values. 
            // 
            // To get grouping fields, iterate through the Fields collection 
            // and find the fields located in the Row Area below the current e.RowField but above the Company field.
            var groupingFields = pivot.Fields.Cast<PivotGridField>().Where(f => f.Visible && f.Area == fieldCompanyName.Area && f.AreaIndex <= fieldCompanyName.AreaIndex && (e.RowField == null || f.AreaIndex > e.RowField.AreaIndex));
            // Get the underlying data.
            var drillDownDataSource = e.CreateDrillDownDataSource().Cast<PivotDrillDownDataRow>();
            List<IEnumerable<PivotDrillDownDataRow>> temporaryList = new List<IEnumerable<PivotDrillDownDataRow>>();
            temporaryList.Add( drillDownDataSource  );
            IEnumerable<IEnumerable<PivotDrillDownDataRow>> dataGroups = temporaryList;
            // Group the entire data source by the recently obtained groupingFields:
            foreach (var field in groupingFields)
            {
                var localField = field;
                dataGroups = dataGroups.SelectMany(groupRows => groupRows.GroupBy(r => r[localField]).Select(g => g.AsEnumerable()));
            }
            // The final step is to iterate through all groups, find a Min value in each group and calculate the sum of all Min values.
            var value = dataGroups.Sum(groupRows => groupRows.Min( r => Convert.ToDecimal(r[e.FieldName])));
            e.CustomValue = value;
        }
    }
}
