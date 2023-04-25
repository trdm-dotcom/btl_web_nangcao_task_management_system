<%@ Page Language="C#" MasterPageFile="MasterPage.Master" AutoEventWireup="true" CodeBehind="TaskAll.aspx.cs" Inherits="btl_web_nangcao_task_management_system.TaskAll" %>

<asp:Content ID="ContentTaskAll" runat="server" ContentPlaceHolderID="mainContentPlaceHolder">
    <div class="messageFeedback">
        <asp:Label ID="errorMessage" runat="server" CssClass="invalid-feedback"></asp:Label>
        <asp:Label ID="successMessage" runat="server" CssClass="success-feedback"></asp:Label>
        <div id="toastBox"></div>
    </div>
    <div class="row">
        <div class="col c-12 m-3 l-3">
            <input class="form-control" />
        </div>
        <div class="col c-6 m-3 l-3">
            <asp:DropDownList ID="employeeDropDownList" runat="server" AutoPostBack="false" CssClass="form-control">
                <Items>
                    <asp:ListItem Text="-Select-" />
                    <asp:ListItem Text="Current User" Value='<%= (long)Session["user"] %>' />
                    <asp:ListItem Text="Unassigned" Value="NaN" />
                </Items>
            </asp:DropDownList>
        </div>
        <div class="col c-6 m-3 l-3">
            <asp:DropDownList ID="statusDropDownList" runat="server" CssClass="form-control" Style="margin-bottom: 2rem">
                <Items>
                    <asp:ListItem Text="-Select-" />
                    <asp:ListItem Text="TO DO" Value="TODO" />
                    <asp:ListItem Text="PROGESS" Value="PROGESS" />
                    <asp:ListItem Text="DONE" Value="DONE" />
                </Items>
            </asp:DropDownList>
        </div>
    </div>
    <asp:GridView ID="TaskGridView" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True" BorderStyle="None" GridLines="None" OnPageIndexChanging="TaskGridView_PageIndexChanging" OnSorting="TaskGridView_Sorting" PageSize="20">
        <Columns>
            <asp:TemplateField HeaderText="No.">
                <ItemTemplate>
                    <%# Container.DataItemIndex + 1 %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:HyperLinkField DataNavigateUrlFields="id" DataNavigateUrlFormatString="TaskView.aspx?task={0}" DataTextField="name" HeaderText="Name" SortExpression="name" />
            <asp:HyperLinkField DataNavigateUrlFields="projectId" DataNavigateUrlFormatString="TaskPage.aspx?project={0}" DataTextField="projectTitle" HeaderText="Project" SortExpression="projectTitle" />
            <asp:BoundField DataField="nameEmployeeAssignee" HeaderText="Assignee"/>
            <asp:BoundField DataField="nameEmployeeReporter" HeaderText="Reporter"/>
            <asp:TemplateField HeaderText="Priority">
                <ItemTemplate>
                    <asp:Label ID="Label1" CssClass='<%# (showPriority(Eval("priority")) == "HIGH" ? "badge badge-danger" : (showPriority(Eval("priority")) == "MEDIUM" ? "badge badge-warning" : "badge badge-primary")) %>' runat="server"><%# showPriority(Eval("priority")) %></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Status">
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server"><%# showStatus(Eval("status")) %></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="createdAt" HeaderText="Created" DataFormatString="{0:dd/MM/yyyy}" SortExpression="createdAt" />
        </Columns>
    </asp:GridView>
</asp:Content>
