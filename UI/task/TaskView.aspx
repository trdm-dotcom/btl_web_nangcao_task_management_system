<%@ Page Language="C#" MasterPageFile="~/UI/MasterPage.Master" AutoEventWireup="true" CodeBehind="TaskView.aspx.cs" Inherits="btl_web_nangcao_task_management_system.UI.task.TaskView" %>

<asp:Content ID="ContentTaskView" runat="server" ContentPlaceHolderID="mainContentPlaceHolder">
    <div class="row no-gutters">
        <div class="col c-12 m-9 l-9">
            <asp:Label ID="nameTaskLabel" runat="server" CssClass="h2"></asp:Label>
            <br />
            <p class="h6">Description</p>
             <asp:Label ID="DescriptionLabel" runat="server" CssClass="h2"></asp:Label>
        </div>
        <div class="col c-12 m-3 l-3">
            <asp:DropDownList ID="statusDropDownList" runat="server" CssClass="form-control" Style="margin-bottom: 2rem">
            </asp:DropDownList>
            <div class="border detail-box">
                <div class="border-botton header-detail-box">
                    <p>Details</p>
                </div>
                <div class="main-detail-box">
                    <div class="child-element-detail-box">
                        <p>Reporter</p>
                        <asp:DropDownList ID="reporterDropDownList" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                    <div class="child-element-detail-box">
                        <p>Assignee</p>
                        <asp:DropDownList ID="AssigneeDropDownList" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                    <div class="child-element-detail-box">
                        <p>QA</p>
                        <asp:DropDownList ID="QADropDownList" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                    <div class="child-element-detail-box">
                        <p>Priority</p>
                        <asp:Label ID="priorityLabel" runat="server"></asp:Label>
                    </div>
                    <div class="child-element-detail-box">
                        <p>Start date</p>
                        <asp:Label ID="startDateLabel" runat="server"></asp:Label>
                    </div>
                    <div class="child-element-detail-box">
                        <p>Estimate date</p>
                        <asp:Label ID="estimateDateLabel" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>