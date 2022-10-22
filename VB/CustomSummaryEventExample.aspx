<%@ Page Language="vb" AutoEventWireup="true"  CodeFile="CustomSummaryEventExample.aspx.vb" Inherits="_Default" %>

<%@ Register assembly="DevExpress.Web.ASPxPivotGrid.v19.2, Version=19.2.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPivotGrid" tagprefix="dxwpg" %>

<%@ Register assembly="DevExpress.Web.ASPxPivotGrid.v19.2, Version=19.2.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPivotGrid" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v19.2, Version=19.2.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Calculate Totals for Minimum Values - Handle CustomSummary Event</title>
</head>
<body>
	<form id="form1" runat="server">
	<div>

	</div>

	<dxwpg:ASPxPivotGrid ID="ASPxPivotGrid1" runat="server" 
		DataSourceID="AccessDataSource1"  ClientIDMode="AutoID" 
		oncustomsummary="ASPxPivotGrid1_CustomSummary" >
		<Fields>
			<dxwpg:PivotGridField ID="fieldProductName" AreaIndex="3" 
				FieldName="ProductName" Area="RowArea"   >
			</dxwpg:PivotGridField>
			<dxwpg:PivotGridField ID="fieldCompanyName" AreaIndex="2" 
				FieldName="CompanyName" Area="RowArea" >
			</dxwpg:PivotGridField>
			<dxwpg:PivotGridField ID="fieldYear" Area="RowArea" AreaIndex="0" 
				FieldName="OrderDate" GroupInterval="DateYear" Caption="Year" 
				UnboundFieldName="fieldOrderYear" >
			</dxwpg:PivotGridField>
			<dxwpg:PivotGridField ID="fieldAmount" Area="DataArea" AreaIndex="0" 
				FieldName="ProductAmount" SummaryType="Sum" Caption="Sum">
			</dxwpg:PivotGridField>
			<dxwpg:PivotGridField ID="fieldMin" Area="DataArea" AreaIndex="1" 
				FieldName="ProductAmount" SummaryType="Min" Caption="Min">
			</dxwpg:PivotGridField>
			<dxwpg:PivotGridField ID="fieldSumOfMin" Area="DataArea" AreaIndex="2" 
				FieldName="ProductAmount" SummaryType="Custom" Caption="Sum Of Min">
			</dxwpg:PivotGridField>
		</Fields>

		<OptionsView RowTotalsLocation="Tree" ShowColumnGrandTotalHeader="False" 
			ShowColumnHeaders="False" ShowDataHeaders="False" ShowFilterHeaders="False" />

		<OptionsPager RowsPerPage="20" >

			<PageSizeItemSettings Visible="True">
			</PageSizeItemSettings>

		</OptionsPager>
	</dxwpg:ASPxPivotGrid>
	<asp:AccessDataSource ID="AccessDataSource1" runat="server" 
		DataFile="~/App_Data/nwind.mdb" 
		SelectCommand="SELECT * FROM [CustomerReports]">
	</asp:AccessDataSource>
	</form>
</body>
</html>