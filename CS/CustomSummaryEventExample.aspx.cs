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
        if (e.RowField != null && e.RowField.AreaIndex >= fieldCompanyName.AreaIndex)
        {            
            e.CustomValue = e.SummaryValue.Min;
        }
        else
        {
            var groupingFields = pivot.Fields.Cast<PivotGridField>().Where(f => f.Visible && f.Area == fieldCompanyName.Area && f.AreaIndex <= fieldCompanyName.AreaIndex && (e.RowField == null || f.AreaIndex > e.RowField.AreaIndex));
            var drillDownDataSource = e.CreateDrillDownDataSource().Cast<PivotDrillDownDataRow>();
            List<IEnumerable<PivotDrillDownDataRow>> temporaryList = new List<IEnumerable<PivotDrillDownDataRow>>();
            temporaryList.Add( drillDownDataSource  );
            IEnumerable<IEnumerable<PivotDrillDownDataRow>> dataGroups = temporaryList;
            foreach (var field in groupingFields)
            {
                var localField = field;
                dataGroups = dataGroups.SelectMany(groupRows => groupRows.GroupBy(r => r[localField]).Select(g => g.AsEnumerable()));
            }
            var value = dataGroups.Sum(groupRows => groupRows.Min( r => Convert.ToDecimal(r[e.FieldName])));
            e.CustomValue = value;
        }
    }
}
