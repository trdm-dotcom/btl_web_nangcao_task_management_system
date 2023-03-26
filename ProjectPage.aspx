<%@ Page Language="C#" MasterPageFile="MasterPage.Master" AutoEventWireup="true" CodeBehind="ProjectPage.aspx.cs" Inherits="btl_web_nangcao_task_management_system.page.project.ProjectPage" %>

<asp:Content ID="ContentProjectPage" runat="server" ContentPlaceHolderID="mainContentPlaceHolder">
    <asp:GridView ID="ProjectGridView" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="ProjectGridView_PageIndexChanging" CssClass="table">
        <Columns>
            <asp:BoundField />
            <asp:BoundField DataField="title" HeaderText="Name" />
            <asp:BoundField DataField="leadName" HeaderText="Lead" />
            <asp:TemplateField HeaderText="Status">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("status") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# showStatus(Eval("status")) %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="startDate" HeaderText="Start date" DataFormatString="{0:dd/MM/yyyy}" />
            <asp:BoundField DataField="estimateDate" HeaderText="Estimate date" DataFormatString="{0:dd/MM/yyyy}" />
            <asp:HyperLinkField DataNavigateUrlFields="id" DataNavigateUrlFormatString="ProjectEdit.aspx?project={0}" Text="Edit" />
        </Columns>
    </asp:GridView>
</asp:Content>
