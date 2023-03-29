<%@ Page Language="C#" MasterPageFile="MasterPage.Master" AutoEventWireup="true" CodeBehind="TaskPage.aspx.cs" Inherits="btl_web_nangcao_task_management_system.page.task.TaskAll" %>

<asp:Content ID="ContentTaskPage" runat="server" ContentPlaceHolderID="mainContentPlaceHolder">
    <asp:Label ID="errorMessage" runat="server" CssClass="invalid-feedback"></asp:Label>

    <div class="row no-gutters">
        <div class="col c-12 m-4 l-4 status status-1">
            <div class="tags has-addons">
                <span class="tag">To do</span>
                <asp:Label runat="server" ID="quantityTaskTodo" CssClass="tag is-dark"></asp:Label>
            </div>
            <asp:ListView ID="toDoListView" runat="server">
                <ItemTemplate>
                    <a href="TaskView.aspx?task=<%# Eval("id") %>">
                        <div class="task">
                            <p><%# Eval("name") %></p>
                            <div class="task__stats">
                                <span  class="<%# (showPriority(Eval("priority")) == "HIGH" ? "text-danger" : (showPriority(Eval("priority")) == "MEDIUM" ? "text-warning" : "text-primary")) %>"><%# showPriority(Eval("priority")) %></span>
                                <span><%# Eval("startDate", "{0:dd/M/yyyy}") %></span>
                                <span><%# Eval("nameEmployeeAssignee") %></span>
                            </div>
                        </div>
                    </a>
                </ItemTemplate>
            </asp:ListView>
        </div>
        <div class="col c-12 m-4 l-4 status status-2">
            <div class="tags has-addons">
                <span class="tag">In-Progress</span>
                <asp:Label runat="server" ID="quantityTaskProgess" CssClass="tag is-dark"></asp:Label>
            </div>
            <asp:ListView ID="progessListView" runat="server">
                <ItemTemplate>
                    <a href="TaskView.aspx?task=<%# Eval("id") %>">
                        <div class="task">
                            <p><%# Eval("name") %></p>
                            <div class="task__stats">
                                <span  class="<%# (showPriority(Eval("priority")) == "HIGH" ? "text-danger" : (showPriority(Eval("priority")) == "MEDIUM" ? "text-warning" : "text-primary")) %>"><%# showPriority(Eval("priority")) %></span>
                                <span><%# Eval("startDate", "{0:dd/M/yyyy}") %></span>
                                <span><%# Eval("nameEmployeeAssignee") %></span>
                            </div>
                        </div>
                    </a>
                </ItemTemplate>
            </asp:ListView>
        </div>
        <div class="col c-12 m-4 status l-4 status-3">
            <div class="tags has-addons">
                <span class="tag">Done</span>
                <asp:Label runat="server" ID="quantityTaskDone" CssClass="tag is-dark"></asp:Label>
            </div>
            <asp:ListView ID="doneListView" runat="server">
                <ItemTemplate>
                    <a href="TaskView.aspx?task=<%# Eval("id") %>">
                        <div class="task">
                            <p><%# Eval("name") %></p>
                            <div class="task__stats">
                                <span  class="<%# (showPriority(Eval("priority")) == "HIGH" ? "text-danger" : (showPriority(Eval("priority")) == "MEDIUM" ? "text-warning" : "text-primary")) %>"><%# showPriority(Eval("priority")) %></span>
                                <span><%# Eval("startDate", "{0:dd/M/yyyy}") %></span>
                                <span><%# Eval("nameEmployeeAssignee") %></span>
                            </div>
                        </div>
                    </a>
                </ItemTemplate>
            </asp:ListView>
        </div>
    </div>
</asp:Content>
