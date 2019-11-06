<%@ Page Language="vb" AutoEventWireup="true" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title></title>
</head>
<body>
	<form id="form1" runat="server">
		<h1>Calculate Totals for Minimum Values</h1>
		<div>
			<dx:ASPxButton ID="ASPxButton1" runat="server" Text="Handle CustomSummary Event" Theme="DevEx" AutoPostBack="False" Width="200">
				<ClientSideEvents Click="function(s, e) {
							window.location = 'CustomSummaryEventExample.aspx';
							}" />
			</dx:ASPxButton>
			<dx:ASPxButton ID="ASPxButton2" runat="server" Text="Use Aggr Expression" Theme="DevEx" AutoPostBack="False" Width="200">
				<ClientSideEvents Click="function(s, e) {
							window.location = 'AggrExpressionExample.aspx';
							}" />
			</dx:ASPxButton>
		</div>
	</form>
</body>
</html>