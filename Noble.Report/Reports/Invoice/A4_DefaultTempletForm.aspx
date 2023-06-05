<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeBehind="A4_DefaultTempletForm.aspx.cs" Inherits="Noble.Report.Reports.Invoice.A4_DefaultTempletForm" %>
<%@ Register Assembly="DevExpress.XtraReports.v22.2.Web.WebForms, Version=22.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">

        <SettingsSearchPanel Visible="True" />
    </dx:ASPxGridView>
    <dx:ASPxWebDocumentViewer Visible="true"  ID="ASPxWebDocumentViewer1" runat="server" style="margin-top: 10px" Height="1500px">
    </dx:ASPxWebDocumentViewer>
</asp:Content>
