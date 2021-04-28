<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AggrExpressionExample.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v19.2, Version=19.2.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPivotGrid" TagPrefix="dxwpg" %>

<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v19.2, Version=19.2.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPivotGrid" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v19.2, Version=19.2.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Calculate Totals for Minimum Values - Use Aggr Expression</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>

        <dxwpg:ASPxPivotGrid ID="ASPxPivotGrid1" runat="server"
            DataSourceID="AccessDataSource1" ClientIDMode="AutoID">
            <OptionsData DataProcessingEngine="Optimized" />
            <Fields>
                <dxwpg:PivotGridField ID="fieldProductName" AreaIndex="3"
                    FieldName="ProductName" Area="RowArea">
                </dxwpg:PivotGridField>
                <dxwpg:PivotGridField ID="fieldCompanyName" AreaIndex="2"
                    FieldName="CompanyName" Area="RowArea">
                </dxwpg:PivotGridField>
                <dxwpg:PivotGridField ID="fieldYear" Area="RowArea" AreaIndex="0"
                    FieldName="OrderDate" GroupInterval="DateYear" Caption="Year"
                    UnboundFieldName="fieldOrderYear">
                </dxwpg:PivotGridField>
                <dxwpg:PivotGridField ID="fieldAmount" Area="DataArea" AreaIndex="0"
                    FieldName="ProductAmount" SummaryType="Sum" Caption="Sum">
                </dxwpg:PivotGridField>
                <dxwpg:PivotGridField ID="fieldMin" Area="DataArea" AreaIndex="1"
                    FieldName="ProductAmount" SummaryType="Min" Caption="Min">
                </dxwpg:PivotGridField>
                <dxwpg:PivotGridField ID="fieldSumOfMin" Area="DataArea" AreaIndex="2"
                    Caption="Sum Of Min">
                    <DataBindingSerializable>
                        <dx:ExpressionDataBinding Expression="Aggr(Min(Aggr(Min([ProductAmount]), GetYear([OrderDate]), [ProductName], [CompanyName])), GetYear([OrderDate]), [CompanyName])" />
                    </DataBindingSerializable>
                </dxwpg:PivotGridField>
            </Fields>

            <OptionsView RowTotalsLocation="Tree" ShowColumnGrandTotalHeader="False"
                ShowColumnHeaders="False" ShowDataHeaders="False" ShowFilterHeaders="False" />

            <OptionsPager RowsPerPage="20">

                <PageSizeItemSettings Visible="True">
                </PageSizeItemSettings>

            </OptionsPager>
        </dxwpg:ASPxPivotGrid>
        <asp:AccessDataSource ID="AccessDataSource1" runat="server"
            DataFile="~/App_Data/nwind.mdb"
            SelectCommand="SELECT * FROM [CustomerReports]"></asp:AccessDataSource>
    </form>
</body>
</html>
