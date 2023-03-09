<%@ Page Language="C#" MasterPageFile="~/page/MasterPage.Master" AutoEventWireup="true" CodeBehind="ProjectAddEmployee.aspx.cs" Inherits="btl_web_nangcao_task_management_system.page.project.ProjectAddEmployee" %>

<asp:Content ID="ContentProjectAddEmployee" runat="server" ContentPlaceHolderID="mainContentPlaceHolder">
    <asp:Panel runat="server" ID="Panel1">
        <asp:Label ID="errorMessage" runat="server" CssClass="invalid-feedback"></asp:Label>
        <div class="form-group">
            <asp:Label ID="Label1" runat="server" Text="Select Project" AssociatedControl="titleTextBox"></asp:Label>
            <asp:DropDownList ID="projectDropDownList" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="projectDropDownList_SelectedIndexChanged" OnDataBound="projectDropDownList_DataBound">
            </asp:DropDownList>
            <asp:Label ID="feedbackProject" runat="server" CssClass="invalid-feedback"></asp:Label>
        </div>
        <div class="form-group">
            <asp:Label ID="employeesLabel" runat="server" Text="Employee:"></asp:Label>
            <div class="form-row no-gutters">
                <div class="col c-5 m-5 l-5">
                    <asp:ListBox ID="allEmployeeListBox" runat="server" Height="100%" Width="100%" CssClass="form-control"></asp:ListBox>
                </div>
                <div class="col c-2 m-2 l-2">
                    <center>
                        <asp:Button ID="singleAddButton" runat="server" Text=">" Style="margin-bottom: 13px" Width="40px" OnClick="singleAddButton_Click" />
                        <br />
                        <asp:Button ID="allAddButton" runat="server" Text=">>" Style="margin-bottom: 13px" Width="40px" OnClick="allAddButton_Click" />
                        <br />
                    </center>
                </div>
                <div class="col c-5 m-5 l-5">
                    <asp:ListBox ID="selectedEmployeeListBox" runat="server" Height="100%" Width="100%" CssClass="form-control"></asp:ListBox>
                </div>
                <asp:Label ID="feedbackSelectedEmployee" runat="server" CssClass="invalid-feedback"></asp:Label>
            </div>
        </div>
        <asp:Button ID="saveButton" runat="server" Text="Save" OnClick="saveButton_Click" />
    </asp:Panel>
</asp:Content>

