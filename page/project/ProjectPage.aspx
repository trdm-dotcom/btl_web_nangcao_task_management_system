<%@ Page Language="C#" MasterPageFile="~/page/MasterPage.Master" AutoEventWireup="true" CodeBehind="ProjectPage.aspx.cs" Inherits="btl_web_nangcao_task_management_system.page.project.ProjectPage" %>

<asp:Content ID="ContentProjectPage" runat="server" ContentPlaceHolderID="mainContentPlaceHolder">
    <asp:GridView ID="ProjectGridView" runat="server" Width="100%" GridLines="None" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="ProjectGridView_PageIndexChanging">
        <Columns>
            <asp:BoundField DataField="title" HeaderText="Name" />
            <asp:BoundField DataField="key" HeaderText="Key" />
            <asp:BoundField DataField="lead" HeaderText="Lead" />
            <asp:BoundField DataField="startTime" HeaderText="Start time" />
            <asp:BoundField DataField="estimateTime" HeaderText="Estimate time" />
            <asp:HyperLinkField DataNavigateUrlFields="key" DataNavigateUrlFormatString="detail.aspx?id={0}" Text="Edit" />
        </Columns>
    </asp:GridView>
</asp:Content>
