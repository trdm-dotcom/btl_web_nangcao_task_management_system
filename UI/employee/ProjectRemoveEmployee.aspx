﻿<%@ Page Language="C#" MasterPageFile="~/UI/MasterPage.Master" AutoEventWireup="true" CodeBehind="ProjectRemoveEmployee.aspx.cs" Inherits="btl_web_nangcao_task_management_system.UI.project.ProjectRemoveEmployee" %>

<asp:Content ID="ContentProjectRemoveEmployee" runat="server" ContentPlaceHolderID="mainContentPlaceHolder">
   <asp:Panel runat="server" ID="Panel1">
       <asp:Label ID="errorMessage" runat="server" CssClass="invalid-feedback"></asp:Label>
       <div class="form-group">
            <asp:Label ID="Label1" runat="server" Text="Select Project" AssociatedControl="titleTextBox"></asp:Label>
            <asp:DropDownList ID="projectDropDownList" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="projectDropDownList_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:Label ID="feedbackProject" runat="server" CssClass="invalid-feedback"></asp:Label>
        </div>
        <div class="form-group">
            <asp:Label ID="Label2" runat="server" Text="Select Emplyee to remove" AssociatedControl="titleTextBox"></asp:Label>
            <asp:DropDownList ID="employeeDropDownList" runat="server" CssClass="form-control">
            </asp:DropDownList>
            <asp:Label ID="feedbackSelectEmployee" runat="server" CssClass="invalid-feedback"></asp:Label>
        </div>
       <asp:Button ID="saveButton" runat="server" Text="Save" OnClick="saveButton_Click"/> 
    </asp:Panel>
</asp:Content>
