<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeBehind="A4_DefaultTempletForm.aspx.cs" Inherits="Noble.Report.Reports.Invoice.A4_DefaultTempletForm" %>
<%@ Register Assembly="DevExpress.XtraReports.v22.2.Web.WebForms, Version=22.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>


<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server" Visible="False">
    <dx:ASPxGridView ID="ASPxGridView1" runat="server"  EnableTheming="True" Theme="Office365" Width="100%"  OnHtmlRowPrepared="ASPxGridView1_HtmlRowPrepared"  >
        <SettingsDetail ShowDetailButtons="False" />
        <SettingsPager PageSize="15">
        </SettingsPager>
        <Settings ShowGroupPanel="True" VerticalScrollableHeight="10" VerticalScrollBarStyle="Standard" VerticalScrollBarMode="Hidden" ShowFooter="True" />
<SettingsPopup>
<FilterControl AutoUpdatePosition="False"></FilterControl>
</SettingsPopup>
        <SettingsSearchPanel Visible="True" />
    </dx:ASPxGridView>
    <dx:ASPxWebDocumentViewer Visible="false"  ID="ASPxWebDocumentViewer1" runat="server" style="margin-top: 10px" Height="1500px">
    </dx:ASPxWebDocumentViewer>
</asp:Content>