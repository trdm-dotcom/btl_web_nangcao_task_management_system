<%@ Page Language="C#" MasterPageFile="~/page/MasterPage.Master" AutoEventWireup="true" CodeBehind="ProjectCreate.aspx.cs" Inherits="btl_web_nangcao_task_management_system.page.ProjectCreate" %>

<asp:Content ID="ContentProjectCreate" runat="server" ContentPlaceHolderID="mainContentPlaceHolder">
    <asp:Panel runat="server" ID="Panel1">
        <asp:Label ID="errorMessage" runat="server" CssClass="invalid-feedback"></asp:Label>
        <div class="form-group">
            <asp:Label ID="Label1" runat="server" Text="Project Title:" AssociatedControl="titleTextBox"></asp:Label>
            <asp:TextBox ID="titleTextBox" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:Label ID="feedbackTitle" runat="server" CssClass="invalid-feedback"></asp:Label>
        </div>
        <div class="form-group">
            <asp:Label ID="Label2" runat="server" Text="Description:" CssClass="labelForm" AssociatedControl="descriptionTextBox"></asp:Label>
            <asp:TextBox ID="descriptionTextBox" runat="server" TextMode="multiline" Rows="10" CssClass="form-control"></asp:TextBox>
            <asp:Label ID="feedbackDescription" runat="server" CssClass="invalid-feedback"></asp:Label>
        </div>
        <div class="form-row no-gutters">
            <div class="form-group col c-12 m-3 l-3">
                <asp:Label ID="Label3" runat="server" Text="Start Date:" AssociatedControl="startDateTextBox"></asp:Label>
                <asp:TextBox ID="startDateTextBox" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                <asp:Label ID="feedbackStartDate" runat="server" CssClass="invalid-feedback"></asp:Label>
            </div>
            <div class="form-group col c-12 m-3 l-3">
                <asp:Label ID="Label4" runat="server" Text="Estimate Date:" AssociatedControlID="estimateDateTextBox"></asp:Label>
                <asp:TextBox ID="estimateDateTextBox" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                <asp:Label ID="feedbackestimateDate" runat="server" CssClass="invalid-feedback"></asp:Label>
            </div>
        </div>
        <div class="form-group">
            <asp:Label ID="employeesLabel" runat="server" Text="Employee:"></asp:Label>
            <div class="form-row no-gutters">
                <div class="col c-5 m-5 l-5">
                    <asp:ListBox ID="allEmployeeListBox" runat="server" Height="100%" Width="100%" CssClass="form-control"></asp:ListBox>
                </div>
                <div class="col c-2 m-2 l-2">
                    <center>
                        <asp:Button ID="singleAddButton" runat="server" Text=">" Style="margin-bottom: 13px" Width="40px" OnClick="singleAddButton_Click"/>
                        <br />                        
                        <asp:Button ID="allAddButton" runat="server" Text=">>" Style="margin-bottom: 13px" Width="40px" OnClick="allAddButton_Click"/>
                        <br />
                        <asp:Button ID="singleRemoveButton" runat="server" Text="<" Style="margin-bottom: 13px" Width="40px" OnClick="singleRemoveButton_Click"/>
                        <br />
                        <asp:Button ID="allRemoveButton" runat="server" Text="<<" Style="margin-bottom: 13px" Width="40px" OnClick="allRemoveButton_Click"/>
                    </center>
                </div>
                <div class="col c-5 m-5 l-5">
                    <asp:ListBox ID="selectedEmployeeListBox" runat="server" Height="100%" Width="100%" CssClass="form-control"></asp:ListBox>
                </div>
            </div>
            <asp:Label ID="feedbackEmployee" runat="server" CssClass="invalid-feedback"></asp:Label>
        </div>
        <div class="form-group">
            <asp:Label ID="Label5" runat="server" Text="Lead:" AssociatedControl="leadDropDownList"></asp:Label>
            <asp:DropDownList ID="leadDropDownList" runat="server" AutoPostBack="false" CssClass="form-control" OnDataBound="leadDropDownList_DataBound">
            </asp:DropDownList>
            <asp:Label ID="feedbackLead" runat="server" CssClass="invalid-feedback"></asp:Label>
        </div>
        <asp:Button ID="saveButton" runat="server" Text="Save" OnClick="saveButton_Click"/>
    </asp:Panel>
</asp:Content>
